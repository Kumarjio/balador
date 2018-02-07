using Michal.Balador.Contracts.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts
{
    public interface IBaladorContext
    {
        //start:helper functions
        Task<object> GetConfiguration(SenderMessages senderMessages);
        Task<ResponseBase> SetConfiguration(SenderMessages senderMessages, object config);
        Task<object> GetContact(SenderMessages senderMessages, string id);
        Task<ResponseBase> SetContact(SenderMessages senderMessages, object contact);

        /// <summary>
        /// Notify Sender Message
        /// </summary>
        /// <param name="senderMessages">current plugin</param>
        /// <param name="id">phone of sender find out email adress</param>
        /// <param name="message">message to email</param>
        /// <returns></returns>
        Task<ResponseBase> NotifySenderMessage(SenderMessages senderMessages, string id, string message);

        //end:helper functions}
    }
}
