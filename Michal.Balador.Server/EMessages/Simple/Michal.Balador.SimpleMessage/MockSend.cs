using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.SimpleMessage
{
   public class SenderMessagesFactory
    {
        public async Task<Response<MockSend>> ConnectAndLogin(string user, string pws)
        {
            var tcs = new TaskCompletionSource<Response<MockSend>>();
            try
            {
                SocketClientTest  test = new SocketClientTest(user, pws);
                test.Connect();

                test.OnLoginSuccess += (a, b) =>
                {
                    Console.WriteLine("data ok={0}", a);
                    string m = "test me...";
                    test.SendData(Encoding.ASCII.GetBytes(m));
                    tcs.SetResult(new Response<MockSend> { IsError = false, Result=new MockSend(test) });
                };
                test.OnLoginFailed += (a) =>
                {

                    Console.WriteLine("data error={0}", a);
                    tcs.SetResult(new Response<MockSend> { IsError = true,Message=a });
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

    public class MockSend : SenderMessages
    {
        SocketClientTest _test;
        
        public MockSend(SocketClientTest test)
        {
            _test = test;
        }
        
        public override async Task<ResponseSend> Send(SendRequest request)
        {
            ResponseSend res = new ResponseSend();
            foreach (var itemMessage in request.Messages)
            {
               await Task.Run(() =>
              {
                  try
                  {
                      _test.SendMessage(itemMessage.Id, itemMessage.Message);
                      res.Result.Add(new ResponseMessage { Id = itemMessage.Id, IsError = false, Message = itemMessage.Message });
                  }
                  catch (Exception e)
                  {
                      res.Result.Add(new ResponseMessage { Id = itemMessage.Id, IsError = true, ErrMessage = e.ToString(), Message = itemMessage.Message });
                  }
              }
            );
        
        }

            return await Task.FromResult<ResponseSend>(res);
        }


    }
}
