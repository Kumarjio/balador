﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Contracts.Dal
{
    public interface IAccountRepository : IRepository
    {
        Task<string> GetUserId(string username);
    }
}
