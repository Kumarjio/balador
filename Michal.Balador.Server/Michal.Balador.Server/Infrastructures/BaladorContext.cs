using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;

namespace Michal.Balador.Infrastructures.Service
{
   [Export(typeof(IBaladorContext))]
   [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BaladorContext : IBaladorContext
    {
        public BaladorContext()
        {
            Log = 5;
        }
        public int Log { get; set; }

        [Import(typeof(IBaladorLogger))]
        IBaladorLogger _logger;

        public Task<object> GetConfiguration(SenderMessages senderMessages)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseBase>  SetConfiguration(SenderMessages senderMessages, object config)
        {
            // throw new NotImplementedException();
            _logger.Log(System.Diagnostics.TraceLevel.Info, config.ToString());
            return await Task.FromResult<ResponseBase>(new ResponseBase { IsError = false, Message = "" });

        }

        public  Task<object> GetContact(SenderMessages senderMessages, string id)
        {
            return null;
            //throw new NotImplementedException();
        }

        public Task<ResponseBase> NotifySenderMessage(SenderMessages senderMessages, string id, string message)
        {
            //send to email adress of sender!!! 
            throw new NotImplementedException();
        }

        public Task<ResponseBase> SetContact(SenderMessages senderMessages, object contact)
        {
            return null;
            //throw new NotImplementedException();
        }

        public IBaladorLogger GetLogger()
        {
            return _logger;
        }
    }
}
