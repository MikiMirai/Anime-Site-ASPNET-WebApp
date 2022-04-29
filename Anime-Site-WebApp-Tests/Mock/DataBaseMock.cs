﻿using ASPNET_WebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Anime_Site_WebApp_Tests.Mock
{
    public static class DataBaseMock
    {
        public static ApplicationDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new ApplicationDbContext(dbContextOptions);
            }
        }
    }
}
