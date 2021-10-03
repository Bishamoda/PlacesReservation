using CoWorking.Models;
using CoWorking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Service
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _workerRepository;
        public WorkerService(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }
        public IQueryable<Worker> GetAllWorkers() => _workerRepository.GetAllWorkers();
        public Worker GetWorkerByIDCheck(int id) => _workerRepository.GetWorkerByIDCheck(id);


    }
}
