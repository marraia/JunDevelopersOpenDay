using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using JunDevelopers.Infra.IoC;
using SimpleInjector.Lifestyles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using SimpleInjector.Integration.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using JunDevelopers.Api.Filtros;
using JunDevelopers.Application;
using JunDevelopers.Application.Interfaces;
using JunDevelopers.Infra.Repositorio;
using JunDevelopers.Dominio.Interfaces;
using JunDevelopers.Infra.Repositorio.Contexto;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace JunDevelopers.Api
{
    public class Startup
    {
        private readonly Container container = ContainerFactory.Container;
        private IConfigurationRoot _config;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            IntegrateSimpleInjector(services);
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "JunDevelopers 2017",
                    Description = "Demo para JunDevelopers OpenDay 2017",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Fernando Mendes", Email = "fmendes@viceri.com.br", Url = "" }
                });

                // Usar a documentação XML dos métodos.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "JunDevelopers.Api.xml");
                options.IncludeXmlComments(xmlPath);
            });
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(container));

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(env.ContentRootPath)
                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                          .AddEnvironmentVariables();

            _config = builder.Build();
           
            InitializeContainer(app);
            
            container.Register<ErrorFilter>();
            container.Verify();
                       

            app.Use((c, next) => container.GetInstance<ErrorFilter>().Invoke(c, next));
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JunDevelopers API V1");
            });

            app.UseMvcWithDefaultRoute();
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            container.RegisterMvcControllers(app);
            container.RegisterMvcViewComponents(app);

            container.Register<IPalestranteAppService, PalestranteAppService>(Lifestyle.Scoped);
            container.Register<IPalestranteRepositorio, PalestranteRepositorio>(Lifestyle.Scoped);

            container.Register<JunContexto>(() =>
            {
                var options = new DbContextOptionsBuilder<JunContexto>();
                options.UseSqlServer(Configuration.GetConnectionString("JunDevelopersDefault"));
                return new JunContexto(options.Options);
            }, Lifestyle.Scoped);

            container.CrossWire<ILoggerFactory>(app);
        }
    }
}
