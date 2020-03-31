using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northwind.DataAccess;
using Northwind.Services.DataAccess;
using Northwind.Services.EntityFrameworkCore;
using Northwind.Services.Products;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace NorthwindApiApp
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
            services.AddControllers();
            services.AddScoped((service) =>
            {
                var sqlConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True");
                sqlConnection.Open();
                return sqlConnection;
            });

            services.AddTransient<NorthwindDataAccessFactory, SqlServerDataAccessFactory>();
            services.AddDbContext<NorthwindContext>(op => op.UseInMemoryDatabase("Northwind"));
            //services.AddScoped<IProductCategoriesManagementService, ProductManagementService>();
            services.AddScoped<IProductCategoriesManagementService, ProductManagementService>();
            services.AddScoped<IProductCategoryManagementService, ProductCategoriesManagementDataAccessService>();
            services.AddScoped<IProductCategoryPicturesService, ProductCategoryPicturesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
