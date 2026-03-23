using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greeny.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime Date { get; set; } = DateTime.Now;

        // Relationships
        public string RecieverId { get; set; }
        public User Reciever { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }

    }
}
