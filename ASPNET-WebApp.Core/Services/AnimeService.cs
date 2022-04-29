using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;
using ASPNET_WebApp.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ASPNET_WebApp.Core.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IApplicationDbRepository repo;

        private readonly ApplicationDbContext dbContext;

        public AnimeService(IApplicationDbRepository _repo, ApplicationDbContext _dbContext)
        {
            repo = _repo;
            dbContext = _dbContext;
        }

        /// <summary>
        /// Create tested, AlreadyExists tested.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> CreateAnime(AnimeCreateViewModel model)
        {
            if (await AnimeAlreadyExistInCollection(model.Name))
            {
                return false;
            }

            if (model == null)
            {
                throw new ArgumentNullException("The create model is null.");
            }

            var anime = new Anime
            {
                Id = model.Id,
                Name = model.Name,
                Image = model.Image,
                Aired = model.Aired,
                Episodes = model.Episodes,
                Status = model.Status,
                Description = model.Description,
                Studios = model.Studios,
                Producers = model.Producers,
                Duration = model.Duration,
                Rating = model.Rating,
            };

            await repo.AddAsync(anime);
            await repo.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Return animes tested.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AnimeListViewModel>> GetAnimes()
        {
            var animes = repo.All<Anime>();

            var animeView = await animes.Select(anime => new AnimeListViewModel()
            {
                Id = anime.Id,
                Name = anime.Name,
                Image = anime.Image,
                Aired = anime.Aired,
                Episodes = anime.Episodes,
                Status = anime.Status,
                ReviewCount = anime.Reviews.Count,
            })
                .OrderBy(a => a.Name)
                .ToListAsync();

            return animeView;
        }

        /// <summary>
        /// Returns animes tested.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AnimeListViewModel>> GetAnimesTrending()
        {
            var animes = repo.All<Anime>();

            var animeView = await animes.Select(anime => new AnimeListViewModel()
            {
                Id = anime.Id,
                Name = anime.Name,
                Image = anime.Image,
                Aired = anime.Aired,
                Episodes = anime.Episodes,
                Status = anime.Status,
                ReviewCount = anime.Reviews.Count,
            })
                .OrderByDescending(a => a.ReviewCount)
                .ToListAsync();

            return animeView;
        }

        /// <summary>
        /// Return anime tested, return exception tested.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Anime> GetAnimeById(string id)
        {
            var anime = await dbContext.Animes
                .Where(x => x.Id == id)
                .Include(x => x.Genres)
                .FirstOrDefaultAsync();

            if (anime == null)
            {
                throw new ArgumentNullException($"The anime with id:{id} does not exist.");
            }

            return anime;
        }

        /// <summary>
        /// Return anime tested, return exception tested.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Anime> GetAnimeForEdit(string id)
        {
            var anime = await dbContext.Animes.Where(x => x.Id == id)
                .Include(x => x.Genres)
                .FirstOrDefaultAsync();

            if (anime == null)
            {
                throw new ArgumentNullException(nameof(anime));
            }

            var foundAnime = new Anime()
            {
                Id = anime.Id,
                Name = anime.Name,
                Image = anime.Image,
                Aired = anime.Aired,
                Episodes = anime.Episodes,
                Status = anime.Status,
                Description = anime.Description,
                Studios = anime.Studios,
                Producers = anime.Producers,
                Genres = anime.Genres,
                Duration = anime.Duration,
                Rating = anime.Rating
            };

            return foundAnime;
        }

        /// <summary>
        /// Returns false tested, updates anime tested.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> UpdateAnime(AnimeEditViewModel model)
        {
            bool result = false;

            var anime = await repo.GetByIdAsync<Anime>(model.Id);

            if (anime != null)
            {
                anime.Name = model.Name;
                anime.Image = model.Image;
                anime.Aired = model.Aired;
                anime.Episodes = model.Episodes;
                anime.Status = model.Status;
                anime.Description = model.Description;
                anime.Studios = model.Studios;
                anime.Producers = model.Producers;
                anime.Genres = model.Genres;
                anime.Duration = model.Duration;
                anime.Rating = model.Rating;

                await repo.SaveChangesAsync();
                result = true;
            }
			else
			{
                throw new ArgumentNullException("The edit model is null.");
            }

            return result;
        }

        public async Task<AnimeDetailsViewModel> GetAnimeDetailsById(string id)
        {
            var anime = await dbContext.Animes.Where(x => x.Id == id)
                .Include(x => x.Reviews)
                .Include(x => x.Genres)
                .FirstOrDefaultAsync();

            if (anime == null)
            {
                throw new ArgumentNullException($"The anime with id:{id} does not exist.");
            }

            StringBuilder sb=new StringBuilder();

            foreach (var genre in anime.Genres)
            {
                sb.Append($"{genre.Name}, ");
            }

            string genres=sb.ToString().TrimEnd();
            var foundAnime = new AnimeDetailsViewModel()
            {
                Id = anime.Id,
                Name = anime.Name,
                Image = anime.Image,
                Aired = anime.Aired,
                Episodes = anime.Episodes,
                Status = anime.Status,
                Description = anime.Description,
                Score = anime.Score,
                Studios = anime.Studios,
                Producers = anime.Producers,
                Genres = genres,
                Duration = anime.Duration,
                Rating = anime.Rating,
                ReviewsCount = anime.Reviews.Count,
            };

            return foundAnime;
        }

        /// <summary>
        /// Returns true tested, returns false if anime donesn't exist tested.
        /// </summary>
        /// <param name="animeName"></param>
        /// <returns></returns>
        public async Task<bool> AnimeAlreadyExistInCollection(string animeName)
        {
            var anime = await dbContext.Animes.FirstOrDefaultAsync(x => x.Name == animeName);

            if (anime == null)
            {
                return false;
            }

            return true;
        }
    }
}
