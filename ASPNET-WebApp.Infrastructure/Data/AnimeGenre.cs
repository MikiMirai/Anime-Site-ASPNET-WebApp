using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_WebApp.Infrastructure.Data
{
    public class AnimeGenre
    {
        [ForeignKey(nameof(Anime))]
        public string AnimeId { get; set; }
        public Anime Anime { get; set; }

        [ForeignKey(nameof(Genre))]
        public string GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
