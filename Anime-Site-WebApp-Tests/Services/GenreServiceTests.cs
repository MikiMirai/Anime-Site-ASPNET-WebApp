using Anime_Site_WebApp_Tests.Mock;
using Anime_Site_WebApp_Tests.TestConstants;
using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;
using ASPNET_WebApp.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Anime_Site_WebApp_Tests.Services
{
	public class GenreServiceTests : IClassFixture<WebAppMock>
    {
        private IGenreService genreService;
        private IApplicationDbRepository repo;
        private readonly DependencyScope scope;

		public GenreServiceTests(WebAppMock appMock)
		{
			scope = appMock.InitDb();
			genreService = scope.ResolveService<IGenreService>();
			repo = scope.ResolveService<IApplicationDbRepository>();
		}

        [Fact]
        public async Task CreateGenre_Should_Add_Genre_To_DB()
        {
            //Arrange
            GenreCreateViewModel genreView = TestDataConstants.GenreCreate;
            var genreCreated = await genreService.CreateGenre(genreView);

            //Act
            var genreCountAfterCreation = await scope.DB.Genres.CountAsync();

            //Assert
            Assert.True(genreCreated);
            Assert.Equal(1, genreCountAfterCreation);
        }

        [Fact]
        public async Task GenreAlreadyExist_Returns_True_If_Genre_Exits()
        {
            //Arrange
            GenreCreateViewModel genreView = TestDataConstants.GenreCreate;
            await genreService.CreateGenre(genreView);

            //Act
            var genreExits = await genreService.GenreAlreadyExistInCollection(genreView.Name);

            //Assert
            Assert.True(genreExits);
        }

        [Fact]
        public async Task GenreAlreadyExist_Returns_False_If_Genre_Doesnt_Exits()
        {
            //Act
            var genreExits = await genreService.GenreAlreadyExistInCollection("Random");

            //Assert
            Assert.False(genreExits);
        }

        [Fact]
        public async Task GetGenreForEdit_Returns_ArgumentNullExeption()
        {
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => genreService.GetGenreForEdit("asd"));
        }

        [Fact]
        public async Task GetGenreForEdit_Returns_Genre()
        {
            //Arrange
            GenreCreateViewModel genreView = TestDataConstants.GenreCreate;
            await genreService.CreateGenre(genreView);

            var genre = await scope.DB.Genres.FirstOrDefaultAsync();

            //Act
            var genreExits = await genreService.GetGenreForEdit(genre.Id);

            //Assert
            Assert.Equal(genre.Id, genreExits.Id);
        }

        [Fact]
        public async Task UpdateGenre_Returns_False()
        {
            //Arrange
            GenreEditViewModel emptyModel = new GenreEditViewModel();

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => genreService.UpdateGenre(emptyModel));
        }

        [Fact]
        public async Task UpdateGenre_Returns_UpdatedGenre()
        {
            //Arrange
            GenreCreateViewModel genreView = TestDataConstants.GenreCreate;
            await genreService.CreateGenre(genreView);

            var genre = await scope.DB.Genres.FirstOrDefaultAsync();

            GenreEditViewModel changedGenre = new GenreEditViewModel
			{
                Id = genre.Id,
                Name = "NewFantasy",
			};

            //Act
            var shouldBeTrue = await genreService.UpdateGenre(changedGenre);
            var updatedGenre = await genreService.GetGenreForEdit(genre.Id);

            //Assert
            Assert.True(shouldBeTrue);
            Assert.Equal(changedGenre.Name, updatedGenre.Name);
        }

        [Fact]
        public async Task GetGenres_Returns_Genres()
        {
            //Arrange
            GenreCreateViewModel genreView = TestDataConstants.GenreCreate;
            await genreService.CreateGenre(genreView);

            //Act
            var genres = await genreService.GetGenres();

            //Assert
            Assert.IsType<List<Genre>>(genres);
        }
    }
}
