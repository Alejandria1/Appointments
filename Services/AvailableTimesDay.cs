using Appointments.Models;
using Google.Protobuf.WellKnownTypes;
using System.Collections.Generic;

namespace Appointments.Services
{

    public class AvailableTimesDay : IAvailableTimesDay
    {

        public List<TimeSpan> GetAllSlots()
        {
            var settings = Settings.Instance;
            List<TimeSpan> availableSlots = new List<TimeSpan>();


            TimeSpan i = settings.DefaultStartTime;

            while (i < settings.DefaultFinishTime)
            {
                availableSlots.Add(i);
                if (i == settings.DefaultLunchTime)
                {
                    i = i.Add(settings.LunchDuration);
                }
                else
                {
                    i = i.Add(settings.AppointmentDuration);
                }
            }
            return availableSlots;

        }

        public List<Appointment> GetFullDayAppointments(List<Appointment> dbresults)
        {
            var settings = Settings.Instance;
            List<TimeSpan> allSlots = GetAllSlots();
            var fullDayApptms = new List<Appointment>();

            if (dbresults.Count > 0)
            {
                foreach (var slot in allSlots)
                {
                    foreach (var appointment in dbresults)
                    {
                        if (appointment.appointment_time == slot)
                        {
                            fullDayApptms.Add(appointment);
                        }
                        else
                        {
                            fullDayApptms.Add(new Appointment
                            {
                                appointment_id = 0,
                                appointment_time = slot,
                                style_name = slot == settings.DefaultLunchTime ? "LunchTime" : "free"
                            });
                        }
                    }


                }
            }
            else
            {
                foreach (var slot in allSlots)
                {
                    fullDayApptms.Add(new Appointment
                    {
                        appointment_id = 0,
                        appointment_time = slot,
                        style_name = slot == settings.DefaultLunchTime ? "LunchTime" : "free"
                    });
                }
            }

            return fullDayApptms;

        }
    }
}