﻿namespace BankSystem.Models
{
    public class Client : IPerson
    {
        public string PassportID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Client))
                return false;
            var client = (Client)obj;
            return (client.PassportID == PassportID);
            
        }

        public static bool operator ==(Client client1, Client client2)
        {
            return client1.Equals(client2);
        }

        public static bool operator !=(Client client1, Client client2)
        {
           return !(client1.Equals(client2));
        }

        public override int GetHashCode()
        {
            return PassportID.GetHashCode();
        }

    }
}
