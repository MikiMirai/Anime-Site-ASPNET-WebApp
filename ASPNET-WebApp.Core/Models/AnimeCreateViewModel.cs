using ASPNET_WebApp.Infrastructure.Data;

namespace ASPNET_WebApp.Core.Models
{
    public class AnimeCreateViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string Aired { get; set; }

        public int Episodes { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string Studios { get; set; }

        public string Producers { get; set; }

        public List<Genre> Genres { get; set; }

        public string Duration { get; set; }

        public string Rating { get; set; }
    }
}
