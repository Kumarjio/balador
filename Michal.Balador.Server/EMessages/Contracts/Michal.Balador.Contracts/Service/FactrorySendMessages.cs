using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Behaviors;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Service
{
    public abstract class FactrorySendMessages : IFactrorySendMessages
    {
        protected string _serviceName = "";
        protected IUnitOfWork _unitOfWork;
        protected IBaladorContext _context;
        protected BehaviorItems<Behavior> _behaviorItems;
        protected AuthenticationManager _authenticationManager;
       
     
        public IBaladorContext Context
        {
            get
            {
                return _context;
            }
        }

        public BehaviorItems<Behavior> BehaviorItems
        {
            get
            {
                return _behaviorItems;
            }
        }

        public FactrorySendMessages(IBaladorContext context)
        {
            _context = context;
        
        }
        public void EnrolInBehaviors(BehaviorItems<Behavior> behaviorItems)
        {
            _behaviorItems = behaviorItems;
        }


        public virtual async Task<ResponseSenderMessages> GetInstance(RegisterSender register)
        {
            register.CanExcute = true;
            ResponseSenderMessages response = await GetSender(register);
            if (!response.IsError)
            {
                _authenticationManager = this.GetAuthenticationManager();
                var token = await _authenticationManager.GetToken(new SignUpSender { Id = register.Id });
                if (token == null || String.IsNullOrWhiteSpace(token.Token))
                {
                    response.IsAutorize = false;
                }
            }
            return response;

        }

        protected abstract Task<ResponseSenderMessages> GetSender(RegisterSender register);


        public virtual string ServiceName
        {
            get
            {
                if (String.IsNullOrEmpty(_serviceName))
                {
                    System.Attribute[] attrs = System.Attribute.GetCustomAttributes(this.GetType());  // Reflection.  
                    var message_type = "";

                    var domain = "";
                    foreach (System.Attribute attr in attrs)
                    {
                        if (attr is ExportMetadataAttribute)
                        {
                            ExportMetadataAttribute a = (ExportMetadataAttribute)attr;
                            if (a.Name == ConstVariable.MESSAGE_TYPE)
                            {
                                message_type = a.Value.ToString();
                            }
                            else if (a.Name == ConstVariable.DOMAIN_NAME)
                            {
                                domain = a.Value.ToString();
                            }
                        }
                    }

                    _serviceName= $"{domain}${message_type}";//this.GetType().FullName;
                }
                return _serviceName;
            }
        }

        public abstract AuthenticationManager GetAuthenticationManager();
        //public virtual async Task<AuthenticationManager> GetAuthenticationManager(RegisterSender register)
        //{
        //    register.CanExcute = false;
        //       ResponseSenderMessages response = await GetSender(register);
        //    if (!response.IsError)
        //    {
        //        return response.Result.GetAuthenticationManager();

        //    }

        //    return null;
        //}
        
    }
}
