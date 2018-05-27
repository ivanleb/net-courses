using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Implementations
{
    [ComplexType]
    public class IndividualInfo
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
    }
}