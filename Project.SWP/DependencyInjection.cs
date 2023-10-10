using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services;
using System;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Project.SWP
{
    public static class DependencyInjection
    {
        public static IServiceCollection BuildServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region AppSettings
            services.AddHttpContextAccessor();
            services.AddSingleton(configuration);
            var connectionString = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
            services.AddSingleton(connectionString);
            services.AddDbContext<AppDBContext>(option => option.UseSqlServer(connectionString.SQLServerDB));
            //services.AddScoped<IClaimsServices, ClaimsServices>();
            //var connectionString = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
            //services.AddSingleton(connectionString);
            //services.AddDbContext<AppDBContext>(option => option.UseSqlServer(configuration.SQLServerDB));
            //services.AddDbContext<AppDBContext>(options =>
            //options.UseSqlServer(configuration.GetConnectionString("SQLServerDB")));
            //var jwtSection = configuration.GetSection("JWTSection").Get<JWTSection>();
            //services.AddSingleton(jwtSection);
            #endregion
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers().AddJsonOptions(opt =>
            opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            #region SwaggerConfig
            services.AddSwaggerGen();
                    //c =>
                    //{
                    //    c.SwaggerDoc("v1", new OpenApiInfo
                    //    {
                    //        Title = "Underwear Ecommerce",
                    //        Version = "v1",
                    //        Description = "This is Our API",
                    //        Contact = new OpenApiContact
                    //        {
                    //            Url = new Uri("https://google.com")
                    //        }
                    //    });
                    //    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    //    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
                    //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    //    {
                    //        Type = SecuritySchemeType.Http,
                    //        In = ParameterLocation.Header,
                    //        BearerFormat = "JWT",
                    //        Scheme = "Bearer",
                    //        Description = "Please input your token"
                    //    });
                    //    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    //    {
                    //        {
                    //            new OpenApiSecurityScheme
                    //            {
                    //                Reference=new OpenApiReference
                    //                {
                    //                    Type=ReferenceType.SecurityScheme,
                    //                    Id="Bearer"
                    //                }
                    //            },
                    //            new string[]{}
                    //        }
                    //    });

                    //});
            #endregion
            return services;
        }
    }
}
