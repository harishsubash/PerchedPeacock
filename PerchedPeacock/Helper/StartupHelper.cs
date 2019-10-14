using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PerchedPeacock.Api;
using PerchedPeacock.Core;
using PerchedPeacock.Domain.Interfaces.Repositories;
using PerchedPeacock.Infra.Persistance.Repositories;
using PerchedPeacock.Infra.Transaction;
using Swashbuckle.AspNetCore.Swagger;

namespace PerchedPeacock.Helper
{
    public static class StartupHelper
    {
        public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<IdentityUser, IdentityRole>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://securetoken.google.com/perchedpeacock";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://securetoken.google.com/perchedpeacock",
                    ValidateAudience = true,
                    ValidAudience = "perchedpeacock",
                    ValidateLifetime = true
                };
            });
        }

        public static void RegisterInjection(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, EfCoreUnitOfWork>();
            services.AddTransient<IParkingLotRepository, ParkingLotRepository>();
            services.AddScoped<ParkingLotApplicationService>();
        }

        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Perched Peacock API", Version = "v1" });
                //options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                //{
                //    Flow = "implicit",
                //    AuthorizationUrl = "https://accounts.google.com/o/oauth2/v2/auth",
                //    Scopes = new Dictionary<string, string>
                //    {
                //        { "profile", "https://www.googleapis.com/auth/userinfo.profile" },
                //        { "email", "https://www.googleapis.com/auth/userinfo.email" }
                //    },
                //    //Type = "oauth2",
                //    //Description = "Google OAuth",
                //});
                //options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                //{
                //    Name = "Authorization",
                //    In = "header",
                //    Type = "apiKey",
                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                //});

                //options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                //{
                //        { "oauth2", new[] { "readAccess", "writeAccess" } }
                //});
            });
        }

        public static void RegisterUi(this IServiceCollection services)
        {
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        public static void RegisterUser(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                var userInfo = context.HttpContext.User;
                return new { userInfo.Claims };
            });
        }

        public static void UseSwaggerDocumentation(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Perched Peacock API V1");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                //c.RoutePrefix = string.Empty;
                //c.OAuthClientId(googleAuthNSection["ClientId"]);
                //c.OAuthAppName("Swagger UI");
                //c.OAuthRealm("/");
            });
        }

        public static void ConfigureClientApp(this IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        public static void ConfigureRoutes(this IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
