
namespace CarRentalDDD.Domain.Models.Shared
{
    public class Email
    {
        public string Value{ get; private set; }

        public Email(string value)
        {
            // *** email validation ***

            this.Value = value;
        }



        public override string ToString()
        {
            return Value;
        }

        public static implicit operator Email(string email) => new Email(email);
        
 
    }
}
