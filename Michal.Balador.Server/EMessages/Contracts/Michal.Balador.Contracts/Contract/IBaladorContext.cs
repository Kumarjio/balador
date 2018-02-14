using Michal.Balador.Contracts.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;

namespace Michal.Balador.Contracts
{
    public interface IBaladorContext
    {
        IBaladorLogger GetLogger();
        //start:helper functions
        Task<T> GetConfiguration<T>(SenderMessages senderMessages);
        Task<ResponseBase> SetConfiguration<T>(SenderMessages senderMessages, T config);
        Task<T> GetContact<T>(SenderMessages senderMessages, string id);
        Task<ResponseBase> SetContact<T>(SenderMessages senderMessages, T contact);

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
