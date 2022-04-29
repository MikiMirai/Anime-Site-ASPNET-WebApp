using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;

namespace ASPNET_WebApp.Core.Contracts
{
    public interface IAnimeService
    {
        Task<bool> CreateAnime(AnimeCreateViewModel model);

        Task<IEnumerable<AnimeListViewModel>> GetAnimes();

        Task<IEnumerable<AnimeListViewModel>> GetAnimesTrending();

        Task<Anime> GetAnimeForEdit(string id);

        Task<Anime> GetAnimeById(string id);

        Task<AnimeDetailsViewModel> GetAnimeDetailsById(string id);

        Task<bool> UpdateAnime(AnimeEditViewModel model);

        Task<bool> AnimeAlreadyExistInCollection(string animeName);
    }
}
