namespace BankSystem.Models
{
    public class Employee : IPerson
    {
        public string PassportID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Employee))
                return false;
            var employee = (Employee)obj;
            return employee.PassportID == PassportID;
        }

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
            return PassportID.GetHashCode();
        }
    }
}
