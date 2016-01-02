using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models
{
    [Table("Recipes")]
    public class Recipe
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        
        [Column("UserId")]
        public string UserId { get; set; }
        
        [Column("Name")]
        public string Name { get; set; }
        
        [Column("RecipeText")]
        public string RecipeText { get; set; } 
    }
}