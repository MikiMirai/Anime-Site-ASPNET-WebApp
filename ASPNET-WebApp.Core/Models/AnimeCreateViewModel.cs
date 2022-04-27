using System.ComponentModel.DataAnnotations;
using static ASPNET_WebApp.Core.Constants.DataValidationConstants;

namespace ASPNET_WebApp.Core.Models
{
    public class AnimeCreateViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(AnimeNameMaxLength, MinimumLength = AnimeNameMinLength, ErrorMessage = MustBeBetween)]
        public string Name { get; set; }

        [Url]
        [Required]
        [StringLength(ImageUrlMaxLength, MinimumLength = ImageUrlMinLength, ErrorMessage = "{0} must be between {2} and {1} characters and must be a valid URL.")]
        public string Image { get; set; }

        [StringLength(ImageUrlMaxLength, MinimumLength = ImageUrlMinLength, ErrorMessage = MustBeBetween)]
        public string Aired { get; set; }

        [Range(0, 10000, ErrorMessage = MustBeBetween)]
        public int Episodes { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = MustBeBetween)]
        public string Status { get; set; }

        [Required]
        [StringLength(DescriptionAnimeMaxLength, MinimumLength = DescriptionAnimeMinLength, ErrorMessage = MustBeBetween)]
        public string Description { get; set; }

        [StringLength(MaxLength200, MinimumLength = MinLength10, ErrorMessage = MustBeBetween)]
        public string Studios { get; set; }

        [Required]
        [StringLength(MaxLength200, MinimumLength = MinLength10, ErrorMessage = MustBeBetween)]
        public string Producers { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = MustBeBetween)]
        public string Duration { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = MustBeBetween)]
        public string Rating { get; set; }
    }
}
