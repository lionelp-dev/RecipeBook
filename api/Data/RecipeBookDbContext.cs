using Microsoft.EntityFrameworkCore;
using RecipeBook.Api.Models;

namespace RecipeBook.Api.Data
{
    public class RecipeBookDbContext : DbContext
    {
        public RecipeBookDbContext(DbContextOptions<RecipeBookDbContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}




