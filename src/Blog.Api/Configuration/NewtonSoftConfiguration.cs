using Newtonsoft.Json;

namespace Blog.api.Configuration
{
    public static class NewtonSoftConfigurationn
    {
        public static IServiceCollection AddNewtonSoftConfiguration( this IServiceCollection service )
        {
            service.AddControllers().AddNewtonsoftJson( options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            } );
            return service;
        }

    }
}
