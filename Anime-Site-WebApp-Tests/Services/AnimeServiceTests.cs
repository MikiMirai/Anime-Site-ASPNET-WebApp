using Anime_Site_WebApp_Tests.Mock;
using Anime_Site_WebApp_Tests.TestConstants;
using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            await animeService.CreateAnime(animeView);

            //Act
            var animeCountAfterCreation = await scope.DB.Animes.CountAsync();

            //Assert
            Assert.Equal(1, animeCountAfterCreation);
        }

        [Fact]
        public async Task AnimeAlreadyExist_Returns_True_If_Anime_Exits()
        {
            //Arrange
            AnimeCreateViewModel animeView = TestDataConstants.AnimeCreate;
            await animeService.CreateAnime(animeView);

            //Act
            var animeExits = await animeService.AnimeAlreadyExistInCollection(animeView.Name);

            //Assert
            Assert.True(animeExits);
        }

        [Fact]
        public async Task AnimeAlreadyExist_Returns_False_If_Anime_Doesnt_Exits()
        {
            //Act
            var animeExits = await animeService.AnimeAlreadyExistInCollection("Random");

            //Assert
            Assert.False(animeExits);
        }

        [Fact]
        public async Task GetAnimeForEdit_Returns_ArgumentNullExeption()
        {
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => animeService.GetAnimeForEdit("asd"));
        }

        [Fact]
        public async Task GetAnimeForEdit_Returns_Anime()
        {
            //Arrange
            AnimeCreateViewModel animeView = TestDataConstants.AnimeCreate;
            await animeService.CreateAnime(animeView);

            var anime = await scope.DB.Animes.FirstOrDefaultAsync();

            //Act
            var animeExits = await animeService.GetAnimeForEdit(anime.Id);

            //Assert
            Assert.Equal(anime.Id, animeExits.Id);
        }

        [Fact]
        public async Task UpdateAnime_Returns_False()
        {
            //Arrange
            AnimeEditViewModel emptyModel = new AnimeEditViewModel();

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => animeService.UpdateAnime(emptyModel));
        }

        [Fact]
        public async Task UpdateAnime_Returns_UpdatedAnime()
        {
            //Arrange
            AnimeCreateViewModel animeView = TestDataConstants.AnimeCreate;
            await animeService.CreateAnime(animeView);

            var anime = await scope.DB.Animes.FirstOrDefaultAsync();
            AnimeEditViewModel changedAnime = TestDataConstants.AnimeUpdated;

            changedAnime.Id = anime.Id;

            //Act
            var shouldBeTrue = await animeService.UpdateAnime(changedAnime);
            var updatedAnime = await animeService.GetAnimeForEdit(anime.Id);

            //Assert
            Assert.True(shouldBeTrue);
            Assert.Equal(updatedAnime.Name, changedAnime.Name);
        }

        [Fact]
        public async Task GetAnimeById_Returns_ArgumentNullExeption()
        {
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => animeService.GetAnimeById("asd"));
        }

        [Fact]
        public async Task GetAnimeById_Returns_Anime()
        {
            //Arrange
            AnimeCreateViewModel animeView = TestDataConstants.AnimeCreate;
            await animeService.CreateAnime(animeView);

            var anime = await scope.DB.Animes.FirstOrDefaultAsync();

            //Act
            var animeExits = await animeService.GetAnimeById(anime.Id);

            //Assert
            Assert.Equal(anime.Id, animeExits.Id);
        }

        [Fact]
        public async Task GetAnimes_Returns_Anime()
        {
            //Arrange
            AnimeCreateViewModel animeView = TestDataConstants.AnimeCreate;
            await animeService.CreateAnime(animeView);

            //Act
            var animes = await animeService.GetAnimes();

            //Assert
            Assert.IsType<List<AnimeListViewModel>>(animes);
        }

        [Fact]
        public async Task GetAnimesTrending_Returns_Anime()
        {
            //Arrange
            AnimeCreateViewModel animeView = TestDataConstants.AnimeCreate;
            await animeService.CreateAnime(animeView);

            //Act
            var animes = await animeService.GetAnimesTrending();

            //Assert
            Assert.IsType<List<AnimeListViewModel>>(animes);
        }
    }
}
