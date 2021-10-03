using CoWorking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Repository
{
    public class WorkSpacesRepository : IWorkSpacesRepository
    {
       private readonly UserDBContext db;

        public WorkSpacesRepository(UserDBContext context)
        {
            db = context;
        }

        public WorkSpace AddWorkSpace(WorkSpace workSpace)
        {
            db.WorkSpace.Add(workSpace);
            db.SaveChanges();
            return workSpace;
        }

        public WorkSpace DeleteWorkSpace(int id)
        {
            WorkSpace workSpace = db.WorkSpace.Find(id);
            if (workSpace == null)
                return null;
            else
                db.WorkSpace.Remove(workSpace);
            db.SaveChanges();
            return workSpace;
        }

        public IQueryable<WorkSpace> GetAllPlaces() => db.WorkSpace.OrderBy(ws =>ws.Id);

        public WorkSpace GetWorkSpaceByID(int id)
        {
            return db.WorkSpace.Single(ws=>ws.Id == id);
        }

        public WorkSpace GetPlacesByIDCheck(int id)
        {
            var check = db.WorkSpace.Find(id);
            if (check == null)
            {
                return null;
            }
            else
            {
                return check;
            } 
        }
        public WorkSpace NameCheck(string workSpaceName)
        {
            var check = db.WorkSpace.FirstOrDefault(w => w.WorkSpaceName == workSpaceName);
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
