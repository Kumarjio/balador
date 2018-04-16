using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;
using WhatsAppApi;

namespace Michal.Balador.NSWhatsApp.ConcreteSender
{
    public class SenderWhatsAppMessagesFactory
    {
        IBaladorContext _context;
        public SenderWhatsAppMessagesFactory(IBaladorContext context)
        {
            _context = context;
        }
        public async Task<Response<WhatsApp>> ConnectAndLogin(string phoneSender, string pws,string nickName)
        {
            var tcs = new TaskCompletionSource<Response<WhatsApp>>();
            try
            {
                WhatsApp whatsApp = new WhatsApp(phoneSender, pws, nickName);
                whatsApp.Connect();
               // mockSend.SetSocketClient()
                whatsApp.OnLoginSuccess += (a, b) =>
                {
                    Console.WriteLine("data ok={0}", a);
                    //string m = "test me...";
                    //whatsApp.SendData(Encoding.ASCII.GetBytes(m));
                    tcs.SetResult(new Response<WhatsApp> { IsError = false, Result = whatsApp });
                };
                whatsApp.OnLoginFailed += (a) =>
                {

                    Console.WriteLine("data error={0}", a);
                    tcs.SetResult(new Response<WhatsApp> { IsError = true, Message = a });
                };
                byte[] nextChallenge = Convert.FromBase64String(phoneSender);
                whatsApp.Connect();
                whatsApp.Login(nextChallenge);
            }
            catch (Exception ee)
            {
                tcs.SetException(ee);
            }

            return await tcs.Task;
        }
    }
}
