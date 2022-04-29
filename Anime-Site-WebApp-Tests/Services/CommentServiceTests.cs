using Anime_Site_WebApp_Tests.Mock;
using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Anime_Site_WebApp_Tests.Services
{
    public class CommentServiceTests : IClassFixture<WebAppMock>
    {
        private IReviewService reviewService;
        private ICommentService commentService;
        private readonly DependencyScope scope;

        public CommentServiceTests(WebAppMock appMock)
        {
            scope = appMock.InitDb();
            reviewService = scope.ResolveService<IReviewService>();
            commentService = scope.ResolveService<ICommentService>();
        }


        [Fact]
        public async Task CreateComment_Should_Add_Comment_To_DB_And_Review()
        {
            //Arrange
            var review = await scope.DB.Reviews.FirstOrDefaultAsync();

            Comment comment = new Comment()
            {
                UserId = WebAppMock.UserId,
                ReviewId = review.Id,
                Description = "Random Review2",
                Date = DateTime.Now.ToString(),
            };
            await commentService.AddCommentToReview(comment);

            //Act
            var commentCountAfterCreation = await scope.DB.Comments.CountAsync();
            var updatedReview = await reviewService.GetReviewById(review.Id);

            //Assert
            Assert.Equal(2, commentCountAfterCreation);
            Assert.Equal(2, updatedReview.Comments.Count);
        }

        [Fact]
        public async Task CreateComment_Should_Return_False_With_No_Review()
        {
            //Arrange
            Comment comment = new Comment();

            //Act
            var shouldBeFalse = await commentService.AddCommentToReview(comment);

            //Assert
            Assert.False(shouldBeFalse);
        }

        [Fact]
        public async Task GetComments_Should_Return_List_Of_Comments()
        {
            //Act
            var comments = await commentService.GetComments();

            //Assert
            Assert.IsType<List<Comment>>(comments);
        }

        [Fact]
        public async Task GetCommentsByReview_Should_Return_Comments_Of_Review()
        {
            //Arrange
            var review = await scope.DB.Reviews.FirstOrDefaultAsync();

            Comment comment = new Comment()
            {
                UserId = WebAppMock.UserId,
                ReviewId = review.Id,
                Description = "Random Review2",
                Date = DateTime.Now.ToString(),
            };
            await commentService.AddCommentToReview(comment);

            //Act
            var comments = await commentService.GetCommentsByReview(review.Id);

            //Assert
            Assert.IsType<List<Comment>>(comments);
            Assert.Equal(2, comments.Count);
        }
    }
}
