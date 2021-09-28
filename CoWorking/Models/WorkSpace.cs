using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Models
{
    public class WorkSpace
    {
        
        public int Id { get; set; }
        public string WorkSpaceName { get; set; }
        
    }
}
