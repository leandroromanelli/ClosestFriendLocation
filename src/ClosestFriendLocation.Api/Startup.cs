using AutoMapper;
using ClosestFriendLocation.Api.Models;
using ClosestFriendLocation.Domain.Entities;
using ClosestFriendLocation.Domain.Interfaces.Services;
using ClosestFriendLocation.Domain.Repositories;
using ClosestFriendLocation.Domain.Services;
using ClosestFriendLocation.Infra.Context;
using ClosestFriendLocation.Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System;
//using System.IO;

namespace ClosestFriendLocation.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1",
                    new OpenApiInfo()
                    {
                        Title = "Romanelli's Closest Friend Location",
                        Version = "v1",
                        Description = "Api collection to locate people",
                        Contact = new OpenApiContact
                        {
                            Name = "Leandro Romanelli",
                            Url = new Uri("https://github.com/leandroromanelli")
                        }
                    });

                //var caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;

                //var nomeAplicacao = PlatformServices.Default.Application.ApplicationName;

                //var caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                //c.IncludeXmlComments(caminhoXmlDoc);
                c.IncludeXmlComments("ClosestFriendLocation.Api.xml");
            });

            services.AddScoped<IFriendRepository, FriendRepository>();
            services.AddScoped<IFriendService, FriendService>();
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddressModel , Address>();
                cfg.CreateMap<FriendModel  , Friend>();
                cfg.CreateMap<LocationModel, Location>();

                cfg.CreateMap<Address, AddressModel >();
                cfg.CreateMap<Friend, FriendModel  >();
                cfg.CreateMap<Location, LocationModel>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<ClosestFriendLocationContext>(options => options.UseInMemoryDatabase("ClosestFriendLocation"));

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    scope.ServiceProvider.GetRequiredService<ClosestFriendLocationContext>().Database.Migrate();
            //}

            app.UseSwagger();
            
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Romanelli's Closest Friend Location - v1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
