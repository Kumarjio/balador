using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;
using TLSharp.Core;

namespace Michal.Balador.Telegram.Authentication
{

    public class BaladorSessionStore : ISessionStore
    {
        IBaladorContext _context;
       
        public BaladorSessionStore(IBaladorContext context)
        {
            _context = context;
        }
        public void Save(Session session)
        {
            
            using (var stream = new FileStream($"{session.SessionUserId}.dat", FileMode.OpenOrCreate))
            {
                var result = session.ToBytes();
                stream.Write(result, 0, result.Length);
            }
        }

        public Session Load(string sessionUserId)
        {
            var sessionFileName = $"{sessionUserId}.dat";
            if (!File.Exists(sessionFileName))
                return null;

            using (var stream = new FileStream(sessionFileName, FileMode.Open))
            {
                var buffer = new byte[2048];
                stream.Read(buffer, 0, 2048);

                return Session.FromBytes(buffer, this, sessionUserId);
            }
        }
    }

}