using System.ComponentModel.DataAnnotations;

namespace ASPNET_WebApp.Core.Models
{
	public class AnimeReviewsListViewModel
	{
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Date { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Description { get; set; }

        public double Score { get; set; }

		public int CommentsCount { get; set; }
    }
}
