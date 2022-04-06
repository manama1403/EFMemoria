using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DemoEnMemoria.Modelo
{
    public class EmpleadoDbContext : DbContext
    {
        public EmpleadoDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Empleados>Empleados {get;set;}

    }
}
