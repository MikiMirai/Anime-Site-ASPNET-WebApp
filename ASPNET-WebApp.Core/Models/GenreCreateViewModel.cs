using System.ComponentModel.DataAnnotations;

namespace ASPNET_WebApp.Core.Models
{
    public class GenreCreateViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Name { get; set; }
    }
}
