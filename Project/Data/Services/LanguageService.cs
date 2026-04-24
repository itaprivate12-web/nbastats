using System.Runtime.CompilerServices;

namespace Project.Data
{
    public class LanguageService
    {
        public string CurrentLanguage { get; private set; } = "en";
        public string CurrentDirection =>
            CurrentLanguage == "he" ? "rtl" : "ltr";

        public event Action OnChange;

        public void SetLanguage(string lang)
        {
            if (CurrentLanguage == lang) return; 

            CurrentLanguage = lang;
            OnChange?.Invoke();
        }


        private Dictionary<string, Dictionary<string, string>> _translations =
            new()
            {
                ["SearchBarPlaceholder"] = new()
                {
                    ["he"] = "חפש שחקן או קבוצה...",
                    ["en"] = "Search for a Player or a Team..."
                },
                ["TopPlayers"] = new()
                {
                    ["he"] = "שחקנים מובילים",
                    ["en"] = "Top Players"
                },
                ["AllNbaTeams"] = new()
                {
                    ["he"] = "כל קבוצות ה-NBA: ",
                    ["en"] = "All NBA Teams: "
                },
                ["Players"] = new()
                {
                    ["he"] = "שחקנים",
                    ["en"] = "Players"
                },
                ["Login"] = new()
                {
                    ["he"] = "להתחבר",
                    ["en"] = "Login"
                },
                ["Register"] = new()
                {
                    ["he"] = "הרשמה",
                    ["en"] = "Register"
                },
                ["Logout"] = new()
                {
                    ["he"] = "יציאה",
                    ["en"] = "Logout"
                },
                ["Teams"] = new()
                {
                    ["he"] = "קבוצות",
                    ["en"] = "Teams"
                },
                ["Home"] = new()
                {
                    ["he"] = "בית",
                    ["en"] = "Home"
                },
                ["Favorites"] = new()
                {
                    ["he"] = "מועדפים",
                    ["en"] = "Favorites"
                },
                ["Search"] = new()
                {
                    ["he"] = "חיפוש",
                    ["en"] = "Search"
                }
            };

        public string T(string key)
        {
            if (_translations.ContainsKey(key) && _translations[key].ContainsKey(CurrentLanguage))
            {
                return _translations[key][CurrentLanguage];
            }

            return key;
        }
    }
}