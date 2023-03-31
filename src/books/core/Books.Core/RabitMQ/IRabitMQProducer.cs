using Books.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.RabitMQ
{
    public interface IRabitMQProducer
    {
        public void SendBookAddedMessage(BookRead book);
        public void SendBookUpdatedMessage(BookRead book);
        public void SendBookDeletedMessage(string ISBN);
        public void SendBookInstanceAddedMessage(string ISBN);
        public void SendBookInstanceDeletedMessage(string ISBN);

    }
}
