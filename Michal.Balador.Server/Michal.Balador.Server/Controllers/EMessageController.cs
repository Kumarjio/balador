using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;
using Michal.Balador.Server.Dal;

namespace Michal.Balador.Server.Controllers
{//http://kennytordeur.blogspot.co.il/2012/08/mef-in-aspnet-mvc-4-and-webapi.html
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EMessageController : ApiController
    {
      
     
        [ImportMany(typeof(IEMessage))]
        IEnumerable<Lazy<IFactrorySendMessages, IDictionary<string, object>>> _senderRules;
        // GET api/<controller>
        public async Task<HttpResponseMessage> Get()
        {
            ConcurrentBag<ResponseSend> resultError = new ConcurrentBag<ResponseSend>();
            List<ResponseSender> senders = new List<ResponseSender>();
            List<string> ss = new List<string>();
            Lazy<IFactrorySendMessages> _utah = _senderRules.Where(s => (string)s.Metadata["MessageType"] == "MockSender").FirstOrDefault();
            var result = "";

            if (_utah != null && _utah.Value != null)
            {
                // var sender=await _utah.Value.GetSender(new RegisterSender { Id="someuser",Pws="12345"});
                MockRepository mockData = new MockRepository();
            
                mockData.mocks.Senders.AsParallel().ForAll(async rs =>
                {
                    var sender = await _utah.Value.GetSender(rs);
                    if (!sender.IsError)
                    {
                       var requestToSend= await mockData.FindMessagesById(rs.Id);
                       requestToSend.Log = System.Threading.Thread.CurrentThread.ManagedThreadId;
                       var responseToSend= await sender.Result.Send(requestToSend);
                        resultError.Add(responseToSend);
                    }
                    else
                    {
                        //CALL Error!!!
                    }
                });

            }

            //foreach (var item in _messages)
            //{
            //    var sender = await item.Value.ConnectAndSend(new Contracts.DataModel.Sender());
            //    senders.Add(sender);
            //    ss.Add(sender.Message);
            //}
            //ss.Add(_myTest.GetMessage());
            //return ss.ToArray();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ConcurrentBag<ResponseSend>>(resultError,
                         new JsonMediaTypeFormatter(),
                          new MediaTypeWithQualityHeaderValue("application/json"))
            };
            return response;



        }

     
        }

}


