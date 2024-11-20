using System;
using System.Windows.Forms;

namespace RRHH
{
    public partial class RegistroUsuarioForm : Form
    {
        public RegistroUsuarioForm()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreUsuario = txtNombreUsuario.Text.Trim();
                string contraseña = txtContraseña.Text.Trim();
                string rol = cmbRol.SelectedItem.ToString();

                if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contraseña) || string.IsNullOrEmpty(rol))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Usuario nuevoUsuario = new Usuario();
                nuevoUsuario.RegistrarUsuario(nombreUsuario, contraseña, rol);

                MessageBox.Show("Usuario registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario
        }
    }
}
