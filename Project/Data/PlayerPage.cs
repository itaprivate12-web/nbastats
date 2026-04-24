namespace Project.Data
{
    public class PlayerPage
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string PPG { get; set; }
        public string APG { get; set; }
        public string RPG { get; set; }
        public string ImageUrl { get; set; }
        public string TeamName { get; set; }
        public string LogoUrl { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string DraftYear { get; set; }
        public int DraftPick { get; set; }
    }
}