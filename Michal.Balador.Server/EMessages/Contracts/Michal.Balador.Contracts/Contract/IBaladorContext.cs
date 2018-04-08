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
        Task<T> GetConfiguration<T>(string serviceName, string id);
        Task<ResponseBase> SetConfiguration<T>(string serviceName, string id, T config);
        Task<T> GetContact<T>(string serviceName, string id);
        Task<ResponseBase> SetContact<T>(string serviceName, string id, T contact);

        /// <summary>
        /// Notify Sender Message
        /// </summary>
        /// <param name="senderMessages">current plugin</param>
        /// <param name="id">phone of sender find out email adress</param>
        /// <param name="message">message to email</param>
        /// <returns></returns>
        Task<ResponseBase> NotifySenderMessage(string serviceName, string id, string message);

        //end:helper functions}
    }
}
