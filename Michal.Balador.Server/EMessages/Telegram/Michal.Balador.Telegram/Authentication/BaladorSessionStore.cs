using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.Telegram.Config;
using TLSharp.Core;

namespace Michal.Balador.Telegram.Authentication
{

    public class BaladorSessionStore : ISessionStore
    {
        IBaladorContext _context;
        ConfigTelegram _configTelegram; IAppMessangerFactrory _provider; SignUpSender _signUpSender;
        public BaladorSessionStore(IBaladorContext context, ConfigTelegram configTelegram, IAppMessangerFactrory provider, SignUpSender signUpSender)
        {
            _context = context; _configTelegram = configTelegram;  _provider= provider; _signUpSender = signUpSender;
        }
        public void Save(Session session)
        {
            _configTelegram.Session = Convert.ToBase64String(session.ToBytes());
           var res= _context.SetConfiguration( _provider.ServiceName, _signUpSender.UserName, _configTelegram).Result;
            
        }

        public Session Load(string sessionUserId)
        {
            ConfigTelegram configTelegram= _context.GetConfiguration<ConfigTelegram>(_provider.ServiceName, _signUpSender.UserName).Result;
            return null;
        //   configTelegram.Session.from
            //var sessionFileName = $"{sessionUserId}.dat";
            //if (!File.Exists(sessionFileName))
            //    return null;

            //using (var stream = new FileStream(sessionFileName, FileMode.Open))
            //{
            //    var buffer = new byte[2048];
            //    stream.Read(buffer, 0, 2048);

            //    return Session.FromBytes(buffer, this, sessionUserId);
            //}

        }
    }

}