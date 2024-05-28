using Appointments.Models;
using Appointments.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace Appointments.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        
        private readonly ILogger<AppointmentController> _logger;
        private readonly Database _db;
        private IAvailableTimesDay _availableTimesDay;

        public AppointmentController(Database db, ILogger<AppointmentController> logger, IAvailableTimesDay availableTimesDay)
        {
            _db = db;
            _logger = logger;
            _availableTimesDay = availableTimesDay;
        }

        [HttpGet(Name = "GetDaySlots")]
        public IActionResult Get(String appointmentDate) 
        {
            using (IDbConnection dbConnection = _db.CreateConnection())
            {
                dbConnection.Open();
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "todaysAppointments";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@dayparam";
                    parameter.Value = appointmentDate;
                    parameter.DbType = DbType.String;
                    command.Parameters.Add(parameter);

                    using (var reader = command.ExecuteReader())
                    {
                        var results = new List<Appointment>();
                        while (reader.Read())
                        {
                            results.Add(new Appointment
                            {
                                appointment_id = reader.GetInt32(0),
                                appointment_time = (TimeSpan)reader.GetValue(1),
                                is_new = reader.GetBoolean(2),
                                style_name = reader.GetString(3),
                                emp_name = reader.GetString(4),
                            });
                        }

                        dbConnection.Close();

                        results = _availableTimesDay.GetFullDayAppointments(results);
                        return Ok(results);
                    }
                }
            }
        }
    }
}