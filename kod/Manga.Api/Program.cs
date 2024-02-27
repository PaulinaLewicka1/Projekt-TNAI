using Manga.Model;
using Manga.Repository.MangaSerie;
using Manga.Repository.MangaVolumes;
using Manga.Repository.Publishers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Pod³¹czenie do bazy danych
//Migracja: Konsola Menad¿era Pakietów -> Api jako startowt -> Wybraæ projekt domyœlny Model -> Add-Migration [nazwa]
var connectionString = builder.Configuration.GetConnectionString("AppDbContext");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

//Wproawdzenie zmian: w konsoli PM -> Update-Database -Verbose

// Add services to the container.
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IMangaSeriesRepository, MangaSeriesRepository>();
builder.Services.AddScoped<IMangaVolumeRepository, MangaVolumeRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Automapper - wersja 13.0.1
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
