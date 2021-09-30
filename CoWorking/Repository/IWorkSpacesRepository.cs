using CoWorking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Repository
{
    public interface IWorkSpacesRepository
    {
        public IQueryable<WorkSpace> GetAllPlaces();
        public WorkSpace GetPlacesByIDCheck(int id);
        public WorkSpace DeleteWorkSpace(int id);
        public WorkSpace GetWorkSpaceByID(int id);
        public WorkSpace AddWorkSpace(WorkSpace workSpace);
        public WorkSpace NameCheck(string workSpaceNAme);
    }
}
