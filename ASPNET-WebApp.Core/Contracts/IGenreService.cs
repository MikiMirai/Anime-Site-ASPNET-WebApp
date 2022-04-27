using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;

namespace ASPNET_WebApp.Core.Contracts
{
    public interface IGenreService
	{
		Task<bool> CreateGenre(GenreCreateViewModel model);

		Task<bool> UpdateGenre(GenreEditViewModel model);

		Task<bool> HasGenre(Anime anime, string name);

		Task<GenreEditViewModel> GetGenreForEdit(string id);

		Task<IEnumerable<Genre>> GetGenres();

		Task<bool> RemoveAnimeGenresAsync(Anime anime, List<Genre> genres);

		Task<bool> GenreAlreadyExistInCollection(string genreName);

		Task<bool> AddAnimeGenresAsync(Anime anime, List<ManageAnimeGenreViewModel> model);
	}
}
