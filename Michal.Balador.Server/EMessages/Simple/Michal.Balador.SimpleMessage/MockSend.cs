using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.SimpleMessage
{
    public class MockSend : SenderMessages
    {
       
        public override Task<ResponseSend> Send(SendRequest request)
        {
            return null;
        }
    }
}
