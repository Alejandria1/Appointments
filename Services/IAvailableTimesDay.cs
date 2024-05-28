using Appointments.Models;

namespace Appointments.Services
{
    public interface IAvailableTimesDay
    {
        public List<TimeSpan> GetAllSlots();
        public List<Appointment> GetFullDayAppointments(List<Appointment> dbresults);
    }
}
