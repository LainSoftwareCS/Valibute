using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Valibute.Attributes
{
    /// <summary>
    /// Create a required validation for a property
    /// </summary>
    /// <param name="name">Property name</param>
    [AttributeUsage(AttributeTargets.Property)]
    public class VRequired : VValidProperty
    {
        /// <summary>
        /// Create a required validation for a property
        /// </summary>
        /// <param name="name">Property name</param>
        public VRequired(string name)
        {
            Name = name;
            ErrorMessage = $"The property {Name} is required";
        }
    }
}
