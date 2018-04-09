using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Behaviors;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts
{
    public interface IFactrorySendMessages
    {
        Task<ResponseSenderMessages> GetInstance(RegisterSender register);
        string ServiceName { get; }
        AuthenticationManager GetAuthenticationManager();
        //void EnrolInBehaviors(BehaviorItems<Behavior> behaviorItems);
        BehaviorItems<Behavior> BehaviorItems { get; }
    }
}
