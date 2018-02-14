using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using Michal.Balador.Infrastructures.Security;

namespace Michal.Balador.Infrastructures.Service
{
   [Export(typeof(IBaladorContext))]
   [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BaladorContext : IBaladorContext
    {
        public const string KeyServiceNameHash = "baladorService";
        public BaladorContext()
        {
            Log = 5;
        }
        public int Log { get; set; }

        [Import(typeof(IBaladorLogger))]
        IBaladorLogger _logger;

        public Task<T> GetConfiguration<T>(SenderMessages senderMessages)
        {
            throw new NotImplementedException();
        }
        public bool GetFile(SenderMessages senderMessages,out string path)
        {
            var defaultAccountFolder = HttpContext.Current.Server.MapPath("~/AccountsFolder");
            var filename = DataSecurity.GetHash(KeyServiceNameHash) + ".txt";
            path = Path.Combine(defaultAccountFolder, filename);
            return File.Exists(path);
        }
        public async Task<ResponseBase>  SetConfiguration<T>(SenderMessages senderMessages, T config)
        {
            var key = senderMessages.ServiceName.GetHashCode();
            Dictionary<int, string> account = null;
            var configData = JsonConvert.SerializeObject(config);
            string pat;
            if (GetFile(senderMessages,out pat))
            {
                var accountConfig = File.ReadAllText(pat);
                 account = JsonConvert.DeserializeObject<Dictionary<int, string>>(accountConfig);
                if (account.ContainsKey(key))
                    account[key] = configData;
                else
                    account.Add(key, configData);
            }
            else
            {
                account = new Dictionary<int, string>();
                account.Add(key, configData);
            }
            var jsonData=JsonConvert.SerializeObject(account);
            File.WriteAllText(pat, jsonData);
            _logger.Log(System.Diagnostics.TraceLevel.Info, config.ToString());

            return await Task.FromResult<ResponseBase>(new ResponseBase { IsError = false, Message = "" });

        }

        public  Task<object> GetContact(SenderMessages senderMessages, string id)
        {
            return null;
            //throw new NotImplementedException();
        }

        public Task<ResponseBase> NotifySenderMessage(SenderMessages senderMessages, string id, string message)
        {
            //send to email adress of sender!!! 
            throw new NotImplementedException();
        }

        public Task<ResponseBase> SetContact(SenderMessages senderMessages, object contact)
        {
            return null;
            //throw new NotImplementedException();
        }

        public IBaladorLogger GetLogger()
        {
            return _logger;
        }
    }
}
