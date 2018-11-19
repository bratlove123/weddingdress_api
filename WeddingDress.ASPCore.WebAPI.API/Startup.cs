using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models;
using Microsoft.AspNetCore.Identity;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using WeddingDress.ASPCore.WebAPI.Services.Interfaces;
using WeddingDress.ASPCore.WebAPI.Services.Services;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Repositories;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Common;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using AutoMapper;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Profiles;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces.ModelFactories;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ModelFactories;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Helpers;

namespace WeddingDress.ASPCore.WebAPI.API
{
    public class Startup
    {
        private readonly SymmetricSecurityKey _signingKey;
        private readonly SigningCredentials _signingCredentials;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            string secretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";
            _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            _signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add DI
            AddApplicationRepositories(services);
            AddApplicationServices(services);
            AddApplicationModelFactories(services);

            //Config database
            services.AddDbContext<WeddingDressDataContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Scoped);

            //Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = _signingCredentials;
            });

            var frontEndAppSettingOptions = Configuration.GetSection(nameof(FrontEndOptions));
            services.Configure<FrontEndOptions>(options =>
            {
                options.Url = frontEndAppSettingOptions[nameof(FrontEndOptions.Url)];
            });

            var secretAppSettingOptions = Configuration.GetSection(nameof(SecretOptions));
            services.Configure<SecretOptions>(options =>
            {
                options.FacebookAppId= "304707076958155";
                options.FacebookAppSecret = "4db63e5052e99845ca973591245a8974";
                options.EmailSecretKey = "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";
            });
            services.AddScoped<RoleManager<IdentityRole>>();

            //Config email life time span
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(3); // .FromDays(1) ...
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Consts.JwtClaimIdentifiers.Rol, Consts.JwtClaims.ApiAccess));
            });

            var builder = services.AddIdentityCore<ApplicationUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.User.RequireUniqueEmail = true;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<WeddingDressDataContext>().AddDefaultTokenProviders();

            services.AddAutoMapper();
            RegisterMapperProfiles();
            services.AddMvc();
            services.AddCors();
        }

        //Private method
        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddTransient<ILeftNavService, LeftNavService>();
        }

        private static void AddApplicationRepositories(IServiceCollection services)
        {
            services.AddSingleton<IUtils, Utils>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ILeftNavRepository, LeftNavRepository>();
        }

        private static void AddApplicationModelFactories(IServiceCollection services)
        {
            services.AddTransient<ILeftNavModelFactory, LeftNavModelFactory>();
        }

        private static void RegisterMapperProfiles()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<RegistrationAppUserMappingProfile>();
                config.AddProfile<LeftNavMappingProfile>();
            });
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //adding customs roles : Question 1
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Manager", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 2
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Here you could create a super user who will maintain the web app
            var poweruser = new ApplicationUser
            {
                UserName = "Administrator",
                Email = "htphong.spkt@gmail.com",
                EmailConfirmed=true
            };

            string userPWD = "P@ssword123";
            var _user = await UserManager.FindByEmailAsync("htphong.spkt@gmail.com");

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role : Question 3
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use((context, next) =>
            {
                if (context.Request.Headers["x-forwarded-proto"] == "https")
                {
                    context.Request.Scheme = "https";
                }
                return next();
            });

            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
            app.UseAuthentication();
            app.UseMvc();

            CreateRoles(provider).Wait();
        }
    }
}
