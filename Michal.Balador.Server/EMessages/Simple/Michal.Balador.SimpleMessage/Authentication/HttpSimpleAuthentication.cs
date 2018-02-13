using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.SimpleMessage
{
   
    public class HttpSimpleAuthentication : AuthenticationManager
    {
        public HttpSimpleAuthentication(IBaladorContext context,SenderMessages senderMessages) : base(context, senderMessages)
        {
        }

        public override string AuthenticationTitle
        {

            get
            {
                return "Http Simple Authentication";
            }
        }

        public override string AuthenticationName
        {
        get
            {
                return "HttpSimpleAuthentication";
            }
        }
      
        public override async Task<ResponseBase> GetObservableToken(SignUpSender signUpSender, string token)
        {
            var result=await this.Context.SetConfiguration(this.SenderMessages, token);

            return result;
        }

        public override SenderLandPageConfiguration Register(SignUpSender signUpSender)
        {
            var senderLandPageConfiguration = new SenderLandPageConfiguration(this.SenderMessages)
            {
                Logo = "",
                MessageEmailTemplate= "http test",
                TextLandPageTemplate="http test",
                
            };
            senderLandPageConfiguration.ExtraFields.Add(new FieldView { Name = "pws", Title = "write password" });
            senderLandPageConfiguration.ExtraFields.Add(new FieldView { Name = "client_id", Title = "client" });
            senderLandPageConfiguration.ExtraFields.Add(new FieldView { Name = "grant_type", Title = "grant type" });
            return senderLandPageConfiguration;
        }

        public override async Task<Response<AuthenticationUser>> SignIn(SignUpSender senderDetail, NameValueCollection extraDataForm)
        {
            var response = new Response<AuthenticationUser>();
            response.Result = new AuthenticationUser { IsTwoFactorAuthentication = true, UserId = senderDetail.Id };
            response.IsError = true;
            var url = "http://localhost:8988/token";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders
             .Accept
             .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var dict = new Dictionary<string, string>();

            var pws=extraDataForm["pws"];
            var grant_type = extraDataForm["grant_type"];
            var client_id = extraDataForm["client_id"];
            
            dict.Add("username", senderDetail.Id);
            dict.Add("password", pws);
            dict.Add("client_id", client_id);
            dict.Add("grant_type", grant_type);

            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(dict) };
            var res = await httpClient.SendAsync(req);
            if (res.IsSuccessStatusCode)
            {
                response.IsError = false;
                var stringRes = await res.Content.ReadAsStringAsync();
                Context.GetLogger().Log(System.Diagnostics.TraceLevel.Verbose, stringRes);

            }

            return response;

        }
    }
}
