using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoEnMemoria.Modelo;

namespace DemoEnMemoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadoDbContext _context;

        public EmpleadosController(EmpleadoDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Empleados> GetEmpleados()
        {
            return _context.Empleados.ToList();
        }
        [HttpGet("{id}")]
        public Empleados GetEmpleadoPorId(int id)
        {
            return _context.Empleados.SingleOrDefault(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var emp = _context.Empleados.SingleOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return NotFound("Empleado con id " + id + " no existe");
            }
            _context.Empleados.Remove(emp);
            _context.SaveChanges();
            return Ok("Empleado con id " + id + "borrado ");
        }

        [HttpPost]
        public IActionResult AdicionarEmpleado(Empleados empleado)
        {
            _context.Empleados.Add(empleado);
            _context.SaveChanges();
            return Created("api/empleados" + empleado.Id, empleado);

        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, Empleados empleado)
        {
            var emp = _context.Empleados.SingleOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return NotFound("Empleado con id " + id + " no existe");
            }
            
            if(empleado.nombre != null)
            {
                emp.nombre = empleado.nombre;
            }
            if (empleado.genero != null)
            {
                emp.genero = empleado.genero;
            }
            if (empleado.edad != 0)
            {
                emp.edad = empleado.edad;
            }
            if (empleado.salario != 0)
            {
                emp.salario = empleado.salario;
            }

            _context.Update(emp);
            _context.SaveChanges();

            return Ok("Empleado con id "+id +" actualizado exitosamente");

        }

    }
}
