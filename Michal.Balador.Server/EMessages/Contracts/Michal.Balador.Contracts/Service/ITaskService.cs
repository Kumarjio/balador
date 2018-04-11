using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Dal;

namespace Michal.Balador.Contracts.Service
{
  public  interface ITaskService: IRepository
    {
        ITaskSchedulerRepository TaskSchedulerRepository { get; }
        IMessageRepository MessageRepository { get; }
    }
}
