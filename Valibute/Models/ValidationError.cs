using Valibute.Attributes;

namespace Valibute.Models
{
    public class ValidationError
    {
        public string PropName { get; set; }
        public string Error { get; set; }

        public ValidationError()
        {

        }
        public ValidationError(string propName, string error)
        {
            PropName = propName;
            Error = error;
        }

        public ValidationError(VValidProperty vValidProperty)
        {
            PropName = vValidProperty.Name;
            Error = vValidProperty.ErrorMessage;
        }
    }
}
