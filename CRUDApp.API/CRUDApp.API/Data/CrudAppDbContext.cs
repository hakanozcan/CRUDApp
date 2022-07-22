using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDApp.API.Data
{
    public class CrudAppDbContext:DbContext
    {
        public CrudAppDbContext(DbContextOptions options):base(options)
        {

            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
