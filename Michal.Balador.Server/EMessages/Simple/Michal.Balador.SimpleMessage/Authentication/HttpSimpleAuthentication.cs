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
    public class HttpSimpleConfigurationAuthentication
    {

    }
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

        public override string AuthenticationName {
        get
            {
                return "HttpSimpleAuthentication";
            }
        }

        public override Task<ResponseBase> GetObservableToken(SignUpSender signUpSender, string token)
        {
            return null;
        }

        public override SenderLandPageConfiguration Register( SignUpSender signUpSender)
        {

            var senderLandPageConfiguration = new SenderLandPageConfiguration(this.SenderMessages)
            {
                Logo = "",
                MessageEmailTemplate= "http test",
                TextLandPageTemplate="http test",
                
            };
            senderLandPageConfiguration.ExtraFields.Add("pws", "write password");
            senderLandPageConfiguration.ExtraFields.Add("client_id", "client");
            senderLandPageConfiguration.ExtraFields.Add("grant_type", "grant type");



            return senderLandPageConfiguration;
        }

        public override async Task<ResponseBase> SignIn(SenderLandPageConfiguration configPageLand, SignUpSender senderDetail, NameValueCollection extraDataForm)
        {
            ResponseBase responseBase = new ResponseBase(); responseBase.IsError = true;
            var url = "http://localhost:1945/token";
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
                responseBase.IsError = false;
                var stringRes = await res.Content.ReadAsStringAsync();
                Context.GetLogger().Log(System.Diagnostics.TraceLevel.Verbose, stringRes);

            }

            return responseBase;

        }
    }
}
