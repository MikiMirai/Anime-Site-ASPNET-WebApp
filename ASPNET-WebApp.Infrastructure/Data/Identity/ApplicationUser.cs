using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ASPNET_WebApp.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public byte[]? ProfilePicture { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
