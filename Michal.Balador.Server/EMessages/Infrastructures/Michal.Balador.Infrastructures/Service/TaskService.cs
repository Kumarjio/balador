using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.Service;
using Michal.Balador.Infrastructures.Dal;
using Michal.Balador.Server.Dal;

namespace Michal.Balador.Infrastructures.Service
{
    [Export(typeof(ITaskService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
   public class TaskService: ITaskService
    {
        protected IUnitOfWork _unitOfWork;
        public IUnitOfWork DbContext { get { return _unitOfWork; } }
        IMessageRepository _messageRepository;
        ITaskSchedulerRepository _taskSchedulerRepository;

        [ImportingConstructor()]
        public TaskService(IUnitOfWork unitOfWork)
        {
            _taskSchedulerRepository = new TaskSchedulerRepository(unitOfWork);
            _messageRepository = new MessageRepository(unitOfWork);

        }
        public ITaskSchedulerRepository TaskSchedulerRepository { get { return _taskSchedulerRepository; } }
        public IMessageRepository MessageRepository { get { return _messageRepository; } }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
