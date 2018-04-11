using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.SimpleMessage
{
    public class SenderMessagesFactory
    {
        IBaladorContext _context;
        public SenderMessagesFactory(IBaladorContext context)
        {
            _context = context;
        }
        public async Task<Response<SocketClientTest>> ConnectAndLogin(string user, string pws)
        {
            var tcs = new TaskCompletionSource<Response<SocketClientTest>>();
            try
            {
                SocketClientTest test = new SocketClientTest(user, pws);
                test.Connect();
               // mockSend.SetSocketClient()
                test.OnLoginSuccess += (a, b) =>
                {
                    Console.WriteLine("data ok={0}", a);
                    string m = "test me...";
                    test.SendData(Encoding.ASCII.GetBytes(m));
                    tcs.SetResult(new Response<SocketClientTest> { IsError = false, Result = test });
                };
                test.OnLoginFailed += (a) =>
                {

                    Console.WriteLine("data error={0}", a);
                    tcs.SetResult(new Response<SocketClientTest> { IsError = true, Message = a });
                };
                test.Login(true);
            }
            catch (Exception ee)
            {
                tcs.SetException(ee);
            }

            return await tcs.Task;
        }
    }
}
