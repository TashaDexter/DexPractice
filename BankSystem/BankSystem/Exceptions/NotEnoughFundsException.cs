using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Exceptions
{
    public class NotEnoughFundsException : Exception
    {
        public NotEnoughFundsException()
        {

        }

        public NotEnoughFundsException(string message) : base(message)
        {

        }
    }
}
