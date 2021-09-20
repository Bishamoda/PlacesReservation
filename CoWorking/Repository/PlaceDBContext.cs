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
    public class PlaceDBContext : IdentityDbContext<IdentityUser>
    {
        public PlaceDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Worker> Worker { get; set; }
    }
}
