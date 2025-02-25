using Common.Infrastructure.Base.Cache;
using Common.Infrastructure.Base.Context;
using Common.Infrastructure.Base.Contracts;
using Common.Infrastructure.Core.Contracts;
using Common.Infrastructure.Core.Repository;
using Common.Infrastructure.Core.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.Presentation.Api
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

            services.AddAutoMapper(AppDomain.CurrentDomain.Load("Application.Core"));

            services.AddDbContext<TenantDbContext>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:4455";
            });

            #region Dipendancy Injection

            services.AddScoped<DbContext, TenantDbContext>();
            services.TryAdd(ServiceDescriptor.Scoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)));

            services.AddTransient<IBaseUnitOfWork, BaseUnitOfWork>();
            services.AddMediatR(typeof(BaseUnitOfWork).Assembly);

            services.AddMediatR(AppDomain.CurrentDomain.Load("Application.Core"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<ICacheManager, CacheManager>();

            #endregion

            #region Swagger Configs

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                o.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(
                            c =>
                            {
                                c.DocInclusionPredicate((docName, apiDesc) =>
                                {
                                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo))
                                    {
                                        return false;
                                    }

                                    System.Collections.Generic.IEnumerable<ApiVersion> versions = methodInfo.DeclaringType
                                        .GetCustomAttributes(true)
                                        .OfType<ApiVersionAttribute>()
                                        .SelectMany(a => a.Versions);

                                    return versions.Any(v => $"v{v.ToString()}" == docName);
                                });
                                c.SwaggerDoc(
                                    "v1.0",
                                    new OpenApiInfo
                                    {
                                        Title = "Camms Common Api",
                                        Version = "v1.0"
                                    });
                                c.SwaggerDoc(
                                    "v2.0",
                                    new OpenApiInfo
                                    {
                                        Title = "Camms Common Api",
                                        Version = "v2.0"
                                    });
                            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(
                  c =>
                  {
                      c.DefaultModelsExpandDepth(0);
                      c.SwaggerEndpoint($"/swagger/v1.0/swagger.json", "Common.Api V1.0");
                      c.SwaggerEndpoint($"/swagger/v2.0/swagger.json", "Common.Api V2.0");
                  }
                  );
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
