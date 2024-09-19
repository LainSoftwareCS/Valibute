namespace Valibute.Attributes
{
    /// <summary>
    /// Create a number validation for a property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VNumber : VValidProperty
    {
        public int Min { get; set; } = int.MinValue;
        public int Max { get; set; } = int.MaxValue;
        /// <summary>
        /// Create a number validation for a property
        /// </summary>
        /// <param name="name">Property name</param>
        public VNumber(string name)
        {
            Name  = name;
            ErrorMessage = ErrorMessage ?? $"The property {Name} needs to be a number";
        }
    }
}
