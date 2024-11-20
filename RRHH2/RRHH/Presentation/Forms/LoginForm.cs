using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RRHH
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text.Trim();

            Usuario usuario = new Usuario();

            if (usuario.ValidarUsuario(nombreUsuario, contraseña))
            {
                // Obtener el rol y el ID del usuario
                string rol = usuario.ObtenerRol(nombreUsuario);
                int usuarioID = usuario.ObtenerUsuarioID(nombreUsuario);

                // Registrar actividad
                ConexionBD conexion = new ConexionBD();
                conexion.RegistrarActividad(usuarioID, "Inicio de sesión exitoso");

                // Pasar el rol al formulario principal
                MainMenuForm menu = new MainMenuForm(rol);
                menu.Show();
                this.Hide();

                MessageBox.Show($"Bienvenido, {rol}.", "Acceso concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Cierra la aplicación
        }
    }
}
