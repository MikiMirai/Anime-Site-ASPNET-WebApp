using System.ComponentModel.DataAnnotations;

namespace ASPNET_WebApp.Core.Models
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} must be at least {2} and at max {1} characters long.")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
    }
}
