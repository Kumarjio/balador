//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Michal.Balador.Contracts.Dal;
//using Michal.Balador.Contracts.DataModel;

//namespace Michal.Balador.Contracts.Behaviors
//{
//    public abstract class PostContactLoadBehavior: Behavior
//    {
//        public PostContactLoadBehavior(IBaladorContext baladorContext) : base(baladorContext)
//        {

//        }

//        public override Task<ResponseBase> Excute<TRequest>(TRequest request, IUnitOfWork unitOfWork)
//        {
//            return Task.FromResult<ResponseBase>(new ResponseBase { });
//        }
//        public abstract Task AddMessage(RequestContactLoaderBehavior request);
//    }
//}
