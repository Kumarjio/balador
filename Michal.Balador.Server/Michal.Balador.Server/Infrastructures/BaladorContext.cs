﻿using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<object> GetConfiguration(SenderMessages senderMessages)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> SetConfiguration(SenderMessages senderMessages, object config)
        {
            throw new NotImplementedException();
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
    }
}
