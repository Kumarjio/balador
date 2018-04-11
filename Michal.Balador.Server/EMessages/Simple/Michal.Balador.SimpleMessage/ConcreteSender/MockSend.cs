using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.SimpleMessage.ConcreteContact;

namespace Michal.Balador.SimpleMessage
{
    public class MockSend : AppMessanger
    {
        SocketClientTest _test;

        //public MockSend(SocketClientTest test, IBaladorContext context, FactrorySendMessages factrorySendMessages) : base(context, factrorySendMessages)
        //{

        //    _test = test;
        //}

        public MockSend(IBaladorContext context, AppMessangerFactrory provider) : base(context, provider)
        {


        }

        public async Task<ResponseAppMessanger> SetSocketClient(SignUpSender sender, bool canExcute = true)
        {
            ResponseAppMessanger response = new ResponseAppMessanger();
            response.Result = this;
            if (_test != null)
            {
                return response;
            }
          
            var authenticationManager = GetAuthenticationManager();
            var token = await authenticationManager.GetToken( sender);
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

        protected override ContactManager GetInstanceContactManger(ContactInfo contact)
        {
            return new ContactTcpSimple(this, contact);
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
