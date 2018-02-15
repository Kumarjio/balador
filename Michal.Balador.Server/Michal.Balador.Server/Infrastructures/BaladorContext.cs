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

        public Task<T> GetConfiguration<T>(SenderMessages senderMessages, string id)
        {
            throw new NotImplementedException();
        }
        public bool GetFile(string id,out string path)
        {
            var defaultAccountFolder = HttpContext.Current.Server.MapPath("~/AccountsFolder");
            var filename = id+ ".txt";
            path = Path.Combine(defaultAccountFolder, filename);
            return File.Exists(path);
        }
        public async Task<ResponseBase>  SetConfiguration<T>(SenderMessages senderMessages, string id, T config)
        {
            var key = DataSecurity.GetHash(senderMessages.ServiceName);// senderMessages.ServiceName.GetHashCode();
            Dictionary<string, string> account = null;
            var configData = JsonConvert.SerializeObject(config);
            string pat;
            if (GetFile(id,out pat))
            {
                var accountConfig = File.ReadAllText(pat);
                 account = JsonConvert.DeserializeObject<Dictionary<string, string>>(accountConfig);
                if (account.ContainsKey(key))
                    account[key] = configData;
                else
                    account.Add(key, configData);
            }
            else
            {
                account = new Dictionary<string, string>();
                account.Add(key, configData);
            }
            var jsonData=JsonConvert.SerializeObject(account);
            File.WriteAllText(pat, jsonData);
            _logger.Log(System.Diagnostics.TraceLevel.Info, config.ToString());

            return await Task.FromResult<ResponseBase>(new ResponseBase { IsError = false, Message = "" });

        }

        public  Task<T> GetContact<T>(SenderMessages senderMessages, string id)
        {
            return null;
            //throw new NotImplementedException();
        }

        public Task<ResponseBase> NotifySenderMessage(SenderMessages senderMessages, string id, string message)
        {
            //send to email adress of sender!!! 
            throw new NotImplementedException();
        }

        public Task<ResponseBase> SetContact<T>(SenderMessages senderMessages, T contact)
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
