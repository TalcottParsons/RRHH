using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RRHH
{
    public class ExperienciaProfesional
    {
        // Propiedades de la clase
        public int ExperienciaID { get; set; }
        public int ColaboradorID { get; set; }
        public string Empresa { get; set; }
        public string Puesto { get; set; }
        public int AñoInicio { get; set; }
        public int AñoFin { get; set; }

        // Método para agregar una experiencia profesional
        public void AgregarExperiencia()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "INSERT INTO ExperienciaProfesional (ColaboradorID, Empresa, Puesto, AñoInicio, AñoFin) " +
                               "VALUES (@ColaboradorID, @Empresa, @Puesto, @AñoInicio, @AñoFin)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ColaboradorID", ColaboradorID);
                comando.Parameters.AddWithValue("@Empresa", Empresa);
                comando.Parameters.AddWithValue("@Puesto", Puesto);
                comando.Parameters.AddWithValue("@AñoInicio", AñoInicio);
                comando.Parameters.AddWithValue("@AñoFin", AñoFin);
                comando.ExecuteNonQuery();
            }
        }

        // Método para actualizar una experiencia profesional
        public void ActualizarExperiencia()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "UPDATE ExperienciaProfesional SET Empresa = @Empresa, Puesto = @Puesto, " +
                               "AñoInicio = @AñoInicio, AñoFin = @AñoFin WHERE ExperienciaID = @ExperienciaID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ExperienciaID", ExperienciaID);
                comando.Parameters.AddWithValue("@Empresa", Empresa);
                comando.Parameters.AddWithValue("@Puesto", Puesto);
                comando.Parameters.AddWithValue("@AñoInicio", AñoInicio);
                comando.Parameters.AddWithValue("@AñoFin", AñoFin);
                comando.ExecuteNonQuery();
            }
        }

        // Método para eliminar una experiencia profesional
        public void EliminarExperiencia()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "DELETE FROM ExperienciaProfesional WHERE ExperienciaID = @ExperienciaID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ExperienciaID", ExperienciaID);
                comando.ExecuteNonQuery();
            }
        }

        // Método para obtener todas las experiencias de un colaborador
        public static List<ExperienciaProfesional> ObtenerExperiencias(int colaboradorID)
        {
            List<ExperienciaProfesional> listaExperiencias = new List<ExperienciaProfesional>();

            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "SELECT ExperienciaID, ColaboradorID, Empresa, Puesto, AñoInicio, AñoFin " +
                               "FROM ExperienciaProfesional WHERE ColaboradorID = @ColaboradorID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ColaboradorID", colaboradorID);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    listaExperiencias.Add(new ExperienciaProfesional
                    {
                        ExperienciaID = Convert.ToInt32(reader["ExperienciaID"]),
                        ColaboradorID = Convert.ToInt32(reader["ColaboradorID"]),
                        Empresa = reader["Empresa"].ToString(),
                        Puesto = reader["Puesto"].ToString(),
                        AñoInicio = Convert.ToInt32(reader["AñoInicio"]),
                        AñoFin = Convert.ToInt32(reader["AñoFin"])
                    });
                }
            }

            return listaExperiencias;
        }
    }
}
