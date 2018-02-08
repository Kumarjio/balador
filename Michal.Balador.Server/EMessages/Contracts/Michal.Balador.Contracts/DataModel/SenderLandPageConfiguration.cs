using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
    public class SenderLandPageConfiguration
    {
        public SenderLandPageConfiguration(SenderMessages senderMessages)
        {
            ExtraFields = new Dictionary<string, string>();
            Id = senderMessages.ServiceName;
        }
        public string Id { private set; get; }
        public string Logo { get; set; }
        public string MessageEmailTemplate { get; set; }
        public string TextLandPageTemplate { get; set; }
       // public SignUpSender SignUpSender { get; set; }
        public Dictionary<string,string> ExtraFields { get; set; }//fieldname,title

    }
}
