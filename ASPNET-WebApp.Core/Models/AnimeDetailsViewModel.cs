using ASPNET_WebApp.Infrastructure.Data;

namespace ASPNET_WebApp.Core.Models
{
    public class AnimeDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public double Score { get; set; }

        public string Aired { get; set; }

        public int Episodes { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public string Studios { get; set; }

        public string Producers { get; set; }

        public string Duration { get; set; }

        public string Rating { get; set; }

        public string Genres { get; set; }

        public int ReviewsCount { get; set; }
    }
}
