using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Behaviors;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Contracts.Contract
{
    public interface ITaskSendsScheduler
    {
        Task<ConcurrentBag<ResponseSend>> Run(BehaviorItems<Behavior> behaviors = null);
    }
}
