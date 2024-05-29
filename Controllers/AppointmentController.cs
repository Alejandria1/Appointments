using Appointments.Models;
using Appointments.Services;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Globalization;

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
        
        [HttpGet(Name = "GetDayAppointment")]
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

        [HttpPost(Name = "newAppointment")]
        public IActionResult NewAppointment(Appointment appointment)
        {
            try
            {
                string temp = appointment.appointment_date.ToString("yyyy-MM-dd");
                DateTime apmtDate = DateTime.ParseExact(temp, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                

                using (IDbConnection dbConnection = _db.CreateConnection())
                {
                    dbConnection.Open();
                    using (var command = dbConnection.CreateCommand())
                    {
                        command.CommandText = @"INSERT INTO APPOINTMENTS (appointment_date, appointment_time, is_new, customer, phone, employee_id, style_id) VALUES (@date, @time, @new, @customer, @phone, @employee_id, @style_id)";

                        var paramDate = command.CreateParameter();
                        paramDate.ParameterName = ("@date");
                        paramDate.Value = apmtDate;
                        command.Parameters.Add(paramDate);

                        var paramTime = command.CreateParameter();
                        paramTime.ParameterName = "@time";
                        paramTime.Value = appointment.appointment_time;
                        command.Parameters.Add(paramTime);

                        var paramNew = command.CreateParameter();
                        paramNew.ParameterName = "@new";
                        paramNew.Value = appointment.is_new;
                        command.Parameters.Add(paramNew);

                        var paramCustomer = command.CreateParameter();
                        paramCustomer.ParameterName = "@customer";
                        paramCustomer.Value = appointment.customer;
                        command.Parameters.Add(paramCustomer);

                        var paramPhone = command.CreateParameter();
                        paramPhone.ParameterName = "@phone";
                        paramPhone.Value = appointment.phone;
                        command.Parameters.Add(paramPhone);

                        var paramEmployeeId = command.CreateParameter();
                        paramEmployeeId.ParameterName = "@employee_id";
                        paramEmployeeId.Value = appointment.employee_id;
                        command.Parameters.Add(paramEmployeeId);

                        var paramStyleId = command.CreateParameter();
                        paramStyleId.ParameterName = "@style_id";
                        paramStyleId.Value = appointment.style_id;
                        command.Parameters.Add(paramStyleId);

                        command.ExecuteNonQuery();

                        dbConnection.Close();
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error" + ex.Message);
                return StatusCode(500, "The appointment could not be created");
            }
            
        }



    }
}