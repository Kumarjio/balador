using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
   public abstract class SenderMessages:IDisposable
    {

        public abstract void Dispose();
        //{
          //  throw new NotImplementedException();
        //}

        public abstract  Task<ResponseSend> Send(SendRequest request);
        IBaladorContext _context;
        public IBaladorContext Context { get { return _context; } }
        public SenderMessages(IBaladorContext context)
        {
            _context = context;
        }

        //public abstract Task<AuthenticationManager> GetAuthenticationManager();
    }
}
