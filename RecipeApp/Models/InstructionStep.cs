using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models
{
    public class InstructionStep
    {
        public int Number { get; set; }
        public string Step { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Equipment> Equipment { get; set; }
    }
}
