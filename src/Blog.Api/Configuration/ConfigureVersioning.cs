using Blog.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.api.Configuration
{
    public static class ConfigureVersions
    {
        public static IServiceCollection ConfigureVersioning( this IServiceCollection services )
        {

            services.AddMvc()
                 .SetCompatibilityVersion( CompatibilityVersion.Version_2_2 );

            services.AddApiVersioning( p =>
            {
                p.DefaultApiVersion = new ApiVersion( 2 , 0 );
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            } );

            services.AddVersionedApiExplorer( p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            } );

            return services;
        }


    }
}
