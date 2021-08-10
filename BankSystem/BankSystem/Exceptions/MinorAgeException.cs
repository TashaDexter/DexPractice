using System;

namespace BankSystem.Exceptions
{
    public class MinorAgeException : Exception
    {
        public MinorAgeException(string message) : base(message)
        {

        }

        public MinorAgeException(string message, int value) : base(message)
        {

        }
    }
}
