using Appointments.Models;
using Appointments.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly Database _db;

        public EmployeesController(Database db, ILogger<EmployeesController> logger)
        {
            _db = db;
            _logger = logger;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult GetEmployees()
        {
            using (IDbConnection dbConnection = _db.CreateConnection())
            {
                dbConnection.Open();
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select employee_id, emp_name, emp_last_name from employees where is_active=true;";

                    using (var reader = command.ExecuteReader())
                    {
                        List<Employee> active_employees = new List<Employee>();

                        while (reader.Read())
                        {
                            
                            active_employees.Add(new Employee { 
                                employee_id = reader.GetInt32(0),
                                emp_name = reader.GetString(1),
                                emp_last_name = reader.GetString(2),
                            });
                        }

                        dbConnection.Close();

                        return Ok(active_employees);
                    }
                }
            }
           
        }

        //// GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
