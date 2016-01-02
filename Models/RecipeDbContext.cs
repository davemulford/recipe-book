using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;

namespace RecipeBook.Models
{
    public class RecipeDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            var builder = new ConfigurationBuilder().AddUserSecrets();
            var configuration = builder.Build();
            
            optionsBuilder.UseNpgsql(configuration["Data:RecipeBookConnection:ConnectionString"]);
        }
    }
}