using ASPNET_WebApp.Infrastructure.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_WebApp.Infrastructure.Data
{
    public class Review
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
        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        [Range(0.0, 10.0)]
        public double Score { get; set; }

        public ICollection<Comment> Reviews { get; set; } = new List<Comment>();
    }
}