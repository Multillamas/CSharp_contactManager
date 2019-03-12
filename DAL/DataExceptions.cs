using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataExceptions: ApplicationException
    {
        public DataExceptions(string message, Exception original): base(message, original)
        {
            
        }
        public DataExceptions(string message) : base(message)
        {

        }
    }
}
