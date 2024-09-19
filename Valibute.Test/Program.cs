

using System.Text.Json;
using Valibute.Test.Models;
using Valibute.Utils;

var product = new Product() { 
    Id = 10, 
    Name = "....", 
    Age = 17,
    Details = new List<ProductDetail>()
    {
        new ProductDetail()
        {
            Name = null
        }
    }
};

var response = ValibuteUtils.Validate(product);

Console.WriteLine(JsonSerializer.Serialize(response, options: new JsonSerializerOptions()
{
    WriteIndented = true,
}));