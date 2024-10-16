using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valibute.Models.WebApi
{
    public class ValidationErrorApiResponse
    {
        public string Message { get; set; } = string.Empty;
        public List<ValidationErrorApiResponseError> Errors { get; set; } = new List<ValidationErrorApiResponseError>();

        public ValidationErrorApiResponse(string message, List<ValidationError> errors)
        {
            this.Message = message;
            this.Errors = errors.Select(error => new ValidationErrorApiResponseError(error.Error,error.PropName)).ToList();
        }
    }

    public class ValidationErrorApiResponseError
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public string PropertyName { get; set; } = string.Empty;
        public ValidationErrorApiResponseError()
        {
            
        }

        public ValidationErrorApiResponseError(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }
    }
}
