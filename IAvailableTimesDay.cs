namespace Appointments
{
    public interface IAvailableTimesDay
    {
        public List<TimeOnly> GetAvailableSlots(List<TimeOnly>? busySlots);
    }
}
