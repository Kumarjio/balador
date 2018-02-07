using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
    public abstract class AuthenticationManager
    {
        public IBaladorContext Context { private set; get; }

        public AuthenticationManager(IBaladorContext context)
        {
            Context = context;
        }

        public abstract string AuthenticationName { get; }

        //step 1 after set signup sender register page or email
        public abstract SenderLandPageConfiguration Register(SenderMessages senderMessages, SignUpSender signUpSender);

        //step 2 signin
        public abstract Task SignIn(SenderMessages senderMessages, SenderLandPageConfiguration configPageLand, SignUpSender senderDetail, NameValueCollection extraDataForm);


        //step 3 after get from sms message token ,the sender write token and send it back to manager
        public abstract Task<ResponseBase> GetObservableToken(SenderMessages senderMessages, SignUpSender signUpSender,string  token);

    }
}
