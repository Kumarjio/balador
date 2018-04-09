using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Behaviors;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts
{
    public interface IFactrorySendMessages:IDisposable
    {
        Task<ResponseSenderMessages> GetInstance(RegisterSender register, BehaviorItems<Behavior> behaviorItems=null);
        string ServiceName { get; }
        AuthenticationManager GetAuthenticationManager();
        BehaviorItems<Behavior> BehaviorItems { get; }

        ITaskSchedulerRepository TaskSchedulerRepository { get; }
    }
}
