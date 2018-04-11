using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Michal.Balador.Contracts.Behaviors;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.Contracts.Service;
using Michal.Balador.Contracts.Util;

namespace Michal.Balador.Infrastructures.Mechanism
{
    [Export(typeof(ITaskSendsScheduler))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TaskSendsScheduler : ITaskSendsScheduler
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(TaskSendsScheduler));
        [ImportMany(typeof(IAppMessangerFactrory))]
        private IEnumerable<Lazy<IAppMessangerFactrory, IDictionary<string, object>>> _senderRules;

        ITaskService _taskService;
        [ImportingConstructor()]
        public TaskSendsScheduler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<ConcurrentBag<ResponseSend>> Run(BehaviorItems<Behavior> behaviors = null)
        {
            ConcurrentBag<ResponseSend> resultError = new ConcurrentBag<ResponseSend>();

            try
            {
                var tasks_job = await _taskService.TaskSchedulerRepository.GetAccountsJob();
                //var mockdata=await _taskService.
                foreach (var task_job in tasks_job)
                {
                    var messaggerShrotName=task_job.MessagesType.GetMessaggerShrotName();
                    Lazy<IAppMessangerFactrory> _utah = _senderRules.Where(s => (string)s.Metadata["MessageType"] == messaggerShrotName).FirstOrDefault();

                    var doStuffBlock = new ActionBlock<RegisterSender>(async rs =>
                    {
                        rs.Log = Thread.CurrentThread.ManagedThreadId;
                        var sender = await _utah.Value.GetInstance(rs);
                        try
                        {
                            //if (!sender.IsError)
                            //{
                            //    var requestToSend = await mockData.FindMessagesById(rs.Id);

                            //    if (requestToSend != null)
                            //    {
                                    
                            //        requestToSend.Log = rs.Log;
                            //        var responseToSendWait = await sender.Result.Send(requestToSend);
                            //        resultError.Add(responseToSendWait);
                            //        Log.Info(responseToSendWait.ToString());
                            //        await manager.NotifyAsync(rs.Id, notifications, null);
                            //    }
                            //}
                            //else
                            //{
                            //    var mes = rs.Log + " " + rs.Id + " " + sender.Message;
                            //    resultError.Add(new ResponseSend { IsError = true, Message = mes });
                            //    Log.Error(mes);
                            //}
                        }
                        catch (Exception ee)
                        {
                            resultError.Add(new ResponseSend { IsError = true, Message = "unhandle" });
                            Log.Error(rs.Log + " " + rs.Id + " " + ee);
                        }
                        finally
                        {
                            if (sender != null && sender.Result != null)
                                sender.Result.Dispose();
                        }
                    });

                    //foreach (var item in mockData.mocks.Senders)
                    //{
                    //    doStuffBlock.Post(item);
                    //}
                    doStuffBlock.Complete();
                    await doStuffBlock.Completion;


                }
            }
            catch (Exception e)
            {

                //resultError.IsError = true;
             //   resultError.Message = e.Message;
            }
            return resultError;
            

        }
    }
}
