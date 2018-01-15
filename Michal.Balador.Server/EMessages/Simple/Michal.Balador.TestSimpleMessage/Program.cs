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

            test.OnLoginSuccess+= (a, b) =>
             {
                 Console.WriteLine("data ok={0}",a);
                 string m = "test me...";
                 test.SendData(Encoding.ASCII.GetBytes(m));

             };
            test.OnLoginFailed += (a) =>
             {
                 Console.WriteLine("data error={0}", a);

             };
            test.Login(true);
            Console.ReadKey();

            // string m = "OK";
            //test.SendData(Encoding.ASCII.GetBytes(m));
            ////test.SendData(new byte[0]);
            ////System.Threading.Thread.Sleep(100);
   
            //test.pollMessage();
            //m = "FAILED";
            //test.SendData(Encoding.ASCII.GetBytes(m));
            //test.pollMessage();
          
        }
    }
}
