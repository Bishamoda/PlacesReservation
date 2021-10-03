using CoWorking.Models;
using CoWorking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Service
{
    public class WorkSpacesService : IWorkSpacesService
    {
        private readonly IWorkSpacesRepository _workSpacesRepository;

        public WorkSpacesService(IWorkSpacesRepository workSpacesRepository)
        {
            _workSpacesRepository = workSpacesRepository;
        }

        public WorkSpace AddWorkSpace(WorkSpace workSpace) => _workSpacesRepository.AddWorkSpace(workSpace);

        public WorkSpace DeleteWorkSpace(int id) => _workSpacesRepository.DeleteWorkSpace(id);

        public IQueryable<WorkSpace> GetAllPlaces() => _workSpacesRepository.GetAllPlaces();

        public WorkSpace GetPlacesByIDCheck(int id) => _workSpacesRepository.GetPlacesByIDCheck(id);

        public WorkSpace GetWorkSpaceByID(int id) => _workSpacesRepository.GetWorkSpaceByID(id);

        public WorkSpace NameCheck(string workSpaceName) => _workSpacesRepository.NameCheck(workSpaceName);


    }
}
