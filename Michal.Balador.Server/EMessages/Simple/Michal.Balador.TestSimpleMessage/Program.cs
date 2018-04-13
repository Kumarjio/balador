using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lior.api.Models;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.SimpleMessage;

namespace Michal.Balador.TestSimpleMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                AccountSend accountInfo = new AccountSend
                {
                    JobId = Guid.Parse("70117B21-CA0C-44C0-9C5D-3E340BBC3452"),
                    UserId = "1bfa6f8e-0000-0000-0000-b573b2c8f820",
                    Messassnger = "com.baladorPlant$MockHttpSender"
                };
                List<object> parameters = new List<object>();
                var query = "exec [dbo].[balador_sp_getContacts] @jobid,@messassnger,@accountid ; ";
                parameters.Add(new SqlParameter("@jobid", accountInfo.JobId));
                parameters.Add(new SqlParameter("@messassnger", accountInfo.Messassnger));
                parameters.Add(new SqlParameter("@accountid", accountInfo.UserId));
                if (db.Database.Connection != null)
                {
                 //   db.Database.ExecuteSqlCommand(query, parameters.ToArray());
                    db.Database.SqlQuery<object>(query, parameters.ToArray()).ToList();
                }
            }
            SocketClientTest test = new SocketClientTest("l", "1");
            test.Connect();

            test.OnLoginSuccess+= (a, b) =>
             {
                 Console.WriteLine("data ok={0}",a);
                 string m = "test me...";
                 test.SendData(Encoding.ASCII.GetBytes(m));

             };
            test.OnLoginFailed += (a) =>
             {
                 Console.WriteLine("data error={0}", a);

             };
            test.Login(true);
            Console.ReadKey();

            // string m = "OK";
            //test.SendData(Encoding.ASCII.GetBytes(m));
            ////test.SendData(new byte[0]);
            ////System.Threading.Thread.Sleep(100);
   
            //test.pollMessage();
            //m = "FAILED";
            //test.SendData(Encoding.ASCII.GetBytes(m));
            //test.pollMessage();
          
        }
    }
}
