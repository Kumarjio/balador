using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.SimpleMessage
{
    //[Export("Simple2", typeof(IEMessage))]
    [Export(typeof(IEMessage))]
    public class Simple2 : IEMessage
    {
        public Task<ResponseSender> ConnectAndSend(Sender send)
        {
            return Task.FromResult(new ResponseSender {Message= "Simple2" });
            //throw new NotImplementedException();
        }
    }
}
