using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Server.Controllers
{//http://kennytordeur.blogspot.co.il/2012/08/mef-in-aspnet-mvc-4-and-webapi.html
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EMessageController : ApiController
    {
        [Import]
        private IMyTest _myTest;
     
        [ImportMany(typeof(IEMessage))]
        IEnumerable<Lazy<IFactrorySendMessages, IDictionary<string, object>>> __messagesRules;
        // GET api/<controller>
        public async Task<IEnumerable<string>> Get()
        {
            List<ResponseSender> senders = new List<ResponseSender>();
            List<string> ss = new List<string>();
            Lazy<IFactrorySendMessages> _utah = __messagesRules.Where(s => (string)s.Metadata["MessageType"] == "MockSender").FirstOrDefault();
            
            
            if (_utah != null && _utah.Value != null)
            {
               var sender=await _utah.Value.GetSender(new RegisterSender { Id="someuser",Pws="12345"});
             //   sender.Result.Send()

            //    var sender2 = await _utah.Value.ConnectAndSend(new Contracts.DataModel.Sender());
            //    ss.Add(sender2.Message);
            }

            //foreach (var item in _messages)
            //{
            //    var sender = await item.Value.ConnectAndSend(new Contracts.DataModel.Sender());
            //    senders.Add(sender);
            //    ss.Add(sender.Message);
            //}
            //ss.Add(_myTest.GetMessage());
            return ss.ToArray();
        }
    }

}


