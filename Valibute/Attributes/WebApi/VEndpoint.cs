namespace Valibute.Attributes.WebApi
{
    [AttributeUsage(AttributeTargets.Method)]
    public class VEndpoint : Attribute
    {
        public Type ValibuteType { get; set; }
        public string ErrorMessage { get; set; }
        public VEndpoint(Type valibuteType)
        {
            ErrorMessage = string.Empty;
            ValibuteType = valibuteType;
        }
        public VEndpoint(string errorMessage, Type valibuteType)
        {
            ErrorMessage = errorMessage;
            ValibuteType = valibuteType;
        }
    }
}
