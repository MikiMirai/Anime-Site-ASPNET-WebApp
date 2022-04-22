using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Infrastructure.Data;
using ASPNET_WebApp.Infrastructure.Data.Repositories;

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
            var genre = await repo.GetByIdAsync<Anime>(id);

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
