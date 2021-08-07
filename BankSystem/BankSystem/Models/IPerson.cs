using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public interface IPerson
    {
        int PassportID { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        int Age { get; set; }
    }
}
