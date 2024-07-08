using BidCalculationService.Infrastruture.Models;
using OpenIddict.Validation.AspNetCore;

namespace BidCalculationService.Api.Configurations
{
    public static class Extensions
    {
        public static IServiceCollection AddOpenIDAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()!;
            services.AddOpenIddict()
                    .AddValidation(options =>
                    {
                        options.SetIssuer(jwtSettings.Issuer);
                        options.AddAudiences(jwtSettings.Audience);

                        options.UseIntrospection()
                               .SetClientId(jwtSettings.ClientId)
                               .SetClientSecret(jwtSettings.ClientSecret);

                        options.UseSystemNetHttp();

                        options.UseAspNetCore();
                    });

            services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
            services.AddAuthorization();

            return services;
        }
    }
}
