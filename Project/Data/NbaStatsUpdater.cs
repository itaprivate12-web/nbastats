using System.Text.Json;

namespace Project.Data
{
    public class NbaStatsUpdater : BackgroundService
    {
        private readonly ILogger<NbaStatsUpdater> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromHours(24);
        private static readonly HttpClient _http = new HttpClient();


        public NbaStatsUpdater(ILogger<NbaStatsUpdater> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("NbaStatsUpdater started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Starting NBA stats refresh...");
                    await RefreshExistingPlayersAsync();
                    _logger.LogInformation("NBA stats refresh complete.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during NBA stats refresh.");
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async Task RefreshExistingPlayersAsync()
        {
            var existingPlayers = DbHelper.RunSelect<Player>("SELECT * FROM Players;");

            if (existingPlayers.Count == 0)
            {
                _logger.LogInformation("No players in DB to update.");
                return;
            }

            _logger.LogInformation($"Refreshing stats for {existingPlayers.Count} players...");

            var allStats = await FetchAllPlayerStatsAsync();
            if (allStats == null) return;

            int updated = 0;
            foreach (var player in existingPlayers)
            {
                if (player.NbaId == 0)
                {
                    _logger.LogWarning($"  ✗ {player.Name} has no NbaId set — skipped.");
                    continue;
                }

                if (allStats.TryGetValue(player.NbaId, out var stats))
                {
                    DbHelper.RunSqlChange(
                        "UPDATE Players SET PPG = {}, APG = {}, RPG = {}, TeamId = {} WHERE Id = {}",
                        stats.PPG, stats.APG, stats.RPG, stats.TeamId, player.Id
                    );
                    updated++;
                    _logger.LogInformation($"  ✓ {player.Name} — PPG:{stats.PPG} APG:{stats.APG} RPG:{stats.RPG}");
                }
                else
                {
                    _logger.LogWarning($"  ✗ {player.Name} (NbaId:{player.NbaId}) not found — skipped.");
                }
            }

            _logger.LogInformation($"Refreshed {updated}/{existingPlayers.Count} players.");
        }

        private async Task<Dictionary<int, PlayerStats>?> FetchAllPlayerStatsAsync()
        {
            // balldontlie free API — no API key, no blocking
            // returns per-game averages for the 2024-25 season
            var url = "https://api.balldontlie.io/v1/stats?seasons[]=2024&per_page=100";
            
            var json = await FetchJson(url);
            if (json == null) return null;

            var root = json.Value;
            var result = new Dictionary<int, PlayerStats>();

            foreach (var item in root.GetProperty("data").EnumerateArray())
            {
                int playerId = item.GetProperty("player_id").GetInt32();

                double ppg = item.TryGetProperty("pts", out var pts) ? pts.GetDouble() : 0;
                double apg = item.TryGetProperty("ast", out var ast) ? ast.GetDouble() : 0;
                double rpg = item.TryGetProperty("reb", out var reb) ? reb.GetDouble() : 0;
                int teamId = item.TryGetProperty("team", out var team)
                             ? team.GetProperty("id").GetInt32()
                             : 0;

                result[playerId] = new PlayerStats
                {
                    PPG = Math.Round(ppg, 1),
                    APG = Math.Round(apg, 1),
                    RPG = Math.Round(rpg, 1),
                    TeamId = teamId,
                };
            }

            return result;
        }

        private async Task<JsonElement?> FetchJson(string url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", "dc918d38-d079-4cb4-9f09-1f4c7f9cf9a9");
                var response = await _http.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonDocument.Parse(content).RootElement;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch: {url}");
                return null;
            }
        }

        private class PlayerStats
        {
            public double PPG { get; set; }
            public double APG { get; set; }
            public double RPG { get; set; }
            public int TeamId { get; set; }
        }
    }
}