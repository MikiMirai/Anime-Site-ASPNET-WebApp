using ASPNET_WebApp.Infrastructure.Data;
using ASPNET_WebApp.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Anime_Site_WebApp_Tests.Mock
{
    public static class ServiceCollectionExtensionMock
    {
        public static IServiceCollection RemoveService<TService>(this IServiceCollection svc)
        {
            var descriptor = svc.SingleOrDefault(
              d => d.ServiceType ==
                  typeof(TService)) ?? throw new ArgumentException(nameof(TService));

            svc.Remove(descriptor);

            return svc;
        }

        public static IServiceCollection AddTestHttpContextAccessor(this IServiceCollection svc)
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var claimsPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] { new Claim(ClaimTypes.NameIdentifier, WebAppMock.UserId.ToString()) }
                    )
                );

            mockHttpContextAccessor.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            svc.RemoveService<IHttpContextAccessor>();

            svc.AddSingleton<IHttpContextAccessor>(mockHttpContextAccessor.Object);

            return svc;
        }

        public static async Task<IServiceProvider> AddTestData(this IServiceProvider serviceProvider)
        {

            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();

                //https://github.com/dotnet/efcore/issues/6282#issuecomment-509684621
                db.ChangeTracker.Clear();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                await userManager.CreateAsync(new ApplicationUser
                {
                    Id = WebAppMock.UserId,
                    UserName = "TestUser",
                    Email = "mail@mail.com",
                    NormalizedUserName = "TESTUSER".Normalize().ToUpperInvariant()
                });

                db.Animes.Add(new Anime
                {
                    Id = WebAppMock.AnimeId,
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
                });

                db.ForumPost.Add(new ForumPost
                {
                    Id = WebAppMock.ForumPostId,
                    UserId = WebAppMock.UserId,
                    Description = "Pineaples and tomatoes don't go good together.",
                    Date = DateTime.Now.ToString(),
                });

                db.Comments.Add(new Comment
                {
                    Id = WebAppMock.CommentId,
                    UserId = WebAppMock.UserId,
                    ReviewId = WebAppMock.ReviewId,
                    Date = DateTime.Now.ToString(),
                    Description = "Cherries and strawberries don't go good together.",
                });

                db.Reviews.Add(new Review
                {
                    Id = WebAppMock.ReviewId,
                    UserId = WebAppMock.UserId,
                    AnimeId = WebAppMock.AnimeId,
                    Description = "Apples and peaches don't go good together.",
                    Date = DateTime.Now.ToString(),
                    Score = 6.0,
                });

                db.SaveChanges();

                return serviceProvider;
            }
        }
    }
}
