using CoWorking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Service
{
    public interface IWorkerService
    {
        public IQueryable<Worker> GetAllWorkers();
        public Worker GetWorkerByIDCheck(int id);
    }
}
