using CoWorking.Models;
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

        [Authorize]
        [HttpGet]
        public IActionResult AddWorkSpace(string ErrorMes)
        {

            ViewData["alarm"] = ErrorMes;
            return View("AddWorkSpace", new WorkSpace());
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddWorkSpace(WorkSpace newWorkSpace, string WorkSpaceName)
        {
            var dublicateworkSpace = _workSpacesRepository.NameCheck(WorkSpaceName);
            if (dublicateworkSpace == null)
            {
                _workSpacesRepository.AddWorkSpace(newWorkSpace);
            }
            else
            {
                string ErrorMes = "There is already such a place!";
                return RedirectToAction("AddWorkSpace", "WorkSpaceTable", new { ErrorMes });
            }

            return RedirectToAction("IndexWorkSpaces", "WorkSpaceTable");
        }

        [Authorize]
        [HttpPost]
        public IActionResult WorkSpaceDelete(int id)
        {
            _workSpacesRepository.DeleteWorkSpace(id);
            return RedirectToAction("IndexWorkSpaces", "WorkSpaceTable");
        }
    }
}
