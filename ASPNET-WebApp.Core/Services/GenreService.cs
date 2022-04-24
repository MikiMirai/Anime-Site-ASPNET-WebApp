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
        public GenreService(ApplicationDbContext _data, IApplicationDbRepository _repo)
        {
            dbContext = _data;
            repo = _repo;
        }

        public async Task<bool> AddAnimeGenresAsync(Anime anime, List<ManageAnimeGenreViewModel> model)
        {
            anime.Genres = new List<Genre>();
            try
            {
                foreach (var genre in model)
                {
                    if (genre.Selected)
                    {
                        Genre tagFound = dbContext.Genres.FirstOrDefault(g => g.Id == genre.GenreId);
                        anime.Genres.Add(tagFound);
                    }

                }

                var animeChanged = await repo.GetByIdAsync<Anime>(anime.Id);
                animeChanged.Genres = anime.Genres;

                await repo.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CreateGenre(string name)
        {
            var genre = new Genre()
            { 
                Name = name
            };
            await dbContext.Genres.AddAsync(genre);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Genre> GetGenreForEdit(string id)
        {
            var genre = await repo.GetByIdAsync<Genre>(id);

            if (genre == null)
            {
                throw new ArgumentNullException(nameof(genre));
            }

            var foundGenre = new Genre()
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

        public async Task<bool> UpdateGenre(Genre model)
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
	}
}
