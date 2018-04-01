using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using lior.api.Helper;
using lior.api.Models;
using Michal.Balador.Contracts.DataModel;
using Michal.Balador.Infrastructures.Utils;
using Michal.Balador.Server.Models;
using Michal.Balador.Server.ViewModel;

namespace Michal.Balador.Server.Controllers
{
    [Authorize]
    //[RoutePrefix("api/balador/SendToClient")]
    public class SendToClientController : ApiController
    {
        [HttpPost]

        public async Task<HttpResponseMessage> Post(ClientMessageRequest request)
        {
            Response<string> responseResult = new Response<string>();
            try
            {
                var id = Guid.NewGuid();
                var user= await Utils.GetUserIdAsync(User);
                if (user == null)
                    throw new ArgumentNullException("no found any user");
                  
                using (ApplicationDbContext context=new ApplicationDbContext())
                {
                    var dt=DateTime.UtcNow;
                    List<object> parameters = new List<object>();
                    var query = "exec [dbo].[createMessage] @message,@user,@clientid,@messageType,@nickName,@replay ";
                    parameters.Add(new SqlParameter("@message", request.Messsage));
                    parameters.Add(new SqlParameter("@user", user));
                    parameters.Add(new SqlParameter("@clientid", request.ClientId));
                    parameters.Add(new SqlParameter("@messageType", request.MesssageType));
                    parameters.Add(new SqlParameter("@nickName", request.NickName));
                    parameters.Add(new SqlParameter("@replay", true));



                    var resultSp = await context.Database.SqlQuery<object>(query, parameters.ToArray()).FirstOrDefaultAsync();
                    
                    //context.ClientMessages.Add(new ClientMessage { Id= id,
                    //    ClientId =request.ContactId,
                    //    Messsage =request.Messsage,
                    //    AccountId =userModel.Id,
                    //    MesssageType= request.MesssageType,
                    //    Status= General.MessageStatus.Pending,
                    //    CreatedOn =dt,
                    //    ModifiedOn= dt,
                    //    ConversationId= id
                    //});
                   // await context.SaveChangesAsync();

                }
                
                responseResult.Message = "add to queue";
                responseResult.Result = "";// resultSp.ToString();
            }
            catch (Exception eee)
            {
                responseResult.IsError = true;
                responseResult.Message = eee.Message;
            }
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<ResponseBase>(responseResult,
                        new JsonMediaTypeFormatter(),
                         new MediaTypeWithQualityHeaderValue("application/json"))
            };
            return response;
        }
    }
}
