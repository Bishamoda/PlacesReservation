using CoWorking.Models;
using CoWorking.Service;
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
        private readonly IWorkSpacesService _workSpacesService;

        public WorkSpaceTableController(IWorkSpacesService workSpacesService)
        {
            _workSpacesService = workSpacesService;

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
                if (User.Identity.Name == "admin")
                {
                    var model = _workSpacesService.GetAllPlaces();
                    return View(model);
                }
                else
                {
                    return RedirectToAction("IndexWorkSpacesUser", "DeviceTableController");
                }

            }
        }
        [Authorize]
        public IActionResult IndexWorkSpacesUser()
        {

            var model = _workSpacesService.GetAllPlaces();
            return View(model);

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
            var dublicateworkSpace = _workSpacesService.NameCheck(WorkSpaceName);
            if (dublicateworkSpace == null)
            {
                _workSpacesService.AddWorkSpace(newWorkSpace);
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
            _workSpacesService.DeleteWorkSpace(id);
            return RedirectToAction("IndexWorkSpaces", "WorkSpaceTable");
        }
    }
}
