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

    public class HttpLiteAuthentication : AuthenticationManager
    {
        public HttpLiteAuthentication(IBaladorContext context, SenderMessages senderMessages) : base(context, senderMessages)
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

        public override SenderLandPageConfiguration Register(SignUpSender signUpSender)
        {

            var senderLandPageConfiguration = new SenderLandPageConfiguration(this.SenderMessages)
            {
                Logo = "",
                MessageEmailTemplate = "http lite",
                TextLandPageTemplate = "http lite",

            };
            senderLandPageConfiguration.ExtraFields.Add(new FieldView { Name = "token", Title = "write token only " });

            return senderLandPageConfiguration;
        }

        public override async Task<Response<AuthenticationUser>> SignIn(SignUpSender senderDetail, NameValueCollection extraDataForm)
        {
            var response = new Response<AuthenticationUser>();
            response.IsError = false;
            response.Result = new AuthenticationUser { IsTwoFactorAuthentication = false, UserId = senderDetail.Id };
            var url = "http://localhost:1945/token";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders
             .Accept
             .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var dict = new Dictionary<string, string>();

            var pws = extraDataForm["token"];

            return response;

        }
    }
}
