namespace BankSystem.Models
{
    public class Employee : IPerson
    {
        public int PassportID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Employee))
                return false;
            var client = (Employee)obj;
            return client.PassportID == PassportID;
        }

        public string Position { get; set; }

        public static bool operator ==(Employee employee1, Employee employee2)
        {
            return employee1.Equals(employee2);
        }

        public static bool operator !=(Employee employee1, Employee employee2)
        {
            return !employee1.Equals(employee2);
        }

        public override int GetHashCode()
        {
            return PassportID + FirstName.Length + LastName.Length+Age+ Position.Length;
        }
    }
}
