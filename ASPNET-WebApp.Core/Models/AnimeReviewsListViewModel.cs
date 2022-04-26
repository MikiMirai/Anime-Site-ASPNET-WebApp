namespace ASPNET_WebApp.Core.Models
{
	public class AnimeReviewsListViewModel
	{
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Date { get; set; }

        public string Description { get; set; }

        public double Score { get; set; }

		public int CommentsCount { get; set; }
    }
}
