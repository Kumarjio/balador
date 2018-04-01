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
    public class MockSend : SenderMessages
    {
        SocketClientTest _test;

        public MockSend(SocketClientTest test, IBaladorContext context) : base(context)
        {

            _test = test;
        }

        public MockSend(IBaladorContext context) : base(context)
        {


        }

        public async Task<ResponseSenderMessages> SetSocketClient(SignUpSender sender, bool canExcute = true)
        {
            ResponseSenderMessages response = new ResponseSenderMessages();
            response.Result = this;
            if (_test != null)
            {
                return response;
            }
          
            var authenticationManager = GetAuthenticationManager();
            var token = await authenticationManager.GetToken(this, sender);
            if (token == null)
            {

                response.IsAutorize = false;
                response.IsError = false;
            }
            else
            {
                if (canExcute)
                {
                    SenderMessagesFactory sendFactory = new SenderMessagesFactory(this.Context);
                    var respndFactory = await sendFactory.ConnectAndLogin(sender.Id, token.Token);
                    if (respndFactory.IsError)
                    {
                        response.IsAutorize = true;
                        response.IsError = true;
                        response.Message = respndFactory.Message;

                    }
                    else
                    {
                        _test = respndFactory.Result;

                    }
                }
                else
                {
                    response.IsAutorize = true;
                    response.IsError = false;
                }
                    
            }
            return response;
        }

        public override void Dispose()
        {
            _test.Disconnect();
        }

        public override AuthenticationManager GetAuthenticationManager()
        {
            return new HttpLiteAuthentication(Context, this);
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
                       var message = _test.SendMessage(itemMessage.Id, itemMessage.Message);
                       res.Result.Add(new ResponseMessage { Id = itemMessage.Id, IsError = false, Message = message + " id=" + itemMessage.Id + " ,Message=" + itemMessage.Message });
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
