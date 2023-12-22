using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PhotoStudiy.General;
using PhotoStudiy.Repositories.Contracts.ReadRepositoriesContracts;
using PhotoStudiy.Services.Anchors;
using PhotoStudiy.Services.Contracts.Exceptions;
using PhotoStudiy.Services.Contracts.Models;
using PhotoStudiy.Services.Validator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services
{
    public static class ServiceExtensionsService  /// <summary>
                                                  /// Регистрация всех сервисов и валидатора
    {                                          /// </summary>
        public static void RegistrationService(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IServiceAnhor>(ServiceLifetime.Scoped);
            service.AddTransient<IServiceValidatorService, ServicesValidatorService>();
        }
    }
}

