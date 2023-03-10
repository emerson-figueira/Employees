using System.Collections.Generic;
using EFCoreCodeFirstSample.Models;
using EFCoreCodeFirstSample.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCodeFirstSample.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDataRepository<Employee> _dataRepository;
        public EmployeeController(IDataRepository<Employee> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/Employee
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Employee> employees = _dataRepository.GetAll();
            return Ok(employees);
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            Employee employee = _dataRepository.Get(id);
            if (employee == null)
            {
                return NotFound("Empregado não encontrado.");
            }
            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Empregado não informado.");
            }
            _dataRepository.Add(employee);
            return CreatedAtRoute(
                  "Get",
                  new { Id = employee.EmployeeId },
                  employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Empregado não informado.");
            }
            Employee employeeToUpdate = _dataRepository.Get(id);
            if (employeeToUpdate == null)
            {
                return NotFound("Nenhum empregado encontrado.");
            }
            _dataRepository.Update(employeeToUpdate, employee);
            return NoContent();
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Employee employee = _dataRepository.Get(id);
            if (employee == null)
            {
                return NotFound("Nenhum empregado encontrado.");
            }
            _dataRepository.Delete(employee);
            return NoContent();
        }
    }
}
