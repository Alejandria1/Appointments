﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Appointments.Models
{

    public class Appointment
    {
        public int appointment_id { get; set; }

        public DateOnly appointment_date { get; set; }
        public TimeSpan appointment_time { get; set; }
        public bool is_new { get; set; }
        public string style_name { get; set; }
        public string emp_name { get; set; }

        public string customer { get; set;}
        public string phone { get; set;}
        public int employee_id { get; set; }
        public int style_id { get; set; }
    
    }
}
