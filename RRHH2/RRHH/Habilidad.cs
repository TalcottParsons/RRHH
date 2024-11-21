using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RRHH
{
    public class Habilidad
    {
        // Propiedades de la clase Habilidad
        public int HabilidadID { get; set; }
        public int ColaboradorID { get; set; }
        public string HabilidadNombre { get; set; } // Cambiado a HabilidadNombre para claridad

        // Método para agregar una nueva habilidad
        public void AgregarHabilidad()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "INSERT INTO Habilidades (ColaboradorID, Habilidad) VALUES (@ColaboradorID, @Habilidad)";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@ColaboradorID", ColaboradorID);
                cmd.Parameters.AddWithValue("@Habilidad", HabilidadNombre);

                cmd.ExecuteNonQuery();
            }
        }

        // Método para actualizar una habilidad existente
        public void ActualizarHabilidad()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "UPDATE Habilidades SET Habilidad = @Habilidad WHERE HabilidadID = @HabilidadID";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@HabilidadID", HabilidadID);
                cmd.Parameters.AddWithValue("@Habilidad", HabilidadNombre);

                cmd.ExecuteNonQuery();
            }
        }

        // Método para eliminar una habilidad
        public void EliminarHabilidad()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "DELETE FROM Habilidades WHERE HabilidadID = @HabilidadID";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@HabilidadID", HabilidadID);

                cmd.ExecuteNonQuery();
            }
        }

        // Método para obtener las habilidades de un colaborador
        public static DataTable ObtenerHabilidades(int colaboradorID)
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "SELECT HabilidadID, Habilidad FROM Habilidades WHERE ColaboradorID = @ColaboradorID";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@ColaboradorID", colaboradorID);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
        }
    }
}
