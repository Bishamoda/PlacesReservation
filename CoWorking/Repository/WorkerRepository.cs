using CoWorking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Repository
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly UserDBContext db;

        public WorkerRepository(UserDBContext context)
        {
            db = context;
        }

        public IQueryable<Worker> GetAllWorkers() => db.Worker.OrderBy(w => w.WorkerID);

        public Worker GetWorkerByIDCheck(int id)
        {
            var check = db.Worker.Find(id);
            if (check == null)
            {
                return null;
            }
            else
            {
                return check;
            }
        }

    }
}
