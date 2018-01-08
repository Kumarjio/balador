using System;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.EMassage.Simple
{
    public class Simple : IEMessage
    {
        public Task Send(MessageSender sender)
        {
            throw new NotImplementedException();
        }
    }
}
