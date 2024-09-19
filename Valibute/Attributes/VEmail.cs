using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Valibute.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class VEmail : VValidProperty
    {
        public VEmail(string name)
        {
            Name = name;
            ErrorMessage = $"The property {Name} need to be an email";
        }
    }
}
