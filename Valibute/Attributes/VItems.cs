using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Valibute.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class VItems : VValidProperty
    {
        public int MinLength { get; set; } = int.MinValue;
        public int MaxLength { get; set; } = int.MaxValue;
        public bool ValidateEachItem {  get; set; } = false;
        public VItems(string name)
        {
            Name = name;
            ErrorMessage = $"The property {Name} is not valid";
        }
    }
}
