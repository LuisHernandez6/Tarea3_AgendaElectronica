using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaElectronicaTarea3.CapaPresentacion
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inicializar base de datos

            string connectionString = "Server=localhost;Database=master;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Crear base de datos si no existe
                string crearBD = @"
                IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'bd_20231804_tarea3')
                BEGIN
                    CREATE DATABASE bd_20231804_tarea3;
                END";

                using (SqlCommand cmd = new SqlCommand(crearBD, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            // Conectar a la base de datos recien creada
            connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Crear tabla Agenda si no existe
                string crearTabla = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Agenda' AND xtype='U')
                CREATE TABLE [dbo].[Agenda] (
                    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    [Nombre] NVARCHAR(100) NOT NULL,
                    [Apellido] NVARCHAR(100) NULL,
                    [FechaNacimiento] DATE NULL,
                    [Direccion] NVARCHAR(200) NULL,
                    [Genero] NVARCHAR(20) NULL,
                    [EstadoCivil] NVARCHAR(20) NULL,
                    [Movil] NVARCHAR(20) NULL,
                    [Telefono] NVARCHAR(20) NULL,
                    [CorreoElectronico] NVARCHAR(100) NULL
                );";

                using (SqlCommand cmd = new SqlCommand(crearTabla, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            Application.Run(new AgendaForm());
        }
    }
}