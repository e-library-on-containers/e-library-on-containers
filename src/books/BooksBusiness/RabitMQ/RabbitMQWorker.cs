using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Books.Business.RabitMQ
{
    public class RabbitMQWorker : IHostedService
    {
        private IModel bookAdded;
        private IModel bookDeleted;
        private IModel bookUpdated;
        private IModel bookInstanceAdded;
        private IModel bookInstanceDeleted;
        private IConnection connection;
        private readonly IBookRepository<BookRead> _booksReadRepository;
        public RabbitMQWorker(IBookRepository<BookRead> booksReadRepository) 
        {
            _booksReadRepository= booksReadRepository;
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            connection = factory.CreateConnection();
            bookAdded = connection.CreateModel();
            bookDeleted = connection.CreateModel();
            bookUpdated = connection.CreateModel();
            bookInstanceAdded = connection.CreateModel();
            bookInstanceDeleted = connection.CreateModel();
            bookAdded.QueueDeclare("book_added", exclusive: false);
            bookUpdated.QueueDeclare("book_updated", exclusive: false);
            bookDeleted.QueueDeclare("book_deleted", exclusive: false);
            bookInstanceAdded.QueueDeclare("book_instance_added", exclusive: false);
            bookInstanceDeleted.QueueDeclare("book_instance_deleted", exclusive: false);
        }

        private void DoStuff()
        {
            var addBookConsumer = new EventingBasicConsumer(bookAdded);
            addBookConsumer.Received += async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Book book = JsonSerializer.Deserialize<Book>(message);
                BookRead bookRead = new BookRead(book);
                await _booksReadRepository.Create(bookRead);
            };

            var updateBookConsumer = new EventingBasicConsumer(bookUpdated);
            updateBookConsumer.Received += async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Book book = JsonSerializer.Deserialize<Book>(message);
                BookRead bookRead = new BookRead(book);
                await _booksReadRepository.Update(bookRead);
            };

            var deleteBookConsumer = new EventingBasicConsumer(bookDeleted);
            deleteBookConsumer.Received += async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var ISBN = Encoding.UTF8.GetString(body);
                await _booksReadRepository.Delete(ISBN);
            };

            var addBookInstanceConsumer = new EventingBasicConsumer(bookInstanceAdded);
            addBookInstanceConsumer.Received += async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var ISBN = Encoding.UTF8.GetString(body);
                BookRead bookRead = await _booksReadRepository.GetByISBN(ISBN);
                bookRead.CopiesCount += 1;
                await _booksReadRepository.Update(bookRead);
            };

            var deleteBookInstanceConsumer = new EventingBasicConsumer(bookInstanceDeleted);
            deleteBookInstanceConsumer.Received += async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var ISBN = Encoding.UTF8.GetString(body);
                BookRead bookRead = await _booksReadRepository.GetByISBN(ISBN);
                bookRead.CopiesCount -= 1;
                await _booksReadRepository.Update(bookRead);
            };

            bookAdded.BasicConsume(queue: "book_added", autoAck: true, consumer: addBookConsumer);
            bookUpdated.BasicConsume(queue: "book_updated", autoAck: true, consumer: updateBookConsumer);
            bookDeleted.BasicConsume(queue: "book_deleted", autoAck: true, consumer: deleteBookConsumer);
            bookInstanceAdded.BasicConsume(queue: "book_instance_added", autoAck: true, consumer: addBookInstanceConsumer);
            bookInstanceDeleted.BasicConsume(queue: "book_instance_deleted", autoAck: true, consumer: deleteBookInstanceConsumer);

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            DoStuff();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            bookAdded.Dispose();
            bookDeleted.Dispose();
            bookUpdated.Dispose();
            bookInstanceDeleted.Dispose();
            bookInstanceAdded.Dispose();
            return Task.CompletedTask;
        }
    }
}
