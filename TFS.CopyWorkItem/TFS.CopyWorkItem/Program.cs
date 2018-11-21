using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFS.CopyWorkItem
{
    class Program
    {
        static void Main(string[] args)
        {
            var action = new EpicList();
            action.Execute();
            Console.ReadKey();
        }
    }
}
