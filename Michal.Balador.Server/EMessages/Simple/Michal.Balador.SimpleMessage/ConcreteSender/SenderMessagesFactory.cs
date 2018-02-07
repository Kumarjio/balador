using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.SimpleMessage
{
    public class SenderMessagesFactory
    {
        IBaladorContext _context;
        public SenderMessagesFactory(IBaladorContext context)
        {
            _context = context;
        }
        public async Task<Response<MockSend>> ConnectAndLogin(string user, string pws)
        {
            var tcs = new TaskCompletionSource<Response<MockSend>>();
            try
            {
                SocketClientTest test = new SocketClientTest(user, pws);
                test.Connect();

                test.OnLoginSuccess += (a, b) =>
                {
                    Console.WriteLine("data ok={0}", a);
                    string m = "test me...";
                    test.SendData(Encoding.ASCII.GetBytes(m));
                    tcs.SetResult(new Response<MockSend> { IsError = false, Result = new MockSend(test,this._context) });
                };
                test.OnLoginFailed += (a) =>
                {

                    Console.WriteLine("data error={0}", a);
                    tcs.SetResult(new Response<MockSend> { IsError = true, Message = a });
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
