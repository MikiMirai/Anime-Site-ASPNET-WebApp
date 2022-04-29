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
	public class ForumPostServiceTests : IClassFixture<WebAppMock>
	{
        private IForumPostService forumPostService;
        private readonly DependencyScope scope;

        public ForumPostServiceTests(WebAppMock appMock)
        {
            scope = appMock.InitDb();
            forumPostService = scope.ResolveService<IForumPostService>();
        }

        [Fact]
        public async Task CreateForumPost_Should_Add_ForumPost_To_DB()
        {
            //Arrange
            ForumPostCreateViewModel forumPost = TestDataConstants.ForumPostCreate;

            //Act
            var forumPostCreated = await forumPostService.CreateForumPost(forumPost, WebAppMock.UserId);
            var animeCountAfterCreation = await scope.DB.ForumPost.CountAsync();

            //Assert
            Assert.True(forumPostCreated);
            Assert.Equal(2, animeCountAfterCreation);
        }

        [Fact]
        public async Task GetPosts_Returns_ForumPosts()
        {
            //Arrange
            ForumPostCreateViewModel forumPost = TestDataConstants.ForumPostCreate;
            var forumPostCreated = await forumPostService.CreateForumPost(forumPost, WebAppMock.UserId);

            //Act
            var forumPosts = await forumPostService.GetPosts();

            //Assert
            Assert.True(forumPostCreated);
            Assert.IsType<List<ForumPostListViewModel>>(forumPosts);
        }
    }
}
