using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts
{
    public interface IContext
    {
      object  GetContact(string id);
        void SaveContact(object contact);
    }
}
