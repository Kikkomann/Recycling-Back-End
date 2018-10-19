using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Recycling.API.Models.Mappings;
using Recycling.Data;
using Recycling.Data.Abstract;
using Recycling.Data.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace Recycling.API
{
    public class Startup
    {
        private string _sqlConnectionString = string.Empty;
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<RecyclingContext>(options => {
                        options.UseSqlServer(_sqlConnectionString,
                            b => b.MigrationsAssembly("Recycling.API"));
            });

            // Add framework services.
            //            services.AddDbContext<RecyclingContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:RecyclingApplicationDB"]));
            //            services.AddSingleton(typeof(IDataRepository<Models.User, long>), typeof(DAL.UserDAL));

            // Repositories
            services.AddScoped<IHubRepository, HubRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWasteManagementRepository, WasteManagementRepository>();
            services.AddScoped<IFractionRepository, FractionRepository>();
            services.AddScoped<IUserHubRepository, UserHubRepository>();

            AutoMapperConfiguration.Configure();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(opts =>
            {
                // Force Camel Case to JSON
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                // If swagger should be accessed by (http://localhost:<port>/) instead of (http://localhost:<port>/swagger)
//                c.RoutePrefix = string.Empty;
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");

            // Uncomment the following line to add a route for porting Web API 2 controllers.
            //routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });

            RecyclingDbInitializer.Initialize(app.ApplicationServices);
        }
    }
}
