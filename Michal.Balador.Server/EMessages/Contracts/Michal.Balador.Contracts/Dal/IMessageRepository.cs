using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Dal
{
    public interface IMessageRepository : IRepository
    {
        Task<ResponseBase> CreateMessage(MessageRequest request);
    }
}
