using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Behaviors;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Job
{
    public interface ITaskSchedulerManager
    {
        Task<ResponseBase> Run(BehaviorItems<Behavior> behaviors=null);
    }
}
