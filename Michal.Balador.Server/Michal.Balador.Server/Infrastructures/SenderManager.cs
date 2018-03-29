using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;
using Michal.Balador.Server.Dal;
using Michal.Balador.Server.Infrastructures.WebHookExstension;
using Microsoft.AspNet.WebHooks;

namespace Michal.Balador.Server.Infrastructures
{
    public class SenderManager
    {
        log4net.ILog _log;
        HttpConfiguration _httpConfiguration;
        public SenderManager(log4net.ILog log, HttpConfiguration httpConfiguration)
        {
            _log = log; _httpConfiguration = httpConfiguration;
        }
        public async Task RunJob()
        {
            ConcurrentBag<ResponseSend> resultError = new ConcurrentBag<ResponseSend>();


        }
            public async Task<ConcurrentBag<ResponseSend>>  Send(IFactrorySendMessages factrorySendMessages, RegisterSender registerSender, ConcurrentBag<ResponseSend> resultError)
        {
            MockRepository mockData = new MockRepository();
            //ConcurrentBag<ResponseSend> resultError = new ConcurrentBag<ResponseSend>();
            registerSender.Log = Thread.CurrentThread.ManagedThreadId;
            var sender = await factrorySendMessages.GetSenderFactory(registerSender);
            try
            {
                if (!sender.IsError)
                {
                    var requestToSend = await mockData.FindMessagesById(registerSender.Id);

                    if (requestToSend != null)
                    {
                        IWebHookManager manager = this._httpConfiguration.DependencyResolver.GetManager();
                        List<NotificationDictionary> notifications = new List<NotificationDictionary>();
                        notifications.Add(new NotificationDictionary(BaladorConst.PreUpdate, new { Request = requestToSend }));
                        await manager.NotifyAsync(registerSender.Id, notifications, null);
                        if (manager is IExposeResult)
                        {

                            IExposeResult iexposeResult = (IExposeResult)manager;
                            if (iexposeResult != null && iexposeResult.NotificationResult != null)
                            {
                                foreach (var messageWebHookClient in iexposeResult.NotificationResult.Messages)
                                {
                                    var messageToChange = requestToSend.Messages.Where(p => p.Id == messageWebHookClient.Id).FirstOrDefault();
                                    if (messageToChange != null)
                                        messageToChange.Message = messageWebHookClient.Message;
                                }
                            }
                        }
                        requestToSend.Log = registerSender.Log;
                        var responseToSendWait = await sender.Result.Send(requestToSend);
                        resultError.Add(responseToSendWait);
                        _log.Info(responseToSendWait.ToString());
                        notifications = new List<NotificationDictionary>();
                        notifications.Add(new NotificationDictionary(BaladorConst.PostUpdate, new { Response = requestToSend }));
                        await manager.NotifyAsync(registerSender.Id, notifications, null);
                    }
                }
                else
                {
                    var mes = registerSender.Log + " " + registerSender.Id + " " + sender.Message;
                    resultError.Add(new ResponseSend { IsError = true, Message = mes });
                    _log.Error(mes);
                }
            }
            catch (Exception ee)
            {
                resultError.Add(new ResponseSend { IsError = true, Message = "unhandle" });
                _log.Error(registerSender.Log + " " + registerSender.Id + " " + ee);
            }
            finally
            {
                if (sender != null && sender.Result != null)
                    sender.Result.Dispose();
            }
            return resultError;
        }
    }
}