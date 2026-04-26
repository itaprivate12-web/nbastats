namespace Project.Data
{
    public class MyPlayer
    {
        public int Id {get; set; }
        public int UserId { get; set; }
        public int? TeamId { get; set; }
        public string Name { get; set; }
        public string PPG { get; set; } 
        public string APG { get; set; } 
        public string RPG { get; set; } 
        public string Position { get; set; }
        public string? TeamName { get; set; }

    }
}