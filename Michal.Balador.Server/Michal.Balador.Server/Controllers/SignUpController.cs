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
using Michal.Balador.Server.Infrastructures.WebHookExstension;
using Michal.Balador.Server.Models;
using Microsoft.AspNet.WebHooks;

namespace Michal.Balador.Server.Controllers
{//http://kennytordeur.blogspot.co.il/2012/08/mef-in-aspnet-mvc-4-and-webapi.html
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SignUpController : ApiController
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(SignUpController));
        [ImportMany(typeof(IFactrorySendMessages))]
        IEnumerable<Lazy<IFactrorySendMessages>> _senderRules;
        // GET api/<controller>
        public async Task<HttpResponseMessage> Get()
        {
            List<FormSignThirdPartyToken> authentications = new List<FormSignThirdPartyToken>();
            List<Contracts.DataModel.AuthenticationManager> authenticationManagers = new List<Contracts.DataModel.AuthenticationManager>();
         
            MockRepository mockData = new MockRepository();
            foreach (var senderRule in _senderRules)
            {
               var sender=await senderRule.Value.GetSender(new RegisterSender { IsAuthenticate=false,Id="1"});
               var authenticationManager = sender.Result.GetAuthenticationManager();
               var configuration= authenticationManager.Register(new SignUpSender {Id="33" });

                authentications.Add(new FormSignThirdPartyToken
                {
                    Id=configuration.Id,
                     Fields=configuration.ExtraFields,
                     Message=configuration.TextLandPageTemplate
                });


            }
                

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<FormSignThirdPartyToken[]>(authentications.ToArray(),
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
