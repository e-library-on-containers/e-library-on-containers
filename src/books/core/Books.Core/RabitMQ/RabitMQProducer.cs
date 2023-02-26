using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Books.Infrastructure.Models;
using Microsoft.Extensions.Configuration;

namespace Books.Business.RabitMQ
{
    public class RabitMQProducer:IRabitMQProducer
    {
        IModel channel;
        private readonly IConfiguration configuration;
        public RabitMQProducer(IConfiguration configuration)
        {
            this.configuration = configuration;
            var factory = new ConnectionFactory();
            configuration.GetSection("RabbitMq").Bind(factory);
            var connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
            channel.QueueDeclare("book_added", exclusive: false);
            channel.QueueDeclare("book_updated", exclusive: false);
            channel.QueueDeclare("book_deleted", exclusive: false);
            channel.QueueDeclare("book_instance_added", exclusive: false);
            channel.QueueDeclare("book_instance_deleted", exclusive: false);
        }

        public void SendBookAddedMessage(BookRead book)
        {
            var json = JsonConvert.SerializeObject(book);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "book_added", body: body);
        }

        public void SendBookDeletedMessage(string ISBN)
        {
            channel.BasicPublish(exchange: "", routingKey: "book_deleted", body: Encoding.UTF8.GetBytes(ISBN));
        }

        public void SendBookInstanceAddedMessage(string ISBN)
        {
            channel.BasicPublish(exchange: "", routingKey: "book_instance_added", body: Encoding.UTF8.GetBytes(ISBN));
        }

        public void SendBookInstanceDeletedMessage(string ISBN)
        {
            channel.BasicPublish(exchange: "", routingKey: "book_instance_deleted", body: Encoding.UTF8.GetBytes(ISBN));
        }

        public void SendBookUpdatedMessage(BookRead book)
        {
            var json = JsonConvert.SerializeObject(book);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "book_updated", body: body);
        }
    }
}
