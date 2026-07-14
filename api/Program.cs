using Microsoft.EntityFrameworkCore;
using RecipeBook.Api.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<RecipeBookDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<RecipeBookDbContext>();
    context.Database.Migrate();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

var api = app.MapGroup("/api");

api.MapGet("/recipes", async (RecipeBookDbContext context) =>
{
    var recipes = await context.Recipes
        .AsNoTracking()
        .OrderBy(recipe => recipe.Id)
        .ToListAsync();

    return Results.Ok(recipes);
})
.WithName("GetRecipes");

app.Run();
