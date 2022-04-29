using Anime_Site_WebApp_Tests.Mock;
using Anime_Site_WebApp_Tests.TestConstants;
using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Anime_Site_WebApp_Tests.Services
{
    public class AnimeServiceTests : IClassFixture<WebAppMock>
    {
        private IAnimeService animeService;
        private readonly DependencyScope scope;

        public AnimeServiceTests(WebAppMock appMock)
        {
            scope = appMock.InitDb();
            animeService = scope.ResolveService<IAnimeService>();
        }

        [Fact]
        public async Task CreateAnime_Should_Add_Anime_To_DB()
        {
            //Arrange
            AnimeCreateViewModel animeView = TestDataConstants.AnimeCreate;

            //Act
            await animeService.CreateAnime(animeView);
            var animeCountAfterCreation = await scope.DB.Animes.CountAsync();

            //Assert
            Assert.Equal(1, animeCountAfterCreation);
        }


    }
}
