using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_WebApp.Core.Services
{
    public class AnimeService : IAnimeService
    {
        public Task<IEnumerable<AnimeListViewModel>> GetAnime()
        {
            throw new NotImplementedException();
        }

        public Task<Anime> GetAnimeById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AnimeEditViewModel> GetAnimeForEdit(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAnime(AnimeEditViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
