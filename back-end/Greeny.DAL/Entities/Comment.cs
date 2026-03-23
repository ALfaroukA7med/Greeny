using Greeny.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greeny.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Votes { get; set; } = 0;

        public DateTime Date {  get; set; } = DateTime.Now;

        //Relationships

        public int UserId { get; set; }
        public User User { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }


    }
}
