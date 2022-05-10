using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TodoApiDTO.Extensions;
using System.IO;
using Business_Layer.Services;
using Business_Layer.Interfaces;
using DataAccessLayer.Repositories;
using DataAccessLayer.Interfaces;
using DataAccessLayer.EF;

namespace TodoApi
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
            string connection = Configuration.GetConnectionString("TodoContextConnection");
            services.AddDbContext<TodoContext>(options => options.UseSqlServer(connection));
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<ITodoServices, TodoServices>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Todo Api",
                    Version = "v1",
                    Description = "Description Todo Api.",
                    Contact = new OpenApiContact
                    {
                        Name = "Askhat Aitkulov",
                        Email = "askhat_91_kz@mail.ru",
                        Url = new Uri("https://example.com/"),
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddFile($"{Directory.GetCurrentDirectory()}\\Logs\\");

            app.UseHttpsRedirection();

            app.UseExceptionHandler("/api/Error");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo Api v1");

                c.RoutePrefix = string.Empty;
            });

        }
    }
}
