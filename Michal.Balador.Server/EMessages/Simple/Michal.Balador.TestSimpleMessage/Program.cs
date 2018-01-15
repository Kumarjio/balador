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
             string m = "OK";
            test.SendData(Encoding.ASCII.GetBytes(m));
            //test.SendData(new byte[0]);
            //System.Threading.Thread.Sleep(100);
   
            test.pollMessage();
            m = "FAILED";
            test.SendData(Encoding.ASCII.GetBytes(m));
            test.pollMessage();
            test.pollMessage();
            test.pollMessage();
        }
    }
}
