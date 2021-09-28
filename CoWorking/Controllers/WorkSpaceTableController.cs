using CoWorking.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Controllers
{
    public class WorkSpaceTableController : Controller
    {
        private readonly IWorkSpacesRepository _workSpacesRepository;

        public WorkSpaceTableController(IWorkSpacesRepository workSpacesRepository)
        {
            _workSpacesRepository = workSpacesRepository;

        }


        [Authorize]
        public IActionResult IndexWorkSpaces()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var model = _workSpacesRepository.GetAllPlaces();
                return View(model);
            }
        }
    }
}
