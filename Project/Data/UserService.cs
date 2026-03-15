using System.Runtime.CompilerServices;

namespace Project.Data
{
    public class UserService
    {
        public List<Player> GetPlayer(string name)
        {
            string sql = "SELECT * FROM Players WHERE Name = {};";

            return DbHelper.RunSelect<Player>(sql, name);
        }

        public List<Player> GetFeaturedPlayers()
        {
            string sql = $@"SELECT Players.Id, Players.TeamId, Players.Name, Players.PPG, Players.APG, Players.RPG, Players.ImageUrl, Teams.TeamName
                           FROM Players
                           JOIN Teams ON Players.TeamId = Teams.Id
                           WHERE Players.Name IN ('Deni Avdija', 'LeBron James', 'Stephen Curry', 'Luka Dončić');";

            return DbHelper.RunSelect<Player>(sql);
        }

        public List<Teams> GetAllTeams()
        {
            string sql = "SELECT * FROM Teams;";

            return DbHelper.RunSelect<Teams>(sql);
        }

        public bool IsPlayer(string search)
        {
            string sql = "SELECT * FROM Players WHERE Name = {}";

            if (DbHelper.RunSelect<Player>(sql, search).Count > 0)
                return true;
            
            return false;
        }

        public List<Search> Search(string search)
        {
            string sql = @"SELECT Id, Name, ImageUrl FROM Players WHERE Name LIKE {}
                        UNION 
                        SELECT Id, TeamName, LogoUrl FROM Teams WHERE TeamName LIKE {};";

            string str = $"%{search}%";

            return DbHelper.RunSelect<Search>(sql, str, str);
        }
    }
}
