using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models
{
    public class RecipesRoot
    {
        //Denna property används för RandomRecipeViewModel
        public List<Recipe> Recipes { get; set; }
        //Denna property används för SearchPageViewModel
        public IEnumerable<Recipe> Results { get; set; }
        public int Offset { get; set; }
        public int Number { get; set; }
        public int TotalResults { get; set; }
    }
}
