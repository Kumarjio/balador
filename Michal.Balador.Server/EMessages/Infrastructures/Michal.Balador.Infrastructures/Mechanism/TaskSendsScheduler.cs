﻿using System;
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

        public async Task<ConcurrentBag<ResponseSend>> Run()
        {
            ConcurrentBag<ResponseSend> resultError = new ConcurrentBag<ResponseSend>();
            Guid jobid = Guid.Empty;
            try
            {
                var doStuffBlock = new ActionBlock<AccountSend>(async rs =>
                {
                    Lazy<IAppMessangerFactrory> _utah = _senderRules.Where(s => (string)s.Metadata[ConstVariable.MESSAGE_TYPE] == rs.MessaggerShrotName).FirstOrDefault();

                    rs.ManagedThreadId = Thread.CurrentThread.ManagedThreadId;
                    if (_utah == null || _utah.Value == null)
                        return;
                    var sender = await _utah.Value.GetAppMessanger(rs);
                    try
                    {
                        if (!sender.IsError && sender.IsAutorize)
                        {
                            var requestToSend = await sender.Result.SendAsync(rs);
                        }
                        else
                        {
                            var mes = rs.ManagedThreadId + " " + rs.Id + " " + sender.Message;
                            resultError.Add(new ResponseSend { IsError = true, Message = mes });
                            Log.Error(mes);
                        }
                    }
                    catch (Exception ee)
                    {
                        resultError.Add(new ResponseSend { IsError = true, Message = "unhandle" });
                        Log.Error(rs.ManagedThreadId + " " + rs.Id + " " + ee);
                    }
                    finally
                    {
                        if (sender != null && sender.Result != null)
                            sender.Result.Dispose();
                    }
                });
                var tasks_job = await _taskService.TaskSchedulerRepository.GetAccountsJob();

                List<AccountSend> accountsenders = new List<AccountSend>();

                foreach (var task_job in tasks_job)
                {
                    if (String.IsNullOrEmpty(task_job.Id))
                        continue;
                    if (jobid == Guid.Empty)
                        jobid = task_job.JobId;

                    var messaggerShrotName = "";

                    var messangers = task_job.MessagesType.Split(',');
                    foreach (var messassnger in messangers)
                    {
                        messaggerShrotName = messassnger.GetMessaggerShrotName();
                        accountsenders.Add(new AccountSend
                        {
                            Email = task_job.Email,
                            Id = task_job.Id,
                            MessagesType = task_job.MessagesType,
                            Messassnger = messassnger,
                            MessaggerShrotName = messaggerShrotName,
                            Name = task_job.Name,
                            UserName = task_job.UserName,
                            JobId = task_job.JobId
                        });

                    }
                }



                foreach (var item in accountsenders)
                {
                    doStuffBlock.Post(item);
                }
                doStuffBlock.Complete();
                await doStuffBlock.Completion;

                await _taskService.TaskSchedulerRepository.Complete(jobid);

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
