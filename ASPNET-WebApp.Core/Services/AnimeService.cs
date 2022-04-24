using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;
using ASPNET_WebApp.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

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
                .ToListAsync();

            return animeView;
        }

        public async Task<Anime> GetAnimeById(string id)
        {
            Anime anime = await repo.GetByIdAsync<Anime>(id);
            return anime;
        }

        public async Task<AnimeEditViewModel> GetAnimeForEdit(string id)
        {
            var anime = await repo.GetByIdAsync<Anime>(id);

            if (anime == null)
            {
                throw new ArgumentNullException(nameof(anime));
            }

            var foundAnime = new AnimeEditViewModel()
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
                Genres = anime.Genres.ToList(),
                Duration = anime.Duration,
                Rating = anime.Rating
            };

            return foundAnime;
        }

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

            return result;
        }

        public async Task<bool> CreateAnime(AnimeCreateViewModel model)
        {
            if (await AnimeAlreadyExistInCollection(model.Name))
            {
                return false;
            }

            var anime = new Anime
            {
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
