using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class MyClassDisposable : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Disposed");
        }
    }
}
