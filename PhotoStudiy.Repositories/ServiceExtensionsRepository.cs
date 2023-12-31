﻿using Microsoft.Extensions.DependencyInjection;
using PhotoStudiy.Repositories.Anchors;
using PhotoStudiy.General;

namespace PhotoStudiy.Repositories
{
    public static class ServiceExtensionsRepository
    {
        public static void RegistrationRepository(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}
