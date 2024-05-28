using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Appointments.Models
{
    [Table("APPOINTMENTS")]
    public class Appointment
    {
        [Key]
        [Column("appointment_id")]
        public int appointment_id { get; set; }
        [Column("appointment_date")]
        public DateOnly appointment_date { get; set; }
        [Column("appointment_time")]
        public TimeOnly appointment_time { get; set; }
        [Column("is_new")]
        public bool is_new { get; set; }
        [Column("employee_id")]
        public int employee_id { get; set; }
        [Column("style_id")]
        public int style_id { get; set; }
    }
}
