using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;
using ASPNET_WebApp.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_WebApp.Core.Services
{
	public class GenreService : IGenreService
	{
        public readonly ApplicationDbContext dbContext;
        public readonly IApplicationDbRepository repo;
        public GenreService(ApplicationDbContext _dbContext, IApplicationDbRepository _repo)
        {
            dbContext = _dbContext;
            repo = _repo;
        }

        public async Task<bool> AddAnimeGenresAsync(Anime anime, List<ManageAnimeGenreViewModel> model)
        {
            try
            {
                var animeGenres = dbContext.Animes.Where(x => x.Id == anime.Id)
                .Include(x => x.Genres)
                .FirstOrDefault();
                foreach (var genre in model)
                {
                    if (genre.Selected)
                    {
                        Genre tagFound = dbContext.Genres.FirstOrDefault(t => t.Id == genre.GenreId);
                        animeGenres.Genres.Add(tagFound);
                        await repo.SaveChangesAsync();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CreateGenre(GenreCreateViewModel model)
        {
            if (await GenreAlreadyExistInCollection(model.Name))
            {
                return false;
            }

            var genre = new Genre()
            { 
                Name = model.Name,
            };

            await dbContext.Genres.AddAsync(genre);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<GenreEditViewModel> GetGenreForEdit(string id)
        {
            var genre = await repo.GetByIdAsync<Genre>(id);

            if (genre == null)
            {
                throw new ArgumentNullException(nameof(genre));
            }

            var foundGenre = new GenreEditViewModel()
            {
                Id = genre.Id,
                Name = genre.Name
            };

            return foundGenre;
        }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            var genres = this.dbContext.Genres.ToList();

            return genres;
        }

        public async Task<bool> HasGenre(Anime anime, string name)
        {
            var genre = dbContext.Genres.Where(g => g.Name == name).FirstOrDefault();

            if (anime.Genres == null)
            {
                return false;
            }

            if (anime.Genres.Contains(genre))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveAnimeGenresAsync(Anime anime, List<Genre> genres)
        {
            anime.Genres = null;

            if (anime.Genres == null)
            {
                await repo.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateGenre(GenreEditViewModel model)
		{
            bool result = false;

            var genre = await repo.GetByIdAsync<Genre>(model.Id);

            if (genre != null)
            {
                genre.Name = model.Name;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<bool> GenreAlreadyExistInCollection(string genreName)
        {
            var genre = await dbContext.Genres.FirstOrDefaultAsync(x => x.Name == genreName);

            if (genre == null)
            {
                return false;
            }

            return true;
        }
    }
}
