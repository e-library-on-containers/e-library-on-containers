using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Business.RabitMQ
{
    public class BookReturnedEvent
    {
        public Guid id { get; set; }
        public DateTime createdAt { get; set; }
        public Guid userId { get; set; }
        public Guid rentalId { get; set; }
        public int bookInstanceId { get; set; }

    }
}
