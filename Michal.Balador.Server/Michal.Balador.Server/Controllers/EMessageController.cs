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
using Michal.Balador.Contracts.Behaviors;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;
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

       
        [Import(typeof(ITaskSendsScheduler))]
        ITaskSendsScheduler _taskSendsScheduler;

        public async Task<HttpResponseMessage> Get()
        {
            var resultError = await _taskSendsScheduler.Run();

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
