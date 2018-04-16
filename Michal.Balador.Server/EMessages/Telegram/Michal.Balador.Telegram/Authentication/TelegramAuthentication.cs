using System;
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
using Michal.Balador.Telegram.Config;
using TLSharp.Core;

//using Newtonsoft.Json.Linq;

namespace Michal.Balador.Telegram.Authentication
{

    public class TelegramAuthentication : AuthenticationManager
    {

        public TelegramAuthentication(IBaladorContext context, IAppMessangerFactrory provider) : base(context, provider)
        {
        }
        public override string AuthenticationTitle
        {

            get
            {
                return "Telegram Authentication";
            }
        }

        public override string AuthenticationName
        {
            get
            {
                return "TelegramAuthentication";
            }
        }

        public override async Task<ResponseBase> SetObservableToken(SignUpSender signUpSender, BToken token)
        {
            ConfigTelegram configTelegram = await Context.GetConfiguration<ConfigTelegram>(Provider.ServiceName, signUpSender.UserName);
            var store = new BaladorSessionStore(this.Context, configTelegram,this.Provider, signUpSender);
            var client = new TelegramClient(configTelegram.Api, configTelegram.Api_Hash, store);
            var user = await client.MakeAuthAsync(configTelegram.Phone, configTelegram.CodeRequest, token.Token);
            configTelegram.Token = token.Token;
            var result = await this.Context.SetConfiguration(this.Provider.ServiceName, signUpSender.UserName, configTelegram);

            return result;
        }

        public override async Task<BToken> GetToken(SignUpSender signUpSender)
        {
            var config = await Context.GetConfiguration<ConfigTelegram>(Provider.ServiceName, signUpSender.UserName);
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
                MessageEmailTemplate = "Telegram",
                TextLandPageTemplate = "Telegram",
                TwoFactorAuthentication = true

            };
            senderLandPageConfiguration.AddExtraFields(new FieldView { Name = "API_ID", Title = "API_ID" })
                .AddExtraFields(new FieldView { Name = "API_HASH", Title = "API_HASH" })
                .AddExtraFields(new FieldView { Name = "phone", Title = "phone" });
            senderLandPageConfiguration.AddAcceptable("Remember It's will replace your whatsApp Application Account  on your mobile phone!!!").AddExplain("Goto API development tools and copy API_ID and API_HASH from your account. You'll need it later.");
            var config = await Context.GetConfiguration<ConfigTelegram>(this.Provider.ServiceName, signUpSender.UserName);
            if (config != null && !String.IsNullOrEmpty(config.Token))
                senderLandPageConfiguration.IsAlreadyRegister = true;

            return senderLandPageConfiguration;
        }

        public override async Task<Response<AuthenticationUser>> SignIn(SignUpSender senderDetail, NameValueCollection extraDataForm)
        {
            var response = new Response<AuthenticationUser>();
            response.Result = new AuthenticationUser { IsTwoFactorAuthentication = true, UserId = senderDetail.UserName };
            response.IsError = true;

            var phone = extraDataForm["phone"];
            var api = extraDataForm["API_ID"];
            var apiHash = extraDataForm["API_HASH"];
            int apiId = int.Parse(api);
            ConfigTelegram configTelegram = await Context.GetConfiguration<ConfigTelegram>(Provider.ServiceName, senderDetail.UserName);
            var store = new BaladorSessionStore(this.Context, configTelegram, this.Provider, senderDetail);
            var client = new TelegramClient(apiId, apiHash, store);
            var isconnected = await client.ConnectAsync();
            if (!isconnected)
            {
                return new Response<AuthenticationUser> { IsError = true, Message = "unable to connect" };
            }
          
            configTelegram = configTelegram ?? new ConfigTelegram { Token = "", Phone = phone, Api = apiId, Api_Hash = apiHash };
            var estaRegistrado = await client.IsPhoneRegisteredAsync(phone);
            if (estaRegistrado)
            {
                if (!client.IsUserAuthorized())
                    configTelegram.CodeRequest = await client.SendCodeRequestAsync(phone);
            }

            var isregister = await client.ConnectAsync();
            if (isregister)
            {
                response.IsError = false;
                await Context.SetConfiguration(this.Provider.ServiceName, senderDetail.UserName, new ConfigTelegram { Token = "", Phone = phone });
            }
            return response;
        }

        public override async Task<ResponseBase> UnRegister(SignUpSender signUpSender)
        {
            ResponseBase response = new ResponseBase();
            response.Message = "unregister done";
            var config = await Context.GetConfiguration<ConfigTelegram>(this.Provider.ServiceName, signUpSender.UserName);
            if (config != null && !String.IsNullOrEmpty(config.Token))
                await Context.SetConfiguration(this.Provider.ServiceName, signUpSender.UserName, new ConfigTelegram());
            return response;
        }
    }
}
