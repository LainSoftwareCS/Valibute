using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Valibute.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class VNumber : VValidProperty
    {
        public int Min { get; set; } = int.MinValue;
        public int Max { get; set; } = int.MaxValue;
        public VNumber(string name)
        {
            Name  = name;
            ErrorMessage = ErrorMessage ?? $"The property {Name} needs to be a number";
        }
    }
}
