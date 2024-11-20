using System;
using System.Data;
using System.Data.SqlClient;

namespace RRHH
{
    public class ConexionBD
    {
        // Cadena de conexión: actualízala si es necesario.
        private readonly string cadenaConexion = "Data Source=DESKTOP-RBBHGTO\\SQLEXPRESS;Initial Catalog=RRHH;User ID=sa;Password=wasawasa;TrustServerCertificate=True";

        // Método para abrir la conexión
        public SqlConnection AbrirConexion()
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al abrir la conexión: {ex.Message}");
            }
            return conexion;
        }

        // Método para cerrar la conexión
        public void CerrarConexion(SqlConnection conexion)
        {
            if (conexion != null && conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }
        }

        // Método para ejecutar consultas SELECT y retornar un DataTable
        public DataTable EjecutarConsulta(string query, SqlParameter[] parametros = null)
        {
            using (SqlConnection conexion = AbrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    if (parametros != null)
                    {
                        cmd.Parameters.AddRange(parametros);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // Método para registrar actividades en el historial
        public void RegistrarActividad(int usuarioID, string accion)
        {
            try
            {
                using (SqlConnection conexion = AbrirConexion())
                {
                    string query = "INSERT INTO HistorialActividades (UsuarioID, Accion) VALUES (@UsuarioID, @Accion)";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                        cmd.Parameters.AddWithValue("@Accion", accion);

                        int filasAfectadas = cmd.ExecuteNonQuery(); // Retorna el número de filas afectadas

                        if (filasAfectadas > 0)
                        {
                            Console.WriteLine("Actividad registrada correctamente.");
                        }
                        else
                        {
                            Console.WriteLine("No se registró la actividad.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar la actividad: {ex.Message}");
            }
        }

    }
}
