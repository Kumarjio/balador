using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core;

namespace Tele.My
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new FileSessionStore();
            var asaf = "972528356489";
            var m = "972543143131";
            var hash = "54a578ab2c70e02c22";
            int apiId = 121845;
            string apiHash = "888c93f37abef8dde1b7a1cf40580ebb";
            var client = new TelegramClient(apiId, apiHash);
            client.ConnectAsync().Wait();
            var estaRegistrado = client.IsPhoneRegisteredAsync("972546252491").Result;
            if (estaRegistrado)
            {
                if (!client.IsUserAuthorized())
                {
                    {
                        hash = client.SendCodeRequestAsync("972546252491").Result;
                        // var code = "63811"; // you can change code in debugger
                        Console.WriteLine("Codigo recibido:");
                        var strCodigo = Console.ReadLine();
                        var user = client.MakeAuthAsync("972546252491", hash, strCodigo).Result;
                    }
                }
                //send message
                //get available contacts
                var result = client.GetContactsAsync().Result;
                //find recipient in contacts
                var userd = result.Users
                    .Where(x => x.GetType() == typeof(TLUser))
                    .Cast<TLUser>()
                    .FirstOrDefault(x => x.Phone == m);



                client.SendMessageAsync(new TLInputPeerUser() { UserId = userd.Id }, "test").Wait();

            }
        }

    }
}
