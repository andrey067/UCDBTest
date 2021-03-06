using Manager.Infra.Context;
using Manager.Services.Services;
using Manager.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Manager.Infra.Interface;
using Manager.Infra.Repositories;
using AutoMapper;
using Manager.Domain.Entities;
using Manager.Services.DTO;
using Manager.API.ViewModels;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Manager.API
{
    public class Startup
    {
        readonly string CorsValidations = "CorsValidations";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();


            #region Automapper
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Produto, ProdutoDTO>().ReverseMap();
                cfg.CreateMap<CreateViewModel, ProdutoDTO>().ReverseMap();
                cfg.CreateMap<UpdateViewModel, ProdutoDTO>().ReverseMap();
            });
            services.AddSingleton(autoMapperConfig.CreateMapper());
            #endregion

            #region InjecaoDepedencia
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            #endregion





            services.AddDbContext<ProdutoContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Manager.Infra")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Manager.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            );


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Manager.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CorsValidations);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
