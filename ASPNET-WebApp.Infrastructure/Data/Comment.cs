using ASPNET_WebApp.Infrastructure.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_WebApp.Infrastructure.Data
{
    public class Comment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        [ForeignKey(nameof(Review))]
        public string ReviewId { get; set; }
        public Review Review { get; set; }

        [Required]
        [StringLength(30)]
        public string Date { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }
    }
}
