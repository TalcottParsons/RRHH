using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace RRHH
{
    public class Referencia
    {
        public int ReferenciaID { get; set; }
        public int ColaboradorID { get; set; }
        public string TipoReferencia { get; set; } // Puede ser "Personal" o "Laboral"
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        // Método para agregar una nueva referencia
        public void AgregarReferencia()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "INSERT INTO Referencias (ColaboradorID, TipoReferencia, Nombre, Telefono) VALUES (@ColaboradorID, @TipoReferencia, @Nombre, @Telefono)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ColaboradorID", ColaboradorID);
                comando.Parameters.AddWithValue("@TipoReferencia", TipoReferencia);
                comando.Parameters.AddWithValue("@Nombre", Nombre);
                comando.Parameters.AddWithValue("@Telefono", Telefono);
                comando.ExecuteNonQuery();
            }
        }

        // Método para actualizar una referencia existente
        public void ActualizarReferencia()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "UPDATE Referencias SET TipoReferencia = @TipoReferencia, Nombre = @Nombre, Telefono = @Telefono WHERE ReferenciaID = @ReferenciaID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ReferenciaID", ReferenciaID);
                comando.Parameters.AddWithValue("@TipoReferencia", TipoReferencia);
                comando.Parameters.AddWithValue("@Nombre", Nombre);
                comando.Parameters.AddWithValue("@Telefono", Telefono);
                comando.ExecuteNonQuery();
            }
        }

        // Método para eliminar una referencia
        public void EliminarReferencia()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "DELETE FROM Referencias WHERE ReferenciaID = @ReferenciaID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ReferenciaID", ReferenciaID);
                comando.ExecuteNonQuery();
            }
        }

        // Método para obtener las referencias de un colaborador
        public static DataTable ObtenerReferencias(int colaboradorID)
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "SELECT ReferenciaID, TipoReferencia, Nombre, Telefono FROM Referencias WHERE ColaboradorID = @ColaboradorID";
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
