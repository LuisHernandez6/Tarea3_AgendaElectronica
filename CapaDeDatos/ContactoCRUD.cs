using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AgendaElectronicaTarea3.CapaDeDatos
{
    public class ContactoCRUD
    {
        private readonly string _connString;

        public ContactoCRUD(string connString)
        {
            _connString = connString;
        }

        public void Insertar(Contacto c)
        {
            using (var con = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(
                @"INSERT INTO Agenda (Nombre,Apellido,FechaNacimiento,Direccion,Genero,EstadoCivil,Movil,Telefono,CorreoElectronico)
                VALUES (@Nombre,@Apellido,@FechaNacimiento,@Direccion,@Genero,@EstadoCivil,@Movil,@Telefono,@CorreoElectronico)", con))
            {
                cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
                cmd.Parameters.AddWithValue("@FechaNacimiento", c.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Direccion", c.Direccion);
                cmd.Parameters.AddWithValue("@Genero", c.Genero);
                cmd.Parameters.AddWithValue("@EstadoCivil", c.EstadoCivil);
                cmd.Parameters.AddWithValue("@Movil", c.Movil);
                cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@CorreoElectronico", c.CorreoElectronico);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Contacto c)
        {
            using (var con = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(
                @"UPDATE Agenda 
                  SET Nombre = @Nombre,
                      Apellido = @Apellido,
                      FechaNacimiento = @FechaNacimiento,
                      Direccion = @Direccion,
                      Genero = @Genero,
                      EstadoCivil = @EstadoCivil,
                      Movil = @Movil,
                      Telefono = @Telefono,
                      CorreoElectronico = @CorreoElectronico
                  WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
                cmd.Parameters.AddWithValue("@FechaNacimiento", c.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Direccion", c.Direccion);
                cmd.Parameters.AddWithValue("@Genero", c.Genero);
                cmd.Parameters.AddWithValue("@EstadoCivil", c.EstadoCivil);
                cmd.Parameters.AddWithValue("@Movil", c.Movil);
                cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@CorreoElectronico", c.CorreoElectronico);
                cmd.Parameters.AddWithValue("@Id", c.Id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (var con = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("DELETE FROM Agenda WHERE Id=@Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Contacto BuscarPorId(int id)
        {
            using (var con = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("SELECT * FROM Agenda WHERE Id=@Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new Contacto
                        {
                            Id = (int)dr["Id"],
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            FechaNacimiento = (DateTime)dr["FechaNacimiento"],
                            Direccion = dr["Direccion"].ToString(),
                            Genero = dr["Genero"].ToString(),
                            EstadoCivil = dr["EstadoCivil"].ToString(),
                            Movil = dr["Movil"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            CorreoElectronico = dr["CorreoElectronico"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public List<Contacto> ObtenerTodos()
        {
            var contactos = new List<Contacto>();
            using (var con = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("SELECT * FROM Agenda", con))
            {
                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        contactos.Add(new Contacto
                        {
                            Id = (int)dr["Id"],
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            FechaNacimiento = (DateTime)dr["FechaNacimiento"],
                            Direccion = dr["Direccion"].ToString(),
                            Genero = dr["Genero"].ToString(),
                            EstadoCivil = dr["EstadoCivil"].ToString(),
                            Movil = dr["Movil"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            CorreoElectronico = dr["CorreoElectronico"].ToString()
                        });
                    }
                }
            }
            return contactos;
        }
    }
}
