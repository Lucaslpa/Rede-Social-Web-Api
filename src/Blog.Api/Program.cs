using Blog.api.Configuration;
using Blog.api.Utils;
using Blog.Data.database;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder( args );
var Services = builder.Services;
var configuration = builder.Configuration;

await Services.Configure( configuration );


var app = builder.Build();

// Configure the HTTP request pipeline.



app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<SwaggerAuthenticationMiddleware>();
    app.UseSwagger();
    app.UseSwaggerUI( options =>
    {
        foreach (var description in app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
        {
            options.SwaggerEndpoint( $"/swagger/{description.GroupName}/swagger.json" , description.GroupName.ToUpperInvariant() );
        }

        options.DocExpansion( DocExpansion.List );
    } );
}

app.MapControllers();

app.Run();
