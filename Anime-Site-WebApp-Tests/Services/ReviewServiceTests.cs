using Anime_Site_WebApp_Tests.Mock;
using Anime_Site_WebApp_Tests.TestConstants;
using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Anime_Site_WebApp_Tests.Services
{
	public class ReviewServiceTests : IClassFixture<WebAppMock>
    {
        private IReviewService reviewService;
        private IAnimeService animeService;
        private readonly DependencyScope scope;

        public ReviewServiceTests(WebAppMock appMock)
        {
            scope = appMock.InitDb();
            reviewService = scope.ResolveService<IReviewService>();
            animeService = scope.ResolveService<IAnimeService>();
        }

        [Fact]
        public async Task CreateReview_Should_Add_Review_To_DB_And_Anime()
        {
            //Arrange
            AnimeCreateViewModel animeView = TestDataConstants.AnimeCreate;
            await animeService.CreateAnime(animeView);

            var createdAnime = await scope.DB.Animes.FirstOrDefaultAsync();

            Review review = TestDataConstants.Review;
            review.AnimeId = createdAnime.Id;
            var reviewAdded = await reviewService.AddReviewToAnime(review);


            //Act
            var reviewCountAfterCreation = await scope.DB.Reviews.CountAsync();
            var updatedAnime = await animeService.GetAnimeById(createdAnime.Id);

            //Assert
            Assert.True(reviewAdded);
            Assert.Equal(2, reviewCountAfterCreation);
            Assert.Equal(2, updatedAnime.Reviews.Count);
            Assert.Equal(6, updatedAnime.Score);
        }


        [Fact]
        public async Task CreateReview_Should_Return_False_With_No_Anime()
        {
            //Arrange
            Review review = TestDataConstants.Review;

            //Act
            var shouldBeFalse = await reviewService.AddReviewToAnime(review);

            //Assert
            Assert.False(shouldBeFalse);
        }

        [Fact]
        public async Task GetReviews_Should_Return_List_Of_Reviews()
        {
            //Arrange
            AnimeCreateViewModel animeView = TestDataConstants.AnimeCreate;
            await animeService.CreateAnime(animeView);

            var createdAnime = await scope.DB.Animes.FirstOrDefaultAsync();

            Review review = TestDataConstants.Review;
            review.AnimeId = createdAnime.Id;
            await reviewService.AddReviewToAnime(review);
            await reviewService.AddReviewToAnime(review);

            //Act
            var reviews = await reviewService.GetReviews();

            //Assert
            Assert.IsType<List<Review>>(reviews);
        }

        [Fact]
        public async Task GetReviewsByAnime_Should_Return_Reviews_By_Anime_Id()
        {
            //Arrange
            AnimeCreateViewModel animeView = TestDataConstants.AnimeCreate;
            await animeService.CreateAnime(animeView);

            var createdAnime = await scope.DB.Animes.FirstOrDefaultAsync();

            Review review = TestDataConstants.Review;
            review.AnimeId = createdAnime.Id;
            await reviewService.AddReviewToAnime(review);
            await reviewService.AddReviewToAnime(review);

            //Act
            var reviews = await reviewService.GetReviewsByAnimeId(createdAnime.Id);

            //Assert
            Assert.IsType<List<AnimeReviewsListViewModel>>(reviews);
            Assert.Equal(3, reviews.Count);
        }

        [Fact]
        public async Task GetReviewById_Returns_Review()
        {
            //Arrange
            AnimeCreateViewModel animeView = TestDataConstants.AnimeCreate;
            await animeService.CreateAnime(animeView);

            var createdAnime = await scope.DB.Animes.FirstOrDefaultAsync();

            Review review = TestDataConstants.Review;
            review.AnimeId = createdAnime.Id;
            await reviewService.AddReviewToAnime(review);

            //Act
            var reviewCreated = await scope.DB.Reviews.FirstOrDefaultAsync();
            var reviewExits = await reviewService.GetReviewById(reviewCreated.Id);

            //Assert
            Assert.Equal(reviewCreated.Id, reviewExits.Id);
        }


    }
}
