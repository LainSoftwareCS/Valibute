using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valibute.Attributes
{    /// <summary>
     /// Create an not empty validation for a property
     /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VNotEmpty : VValidProperty
    {
        /// <summary>
        /// Create an not empty validation for a property
        /// </summary>
        /// <param name="name">Property name</param>
        public VNotEmpty(string name)
        {
            Name = name;
            ErrorMessage = $"The property {Name} can't be null";
        }
    }
}
