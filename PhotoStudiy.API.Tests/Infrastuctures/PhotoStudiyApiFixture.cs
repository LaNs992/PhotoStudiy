using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Context;
using PhotoStudiy.Context.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhotoStudiy.API.Tests.Infrastuctures
{
    public class PhotoStudiyApiFixture : IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory factory;
        private PhotoStudiyContext? ticketSellingContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TicketSellingApiFixture"/>
        /// </summary>
        public PhotoStudiyApiFixture()
        {
            factory = new CustomWebApplicationFactory();
        }

        Task IAsyncLifetime.InitializeAsync() => PhotoStudiyContext.Database.MigrateAsync();

        async Task IAsyncLifetime.DisposeAsync()
        {
            await PhotoStudiyContext.Database.EnsureDeletedAsync();
            await PhotoStudiyContext.Database.CloseConnectionAsync();
            await PhotoStudiyContext.DisposeAsync();
            await factory.DisposeAsync();
        }

        public CustomWebApplicationFactory Factory => factory;

        public IPhotoStudiyContext Context => PhotoStudiyContext;

        public IUnitOfWork UnitOfWork => PhotoStudiyContext;

        internal PhotoStudiyContext PhotoStudiyContext
        {
            get
            {
                if (ticketSellingContext != null)
                {
                    return ticketSellingContext;
                }

                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                ticketSellingContext = scope.ServiceProvider.GetRequiredService<PhotoStudiyContext>();
                return ticketSellingContext;
            }
        }
    }
}
