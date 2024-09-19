namespace Valibute.Attributes
{
    public abstract class VValidProperty : Attribute
    {
        public string Name { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public VValidProperty()
        {

        }
    }
}
