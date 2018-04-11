using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Behaviors;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.Contracts.Service;

namespace Michal.Balador.Contracts.Mechanism
{
    public abstract class AppMessangerFactrory : IAppMessangerFactrory
    {
        protected string _serviceName = "";
        //protected IUnitOfWork _unitOfWork;
        protected IBaladorContext _context;
        protected BehaviorItems<Behavior> _behaviorItems;
        protected AuthenticationManager _authenticationManager;
        ITaskService _taskService;//per appmassanger no per account!!!

        public IBaladorContext Context
        {
            get
            {
                return _context;
            }
        }
        //public IUnitOfWork UnitOfWork
        //{
        //    get
        //    {
        //        return _taskService.DbContext;
        //    }
        //}
        public BehaviorItems<Behavior> BehaviorItems
        {
            get
            {
                return _behaviorItems;
            }
        }

        public ITaskService TaskService
        {
            get
            {
                return _taskService;
            }
        }

        public AppMessangerFactrory(IBaladorContext context, ITaskService taskService)
        {
            _context = context; _taskService = taskService;
        }

        public void EnrolInBehaviors(BehaviorItems<Behavior> behaviorItems)
        {
            _behaviorItems = behaviorItems;
        }

        [Obsolete("WILL REMOVE", false)]
        public virtual async Task<ResponseAppMessanger> GetInstance(RegisterSender register, BehaviorItems<Behavior> behaviorItems = null)
        {
            EnrolInBehaviors(behaviorItems);
            register.CanExcute = true;
            ResponseAppMessanger response = await GetSender(register);
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

        [Obsolete("WILL REMOVE", false)]
        protected abstract Task<ResponseAppMessanger> GetSender(RegisterSender register);
        
        protected abstract Task<ResponseAppMessanger> GetSender(AccountSend accountSend);

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

        public virtual void Dispose()
        {
            _taskService.Dispose();
        }

        public async Task<ResponseAppMessanger> GetAppMessanger(AccountSend accountSend)
        {
            
           
            ResponseAppMessanger response = await GetSender(accountSend);
            if (!response.IsError)
            {
                _authenticationManager = this.GetAuthenticationManager();
                var token = await _authenticationManager.GetToken(new SignUpSender { Id = accountSend.Id });
                if (token == null || String.IsNullOrWhiteSpace(token.Token))
                {
                    response.IsAutorize = false;
                }
            }
            return response;
        }
    }
}   
