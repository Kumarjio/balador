using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts
{
    public interface IFactrorySendMessages
    {
        Task<ResponseSenderMessages> GetInstance(RegisterSender register);
        string ServiceName  {  get; }
            
                //Task<AuthenticationManager> GetAuthenticationManager(RegisterSender register);
       AuthenticationManager GetAuthenticationManager();
    }
}
