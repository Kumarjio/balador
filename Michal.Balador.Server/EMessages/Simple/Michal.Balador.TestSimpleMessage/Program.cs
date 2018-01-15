using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.SimpleMessage;

namespace Michal.Balador.TestSimpleMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketClientTest test = new SocketClientTest("l", "1");
            test.Connect();
             string m = "ddd";
            test.SendData(Encoding.UTF8.GetBytes(m));
         
        }
    }
}
