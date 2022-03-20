using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeApp.Models
{
    public class AnalyzedInstruction
    {
        public string Name { get; set; }
        public List<InstructionStep> Steps { get; set; }
    }
}
