using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
    public class FieldView
    {
        public string Name { get; set; }
        public string Title { get; set; }
    }
    public class SenderLandPageConfiguration
    {

        public SenderLandPageConfiguration(SenderMessagesService senderMessages)
        {
            ExtraFields = new List<FieldView>();
            Id = senderMessages.ServiceName;
        }
        public string Id { private set; get; }
        public string Logo { get; set; }
        public string MessageEmailTemplate { get; set; }
        public string TextLandPageTemplate { get; set; }
        // public SignUpSender SignUpSender { get; set; }
        // public Dictionary<string,string> ExtraFields { get; set; }//fieldname,title
        public List<FieldView> ExtraFields { get; set; }//fieldname,title
        public bool IsAlreadyRegister { get; set; }
        public bool TwoFactorAuthentication { get; set; }

    }
}
