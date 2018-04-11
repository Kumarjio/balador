using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Behaviors;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Service
{
    public class TaskSchedulerManager:IDisposable
    {
        ITaskSchedulerRepository _taskSchedulerRepository;
        public TaskSchedulerManager(ITaskSchedulerRepository taskSchedulerRepository)
        {
            _taskSchedulerRepository = taskSchedulerRepository;
        }

        public void Dispose()
        {
            _taskSchedulerRepository.Dispose();
        }

        public async Task<ResponseBase> Run(BehaviorItems<Behavior> behaviors = null)
        {
            
            return await Task.FromResult(new ResponseBase());
        }
    }
}
