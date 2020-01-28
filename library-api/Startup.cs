using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library_api.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace library_api
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

            Action<Global> globalOptions = (opt =>
            {
                opt.Environment = "development";
                opt.SetBaseUrl();
            });
            services.Configure(globalOptions);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<Global>>().Value);

            services.AddCors(options =>
            options.AddPolicy("MyPolicy",
                builder => {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                }
            )
        );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
