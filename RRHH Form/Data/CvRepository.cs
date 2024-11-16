using RRHH_Form.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class CvRepository
{
    private string connectionString = "Data Source=DESKTOP-PHBGA20;Initial Catalog=CurriculumsDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True"; // Cambia esto por tu cadena de conexión real

    // Crear (Insertar)
    public void AgregarCv(CurriculumVitae cv)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Cvs (Nombre, Phone, Email, Objetivo, Departamento, Titulo, Institucion, Desde, Hasta, Cargo, Entidad, Competencias, RLNombre, RLPhone, RPNombre, RPPhone, Foto) " +
                           "VALUES (@Nombre, @Phone, @Email, @Objetivo, @Departamento, @Titulo, @Institucion, @Desde, @Hasta, @Cargo, @Entidad, @Competencias, @RLNombre, @RLPhone, @RPNombre, @RPPhone, @Foto)";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Nombre", cv.Nombre);
            cmd.Parameters.AddWithValue("@Phone", cv.Phone);
            cmd.Parameters.AddWithValue("@Email", cv.Email);
            cmd.Parameters.AddWithValue("@Objetivo", cv.Objetivo);
            cmd.Parameters.AddWithValue("@Departamento", cv.Departamento);
            cmd.Parameters.AddWithValue("@Titulo", cv.Titulo);
            cmd.Parameters.AddWithValue("@Institucion", cv.Institucion);
            cmd.Parameters.AddWithValue("@Desde", cv.Desde);
            cmd.Parameters.AddWithValue("@Hasta", cv.Hasta);
            cmd.Parameters.AddWithValue("@Cargo", cv.Cargo);
            cmd.Parameters.AddWithValue("@Entidad", cv.Entidad);
            cmd.Parameters.AddWithValue("@Competencias", cv.Competencias);
            cmd.Parameters.AddWithValue("@RLNombre", cv.RLNombre);
            cmd.Parameters.AddWithValue("@RLPhone", cv.RLPhone);
            cmd.Parameters.AddWithValue("@RPNombre", cv.RPNombre);
            cmd.Parameters.AddWithValue("@RPPhone", cv.RPPhone);

            // Si el campo Foto es opcional, comprueba si tiene valor y asigna DBNull.Value si no lo tiene.
            if (!string.IsNullOrEmpty(cv.Foto))
            {
                cmd.Parameters.AddWithValue("@Foto", cv.Foto);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Foto", DBNull.Value);
            }

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    // Leer (Obtener todos los registros)
    public List<CurriculumVitae> ObtenerTodosCvs()
    {
        List<CurriculumVitae> cvs = new List<CurriculumVitae>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Cvs";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cvs.Add(new CurriculumVitae
                {
                    Nombre = reader["Nombre"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"].ToString(),
                    Objetivo = reader["Objetivo"].ToString(),
                    Departamento = reader["Departamento"].ToString(),
                    Titulo = reader["Titulo"].ToString(),
                    Institucion = reader["Institucion"].ToString(),
                    Desde = Convert.ToDateTime(reader["Desde"]),
                    Hasta = Convert.ToDateTime(reader["Hasta"]),
                    Cargo = reader["Cargo"].ToString(),
                    Entidad = reader["Entidad"].ToString(),
                    Competencias = reader["Competencias"].ToString(),
                    RLNombre = reader["RLNombre"].ToString(),
                    RLPhone = reader["RLPhone"].ToString(),
                    RPNombre = reader["RPNombre"].ToString(),
                    RPPhone = reader["RPPhone"].ToString(),
                    Foto = reader["Foto"] != DBNull.Value ? reader["Foto"].ToString() : null // Maneja nulos correctamente
                });
            }
        }
        return cvs;
    }

    // Actualizar (Modificar un registro)
    public void ActualizarCv(CurriculumVitae cv)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "UPDATE Cvs SET Phone = @Phone, Email = @Email, Objetivo = @Objetivo, Departamento = @Departamento, " +
                           "Titulo = @Titulo, Institucion = @Institucion, Desde = @Desde, Hasta = @Hasta, Cargo = @Cargo, Entidad = @Entidad, " +
                           "Competencias = @Competencias, RLNombre = @RLNombre, RLPhone = @RLPhone, RPNombre = @RPNombre, RPPhone = @RPPhone, Foto = @Foto " +
                           "WHERE Nombre = @Nombre";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Nombre", cv.Nombre);
            cmd.Parameters.AddWithValue("@Phone", cv.Phone);
            cmd.Parameters.AddWithValue("@Email", cv.Email);
            cmd.Parameters.AddWithValue("@Objetivo", cv.Objetivo);
            cmd.Parameters.AddWithValue("@Departamento", cv.Departamento);
            cmd.Parameters.AddWithValue("@Titulo", cv.Titulo);
            cmd.Parameters.AddWithValue("@Institucion", cv.Institucion);
            cmd.Parameters.AddWithValue("@Desde", cv.Desde);
            cmd.Parameters.AddWithValue("@Hasta", cv.Hasta);
            cmd.Parameters.AddWithValue("@Cargo", cv.Cargo);
            cmd.Parameters.AddWithValue("@Entidad", cv.Entidad);
            cmd.Parameters.AddWithValue("@Competencias", cv.Competencias);
            cmd.Parameters.AddWithValue("@RLNombre", cv.RLNombre);
            cmd.Parameters.AddWithValue("@RLPhone", cv.RLPhone);
            cmd.Parameters.AddWithValue("@RPNombre", cv.RPNombre);
            cmd.Parameters.AddWithValue("@RPPhone", cv.RPPhone);

            // Si el campo Foto es opcional, comprueba si tiene valor y asigna DBNull.Value si no lo tiene.
            if (!string.IsNullOrEmpty(cv.Foto))
            {
                cmd.Parameters.AddWithValue("@Foto", cv.Foto);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Foto", DBNull.Value);
            }

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    // Eliminar (Borrar un registro)
    public void EliminarCv(string nombre)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Cvs WHERE Nombre = @Nombre";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nombre", nombre);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
