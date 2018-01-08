using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Contract
{
    public interface IEMessage
    {
        Task Send(MessageSender sender);
    }
}
