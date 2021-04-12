using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCore
{
    public class PolicyConstantModel
    {
        public const string ViewPolicy = "ViewPolicy";
        public const string AddPolicy = "AddPolicy";
        public const string EditPolicy = "EditPolicy";
        public const string DeletePolicy = "DeletePolicy";
    }
    public class Startup
    {
        //<aspNetCore processPath="c:\Program Files\dotnet\dotnet.exe" arguments="C:\Users\mohamed.mahlies\source\repos\WebApplicationCore\WebApplicationCore\bin\Debug\netcoreapp2.2\WebApplicationCore.dll" stdoutLogEnabled="true" stdoutLogFile=".\logs\stdout" hostingModel="InProcess">
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder               
                .SetIsOriginAllowed(origin => true)                        
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                .AllowCredentials()
                );
            });
            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.Configure<MvcOptions>(options =>
            //{
            // //   options.Filters.Add(new CorsAuthorizationFilterFactory("AllowOrigin"));
            //});


            //services.AddAuthentication(IISDefaults.AuthenticationScheme);
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("DefaultPolicy", policy => policy.Requirements.Add(new DefaultPolicyRequirement(0)));
            //    options.AddPolicy(PolicyConstantModel.AddPolicy, policy => policy.Requirements.Add(new DefaultPolicyRequirement((int)HttpAction.Add)));
            //    options.AddPolicy(PolicyConstantModel.EditPolicy, policy => policy.Requirements.Add(new DefaultPolicyRequirement((int)HttpAction.Edit)));
            //    options.AddPolicy(PolicyConstantModel.ViewPolicy, policy => policy.Requirements.Add(new DefaultPolicyRequirement((int)HttpAction.View)));
            //    options.AddPolicy(PolicyConstantModel.DeletePolicy, policy => policy.Requirements.Add(new DefaultPolicyRequirement((int)HttpAction.Delete)));
            //});
            //services.AddScoped<IAuthorizationHandler, DefaultPolicyHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePreflight();
            app.UseCors("AllowOrigin");
            app.UseAuthentication();
            app.UseMvc();
            // app.UseCors("AllowOrigin");
        }
    }
}
