using System;

namespace Appointments
{
    public sealed class Settings
    {
        // Campo estático y de solo lectura que contiene la única instancia de la clase.
        private static readonly Lazy<Settings> instance = new Lazy<Settings>(() => new Settings());

        // Propiedad pública que permite acceder a la instancia única.
        public static Settings Instance
        {
            get
            {
                return instance.Value;
            }
        }

        // Constructor privado para evitar la instanciación directa.
        private Settings()
        {
            // Inicializar configuraciones predeterminadas aquí.
            LoadDefaultSettings();
        }

        // Método para cargar configuraciones predeterminadas.
        private void LoadDefaultSettings()
        {
            AppointmentDuration = new TimeSpan(2, 0, 0);
            LunchDuration = new TimeSpan(1, 0, 0);
            DefaultStartTime = new TimeSpan(9, 0, 0);
            DefaultLunchTime = new TimeSpan(15, 0, 0);
            DefaultFinishTime = new TimeSpan(20, 0, 0);
        }

        // Propiedades de configuración.
        public TimeSpan AppointmentDuration { get; set; }
        public TimeSpan DefaultStartTime { get; set; }
        public TimeSpan DefaultLunchTime { get; set; }
        public TimeSpan DefaultFinishTime { get; set; }
        public TimeSpan LunchDuration { get; set; }
    }
}
