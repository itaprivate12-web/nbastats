using System.Runtime.CompilerServices;

namespace Project.Data
{
    public class UserService
    {
        public List<Player> GetAllPlayers()
        {
            string sql = "SELECT * FROM Players;";

            return DbHelper.RunSelect<Player>(sql);
        }

        public List<Teams> GetAllTeams()
        {
            string sql = "SELECT * FROM Teams;";

            return DbHelper.RunSelect<Teams>(sql);
        }

        public List<Player> GetPlayer(string name)
        {
            string sql = "SELECT * FROM Players WHERE Name = {};";

            return DbHelper.RunSelect<Player>(sql, name);
        }

        public List<Player> GetPlayerById(int id)
        {             
            string sql = "SELECT * FROM Players WHERE Id = {};";

            return DbHelper.RunSelect<Player>(sql, id);
        }
    
        public List<Teams> GetTeamById(int id)
        {
            string sql = "SELECT * FROM Teams WHERE Id = {};";
            return DbHelper.RunSelect<Teams>(sql, id);
        }

        public List<Player> GetFeaturedPlayers()
        {
            string sql = $@"SELECT Players.Id, Players.TeamId, Players.Name, Players.PPG, Players.APG, Players.RPG, Players.ImageUrl, Teams.TeamName
                           FROM Players
                           JOIN Teams ON Players.TeamId = Teams.Id
                           WHERE Players.Name IN ('Deni Avdija', 'LeBron James', 'Stephen Curry', 'Luka Dončić');";

            return DbHelper.RunSelect<Player>(sql);
        }
        public bool IsPlayer(string search)
        {
            string sql = "SELECT * FROM Players WHERE Name LIKE {};";

            return DbHelper.RunSelect<Player>(sql, $"%{search}%").Count > 0;
        }

        public List<Search> Search(string search)
        {
            string sql = @"SELECT Id, Name, ImageUrl, 1 as IsPlayer FROM Players WHERE Name LIKE {}
                        UNION 
                        SELECT Id, TeamName, LogoUrl, 0 as IsPlayer FROM Teams WHERE TeamName LIKE {};";

            string str = $"%{search}%";

            return DbHelper.RunSelect<Search>(sql, str, str);
        }

        public List<PlayerPage> GetPlayerPageById(int id)
        {
            string sql = @"SELECT Players.TeamId, Players.Name, Players.Position, Players.PPG, Players.APG, Players.RPG, Players.ImageUrl, Teams.TeamName, Teams.LogoUrl, Info.Weight, Info.Height, Info.DraftYear, Info.DraftPick
                            FROM Players
                            JOIN Info ON Players.Id = Info.PlayerId
                            JOIN Teams ON Players.TeamId = Teams.Id
                            WHERE Players.Id = {};";

            return DbHelper.RunSelect<PlayerPage>(sql, id);
        }
    }
}
