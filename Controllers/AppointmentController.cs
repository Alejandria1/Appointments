using Appointments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Appointments.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        
        private readonly ILogger<AppointmentController> _logger;
        private readonly AppointmentsDBContext _appointmentsDbContext;
        private IAvailableTimesDay _availableTimesDay;

        public AppointmentController(AppointmentsDBContext appointmentsDBContext, ILogger<AppointmentController> logger, IAvailableTimesDay availableTimesDay)
        {
            _appointmentsDbContext = appointmentsDBContext;
            _logger = logger;
            _availableTimesDay = availableTimesDay;
        }

        [HttpGet(Name = "GetAvailableTimes")]
        public List<TimeOnly> Get(DateOnly appointmentDate)
        {

            List<TimeOnly> apptmsCurrent = new List<TimeOnly>();

            List<TimeOnly> apptmsAvailable = new List<TimeOnly>();

            var appointments = _appointmentsDbContext.Appointments
                .FromSqlRaw($"SELECT appointment_id, appointment_date,appointment_time, is_new, employee_id, style_id from appointments where appointment_date like '{appointmentDate}'")
                .ToList(); //corregir a solo sacar los times
            appointments.ForEach(appointment => { apptmsCurrent.Add(appointment.appointment_time); });

            apptmsAvailable = _availableTimesDay.GetAvailableSlots(apptmsCurrent);
           //necesito sacar la lista de los times disponibles del dia, y quitar esos q ya estan ocupados

            
            return apptmsAvailable;
        }
    }
}