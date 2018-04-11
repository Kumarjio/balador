using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.SimpleMessage
{
   // [Export("Simple2", typeof(IEMessage))]
    [Export(typeof(IEMessage))]
    [ExportMetadata("MessageType", "Simple_2")]
    public class Simple2 : IEMessage
    {
        public Task<ResponseSender> ConnectAndSend(Sender send)
        {
            return Task.FromResult(new ResponseSender {Message= "Simple2" });
            //throw new NotImplementedException();
        }
    }
}
