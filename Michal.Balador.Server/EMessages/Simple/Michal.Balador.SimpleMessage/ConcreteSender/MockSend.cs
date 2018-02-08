using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.SimpleMessage
{
    public class MockSend : SenderMessages
    {
        SocketClientTest _test;
        
        public MockSend(SocketClientTest test,IBaladorContext context):base(context)
        {
            
            _test = test;
        }

        public override void Dispose()
        {
            _test.Disconnect();
        }

        public override AuthenticationManager GetAuthenticationManager()
        {
            return new HttpSimpleAuthentication(Context);
        }

        public override bool IsAuthorized()
        {
            throw new NotImplementedException();
        }

        public override async Task<ResponseSend> Send(SendRequest request)
        {
            ResponseSend res = new ResponseSend();
            res.Result = new List<ResponseMessage>();
            res.Id = request.Id;
            res.Log = request.Log;
            foreach (var itemMessage in request.Messages)
            {
               await Task.Run(() =>
              {
                  try
                  {
                     var message= _test.SendMessage(itemMessage.Id, itemMessage.Message);
                      res.Result.Add(new ResponseMessage { Id = itemMessage.Id, IsError = false, Message = message +" id="+ itemMessage.Id+ " ,Message=" + itemMessage.Message });
                  }
                  catch (Exception e)
                  {
                      res.Result.Add(new ResponseMessage { Id = itemMessage.Id, IsError = true, ErrMessage = e.ToString(), Message = itemMessage.Message });
                  }
              }
            );
        }
            //   return await Task.FromResult(res);
            return res;
        }


    }
}
