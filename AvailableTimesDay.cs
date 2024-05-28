using Appointments.Models;

namespace Appointments
{

    public class AvailableTimesDay: IAvailableTimesDay
    {
//        public DateOnly Date { get; set; }

        //public List<TimeOnly> AvailableTimes { get; set; }

        public List<TimeOnly> GetAvailableSlots(List<TimeOnly>? busySlots)
        {
            var settings = Settings.Instance;
            List<TimeOnly> availableSlots = new List<TimeOnly>();
           
            
            for (TimeOnly i = settings.DefaultStartTime; i < settings.DefaultLunchTime;  i = i.AddMinutes(120))
            {
                if (busySlots.Count != 0)
                {
                    foreach (var item in busySlots)
                    {
                        if (item != i)
                        {
                            availableSlots.Add(i);
                        }
                    }
                }
                else
                {
                    availableSlots.Add(i);
                }
   
            }


            return availableSlots;
           
        }
    }
}