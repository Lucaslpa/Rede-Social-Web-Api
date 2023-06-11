using Blog.api.Configuration;
using Blog.Data.database;



var builder = WebApplication.CreateBuilder( args );
var Services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
Services.AddNewtonSoftConfiguration();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
Services.AddEndpointsApiExplorer();
Services.AddSwaggerGen();
Services.AddDependencyInjection();
Services.AddAuthorization();
Services.AddDbContext<MyDatabaseContext>();
Services.AddIdentityConfiguration();
Services.AddAutoMapperProfiles();
Services.ConfigureJwt( configuration );



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
