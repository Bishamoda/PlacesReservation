using CoWorking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorking.Repository
{
    public class UserDBContext : DbContext
    {

        public UserDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<WorkSpace> WorkSpace{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Device> Devices { get; set; }


    }
}
