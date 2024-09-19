using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Valibute.Attributes
{
    public class VValidProperty : Attribute
    {
        public string Name { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public VValidProperty()
        {

        }
    }
}
