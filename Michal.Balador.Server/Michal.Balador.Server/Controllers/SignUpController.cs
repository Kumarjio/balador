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

        public async Task<HttpResponseMessage> Get()
        {
            List<FormSignThirdPartyToken> authentications = new List<FormSignThirdPartyToken>();

            MockRepository mockData = new MockRepository();
            foreach (var senderRule in _senderRules)
            {
                var sender = await senderRule.Value.GetSender(new RegisterSender { IsAuthenticate = false, Id = "1" });
                var authenticationManager = sender.Result.GetAuthenticationManager();
                var configuration = authenticationManager.Register(new SignUpSender { Id = "33" });

                authentications.Add(new FormSignThirdPartyToken
                {
                    Id = configuration.Id,
                    Fields = configuration.ExtraFields,
                    Message = configuration.TextLandPageTemplate,
                    Name = authenticationManager.AuthenticationName,
                    Title = authenticationManager.AuthenticationTitle
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


        [HttpPost]
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request)
        {

            ResponseBase responseResult = new ResponseBase();
            try
            {
                var str = await request.Content.ReadAsStringAsync();

            }
            catch (Exception eee)
            {
                responseResult.IsError = true;
                responseResult.Message = eee.Message;
                //throw;
            }
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ResponseBase>(responseResult,
                        new JsonMediaTypeFormatter(),
                         new MediaTypeWithQualityHeaderValue("application/json"))
            };
            return response;
        }
    }
        
    }