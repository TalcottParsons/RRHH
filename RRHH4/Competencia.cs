using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RRHH
{
    public class Competencia
    {
        public int CompetenciaID { get; set; }
        public int ColaboradorID { get; set; }
        public string CompetenciaNombre { get; set; }
        public string Dominio { get; set; }

        // Método para agregar una nueva competencia
        public void AgregarCompetencia()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "INSERT INTO Competencias (ColaboradorID, Competencia, Dominio) VALUES (@ColaboradorID, @Competencia, @Dominio)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ColaboradorID", ColaboradorID);
                comando.Parameters.AddWithValue("@Competencia", CompetenciaNombre);
                comando.Parameters.AddWithValue("@Dominio", Dominio);
                comando.ExecuteNonQuery();
            }
        }

        // Método para actualizar una competencia existente
        public void ActualizarCompetencia()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "UPDATE Competencias SET Competencia = @Competencia, Dominio = @Dominio WHERE CompetenciaID = @CompetenciaID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@CompetenciaID", CompetenciaID);
                comando.Parameters.AddWithValue("@Competencia", CompetenciaNombre);
                comando.Parameters.AddWithValue("@Dominio", Dominio);
                comando.ExecuteNonQuery();
            }
        }

        // Método para eliminar una competencia
        public void EliminarCompetencia()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "DELETE FROM Competencias WHERE CompetenciaID = @CompetenciaID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@CompetenciaID", CompetenciaID);
                comando.ExecuteNonQuery();
            }
        }

        // Método para obtener las competencias de un colaborador
        public static DataTable ObtenerCompetencias(int colaboradorID)
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "SELECT CompetenciaID, Competencia, Dominio FROM Competencias WHERE ColaboradorID = @ColaboradorID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ColaboradorID", colaboradorID);
                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                return dt;
            }
        }
    }
}
