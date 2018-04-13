using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.SimpleMessage.ConcreteContact;

namespace Michal.Balador.SimpleMessage
{
    public class MockHttpSend : AppMessanger
    {
        HttpClientTest _test;
        
        public MockHttpSend(IBaladorContext context, AppMessangerFactrory provider) :base(context, provider)
        {
            _test = new HttpClientTest();
        }
        public HttpClientTest HttpClientTest { get { return _test; } }

        public override void Dispose()
        {
            _test.Disconnect();
        }

        protected override ContactManager GetInstanceContactManger(ContactInfo contact)
        {
            return new ContactHttpSimple(this, contact);
        }

        public override async Task<ResponseSend> Send(SendRequest request)
        {
            var configAccount=await Context.GetConfiguration<ConfigHttp>(this.Provider.ServiceName, request.Id);

              var contact= this.Context.GetContact<ContactHttpSend>(this.Provider.ServiceName, "ddd");
            this.Context.GetLogger().Log(System.Diagnostics.TraceLevel.Info, configAccount.RefreshToken, null);

            ResponseSend res = new ResponseSend();
            res.Result = new List<ResponseMessage>();
            res.Id = request.Id;
            res.Log = request.Log;
            foreach (var itemMessage in request.Messages)
            {
                  try
                  {
                     var message=   await _test.SendMessage(request.ToString());
                      res.Result.Add(new ResponseMessage { ClientId = itemMessage.ClientId, IsError = false, Message = message +" id="+ itemMessage.ClientId+ " ,Message=" + itemMessage.Message });
                  }
                  catch (Exception e)
                  {
                      res.Result.Add(new ResponseMessage { ClientId = itemMessage.ClientId, IsError = true, ErrMessage = e.ToString(), Message = itemMessage.Message });
                  }
        }
            return await Task.FromResult(res);
        }


    }
}
