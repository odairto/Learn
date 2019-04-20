using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using MyOdataApi.Model;
using Newtonsoft.Json.Serialization;

namespace MyOdataApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();
            services.AddSingleton(sp => new ODataUriResolver { EnableCaseInsensitive = true });

            services.AddMvc().AddJsonOptions(opt =>
            {
                if (opt.SerializerSettings.ContractResolver != null)
                {
                    var resolver = opt.SerializerSettings.ContractResolver as DefaultContractResolver;
                    resolver.NamingStrategy = null;
                }
            });
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(ConfigureODataRoutes);
        }

        private static void ConfigureODataRoutes(IRouteBuilder routes)
        {
            var model = GetEdmModel();
            routes.MapODataServiceRoute("ODataRoute", "odata", model);
            routes.Filter(QueryOptionSetting.Allowed);
            routes.OrderBy();
            routes.Count();
            routes.Select();
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Car>(nameof(Car));
            var cars = builder.EntitySet<Car>("Games").EntityType;
            cars.ComplexProperty(y => y.Manufacturer);
            return builder.GetEdmModel();
        }
    }
}
