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

namespace Michal.Balador.Contracts.Mechanism
{
    public abstract class AppMessangerFactrory : IAppMessangerFactrory
    {
        protected string _serviceName = "";
        //protected IUnitOfWork _unitOfWork;
        protected IBaladorContext _context;
        protected BehaviorItems<Behavior> _behaviorItems;
        protected AuthenticationManager _authenticationManager;
        ITaskSchedulerRepository _taskSchedulerRepository;

        public IBaladorContext Context
        {
            get
            {
                return _context;
            }
        }
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _taskSchedulerRepository.DbContext;
            }
        }
        public BehaviorItems<Behavior> BehaviorItems
        {
            get
            {
                return _behaviorItems;
            }
        }

        public ITaskSchedulerRepository TaskSchedulerRepository
        {
            get
            {
                return _taskSchedulerRepository;
            }
        }

        public AppMessangerFactrory(IBaladorContext context, ITaskSchedulerRepository taskSchedulerRepository)
        {
            _context = context; _taskSchedulerRepository = taskSchedulerRepository;
        }

        protected void EnrolInBehaviors(BehaviorItems<Behavior> behaviorItems)
        {
            _behaviorItems = behaviorItems;
        }

        public virtual async Task<ResponseAppMessanger> GetInstance(RegisterSender register, BehaviorItems<Behavior> behaviorItems=null)
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

        protected abstract Task<ResponseAppMessanger> GetSender(RegisterSender register);


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
            _taskSchedulerRepository.Dispose();
        }

    }
}
