using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
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
        private IModel bookReturned;
        private IModel bookRented;
        private IConnection connection;
        private string rentQueue;
        private string returnQueue;
        private readonly IBookRepository<BookRead> _booksReadRepository;
        private readonly IBookInstancesRepository<BookInstance> _booksInstancesRepository;
        private readonly IConfiguration configuration;
        public RabbitMQWorker(IBookRepository<BookRead> booksReadRepository, IBookInstancesRepository<BookInstance> bookInstancesRepository, IConfiguration configuration) 
        {
            this.configuration = configuration;
            _booksInstancesRepository= bookInstancesRepository;
            _booksReadRepository= booksReadRepository;
            var factory = new ConnectionFactory();
            configuration.GetSection("RabbitMq").Bind(factory);
            connection = factory.CreateConnection();
            bookAdded = connection.CreateModel();
            bookDeleted = connection.CreateModel();
            bookUpdated = connection.CreateModel();
            bookInstanceAdded = connection.CreateModel();
            bookInstanceDeleted = connection.CreateModel();
            bookRented = connection.CreateModel();
            bookReturned = connection.CreateModel();
            bookAdded.QueueDeclare("book_added", exclusive: false);
            bookUpdated.QueueDeclare("book_updated", exclusive: false);
            bookDeleted.QueueDeclare("book_deleted", exclusive: false);
            bookInstanceAdded.QueueDeclare("book_instance_added", exclusive: false);
            bookInstanceDeleted.QueueDeclare("book_instance_deleted", exclusive: false);
            //bookRented.ExchangeDeclare(exchange: "rent", type: "fanout");
            rentQueue = bookRented.QueueDeclare().QueueName;
            bookRented.QueueBind(queue: rentQueue,
                              exchange: "rentals",
                              routingKey: "rental.rent");
            bookReturned.ExchangeDeclare(exchange: "return", type: "fanout");
            returnQueue = bookReturned.QueueDeclare().QueueName;
            bookReturned.QueueBind(queue: returnQueue,
                              exchange: "retals",
                              routingKey: "rental.return");
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

            var rentBookInstanceConsumer = new EventingBasicConsumer(bookRented);
            rentBookInstanceConsumer.Received += async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                BookRentedEvent bookRentedEvent = JsonSerializer.Deserialize<BookRentedEvent>(message);
                BookInstance bookInstance = await _booksInstancesRepository.GetById(bookRentedEvent.bookInstanceId);
                bookInstance.IsAvailable = false;
                await _booksInstancesRepository.Update(bookInstance);
            };

            var returnBookInstanceConsumer = new EventingBasicConsumer(bookReturned);
            returnBookInstanceConsumer.Received += async (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                BookReturnedEvent bookReturnedEvent = JsonSerializer.Deserialize<BookReturnedEvent>(message);
                BookInstance bookInstance = await _booksInstancesRepository.GetById(bookReturnedEvent.bookInstanceId);
                bookInstance.IsAvailable = true;
                await _booksInstancesRepository.Update(bookInstance);
            };

            bookAdded.BasicConsume(queue: "book_added", autoAck: true, consumer: addBookConsumer);
            bookUpdated.BasicConsume(queue: "book_updated", autoAck: true, consumer: updateBookConsumer);
            bookDeleted.BasicConsume(queue: "book_deleted", autoAck: true, consumer: deleteBookConsumer);
            bookInstanceAdded.BasicConsume(queue: "book_instance_added", autoAck: true, consumer: addBookInstanceConsumer);
            bookInstanceDeleted.BasicConsume(queue: "book_instance_deleted", autoAck: true, consumer: deleteBookInstanceConsumer);
            bookRented.BasicConsume(queue:rentQueue, autoAck: true, consumer: rentBookInstanceConsumer);
            bookReturned.BasicConsume(queue: returnQueue, autoAck: true, consumer: returnBookInstanceConsumer);

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
