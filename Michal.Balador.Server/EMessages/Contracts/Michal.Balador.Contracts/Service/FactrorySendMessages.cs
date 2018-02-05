﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Service
{
    public abstract class FactrorySendMessages: IFactrorySendMessages
    {
        protected IBaladorContext _context;
       
        public FactrorySendMessages(IBaladorContext context)
        {
            _context = context;
        }

        public abstract  Task<ResponseSenderMessages> GetSender(RegisterSender register);
    }
}