using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Behaviors;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.Contracts.Service;

namespace Michal.Balador.Contracts.Contract
{
    public interface IAppMessangerFactrory:IDisposable
    {
        //[Obsolete("WILL REMOVE",false)]
        //Task<ResponseAppMessanger> GetInstance(RegisterSender register);

        string ServiceName { get; }
        AuthenticationManager GetAuthenticationManager();
        BehaviorItems<Behavior> BehaviorItems { get; }

        ITaskService TaskService { get; }
   
        Task<ResponseAppMessanger> GetAppMessanger(AccountSend register);


       
    }
}
