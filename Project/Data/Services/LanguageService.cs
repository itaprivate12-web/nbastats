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
                ["PPG"] = new()
                {
                    ["he"] = "ממוצע נקודות למשחק:",
                    ["en"] = "Points Per Game:"
                },
                ["APG"] = new()
                {
                    ["he"] = "ממוצע אסיסטים למשחק:",
                    ["en"] = "Assist Per Game:"
                },
                ["RPG"] = new()
                {
                    ["he"] = "ממוצע ריבאונדים למשחק:",
                    ["en"] = "Rebounds Per Game:"
                },
                ["Height"] = new()
                {
                    ["he"] = "גובה:",
                    ["en"] = "Height:"
                },
                ["Weight"] = new()
                {
                    ["he"] = "משקל:",
                    ["en"] = "Weight"
                },
                ["DraftYear"] = new()
                {
                    ["he"] = "שנת דראפט:",
                    ["en"] = "Draft Year"
                },
                ["DraftPick"] = new()
                {
                    ["he"] = "בחירת דראפט:",
                    ["en"] = "Draft Pick:"
                },
                ["Teammates"] = new()
                {
                    ["he"] = "חברים לקבוצה",
                    ["en"] = "Teammates"
                },
                ["SearchBarPlaceholder"] = new()
                {
                    ["he"] = "חפש שחקן או קבוצה...",
                    ["en"] = "Search for a Player or a Team..."
                },
                ["TopPlayers"] = new()
                {
                    ["he"] = "שחקנים מובילים:",
                    ["en"] = "Top Players:"
                },
                ["AllNbaTeams"] = new()
                {
                    ["he"] = "כל קבוצות ה-NBA: ",
                    ["en"] = "All NBA Teams: "
                },
                ["ShortName"] = new()
                {
                    ["he"] = "קיצור:",
                    ["en"] = "ShortName:"
                },
                ["Conference"] = new()
                {
                    ["he"] = "מיקום:",
                    ["en"] = "Conference:"
                },
                ["Coach"] = new()
                {
                    ["he"] = "מאמן:",
                    ["en"] = "Coach:"
                },
                ["Arena"] = new()
                {
                    ["he"] = "אולם:",
                    ["en"] = "Arena:"
                },
                ["TeamsPlayers"] = new()
                {
                    ["he"] = "שש:",
                    ["en"] = "Team's Players:"
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
                ["Name"] = new()
                {
                    ["he"] = "שם משתמש או אימייל:",
                    ["en"] = "Username or Email:"
                },
                ["Password"] = new()
                {
                    ["he"] = "סיסמא:",
                    ["en"] = "Password:"
                },
                ["RememberMe"] = new()
                {
                    ["he"] = "תזכור אותי",
                    ["en"] = "Remember Me"
                },
                ["Credentials"] = new()
                {
                    ["he"] = "פרטי התחברות לא נכונים",
                    ["en"] = "Wrong credentials"
                },
                ["Register"] = new()
                {
                    ["he"] = "הרשמה",
                    ["en"] = "Register"
                },
                ["Create"] = new()
                {
                    ["he"] = "צור משתמש",
                    ["en"] = "Create Account"
                },
                ["FullName"] = new()
                {
                    ["he"] = "שם מלא:",
                    ["en"] = "Full Name:"
                },
                ["Email"] = new()
                {
                    ["he"] = "אימייל:",
                    ["en"] = "Email:"
                },
                ["Already"] = new()
                {
                    ["he"] = "כבר יצרת משתמש?",
                    ["en"] = "Already have an account?"
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
                ["AllNbaPlayers"] = new()
                {
                    ["he"] = "כל שחקני ה-NBA:",
                    ["en"] = "All NBA Players:"
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