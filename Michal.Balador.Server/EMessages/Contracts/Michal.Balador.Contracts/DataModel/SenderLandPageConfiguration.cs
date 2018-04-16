using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;

namespace Michal.Balador.Contracts.Mechanism
{
    public class FieldView
    {
        public string FieldViewType { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }
    public class SenderLandPageConfiguration
    {
        public SenderLandPageConfiguration(string serviceName)
        {
            ExtraFields = new List<FieldView>();
            Id = serviceName;
        }
        //public SenderLandPageConfiguration(SenderMessagesService senderMessages)
        //{
        //    ExtraFields = new List<FieldView>();
        //    Id = senderMessages.ServiceName;
        //}
        public string Id { private set; get; }
        public string Logo { get; set; }
        public string MessageEmailTemplate { get; set; }
        public string TextLandPageTemplate { get; set; }
        // public SignUpSender SignUpSender { get; set; }
        // public Dictionary<string,string> ExtraFields { get; set; }//fieldname,title
        public List<FieldView> ExtraFields { get; set; }//fieldname,title
        public bool IsAlreadyRegister { get; set; }
        public bool TwoFactorAuthentication { get; set; }
        public string AcceptTemplate { get; set; }
        public string Explain { get; set; }
        public SenderLandPageConfiguration AddExtraFields(FieldView fieldView)
        {
            ExtraFields.Add(fieldView);
            return this;
        }
        public SenderLandPageConfiguration AddAcceptable(string acceptTemplate)
        {
            AcceptTemplate = acceptTemplate;
            ExtraFields.Add(new FieldView { FieldViewType = typeof(bool).Name, Name = ConstVariable.CHECKBOX_Accept, Title = "" });
            return this;
        }
        public SenderLandPageConfiguration AddExplain(string explain)
        {
            Explain = explain;
            return this;
        }
    }
}
