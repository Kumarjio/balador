﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;
using Michal.Balador.Contracts.Service;

namespace Michal.Balador.SimpleMessage
{
    [Export(typeof(IFactrorySendMessages))]
    [ExportMetadata("MessageType", "MockSender")]
    public class MockSender : FactrorySendMessages
    {
        [ImportingConstructor()]
        public MockSender(IBaladorContext context) :base(context)
        {

        }
        protected override async Task<ResponseSenderMessages> GetSender(RegisterSender register)
        {
            ResponseSenderMessages response = new ResponseSenderMessages();
            try
            {
                var mockSend = new MockSend(Context);
                response =await mockSend.SetSocketClient(new SignUpSender { Id= register.Id});
                return response;
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.Message = e.Message;
            }
            return response;
        }

       
    }
}
