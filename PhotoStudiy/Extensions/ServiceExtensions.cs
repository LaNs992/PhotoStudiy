using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using PhotoStudiy.API.AutoMappers;
using PhotoStudiy.Common.Entity;
using PhotoStudiy.Common.Entity.InterfaceDB;
using PhotoStudiy.Context;
using PhotoStudiy.Repositories;
using PhotoStudiy.Services;
using PhotoStudiy.Services.AutoMappers;

namespace PhotoStudiy.API.Extensions
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Регистрирует все сервисы, репозитории и все что нужно для контекста
        /// </summary>
        public static void RegistrationSRC(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IDbWriterContext, DbWriterContext>();
            services.RegistrationService();
            services.RegistrationRepository();
            services.RegistrationContext();
            services.AddAutoMapper(typeof(APIMappers), typeof(ServiceMapper));
        }

        /// <summary>
        /// Включает фильтры и ставит шрифт на перечесления
        /// </summary>
        /// <param name="services"></param>
        public static void RegistrationControllers(this IServiceCollection services)
        {
            services.AddControllers(x =>
            {
                x.Filters.Add<PhotoStudiyExceptionFilter>();
            })
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.Converters.Add(new StringEnumConverter
                    {
                        CamelCaseText = false
                    });
                });
        }

        /// <summary>
        /// Настройки свагера
        /// </summary>
        public static void RegistrationSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Client", new OpenApiInfo { Title = "Клиенты", Version = "v1" });
                c.SwaggerDoc("Photograph", new OpenApiInfo { Title = "Фотографы", Version = "v1" });
                c.SwaggerDoc("Dogovor", new OpenApiInfo { Title = "Договоры", Version = "v1" });
                c.SwaggerDoc("PhotoSet", new OpenApiInfo { Title = "Фотосеты", Version = "v1" });
                c.SwaggerDoc("Product", new OpenApiInfo { Title = "Продукты", Version = "v1" });
                c.SwaggerDoc("Recvisit", new OpenApiInfo { Title = "Реквизиты", Version = "v1" });
                c.SwaggerDoc("Uslugi", new OpenApiInfo { Title = "Услуги", Version = "v1" });


                var filePath = Path.Combine(AppContext.BaseDirectory, "PhotoStudiy.API.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        /// <summary>
        /// Настройки свагера
        /// </summary>
        public static void CustomizeSwaggerUI(this WebApplication web)
        {
            web.UseSwagger();
            web.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Photograph/swagger.json", "Фотографы");
                x.SwaggerEndpoint("Client/swagger.json", "Клиенты");
                x.SwaggerEndpoint("Dogovor/swagger.json", "Договоры");
                x.SwaggerEndpoint("PhotoSet/swagger.json", "Фотосеты");
                x.SwaggerEndpoint("Product/swagger.json", "Продукты");
                x.SwaggerEndpoint("Recvisit/swagger.json", "Реквизиты");
                x.SwaggerEndpoint("Uslugi/swagger.json", "Услуги");
            });
        }
    }
}
