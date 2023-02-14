using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdoTask
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            ADOOperations adoOperations = new ADOOperations();
            adoOperations.ShowOptions();
        }
    }
}
