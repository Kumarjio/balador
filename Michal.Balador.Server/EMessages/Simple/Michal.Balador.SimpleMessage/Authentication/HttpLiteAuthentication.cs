﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.SimpleMessage
{
   
    public class HttpLiteAuthentication : AuthenticationManager
    {
        //public HttpLiteAuthentication(IBaladorContext context, SenderMessagesService senderMessages) : base(context, senderMessages)
        //{
        //}

        public HttpLiteAuthentication(IBaladorContext context, IAppMessangerFactrory provider) : base(context, provider)
        {
        }

        public override string AuthenticationTitle
        {

            get
            {
                return "Http Lite Authentication";
            }
        }

        public override string AuthenticationName
        {
            get
            {
                return "HttpLiteAuthentication";
            }
        }

        public override async Task<BToken> GetToken( SignUpSender signUpSender)
        {
            ConfigHttpLite config = await Context.GetConfiguration<ConfigHttpLite>(Provider.ServiceName, signUpSender.UserName);
            if (config != null && !String.IsNullOrEmpty(config.Token))
            {
                return config;
            }
            return null; 
        }

        public override async Task<SenderLandPageConfiguration> Register(SignUpSender signUpSender)
        {
            var senderLandPageConfiguration = new SenderLandPageConfiguration(this.Provider.ServiceName)
            {
                Logo = "",
                MessageEmailTemplate = "http lite",
                TextLandPageTemplate = "http lite",
                TwoFactorAuthentication=false

            };
            senderLandPageConfiguration.ExtraFields.Add(new FieldView { Name = "token", Title = "write token only " });

            var token = await GetToken(signUpSender);
            if (token != null)
            {
                senderLandPageConfiguration.IsAlreadyRegister = true;
            }
           
            return senderLandPageConfiguration;
        }

        public override async Task<Response<AuthenticationUser>> SignIn(SignUpSender senderDetail, NameValueCollection extraDataForm)
        {
            var response = new Response<AuthenticationUser>();
            response.IsError = false;
            response.Result = new AuthenticationUser { IsTwoFactorAuthentication = false, UserId = senderDetail.UserName };
            var url = "http://localhost:1945/token";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders
             .Accept
             .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var dict = new Dictionary<string, string>();

            var pws = extraDataForm["token"];
           await Context.SetConfiguration(this.Provider.ServiceName, senderDetail.UserName, new ConfigHttpLite { Token=pws});
            return response;

        }
        public override async Task<ResponseBase> UnRegister(SignUpSender signUpSender)
        {
            ResponseBase response = new ResponseBase();
            var config = await Context.GetConfiguration<ConfigHttpLite>(this.Provider.ServiceName, signUpSender.UserName);
            if (config != null && !String.IsNullOrEmpty(config.Token))
            {
                await Context.SetConfiguration(this.Provider.ServiceName, signUpSender.UserName, new ConfigHttpLite());

            }
            return response;
        }
        public override async Task<ResponseBase> SetObservableToken(SignUpSender signUpSender, BToken token)
        {
            var config = await Context.GetConfiguration<ConfigHttpLite>(this.Provider.ServiceName, signUpSender.UserName);
            if (config == null)
            {
                config = new ConfigHttpLite { Token = token.Token, UserId = signUpSender.UserName };
            }
            else
            {
                config.Token = token.Token;
            }
            var result = await this.Context.SetConfiguration(this.Provider.ServiceName, signUpSender.UserName, config);

            return result;
        }
    }
}
