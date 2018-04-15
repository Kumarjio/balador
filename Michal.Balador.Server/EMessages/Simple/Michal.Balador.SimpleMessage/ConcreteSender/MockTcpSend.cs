using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.SimpleMessage.ConcreteContact;

namespace Michal.Balador.SimpleMessage
{
    public class MockTcpSend : AppMessanger
    {
        SocketClientTest _test;
        public SocketClientTest SocketClientTest { get { return _test; } }

        //public MockSend(SocketClientTest test, IBaladorContext context, FactrorySendMessages factrorySendMessages) : base(context, factrorySendMessages)
        //{

        //    _test = test;
        //}

        public MockTcpSend(IBaladorContext context, AppMessangerFactrory provider) : base(context, provider)
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
            var token = await authenticationManager.GetToken(sender);
            if (token == null)
            {

                response.IsAutorize = false;
                response.IsError = false;
            }
            else
            {
                response.IsAutorize = true;
                SenderMessagesFactory sendFactory = new SenderMessagesFactory(this.Context);
                var respndFactory = await sendFactory.ConnectAndLogin(sender.UserName, token.Token);
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
                       var message = _test.SendMessage(itemMessage.ClientId, itemMessage.Message);
                       res.Result.Add(new ResponseMessage { ClientId = itemMessage.ClientId, IsError = false, Message = message + " id=" + itemMessage.ClientId + " ,Message=" + itemMessage.Message });
                   }
                   catch (Exception e)
                   {
                       res.Result.Add(new ResponseMessage { ClientId = itemMessage.ClientId, IsError = true, ErrMessage = e.ToString(), Message = itemMessage.Message });
                   }
               }
             );
            }
            //   return await Task.FromResult(res);
            return res;
        }


    }
}
