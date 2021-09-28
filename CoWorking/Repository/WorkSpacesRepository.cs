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

        public IQueryable<WorkSpace> GetAllPlaces() => db.WorkSpace.OrderBy(w =>w.Id);


    }
}
