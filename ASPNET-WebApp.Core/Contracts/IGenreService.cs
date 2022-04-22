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

		Task<Genre> GetGenreForEdit(string id);

		Task<IEnumerable<Genre>> GetGenres();
	}
}
