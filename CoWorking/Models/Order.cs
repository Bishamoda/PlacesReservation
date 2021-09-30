using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Это поле обязательно")]
        public int WorkerId { get; set; }

        [Required(ErrorMessage = "Неверная дата")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Неверная дата")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Это поле обязательно")]
        public int WorkSpaceId { get; set; }
        public string DevicesId { get; set; }
        
    }
}
