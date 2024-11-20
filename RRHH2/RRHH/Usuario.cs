using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace RRHH
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; }

        // Método para encriptar contraseñas
        private string EncriptarContraseña(string contraseña)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                StringBuilder resultado = new StringBuilder();
                foreach (byte b in bytes)
                {
                    resultado.Append(b.ToString("x2"));
                }
                return resultado.ToString();
            }
        }

        // Validar credenciales del usuario
        public bool ValidarUsuario(string nombreUsuario, string contraseña)
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contraseña";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                comando.Parameters.AddWithValue("@Contraseña", EncriptarContraseña(contraseña));
                return (int)comando.ExecuteScalar() > 0;
            }
        }

        // Obtener el rol del usuario
        public string ObtenerRol(string nombreUsuario)
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "SELECT Rol FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                return comando.ExecuteScalar()?.ToString();
            }
        }

        // Obtener el ID del usuario
        public int ObtenerUsuarioID(string nombreUsuario)
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "SELECT UsuarioID FROM Usuarios WHERE NombreUsuario = @NombreUsuario";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                object resultado = comando.ExecuteScalar();

                if (resultado != null)
                {
                    return Convert.ToInt32(resultado);
                }
                else
                {
                    throw new Exception("Usuario no encontrado.");
                }
            }
        }

        // Método para registrar un nuevo usuario
        public void RegistrarUsuario(string nombreUsuario, string contraseña, string rol)
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "INSERT INTO Usuarios (NombreUsuario, Contraseña, Rol) VALUES (@NombreUsuario, @Contraseña, @Rol)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                comando.Parameters.AddWithValue("@Contraseña", EncriptarContraseña(contraseña));
                comando.Parameters.AddWithValue("@Rol", rol);
                comando.ExecuteNonQuery();
            }
        }
    }
}