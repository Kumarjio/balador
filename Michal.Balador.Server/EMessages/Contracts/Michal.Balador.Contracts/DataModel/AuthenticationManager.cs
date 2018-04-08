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
        protected BToken _token;
        public IBaladorContext Context { private set; get; }
        public SenderMessagesService SenderMessages { private set; get; }
        public AuthenticationManager(IBaladorContext context, SenderMessagesService senderMessages)
        {
            Context = context;
            SenderMessages = senderMessages;
        }
        public abstract string AuthenticationTitle { get; }
        public abstract string AuthenticationName { get; }

        public string ServiceName
        {
            get
            {
                return SenderMessages.ServiceName;
            }
        }
        public abstract Task<BToken> GetToken(SenderMessagesService senderMessages, SignUpSender signUpSender);




        //step 1 after set signup sender register page or email
        public abstract Task<SenderLandPageConfiguration> Register(SignUpSender signUpSender);

        //step 2 signin
        public abstract Task<Response<AuthenticationUser>> SignIn(SignUpSender senderDetail, NameValueCollection extraDataForm);


        //step 3 after get from sms message token ,the sender write token and send it back to manager
        public virtual async Task<ResponseBase> SetObservableToken(SignUpSender signUpSender, BToken token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            return await Task.FromResult<ResponseBase>(new ResponseBase { IsError = false, Message = "" });
        }

        public abstract Task<ResponseBase> UnRegister(SignUpSender signUpSender);

    }
}
