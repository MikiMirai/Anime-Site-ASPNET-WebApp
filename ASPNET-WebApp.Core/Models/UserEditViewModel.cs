using System.ComponentModel.DataAnnotations;

namespace ASPNET_WebApp.Core.Models
{
    public class UserEditViewModel
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }
    }
}
