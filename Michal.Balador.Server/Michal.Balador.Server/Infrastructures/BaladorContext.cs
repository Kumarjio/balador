using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Mechanism;
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

        public async Task<T> GetConfiguration<T>(string serviceName, string id)
        {
            string pat;
            var key = DataSecurity.GetHash(serviceName);
            Dictionary<string, string> account = null;

            if (GetConfigFile(id, out pat))
            {
                //var dataProtected = DataSecurity.GetDataProtector();
                account = await GetDataConfig(pat);
                if (account.ContainsKey(key))
                {
                    var data = account[key];
                    return DeserializeObject<T>(data);
                    //var unProtectedData = UTF8Encoding.UTF8.GetString(dataProtected.Unprotect(Convert.FromBase64String(data)));
                    //return JsonConvert.DeserializeObject<T>(unProtectedData);
                }
            }
            return await Task.FromResult(default(T));
        }

        T DeserializeObject<T>(string data)
        {
         ////   #if DEBUG
          //  return JsonConvert.DeserializeObject<T>(data);
           // #else
                 var dataProtected = DataSecurity.GetDataProtector(System.Configuration.ConfigurationManager.AppSettings["k"].ToString());
                 var unProtectedData = UTF8Encoding.UTF8.GetString(dataProtected.Unprotect(Convert.FromBase64String(data)));
                 return JsonConvert.DeserializeObject<T>(unProtectedData);
         //   #endif
            }

        string GetprotectedData(string data)
        {
           // #if DEBUG
          //        return data;
          //  #else
                var dataProtected = DataSecurity.GetDataProtector(System.Configuration.ConfigurationManager.AppSettings["k"].ToString());
                var protectedData = Convert.ToBase64String(dataProtected.Protect(UTF8Encoding.UTF8.GetBytes(data)));
                 return protectedData;
         //   #endif
        }

        async Task<Dictionary<string, string>> GetDataConfig(string pat)
        {
            using (StreamReader reader = File.OpenText(pat))
            {
                var  config = await reader.ReadToEndAsync();
                Dictionary<string, string> account = JsonConvert.DeserializeObject<Dictionary<string, string>>(config);
                return account;
            }
        }

        bool GetConfigFile(string id,out string path)
        {
            var defaultAccountFolder = System.Configuration.ConfigurationManager.AppSettings["f"].ToString();// HttpContext.Current.Server.MapPath("~/AccountsFolder");
            var filename = id+ ".txt";
            path = Path.Combine(defaultAccountFolder, filename);
            return File.Exists(path);
        }

        bool GetContactFile(string id, out string path)
        {
            var defaultAccountFolder = System.Configuration.ConfigurationManager.AppSettings["c"].ToString();// HttpContext.Current.Server.MapPath("~/AccountsFolder");
            var filename = id + ".txt";
            path = Path.Combine(defaultAccountFolder, filename);
            return File.Exists(path);
        }

        public async Task<ResponseBase>  SetConfiguration<T>(string serviceName, string id, T config)
        {
            var key = DataSecurity.GetHash(serviceName);
            Dictionary<string, string> account = null;
            var configData = JsonConvert.SerializeObject(config);
            // var dataProtected= DataSecurity.GetDataProtector();
            // var protectedData = Convert.ToBase64String(dataProtected.Protect(UTF8Encoding.UTF8.GetBytes(configData)));
            var protectedData = GetprotectedData(configData);

            string pat;
            if (GetConfigFile(id,out pat))
            {
                account =await GetDataConfig(pat);
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
            using (StreamWriter outputFile = new StreamWriter(pat))
            {
                await outputFile.WriteAsync(jsonData);
            }
            _logger.Log(System.Diagnostics.TraceLevel.Info, config.ToString());
            return await Task.FromResult<ResponseBase>(new ResponseBase { IsError = false, Message = "" });

        }

        public async Task<T> GetContact<T>(string serviceName, string id)
        {
            string pat;
            var key = DataSecurity.GetHash(serviceName);
            Dictionary<string, string> contact = null;

            if (GetContactFile(id, out pat))
            {
               // var dataProtected = DataSecurity.GetDataProtector();
                contact = await GetDataConfig(pat);
                if (contact.ContainsKey(key))
                {
                    var data = contact[key];
                     return DeserializeObject<T>(data);
                    //var unProtectedData = UTF8Encoding.UTF8.GetString(dataProtected.Unprotect(Convert.FromBase64String(data)));
                    //return JsonConvert.DeserializeObject<T>(unProtectedData);
                }
            }
            return await Task.FromResult(default(T));
        }

        public Task<ResponseBase> NotifySenderMessage(string serviceName, string id, string message)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseBase> SetContact<T>(string serviceName, string id, T contact)
        {
            var key = DataSecurity.GetHash(serviceName);
            Dictionary<string, string> contactData = null;
            var configContactData = JsonConvert.SerializeObject(contact);
          //  var dataProtected = DataSecurity.GetDataProtector();
            //var protectedData = Convert.ToBase64String(dataProtected.Protect(UTF8Encoding.UTF8.GetBytes(configContactData)));
            var protectedData = GetprotectedData(configContactData);
            string pat;
            if (GetContactFile(id, out pat))
            {
                contactData = await GetDataConfig(pat);
                if (contactData.ContainsKey(key))
                    contactData[key] = protectedData;
                else
                    contactData.Add(key, protectedData);
            }
            else
            {
                contactData = new Dictionary<string, string>();
                contactData.Add(key, protectedData);
            }
            var jsonData = JsonConvert.SerializeObject(contactData);
            using (StreamWriter outputFile = new StreamWriter(pat))
            {
                await outputFile.WriteAsync(jsonData);
            }
            _logger.Log(System.Diagnostics.TraceLevel.Info, contact.ToString());
            return await Task.FromResult<ResponseBase>(new ResponseBase { IsError = false, Message = "" });
        }

        public IBaladorLogger GetLogger()
        {
            return _logger;
        }
    }
}
