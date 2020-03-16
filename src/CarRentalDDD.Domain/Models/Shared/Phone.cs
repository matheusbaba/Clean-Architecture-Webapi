using CarRentalDDD.Domain.SeedWork;

namespace CarRentalDDD.Domain.Models.Shared
{
    public class Phone
    {
        public string Value { get; private set; }

        public Phone(string value)
        {
            if(!int.TryParse(value, out int x) || value.Length < 6)
                throw new OInvalidArgumentException(nameof(Phone));

            this.Value = value;
        }
        public override string ToString()
        {
            return Value;
        }


        public static implicit operator Phone(string phone) => new Phone(phone);
    }
}
