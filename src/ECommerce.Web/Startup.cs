using System;
using System.IO;
using System.Reflection;


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

using Newtonsoft.Json.Converters;

using Swashbuckle.AspNetCore.Swagger;

namespace Sample.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddJsonOptions(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); });
            services.AddApiVersioning(options => { options.ReportApiVersions = true; });

            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            services.AddMvcCore().AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(
                options =>
                {
                    options.IncludeXmlComments(XmlCommentsFilePath);

                    // resolve the IApiVersionDescriptionProvider service
                    // note: that we have to build a temporary service provider here because one has not been created yet
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                    // add a swagger document for each discovered API version
                    // note: you might choose to skip or document deprecated API versions differently
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                    }
                });

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICustomerQueryService, CustomerQueryService>();
            services.AddTransient<ICustomerReadOnlyRepository, CustomerReadOnlyRepository>();
            services.AddTransient<IAccountQueryService, AccountQueryService>();
            services.AddTransient<IAccountReadOnlyRepository, AccountReadOnlyRepository>();
            services.AddTransient<IConnectionFactory>(provider => new ConnectionFactory(Configuration.GetConnectionString("DefaultConnection")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseMvc(routes =>
            {
                // Health Check
                routes.MapGet("api/health",
                    context => context.Response.WriteAsync("OK"));
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                // build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
        }

        private Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info
            {
                Title = $"Sample API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "Sample api for sample stuff"
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }

        private string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";

                return Path.Combine(basePath, fileName);
            }
        }
    }
}
