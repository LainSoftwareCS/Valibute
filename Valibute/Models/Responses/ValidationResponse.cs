namespace Valibute.Models.Responses
{
    public class ValidationResponse
    {
        public bool IsValid { get; set; } = true;
        public List<ValidationError>? Errors { get; set; }
    }
}
