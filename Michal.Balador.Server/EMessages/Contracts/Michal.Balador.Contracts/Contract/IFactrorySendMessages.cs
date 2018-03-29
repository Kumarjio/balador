﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts
{
    public interface IFactrorySendMessages
    {
        Task<ResponseSenderMessages> GetSenderFactory(RegisterSender register);

        AuthenticationManager AuthenticationManager
        {
            get;
        }
    }
}
