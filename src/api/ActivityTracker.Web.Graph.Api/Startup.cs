using ActivityTracker.Data.Graph.Schema;
using ActivityTracker.Web.Graph.Api.Ioc;
using ActivityTracker.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityTracker.Web.Graph.Api
{
    public class Startup
    {
        private const string AppName = "ActivityTracker.Web.Api";
        private const string AppVersion = "v1";

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.InstallServices();
            services.InstallPersistence();
            services.InstallGraph(Environment);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
            
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = JsonExceptionMiddleware.Invoke
            });
            
            app.UseWebSockets();
            app.UseGraphQLWebSockets<ActivitiesSchema>();
            app.UseGraphQL<ActivitiesSchema>();
            app.UseGraphQLGraphiQL();
        }
    }
}
