using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Context.Contracts;

namespace PhotoStudiy.Context
{
    /// <summary>
    /// Методы пасширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensionsContext
    {
        /// <summary>
        /// Регистрирует все что связано с контекстом
        /// </summary>
        /// <param name="service"></param>
        public static void RegistrationContext(this IServiceCollection service)
        {
            service.TryAddScoped<IPhotoStudiyContext>(provider => provider.GetRequiredService<PhotoStudiyContext>());
            service.TryAddScoped<IDbRead>(provider => provider.GetRequiredService<PhotoStudiyContext>());
            service.TryAddScoped<IDbWriter>(provider => provider.GetRequiredService<PhotoStudiyContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<PhotoStudiyContext>());
        }
    }
}
