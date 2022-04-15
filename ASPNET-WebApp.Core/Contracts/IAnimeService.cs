using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;

namespace ASPNET_WebApp.Core.Contracts
{
    public interface IAnimeService
    {
        Task<IEnumerable<AnimeListViewModel>> GetAnime();

        Task<AnimeEditViewModel> GetAnimeForEdit(string id);

        Task<bool> UpdateAnime(AnimeEditViewModel model);

        Task<Anime> GetAnimeById(string id);
    }
}
