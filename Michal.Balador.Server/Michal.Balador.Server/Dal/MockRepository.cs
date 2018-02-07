﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Server.Dal
{
    public class DataSendersMock
    {
        public List<RegisterSender> Senders { get; set; }
    }

   

   

    public class MockRepository
    {
        List<SendRequest> sendRequests;

        List<SignUpSender> signUpSenders;
        public MockRepository()
        {
            mocks = new DataSendersMock();
            mocks.Senders = new List<RegisterSender>();

            signUpSenders = new List<SignUpSender>();

            mocks.Senders.Add(new RegisterSender { Id = "rt", Pws = "111" });
           // mocks.Senders.Add(new RegisterSender { Id = "2", Pws = "222" });
          //  mocks.Senders.Add(new RegisterSender { Id = "3", Pws = "333" });

            sendRequests = new List<SendRequest>();
            var se1 = new SendRequest { Id = "rt", Messages = new List<MessageItem>()};
            var se2 = new SendRequest { Id = "2", Messages = new List<MessageItem>() };
            var se3 = new SendRequest { Id = "3", Messages = new List<MessageItem>() };
          
            se1.Messages.Add(new MessageItem { Id = "a", Message = "x" });
            se1.Messages.Add(new MessageItem { Id = "aa", Message = "xx" });
            //se1.Messages.Add(new MessageItem { Id = "aaa", Message = "xxx" });

            se2.Messages.Add(new MessageItem { Id = "b", Message = "q" });
            se2.Messages.Add(new MessageItem { Id = "bb", Message = "qq" });
        //    se2.Messages.Add(new MessageItem { Id = "bbb", Message = "qqq" });

            se3.Messages.Add(new MessageItem { Id = "c", Message = "m" });
            se3.Messages.Add(new MessageItem { Id = "cc", Message = "mm" });
            se3.Messages.Add(new MessageItem { Id = "ccc", Message = "mmm" });
            sendRequests.Add(se1);
            sendRequests.Add(se2);
            sendRequests.Add(se3);
        }

        public async Task<SendRequest> FindMessagesById(string id)
        {
            return await Task.FromResult(sendRequests.Where(s => s.Id == id).FirstOrDefault());

        }

        public DataSendersMock mocks { get; private set;}
    }
}