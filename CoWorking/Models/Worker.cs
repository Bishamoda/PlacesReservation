using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Models
{
    public class Worker
    {
        public int WorkerID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
