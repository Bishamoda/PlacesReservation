using CoWorking.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Controllers
{
    public class WorkersTableController : Controller
    {
        private readonly IWorkerRepository _workerRepository;

        public WorkersTableController(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        [Authorize]
        public IActionResult IndexWorkers()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var model = _workerRepository.GetAllWorkers();
                return View(model);
            }
        }
    }

}
