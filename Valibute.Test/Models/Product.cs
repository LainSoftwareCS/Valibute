using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valibute.Attributes;

namespace Valibute.Test.Models
{
    public class ProductDetail
    {
        [VRequired("name")]
        public string? Name { get; set; }
    }

    public class Product
    {
        [VRequired("id")]        //[VNumber(Min = 1)]
        public int? Id { get; set; }
        [VRequired("name")]
        public string? Name { get; set; }
        [VNumber("age", Min = 18, ErrorMessage = "The person is underage")]
        public int Age { get; set; }

        [VItems("details", ValidateEachItem = true)]
        public List<ProductDetail> Details { get; set; } = new List<ProductDetail>();
    }
}
