﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.SimpleMessage
{
    public class MockHttpSend : SenderMessages
    {
        HttpClientTest _test;
        
        public MockHttpSend(IBaladorContext context):base(context)
        {
            _test = new HttpClientTest();
        }
        public override void Dispose()
        {
            _test.Disconnect();
        }

        public override AuthenticationManager GetAuthenticationManager()
        {
            return new HttpSimpleAuthentication(Context);
        }

        public override async Task<ResponseSend> Send(SendRequest request)
        {
           var contact= this.Context.GetContact(this, "ddd");
            ResponseSend res = new ResponseSend();
            res.Result = new List<ResponseMessage>();
            res.Id = request.Id;
            res.Log = request.Log;
            foreach (var itemMessage in request.Messages)
            {
                  try
                  {
                     var message=   await _test.SendMessage(request.ToString());
                      res.Result.Add(new ResponseMessage { Id = itemMessage.Id, IsError = false, Message = message +" id="+ itemMessage.Id+ " ,Message=" + itemMessage.Message });
                  }
                  catch (Exception e)
                  {
                      res.Result.Add(new ResponseMessage { Id = itemMessage.Id, IsError = true, ErrMessage = e.ToString(), Message = itemMessage.Message });
                  }
        }
            return await Task.FromResult(res);
        }


    }
}
