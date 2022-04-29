using Anime_Site_WebApp_Tests.Mock;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;
using System;

namespace Anime_Site_WebApp_Tests.TestConstants
{
	public class TestDataConstants
    {
        public static readonly string AnimeId = Guid.NewGuid().ToString();
        public static readonly string UserId = Guid.NewGuid().ToString();
        public static readonly string ForumPostId = Guid.NewGuid().ToString();
        public static readonly string GenreId = Guid.NewGuid().ToString();

        public static AnimeCreateViewModel AnimeCreate => new AnimeCreateViewModel()
        {
            Name = "The Genius Prince's Guide to Raising a Nation Out of Debt",
            Image = "https://cdn.myanimelist.net/images/anime/1263/119511.jpg",
            Aired = "Jan 11, 2022 to Mar 29, 2022",
            Episodes = 12,
            Status = "Finished Airing",
            Description = "The king of Natra has fallen ill, leaving the only hope for his kingdom to his son, Prince Wein Salema Arbalest. Known to be capable and wise, he is the perfect candidate to become the prince regent. However, if the prince has anything to say about the matter, he would rather sell off the Kingdom of Natra to the highest bidder! Since he wields the authority of the throne, no one can stop Wein from auctioning off the country and using the profits to retire in comfort.All he needs to do is raise the value of the small kingdom to maximize his gains.But whether Wein's grand plan will succeed remains to be seen, as his wit often surpasses even his own expectations—much to the benefit of the oblivious citizens of Natra.",
            Studios = "Yokohama Animation Lab",
            Producers = "AT-X, Studio Mausu, NBCUniversal Entertainment Japan, Tokyo MX, RAY, DMM pictures, BS NTV, SB Creative",
            Duration = "24 min. per ep.",
            Rating = "PG-13 - Teens 13 or older",
        };

        public static AnimeEditViewModel AnimeUpdated => new AnimeEditViewModel()
        {
            Name = "The Genius Prince's Guide (Changed)",
            Image = "https://cdn.myanimelist.net/images/anime/1263/119511.jpg",
            Aired = "Jan 11, 2022 to Mar 29, 2022",
            Episodes = 12,
            Status = "Finished Airing",
            Description = "The king of Natra has fallen ill, leaving the only hope for his kingdom to his son, Prince Wein Salema Arbalest. Known to be capable and wise, he is the perfect candidate to become the prince regent. However, if the prince has anything to say about the matter, he would rather sell off the Kingdom of Natra to the highest bidder! Since he wields the authority of the throne, no one can stop Wein from auctioning off the country and using the profits to retire in comfort.All he needs to do is raise the value of the small kingdom to maximize his gains.But whether Wein's grand plan will succeed remains to be seen, as his wit often surpasses even his own expectations—much to the benefit of the oblivious citizens of Natra.",
            Studios = "Yokohama Animation Lab",
            Producers = "AT-X, Studio Mausu, NBCUniversal Entertainment Japan, Tokyo MX, RAY, DMM pictures, BS NTV, SB Creative",
            Duration = "24 min. per ep.",
            Rating = "PG-13 - Teens 13 or older",
        };

        public static ForumPostCreateViewModel ForumPostCreate => new ForumPostCreateViewModel()
        {
            Id = ForumPostId,
            Description = "Some random description #15617",
        };

        public static Review Review => new Review()
        {
            UserId = WebAppMock.UserId,
            Date = DateTime.Now.ToString(),
            Description = "The king of Natra has fallen ill.",
            Score = 6.0,
        };

        public static GenreCreateViewModel GenreCreate => new GenreCreateViewModel()
        {
            Id = GenreId,
            Name = "Fantasy"
        };
    }
}
