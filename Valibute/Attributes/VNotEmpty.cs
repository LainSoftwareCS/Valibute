using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valibute.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class VNotEmpty : VValidProperty
    {
        public VNotEmpty(string name)
        {
            Name = name;
            ErrorMessage = $"The property {Name} can't be null";
        }
    }
}
