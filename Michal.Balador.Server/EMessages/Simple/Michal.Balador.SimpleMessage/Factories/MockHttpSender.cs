﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.DataModel;
using Michal.Balador.Contracts.Service;

namespace Michal.Balador.SimpleMessage
{
    [Export(typeof(IFactrorySendMessages))]
    [ExportMetadata(ConstVariable.MESSAGE_TYPE, "MockHttpSender")]
    [ExportMetadata(ConstVariable.DOMAIN_NAME, "com.baladorPlant")]
    public class MockHttpSender : FactrorySendMessages
    {
        [ImportingConstructor()]
        public MockHttpSender(IBaladorContext context) : base(context)
        {
        }
        protected async override Task<ResponseSenderMessages> GetSender(RegisterSender register)
        {
            ResponseSenderMessages response = new ResponseSenderMessages();
            try
            {
                var mckHttpSend = new MockHttpSend(this.Context,this);
                response.Result = mckHttpSend;
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.Message = e.Message;
            }
            return await  Task.FromResult<ResponseSenderMessages>( response);
        }
    }
}