using System.ComponentModel.DataAnnotations;

namespace ASPNET_WebApp.Core.Models
{
    public class ManageAnimeGenreViewModel
    {
        public string GenreId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "The Genre Name must be between {2} and {1} characters.")]
        public string GenreName { get; set; }

        public bool Selected { get; set; }
    }
}
