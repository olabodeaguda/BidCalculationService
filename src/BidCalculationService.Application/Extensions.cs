using AutoMapper;
using BidCalculationService.Application.Handlers.Behaviours;
using BidCalculationService.Application.Helpers;
using BidCalculationService.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace BidCalculationService.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMapperConfiguration();
            services.AddMediatR(_ =>
            {
                _.RegisterServicesFromAssemblies(new[] { typeof(Extensions).Assembly });
                _.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });
            services.AddValidatorsFromAssemblyContaining<CreateAccountRequestValidator>();

            return services;
        }

        public static void AddMapperConfiguration(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoProfile>();
            });

            services.AddSingleton(configuration.CreateMapper());
        }

        public static string ToSha256(this string input)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
