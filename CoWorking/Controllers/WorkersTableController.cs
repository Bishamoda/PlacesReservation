using CoWorking.Service;
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
        private readonly IWorkerService _workerService;

        public WorkersTableController(IWorkerService workerService)
        {
            _workerService = workerService;
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
                var model = _workerService.GetAllWorkers();
                return View(model);
            }
        }
    }

}
