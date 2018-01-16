using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.SimpleMessage
{
   

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
