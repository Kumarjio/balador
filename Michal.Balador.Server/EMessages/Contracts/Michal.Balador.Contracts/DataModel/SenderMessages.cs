using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Service;

namespace Michal.Balador.Contracts.DataModel
{
   public abstract class SenderMessages:IDisposable
    {
        protected FactrorySendMessages _provider;
        public string ServiceName
        {
            get
            {
                // Using reflection.  
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(_provider.GetType());  // Reflection.  
                var message_type = "";

                var domain = "";
                // Displaying output.  
                foreach (System.Attribute attr in attrs)
                {
                    if (attr is ExportMetadataAttribute)
                    {
                         ExportMetadataAttribute a = (ExportMetadataAttribute)attr;
                        if(a.Name== ConstVariable.MESSAGE_TYPE)
                        {
                            message_type = a.Value.ToString();
                        }
                        else if (a.Name == ConstVariable.DOMAIN_NAME)
                        {
                            domain = a.Value.ToString();
                        }

                        System.Console.WriteLine("   {0}, version {1}", a.Name, a.Value);
                    }
                }

                return $"{domain}.{message_type}";//this.GetType().FullName;
            }
        }

        
        public abstract void Dispose();
        public abstract  Task<ResponseSend> Send(SendRequest request);
        IBaladorContext _context;
        public IBaladorContext Context { get { return _context; } }

        public SenderMessages(IBaladorContext context, FactrorySendMessages provider)
        {
            _context = context; _provider = provider;
        }

        public abstract AuthenticationManager GetAuthenticationManager();
    }
}
