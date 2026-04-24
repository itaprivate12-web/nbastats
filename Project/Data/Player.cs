namespace Project.Data
{
    public class Player
    {
        public int Id { get; set; }
        public int NbaId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public double PPG { get; set; }
        public double APG { get; set; }
        public double RPG { get; set; }
        public string ImageUrl { get; set; }
        public string TeamName { get; set; }
        public string Position { get; set; }
    }
}
