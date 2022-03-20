using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models
{
    public class ExtendedIngredient
    {
        public int Id { get; set; }
        public string Aisle { get; set; }
        public string Image { get; set; }
        public string Consistency { get; set; }
        public string Name { get; set; }
        public string NameClean { get; set; }
        public string Original { get; set; }
        public string OriginalString { get; set; }
        public string OriginalName { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public List<string> Meta { get; set; }
        public List<string> MetaInformation { get; set; }
    }
}
