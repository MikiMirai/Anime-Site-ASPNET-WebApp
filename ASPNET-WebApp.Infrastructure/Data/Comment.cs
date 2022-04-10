using ASPNET_WebApp.Infrastructure.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_WebApp.Infrastructure.Data
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        [MaxLength(450)]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }
    }
}
