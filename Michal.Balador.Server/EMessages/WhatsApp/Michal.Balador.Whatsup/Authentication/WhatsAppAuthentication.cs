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
using Michal.Balador.NSWhatsApp.Config;
//using Newtonsoft.Json.Linq;

namespace Michal.Balador.NSWhatsApp.Authentication
{
   
    public class WhatsAppAuthentication: AuthenticationManager
    {
       
        public WhatsAppAuthentication(IBaladorContext context, IAppMessangerFactrory provider) : base(context, provider)
        {
        }
        public override string AuthenticationTitle
        {

            get
            {
                return "NSWhatsApp Authentication";
            }
        }

        public override string AuthenticationName
        {
        get
            {
                return "WhatsAppAuthentication";
            }
        }
      
        public override async Task<ResponseBase> SetObservableToken(SignUpSender signUpSender, BToken token)
        {
            var config = await Context.GetConfiguration<ConfigWhatsApp>(this.Provider.ServiceName, signUpSender.UserId);
            config.Token = token.Token;
            var pws= WhatsAppApi.Register.WhatsRegisterV2.RegisterCode(config.Phone, config.Token);
            //config.TempPwsSms = config.Token;
            config.Token = pws;//curent

            var result =await this.Context.SetConfiguration(this.Provider.ServiceName, signUpSender.UserId, config);

            return result;
        }

        public override async Task<BToken> GetToken(SignUpSender signUpSender)
        {
            ConfigWhatsApp config = await Context.GetConfiguration<ConfigWhatsApp>(Provider.ServiceName, signUpSender.UserId);
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
                Logo = "/Resources/com.baladorPlant/WhatsApp/whatsapp64x64.png",
                MessageEmailTemplate = "WhatsApp",
                TextLandPageTemplate= "WhatsApp",
                TwoFactorAuthentication = true

            };
            senderLandPageConfiguration
                .AddExtraFields(new FieldView { Name = "phone", Title = "phone" })
                .AddExtraFields(new FieldView { Name = "method", Title = "method sms" })
                .AddAcceptable("Remember It's will replace your whatsApp Application Account  on your mobile phone!!!")
                .AddHelpFile("/Resources/com.baladorPlant/WhatsApp/helpfile.html");
           
            var config = await Context.GetConfiguration<ConfigWhatsApp>(this.Provider.ServiceName, signUpSender.UserId);
            if (config != null && !String.IsNullOrEmpty(config.Token))
            {
                senderLandPageConfiguration.IsAlreadyRegister = true;
            }
    
            return senderLandPageConfiguration;
        }

        public override async Task<Response<AuthenticationUser>> SignIn(SignUpSender senderDetail, NameValueCollection extraDataForm)
        {
            var response = new Response<AuthenticationUser>();
            response.Result = new AuthenticationUser { IsTwoFactorAuthentication = true, UserId = senderDetail.UserId };
            response.IsError = true;
         
            var phone = extraDataForm["phone"];
            var method = "sms";// extraDataForm["method"];


            string password="";
            var isregister=WhatsAppApi.Register.WhatsRegisterV2.RequestCode(phone, out password, method);

            if (isregister)
            {
                response.IsError = false;
              await Context.SetConfiguration(this.Provider.ServiceName, senderDetail.UserId, new ConfigWhatsApp {  Token = password,Phone= phone });
            
            }
            return response;
        }

        public override async Task<ResponseBase> UnRegister(SignUpSender signUpSender)
        {
            ResponseBase response = new ResponseBase();
            response.Message = "unregister done";
            var config = await Context.GetConfiguration<ConfigWhatsApp>(this.Provider.ServiceName, signUpSender.UserId);
            if (config != null && !String.IsNullOrEmpty(config.Token))
            {
                await Context.SetConfiguration(this.Provider.ServiceName, signUpSender.UserId, new ConfigWhatsApp());

            }
            return response;
        }
    }
}
