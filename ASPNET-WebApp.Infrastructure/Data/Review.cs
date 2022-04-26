using ASPNET_WebApp.Infrastructure.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_WebApp.Infrastructure.Data
{
    public class Review
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(Anime))]
        public string AnimeId { get; set; }
        public Anime Anime { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        [Range(0.0, 10.0)]
        public double Score { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}