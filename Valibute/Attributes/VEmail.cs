namespace Valibute.Attributes
{        
    /// <summary>
    /// Create an email validation for a property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VEmail : VValidProperty
    {
        /// <summary>
        /// Create an email validation for a property
        /// </summary>
        /// <param name="name">Property name</param>
        public VEmail(string name)
        {
            Name = name;
            ErrorMessage = $"The property {Name} need to be an email";
        }
    }
}
