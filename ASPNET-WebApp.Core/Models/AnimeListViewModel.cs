namespace ASPNET_WebApp.Core.Models
{
    public class AnimeListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string Aired { get; set; }
        public int Episodes { get; set; }
        public string Status { get; set; }
        public int ReviewCount { get; set; }
    }
}