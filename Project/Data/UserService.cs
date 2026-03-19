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

        public List<TeamPage> GetTeamPageById(int id)
        {
            string sql = @"SELECT Teams.Id, Teams.TeamName, Teams.LogoUrl, TeamInfo.Conference, TeamInfo.ShortName, TeamInfo.Arena, TeamInfo.Coach
                            FROM TeamInfo
                            JOIN Teams ON TeamInfo.TeamId = Teams.Id
                            WHERE Teams.Id = {};";

            return DbHelper.RunSelect<TeamPage>(sql, id);
        }

        public List<Player> GetTeamMates(int teamId, int id)
        {
            string sql = "SELECT * FROM Players WHERE TeamId = {} AND Id != {}";

            return DbHelper.RunSelect<Player>(sql, teamId, id);
        }

        public List<Player> SortByPPG(List<Player> Players)
        {
            string sql = "SELECT * FROM Players ORDER BY PPG DESC;";

            return DbHelper.RunSelect<Player>(sql);
        }

        public List<Player> SortByAPG(List<Player> Players)
        {
            string sql = "SELECT * FROM Players ORDER BY APG DESC;";

            return DbHelper.RunSelect<Player>(sql);
        }

        public List<Player> SortByRPG(List<Player> Players)
        {
            string sql = "SELECT * FROM Players ORDER BY RPG DESC;";

            return DbHelper.RunSelect<Player>(sql);
        }

        public List<Player> GetPlayersInTeam(int id)
        {
            string sql = "SELECT * FROM Players WHERE TeamId = {};";

            return DbHelper.RunSelect<Player>(sql, id);
        }

        public List<Teams> SortByWest(List<Teams> teams)
        {
            string sql = "SELECT * FROM Teams WHERE Conference = 'West';";
            
            return DbHelper.RunSelect<Teams>(sql);
        }

        public List<Teams> SortByEast(List<Teams> teams)
        {
            string sql = "SELECT * FROM Teams WHERE Conference = 'East';";
            
            return DbHelper.RunSelect<Teams>(sql);
        }

        public void CreateAccount(string fullName, string userName, string email, string password)
        {
            string sql = @"INSERT INTO Users(FullName, UserName, Email, Password)
                        VALUES ({}, {}, {}, {})";

            DbHelper.RunSqlChange(sql, fullName, userName, email, password);
        }

        public bool CheckLogin(string name, string password)
        {
            string sql = "SELECT * FROM Users WHERE UserName = {} AND Password = {} OR Email = {} AND Password = {}";

            return DbHelper.RunSelect<Users>(sql, name, password, name, password).Count > 0;
        }
    }
}
