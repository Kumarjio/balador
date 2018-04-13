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
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.Server.Dal;
using Michal.Balador.Server.Infrastructures.WebHookExstension;
using Microsoft.AspNet.WebHooks;

namespace Michal.Balador.Server.Controllers
{//http://kennytordeur.blogspot.co.il/2012/08/mef-in-aspnet-mvc-4-and-webapi.html
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EMessageOldController : ApiController
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(EMessageController));
        [ImportMany(typeof(IAppMessangerFactrory))]
        IEnumerable<Lazy<IAppMessangerFactrory, IDictionary<string, object>>> _senderRules;

        public async Task<HttpResponseMessage> Get()
        {
            ConcurrentBag<ResponseSend> resultError = new ConcurrentBag<ResponseSend>();
            List<ResponseSender> senders = new List<ResponseSender>();
            Lazy<IAppMessangerFactrory> _utah = _senderRules.Where(s => (string)s.Metadata["MessageType"] == "MockHttpSender").FirstOrDefault();
            MockRepository mockData = new MockRepository();

            var doStuffBlock = new ActionBlock<RegisterSender>(async rs =>
            {
                rs.Log = Thread.CurrentThread.ManagedThreadId;
                var sender = await _utah.Value.GetInstance(rs);
                try
                {
                    if (!sender.IsError)
                    {
                        var requestToSend = await mockData.FindMessagesById(rs.Id);

                        if (requestToSend != null)
                        {
                            IWebHookManager manager = this.Configuration.DependencyResolver.GetManager();
                            List<NotificationDictionary> notifications = new List<NotificationDictionary>();
                            notifications.Add(new NotificationDictionary(BaladorConst.PreUpdate, new { Request = requestToSend }));
                            await manager.NotifyAsync(rs.Id, notifications, null);
                            if (manager is IExposeResult)
                            {

                                IExposeResult iexposeResult = (IExposeResult)manager;
                                if (iexposeResult != null && iexposeResult.NotificationResult != null)
                                {
                                    foreach (var messageWebHookClient in iexposeResult.NotificationResult.Messages)
                                    {
                                        var messageToChange = requestToSend.Messages.Where(p => p.ClientId == messageWebHookClient.ClientId).FirstOrDefault();
                                        if (messageToChange != null)
                                          messageToChange.Message = messageWebHookClient.Message; 
                                    }
                                }
                            }
                            requestToSend.Log = rs.Log;
                            var responseToSendWait = await sender.Result.Send(requestToSend);
                            resultError.Add(responseToSendWait);
                            Log.Info(responseToSendWait.ToString());
                            notifications = new List<NotificationDictionary>();
                            notifications.Add(new NotificationDictionary(BaladorConst.PostUpdate, new { Response = requestToSend }));
                            await manager.NotifyAsync(rs.Id, notifications, null);
                        }
                    }
                    else
                    {
                        var mes = rs.Log + " " + rs.Id + " " + sender.Message;
                        resultError.Add(new ResponseSend { IsError = true, Message = mes });
                        Log.Error(mes);
                    }
                }
                catch (Exception ee)
                {
                    resultError.Add(new ResponseSend { IsError = true, Message = "unhandle" });
                    Log.Error(rs.Log + " " + rs.Id + " " + ee);
                }
                finally
                {
                    if (sender != null && sender.Result != null)
                        sender.Result.Dispose();
                }
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
