using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Server.Controllers
{//http://kennytordeur.blogspot.co.il/2012/08/mef-in-aspnet-mvc-4-and-webapi.html
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JobController : ApiController
    {
        [Import]
        private IUnitOfWork _unitOfWork;
        [Import]
        private IMyTest _myTest;
        [ImportMany(typeof(IEMessage))]
        IEnumerable<Lazy<IEMessage>> _messages;
        [ImportMany(typeof(IEMessage))]
        IEnumerable<Lazy<IEMessage, IDictionary<string, object>>> __messagesRules;
        // GET api/<controller>
        public async Task<IEnumerable<string>> Get()
        {
           
            //  _unitOfWork.Get<a>
            List<ResponseSender> senders = new List<ResponseSender>();
            List<string> ss = new List<string>();
            Lazy<IEMessage> _utah = __messagesRules.Where(s => (string)s.Metadata["MessageType"] == "Simple_2").FirstOrDefault();
            if(_utah!=null && _utah.Value != null)
            {
                var sender2 = await _utah.Value.ConnectAndSend(new Contracts.Mechanism.Sender());
                ss.Add(sender2.Message);
            }

            foreach (var item in _messages)
            {
                var sender = await item.Value.ConnectAndSend(new Contracts.Mechanism.Sender());
                senders.Add(sender);
                ss.Add(sender.Message);
            }
            ss.Add(_myTest.GetMessage());
            return ss.ToArray();
        }
    }

    public interface IMyTest
    {
        String GetMessage();
    }

    [Export(typeof(IMyTest))]
    public class MyTest1 : IMyTest
    {
        public MyTest1()
        {
            creationDate = DateTime.Now;
        }

        public string GetMessage()
        {
            return String.Format("MyTest1 created at {0}", creationDate.ToString("hh:mm:ss"));
        }

        private DateTime creationDate;
    }
}


