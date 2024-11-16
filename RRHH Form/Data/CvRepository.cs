using RRHH_Form.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class CvRepository
{
    private string connectionString = "Data Source=DESKTOP-PHBGA20;Initial Catalog=CurriculumsDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True"; // Change to your actual connection string

    // Create (Insert)
    public void AgregarCv(CurriculumVitae cv)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Cvs (Nombre, Phone, Email, Objetivo, Departamento, Titulo, Institucion, Desde, Hasta, Cargo, Entidad, Competencias, RLNombre, RLPhone, RPNombre, RPPhone, Foto) " +
                           "VALUES (@Nombre, @Phone, @Email, @Objetivo, @Departamento, @Titulo, @Institucion, @Desde, @Hasta, @Cargo, @Entidad, @Competencias, @RLNombre, @RLPhone, @RPNombre, @RPPhone, @Foto)";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.NVarChar).Value = cv.Nombre;
            cmd.Parameters.Add("@Phone", System.Data.SqlDbType.NVarChar).Value = cv.Phone;
            cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = cv.Email;
            cmd.Parameters.Add("@Objetivo", System.Data.SqlDbType.NVarChar).Value = cv.Objetivo;
            cmd.Parameters.Add("@Departamento", System.Data.SqlDbType.NVarChar).Value = cv.Departamento;
            cmd.Parameters.Add("@Titulo", System.Data.SqlDbType.NVarChar).Value = cv.Titulo;
            cmd.Parameters.Add("@Institucion", System.Data.SqlDbType.NVarChar).Value = cv.Institucion;
            cmd.Parameters.Add("@Desde", System.Data.SqlDbType.Date).Value = cv.Desde;
            cmd.Parameters.Add("@Hasta", System.Data.SqlDbType.Date).Value = cv.Hasta;
            cmd.Parameters.Add("@Cargo", System.Data.SqlDbType.NVarChar).Value = cv.Cargo;
            cmd.Parameters.Add("@Entidad", System.Data.SqlDbType.NVarChar).Value = cv.Entidad;
            cmd.Parameters.Add("@Competencias", System.Data.SqlDbType.NVarChar).Value = cv.Competencias;
            cmd.Parameters.Add("@RLNombre", System.Data.SqlDbType.NVarChar).Value = cv.RLNombre;
            cmd.Parameters.Add("@RLPhone", System.Data.SqlDbType.NVarChar).Value = cv.RLPhone;
            cmd.Parameters.Add("@RPNombre", System.Data.SqlDbType.NVarChar).Value = cv.RPNombre;
            cmd.Parameters.Add("@RPPhone", System.Data.SqlDbType.NVarChar).Value = cv.RPPhone;

            // Handle Foto
            cmd.Parameters.Add("@Foto", System.Data.SqlDbType.NVarChar).Value = string.IsNullOrEmpty(cv.Foto) ? (object)DBNull.Value : cv.Foto;

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    // Read (Get all records)
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
                    Id = Convert.ToInt32(reader["Id"]),
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
                    Foto = reader["Foto"] != DBNull.Value ? reader["Foto"].ToString() : null // Handle nulls correctly
                });
            }
        }
        return cvs;
    }

    // Update (Modify a record)
    public void ActualizarCv(CurriculumVitae cv)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "UPDATE Cvs SET Nombre = @Nombre, Phone = @Phone, Email = @Email, Objetivo = @Objetivo, Departamento = @Departamento, " +
                           "Titulo = @Titulo, Institucion = @Institucion, Desde = @Desde, Hasta = @Hasta, Cargo = @Cargo, Entidad = @Entidad, " +
                           "Competencias = @Competencias, RLNombre = @RLNombre, RLPhone = @RLPhone, RPNombre = @RPNombre, RPPhone = @RPPhone, Foto = @Foto " +
                           "WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = cv.Id;
            cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.NVarChar).Value = cv.Nombre;
            cmd.Parameters.Add("@Phone", System.Data.SqlDbType.NVarChar).Value = cv.Phone;
            cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = cv.Email;
            cmd.Parameters.Add("@Objetivo", System.Data.SqlDbType.NVarChar).Value = cv.Objetivo;
            cmd.Parameters.Add("@Departamento", System.Data.SqlDbType.NVarChar).Value = cv.Departamento;
            cmd.Parameters.Add("@Titulo", System.Data.SqlDbType.NVarChar).Value = cv.Titulo;
            cmd.Parameters.Add("@Institucion", System.Data.SqlDbType.NVarChar).Value = cv.Institucion;
            cmd.Parameters.Add("@Desde", System.Data.SqlDbType.Date).Value = cv.Desde;
            cmd.Parameters.Add("@Hasta", System.Data.SqlDbType.Date).Value = cv.Hasta;
            cmd.Parameters.Add("@Cargo", System.Data.SqlDbType.NVarChar).Value = cv.Cargo;
            cmd.Parameters.Add("@Entidad", System.Data.SqlDbType.NVarChar).Value = cv.Entidad;
            cmd.Parameters.Add("@Competencias", System.Data.SqlDbType.NVarChar).Value = cv.Competencias;
            cmd.Parameters.Add("@RLNombre", System.Data.SqlDbType.NVarChar).Value = cv.RLNombre;
            cmd.Parameters.Add("@RLPhone", System.Data.SqlDbType.NVarChar).Value = cv.RLPhone;
            cmd.Parameters.Add("@RPNombre", System.Data.SqlDbType.NVarChar).Value = cv.RPNombre;
            cmd.Parameters.Add("@RPPhone", System.Data.SqlDbType.NVarChar).Value = cv.RPPhone;
            cmd.Parameters.Add("@Foto", System.Data.SqlDbType.NVarChar).Value = string.IsNullOrEmpty(cv.Foto) ? (object)DBNull.Value : cv.Foto;

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    // Delete (Remove a record)
    public void EliminarCv(int id)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Cvs WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
