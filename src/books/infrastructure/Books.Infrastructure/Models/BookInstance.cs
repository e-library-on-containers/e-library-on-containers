using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Models
{
    public class BookInstance
    {
        public int InstanceId { get; set; }
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }
    }
}
