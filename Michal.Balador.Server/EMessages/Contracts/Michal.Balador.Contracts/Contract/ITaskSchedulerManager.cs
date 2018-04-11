using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Behaviors;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Contracts.Contract
{
    public interface ITaskSchedulerManager
    {
        Task<ResponseBase> Run(BehaviorItems<Behavior> behaviors=null);
    }
}
