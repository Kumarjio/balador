using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Server.Controllers
{//http://kennytordeur.blogspot.co.il/2012/08/mef-in-aspnet-mvc-4-and-webapi.html
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JobController : ApiController
    {
        [Import]
        private IMyTest _myTest;
        [ImportMany(typeof(IEMessage))]
        IEnumerable<Lazy<IEMessage>> _messages;
        // GET api/<controller>
        public async Task<IEnumerable<string>> Get()
        {
            List<ResponseSender> senders = new List<ResponseSender>();
            List<string> ss = new List<string>();
            foreach (var item in _messages)
            {
                var sender = await item.Value.ConnectAndSend(new Contracts.DataModel.Sender());
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


