namespace Valibute.Models.Responses
{
    public class ErrorValidationResponse : ValidationResponse
    {
        public ErrorValidationResponse(List<ValidationError> errors)
        {
            IsValid = false;
            Errors = errors;
        }

        public ErrorValidationResponse(string error)
        {
            Errors = new List<ValidationError>()
            {
                new ValidationError("General", error)
            };
        }
    }
}
