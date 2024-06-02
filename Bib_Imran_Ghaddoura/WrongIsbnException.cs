using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_Imran_Ghaddoura
{
    internal class WrongIsbnException : ApplicationException
    {
        public WrongIsbnException(string message) : base(message)
        {
        }
    }
}
