using System.ComponentModel.DataAnnotations;
using static ASPNET_WebApp.Core.Constants.DataValidationConstants;

namespace ASPNET_WebApp.Core.Models
{
    public class ForumPostCreateViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(MaxLength2000, MinimumLength = MinLength10, ErrorMessage = MustBeBetween)]
        public string Description { get; set; }
    }
}
