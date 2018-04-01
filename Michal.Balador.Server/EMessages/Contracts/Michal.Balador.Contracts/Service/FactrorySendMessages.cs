using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Service
{
    public abstract class FactrorySendMessages : IFactrorySendMessages
    {
        protected IBaladorContext _context;
        protected AuthenticationManager _authenticationManager;
        public IBaladorContext Context
        {
            get
            {
                return _context;
            }
        }

        public FactrorySendMessages(IBaladorContext context)
        {
            _context = context;
        }
        public virtual async Task<ResponseSenderMessages> GetInstance(RegisterSender register)
        {
            ResponseSenderMessages response = await GetSender(register);
            if (!response.IsError)
            {
                _authenticationManager = response.Result.GetAuthenticationManager();
                var token = await _authenticationManager.GetToken(response.Result, new SignUpSender { Id = register.Id });
                if (token == null || String.IsNullOrWhiteSpace(token.Token))
                {
                    response.IsAutorize = false;
                }
            }
            return response;

        }

        protected abstract Task<ResponseSenderMessages> GetSender(RegisterSender register);


        public virtual async Task<AuthenticationManager> GetAuthenticationManager(RegisterSender register)
        {
            ResponseSenderMessages response = await GetSender(register);
            if (!response.IsError)
            {
                return response.Result.GetAuthenticationManager();
                
            }

            return null;
        }

        //public virtual AuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return _authenticationManager;
        //    }
        //}
    }
}
