using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.RabitMQ
{
    internal class BookRentedEvent
    {
        public Guid id { get; set; }
        public DateTime createdAt { get; set; }
        public Guid userId { get; set; }
        public Guid rentalId { get; set; }
        public int bookInstanceId { get; set; }
        public int forHowManyDays { get; set; }
    }
}
