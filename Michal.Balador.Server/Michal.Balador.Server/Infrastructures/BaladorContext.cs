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

        public async Task<T> GetConfiguration<T>(SenderMessages senderMessages, string id)
        {
            string pat;
            var key = DataSecurity.GetHash(senderMessages.ServiceName);
            Dictionary<string, string> account = null;
      
            if (GetFile(id, out pat))
            {
                var dataProtected = DataSecurity.GetDataProtector();
                account = GetAccountConfig(pat);
                if (account.ContainsKey(key))
                {
                   var data= account[key];
                   var unProtectedData=UTF8Encoding.UTF8.GetString( dataProtected.Unprotect(Convert.FromBase64String(data)));
                   return JsonConvert.DeserializeObject<T>(unProtectedData);
                }
            }
            return await Task.FromResult(default(T));
        }

        Dictionary<string, string> GetAccountConfig(string pat)
        {
            var accountConfig = File.ReadAllText(pat);
            Dictionary<string, string>  account = JsonConvert.DeserializeObject<Dictionary<string, string>>(accountConfig);
            return account;
        }

        bool GetFile(string id,out string path)
        {
            var defaultAccountFolder = System.Configuration.ConfigurationManager.AppSettings["f"].ToString();// HttpContext.Current.Server.MapPath("~/AccountsFolder");
            var filename = id+ ".txt";
            path = Path.Combine(defaultAccountFolder, filename);
            return File.Exists(path);
        }

        public async Task<ResponseBase>  SetConfiguration<T>(SenderMessages senderMessages, string id, T config)
        {
            var key = DataSecurity.GetHash(senderMessages.ServiceName);
            Dictionary<string, string> account = null;
            var configData = JsonConvert.SerializeObject(config);
            var dataProtected= DataSecurity.GetDataProtector();
            var protectedData = Convert.ToBase64String(dataProtected.Protect(UTF8Encoding.UTF8.GetBytes(configData)));

            string pat;
            if (GetFile(id,out pat))
            {
                account = GetAccountConfig(pat);
                if (account.ContainsKey(key))
                    account[key] = protectedData;
                else
                    account.Add(key, protectedData);
            }
            else
            {
                account = new Dictionary<string, string>();
                account.Add(key, protectedData);
            }
            var jsonData=JsonConvert.SerializeObject(account);
            File.WriteAllText(pat, jsonData);
            _logger.Log(System.Diagnostics.TraceLevel.Info, config.ToString());

            return await Task.FromResult<ResponseBase>(new ResponseBase { IsError = false, Message = "" });

        }

        public  Task<T> GetContact<T>(SenderMessages senderMessages, string id)
        {
            return null;
        }

        public Task<ResponseBase> NotifySenderMessage(SenderMessages senderMessages, string id, string message)
        {
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
