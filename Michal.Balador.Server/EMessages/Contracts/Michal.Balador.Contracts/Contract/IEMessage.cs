﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts
{
    /// <summary>
    /// For Test Only
    /// </summary>
    public interface IEMessage
    {
        Task<ResponseSender> ConnectAndSend(Sender send);
    }
}
