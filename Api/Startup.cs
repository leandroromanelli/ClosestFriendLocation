using Domain.Repositories;
using Domain.Services;
using Infra.Context;
using Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Environment.CurrentDirectory, "App_Data"));

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

                var caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;

                var nomeAplicacao = PlatformServices.Default.Application.ApplicationName;

                var caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });

            services.AddScoped<IFriendRepository, FriendRepository>();

            services.AddScoped<LocationService>();

            services.AddDbContext<ClosestFriendLocationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ClosestFriendLocationContext>().Database.Migrate();
            }

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
