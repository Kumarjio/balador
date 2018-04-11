using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
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
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.Infrastructures.Utils;
using Michal.Balador.Server.Models;
using Michal.Balador.Server.ViewModel;

namespace Michal.Balador.Server.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class SendToClientController : ApiController
    {
        [Import]
        private IUnitOfWork _unitOfWork;

        [Import]
        private IMessageRepository _messageRepository;
        [HttpPost]
        public async Task<HttpResponseMessage> Post(ClientMessageRequest request)
        {
            Response<string> responseResult = new Response<string>();
            try
            {
                var id = Guid.NewGuid();
                var user = await Utils.GetUserIdAsync(User);
                if (user == null)
                    throw new ArgumentNullException("no found any user");
                using (_messageRepository)
                {
                    var res = await _messageRepository.CreateMessage(
                    new MessageRequest
                    {
                        ClientId = request.ClientId,
                        Messsage = request.Messsage,
                        MesssageType = request.MesssageType,
                        NickName = request.NickName,
                        User = user
                    });
                }
                    

                //using (_unitOfWork)
                //{
                //    var dt=DateTime.UtcNow;
                //    List<object> parameters = new List<object>();
                //    var query = "exec [dbo].[createMessage] @message,@user,@clientid,@messageType,@nickName,@replay ";
                //    parameters.Add(new SqlParameter("@message", request.Messsage));
                //    parameters.Add(new SqlParameter("@user", user));
                //    parameters.Add(new SqlParameter("@clientid", request.ClientId));
                //    parameters.Add(new SqlParameter("@messageType", request.MesssageType));
                //    parameters.Add(new SqlParameter("@nickName", request.NickName));
                //    parameters.Add(new SqlParameter("@replay", true));

                //    var resultSp = await _unitOfWork.Database.SqlQuery<object>(query, parameters.ToArray()).FirstOrDefaultAsync();
                //}

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
