using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.Infrastructures.Dal;

namespace Michal.Balador.Server.Dal
{
    [Export(typeof(IMessageRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MessageRepository : Repository, IMessageRepository

    {
        [ImportingConstructor()]
        public MessageRepository(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase> CreateMessage(MessageRequest request)
        {

            using (_unitOfWork)
            {
                var dt = DateTime.UtcNow;
                List<object> parameters = new List<object>();
                var query = "exec [dbo].[createMessage] @message,@user,@clientid,@messageType,@nickName,@replay ";
                parameters.Add(new SqlParameter("@message", request.Messsage));
                parameters.Add(new SqlParameter("@user", request.User));
                parameters.Add(new SqlParameter("@clientid", request.ClientId));
                parameters.Add(new SqlParameter("@messageType", request.MesssageType));
                parameters.Add(new SqlParameter("@nickName", request.NickName));
                parameters.Add(new SqlParameter("@replay", true));

                var resultSp = await _unitOfWork.Database.SqlQuery<object>(query, parameters.ToArray()).FirstOrDefaultAsync();
            }

            return await Task.FromResult(new ResponseBase());

        }

    }
}