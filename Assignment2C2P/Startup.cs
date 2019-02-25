using Assignment2C2P.Models;
using Assignment2C2P.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;

namespace Assignment2C2P
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
            services.AddMvc(setup => setup.AllowEmptyInputInBodyModelBinding = true).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Register Swagger (NSwag)
            services.AddSwaggerDocument(options =>
            {
                options.Title = "Customer Inquiry API";
                options.SchemaType = SchemaType.OpenApi3;
            });

            //Register DbContext
            services.AddDbContext<Assignment2C2PContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Assignment2C2PDB")));

            //Register service
            services.AddScoped<ICustomerService, CustomerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseMvc();
        }
    }
}
