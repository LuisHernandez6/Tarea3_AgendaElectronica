using System;

namespace AgendaElectronicaTarea3.CapaDeDatos
{
    public class Contacto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Genero { get; set; }
        public string EstadoCivil { get; set; }
        public string Movil { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
