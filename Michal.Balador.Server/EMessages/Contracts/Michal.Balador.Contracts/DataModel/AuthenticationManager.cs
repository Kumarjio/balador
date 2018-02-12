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
        public SenderMessages SenderMessages { private set; get; }
        public AuthenticationManager(IBaladorContext context, SenderMessages senderMessages)
        {
            Context = context;
            SenderMessages = senderMessages;
        }
        public abstract string AuthenticationTitle { get; }
        public abstract string AuthenticationName { get; }

        //step 1 after set signup sender register page or email
        public abstract SenderLandPageConfiguration Register( SignUpSender signUpSender);

        //step 2 signin
        public abstract Task<ResponseBase> SignIn(SignUpSender senderDetail, NameValueCollection extraDataForm);


        //step 3 after get from sms message token ,the sender write token and send it back to manager
        public abstract Task<ResponseBase> GetObservableToken(SignUpSender signUpSender,string  token);

    }
}
