using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Valibute.Attributes
{
    /// <summary>
    /// Create an array validation for a property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VItems : VValidProperty
    {
        public int MinLength { get; set; } = int.MinValue;
        public int MaxLength { get; set; } = int.MaxValue;
        public bool ValidateEachItem {  get; set; } = false;
        /// <summary>
        /// Create an array validation for a property
        /// </summary>
        /// <param name="name">Property name</param>
        public VItems(string name)
        {
            Name = name;
            ErrorMessage = $"The property {Name} is not valid";
        }
    }
}
