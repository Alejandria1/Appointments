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
            AppointmentDuration = TimeSpan.FromMinutes(120);
            DefaultStartTime = new TimeOnly(9, 0);
            DefaultLunchTime = new TimeOnly(15, 0);
            DefaultFinishTime = new TimeOnly(20, 0);
        }

        // Propiedades de configuración.
        public TimeSpan AppointmentDuration { get; set; }
        public TimeOnly DefaultStartTime { get; set; }
        public TimeOnly DefaultLunchTime { get; set; }
        public TimeOnly DefaultFinishTime { get; set; }
    }
}
