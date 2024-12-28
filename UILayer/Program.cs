using Microsoft.EntityFrameworkCore;

using MyCleanArchitectureApp.Applications.Services;
using MyCleanArchitectureApp.Applications.ServicesContracts;
using MyCleanArchitectureApp.Domain.RepositoryContracts;
using MyCleanArchitectureApp.Infrastructure.Repositories;
using MyCleanArchitectureApp.Infrastructure;
using Microsoft.Extensions.Configuration;
using MyCleanArchitectureApp.Applications;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(
        builder.Configuration.GetConnectionString("MyWebAppConnectionString"),
        sqlOptions => sqlOptions.MigrationsAssembly("MyCleanArchitectureApp.Infrastructure") // Migrations assembly set
    )
);




builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IMoviesService, MovieService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();


builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movie}/{action=Index}/{id?}");

app.Run();
