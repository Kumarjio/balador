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
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Web.Http;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;
using Michal.Balador.Server.Dal;
using Michal.Balador.Server.Infrastructures;
using Michal.Balador.Server.Infrastructures.WebHookExstension;
using Microsoft.AspNet.WebHooks;

namespace Michal.Balador.Server.Controllers
{//http://kennytordeur.blogspot.co.il/2012/08/mef-in-aspnet-mvc-4-and-webapi.html
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EMessageController : ApiController
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(EMessageController));
        [ImportMany(typeof(IFactrorySendMessages))]
        IEnumerable<Lazy<IFactrorySendMessages, IDictionary<string, object>>> _senderRules;

        public async Task<HttpResponseMessage> Get()
        {
            ConcurrentBag<ResponseSend> resultError = new ConcurrentBag<ResponseSend>();
            List<ResponseSender> senders = new List<ResponseSender>();
            Lazy<IFactrorySendMessages> _utah = _senderRules.Where(s => (string)s.Metadata["MessageType"] == "MockHttpSender").FirstOrDefault();
            MockRepository mockData = new MockRepository();

            var doStuffBlock = new ActionBlock<RegisterSender>(async rs =>
            {
                rs.Log = Thread.CurrentThread.ManagedThreadId;
                SenderManager senderManager = new SenderManager(Log, this.Configuration);
                resultError= await senderManager.Send(_utah.Value, rs);

            });

            foreach (var item in mockData.mocks.Senders)
            {
                doStuffBlock.Post(item);
            }
            doStuffBlock.Complete();
            await doStuffBlock.Completion;

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ResponseSend[]>(resultError.ToArray(),
                         new JsonMediaTypeFormatter(),
                          new MediaTypeWithQualityHeaderValue("application/json"))
            };
            return response;
        }
    }
}
