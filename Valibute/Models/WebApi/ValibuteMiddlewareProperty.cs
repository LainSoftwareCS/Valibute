namespace Valibute.Models.WebApi
{
    public class ValibuteMiddlewareProperty
    {
        public bool IsEnabled { get; set; }
        public Type ValibuteType { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public ValibuteMiddlewareProperty(bool isEnabled, Type valibuteType)
        {
            IsEnabled = isEnabled;
            ValibuteType = valibuteType;
        }

        public ValibuteMiddlewareProperty(bool isEnabled, Type valibuteType, string errorMessage) : this(isEnabled, valibuteType)
        {
            ErrorMessage = errorMessage;
        }
    }
}
