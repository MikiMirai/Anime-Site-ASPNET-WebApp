using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_WebApp.Core.Contracts
{
	public interface IGenreService
	{
		Task<bool> CreateGenre(string name);

		Task<bool> UpdateGenre(Genre model);

		Task<bool> HasGenre(Anime anime, string name);

		Task<Genre> GetGenreForEdit(string id);

		Task<IEnumerable<Genre>> GetGenres();

		Task<bool> RemoveAnimeGenresAsync(Anime anime, List<Genre> genres);

		Task<bool> AddAnimeGenresAsync(Anime anime, List<ManageAnimeGenreViewModel> model);
	}
}
