using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.SimpleMessage
{
    public class HttpSimpleAuthentication : AuthenticationManager
    {
        public HttpSimpleAuthentication(IBaladorContext context) : base(context)
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

        public override Task<ResponseBase> GetObservableToken(SenderMessages senderMessages, SignUpSender signUpSender, string token)
        {
           throw new NotImplementedException();
        }

        public override SenderLandPageConfiguration Register(SenderMessages senderMessages, SignUpSender signUpSender)
        {
            throw new NotImplementedException();
        }

        public override Task SignIn(SenderMessages senderMessages, SenderLandPageConfiguration configPageLand, SignUpSender senderDetail, NameValueCollection extraDataForm)
        {
            throw new NotImplementedException();
        }
    }
}
