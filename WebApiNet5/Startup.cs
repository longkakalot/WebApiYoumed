using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNet5.Repository;
using WebApiNet5.Services;

namespace WebApiNet5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public class ConnectionStrings
        {
            public string SqlServer { get; set; }

            public string SqlFpt { get; set; }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.AddControllers();

            //Register dapper in scope    
            services.AddScoped<IDapperService, DapperService>();
            services.AddTransient<IThongTinHanhChinhRepo, ThongTinHanhChinhRepo>();


            //Authorize all Controllers and Actions
            //services.AddMvcCore(options =>
            //    {
            //        options.Filters.Add(new AuthorizeFilter());
            //    })
            //    .AddAuthorization();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://192.168.102.72:4433";//IdentityServer URL
                    options.RequireHttpsMetadata = true;       //False for local addresses, true ofcourse for live scenarios
                    options.ApiName = "ApiOne";
                    options.ApiSecret = "supersecret";
                });

            //import JWT Bearer
            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer("Bearer", config => {
            //        config.Authority = "https://192.168.102.72:4433";
            //        config.Audience = "ApiOne";
            //        config.RequireHttpsMetadata = true;
            //    });


            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer("Bearer", options => {
            //        options.Authority = "https://192.168.102.72:4433";
            //        options.Audience = "ApiOne";
            //        //options.MetadataAddress = "/.well-known/openid-configuration";
            //        options.RequireHttpsMetadata = true;
            //        options.IncludeErrorDetails = true;
            //        //options.TokenValidationParameters = new TokenValidationParameters()
            //        //{
            //        //    NameClaimType = OpenIdConnectConstants.Claims.Subject,
            //        //    RoleClaimType = OpenIdConnectConstants.Claims.Role,
            //        //};
            //    });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            }            

            app.UseRouting();
            app.UseStaticFiles();
            loggerFactory.AddFile("Logs/mylog-{Date}.txt");
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
