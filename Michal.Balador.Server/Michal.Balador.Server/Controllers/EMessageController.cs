﻿using System;
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
        // GET api/<controller>
        public async Task<HttpResponseMessage> Get()
        {
            ConcurrentBag<ResponseSend> resultError = new ConcurrentBag<ResponseSend>();
            List<ResponseSender> senders = new List<ResponseSender>();
            Lazy<IFactrorySendMessages> _utah = _senderRules.Where(s => (string)s.Metadata["MessageType"] == "MockHttpSender").FirstOrDefault();
            MockRepository mockData = new MockRepository();

            var doStuffBlock = new ActionBlock<RegisterSender>(async rs =>
            {
                rs.Log = Thread.CurrentThread.ManagedThreadId;
                var sender = await _utah.Value.GetSender(rs);
                try
                {
                    if (!sender.IsError)
                    {
                        var requestToSend = await mockData.FindMessagesById(rs.Id);

                        if (requestToSend != null)
                        {
                            IWebHookManager manager = this.Configuration.DependencyResolver.GetManager();
                            List<NotificationDictionary> notifications = new List<NotificationDictionary>();
                            notifications.Add(new NotificationDictionary("preUpdate", new { P1 = "p1" }));
                            await manager.NotifyAsync(rs.Id, notifications, null);
                            if (manager is IExposeResult)
                            {
                                foreach (var itemNotification in ((IExposeResult)manager).NotificationsResult)
                                {
                                    Log.Info(itemNotification);
                                }
                            }
                            requestToSend.Log = rs.Log;
                            var responseToSendWait = await sender.Result.Send(requestToSend);
                            resultError.Add(responseToSendWait);
                            Log.Info(responseToSendWait.ToString());
                           

                            notifications = new List<NotificationDictionary>();
                            notifications.Add(new NotificationDictionary("postUpdate", new { P1 = "2eee" }));
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
            } );
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


/*
  public async Task<HttpResponseMessage> Get()
        {
            ConcurrentBag<ResponseSend> resultError = new ConcurrentBag<ResponseSend>();
            List<ResponseSender> senders = new List<ResponseSender>();
    
            //Lazy<IFactrorySendMessages> _utah = _senderRules.Where(s => (string)s.Metadata["MessageType"] == "MockSender").FirstOrDefault();
           Lazy<IFactrorySendMessages> _utah = _senderRules.Where(s => (string)s.Metadata["MessageType"] == "MockHttpSender").FirstOrDefault();

           
                // var sender=await _utah.Value.GetSender(new RegisterSender { Id="someuser",Pws="12345"});
                MockRepository mockData = new MockRepository();

                mockData.mocks.Senders.AsParallel().ForAll(async rs =>
                       {
                           rs.Log = System.Threading.Thread.CurrentThread.ManagedThreadId;
                           var sender = await _utah.Value.GetSender(rs);
                           try
                           {
                               if (!sender.IsError)
                               {
                                   var requestToSend = await mockData.FindMessagesById(rs.Id);
                               
                                   if (requestToSend != null)
                                   {
                                       requestToSend.Log = rs.Log;
                                      // var responseToSendWait =  sender.Result.Send(requestToSend);
                                      //responseToSendWait.Wait();
                                      //var responseToSend = responseToSendWait.Result;
                                      //var responseToSendWait = sender.Result.Send(requestToSend).Result;

                                       //resultError.Add(responseToSendWait);
                                       //Log.Info(responseToSendWait.ToString());
                                       //var responseToSendWait = await sender.Result.Send(requestToSend);
                                     var responseToSendWait =  sender.Result.Send(requestToSend).Result;
                                       resultError.Add(responseToSendWait);
                                      Log.Info(responseToSendWait.ToString());
                                   }
                               }
                               else
                               {
                                   var mes = rs.Log + " " + rs.Id + " " + sender.Message;
                                   resultError.Add(new ResponseSend { IsError=true,Message= mes });
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
                
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ResponseSend[]>(resultError.ToArray(),
                         new JsonMediaTypeFormatter(),
                          new MediaTypeWithQualityHeaderValue("application/json"))
            };
            return response;
        }
 */
