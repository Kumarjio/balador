using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
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
                var user= await Utils.GetUserIdAsync(User);
                if (user == null)
                    throw new ArgumentNullException("no found any user");
                   var id = Guid.NewGuid();
                using (ApplicationDbContext context=new ApplicationDbContext())
                {
                    var dt=DateTime.UtcNow;

                    var userModel=context.Users.Where(d => d.UserName == user).FirstOrDefault();
                    if (userModel == null)
                        throw new ArgumentNullException($"no found user {user}");
                    context.ClientMessages.Add(new ClientMessage { Id=Guid.NewGuid(),
                        ClientId =request.ClientId,
                        Messsage =request.Messsage,
                        AccountId =userModel.Id,
                        CreatedOn=dt,
                        ModifiedOn= dt
                    });
                    await context.SaveChangesAsync();

                }
                
                responseResult.Message = "add to queue";
                responseResult.Result = id.ToString();
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
