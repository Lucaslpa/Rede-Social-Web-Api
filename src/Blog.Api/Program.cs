using Blog.api.Configuration;
using Blog.Data.database;



var builder = WebApplication.CreateBuilder( args );
var Services = builder.Services;
var configuration = builder.Configuration;

Services.Configure( configuration );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
