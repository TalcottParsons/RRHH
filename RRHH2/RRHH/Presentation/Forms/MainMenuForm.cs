using System;
using System.Windows.Forms;

namespace RRHH
{
    public partial class MainMenuForm : Form
    {
        private string RolUsuario;

        // Constructor principal con rol como parámetro
        public MainMenuForm(string rolUsuario)
        {
            InitializeComponent();
            RolUsuario = rolUsuario; // Asigna el rol recibido
            AplicarRestriccionesPorRol(); // Aplica restricciones iniciales
        }

        // Constructor sin parámetros (para compatibilidad)
        public MainMenuForm()
        {
            InitializeComponent();
            RolUsuario = "Operador"; // Rol predeterminado si no se especifica
            AplicarRestriccionesPorRol();
        }

        private void AplicarRestriccionesPorRol()
        {
            if (RolUsuario == "Operador")
            {
                btnGestionColaboradores.Enabled = false; // Deshabilitar gestión de colaboradores
                btnHistorial.Enabled = false;           // Deshabilitar historial
                btnAgregarUsuario.Enabled = false;      // Deshabilitar agregar usuario
            }
            else if (RolUsuario == "Admin")
            {
                btnGestionColaboradores.Enabled = true; // Habilitar gestión de colaboradores
                btnHistorial.Enabled = true;           // Habilitar historial
                btnAgregarUsuario.Enabled = true;      // Habilitar agregar usuario
            }
        }

        private void MainMenuForm_Activated(object sender, EventArgs e)
        {
            AplicarRestriccionesPorRol(); // Reaplica las restricciones al activarse el formulario
        }

        private void btnGestionColaboradores_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ocultar el menú principal
            using (ColaboradoresForm colaboradoresForm = new ColaboradoresForm())
            {
                colaboradoresForm.ShowDialog(); // Abre el formulario secundario como modal
            }
            this.Show(); // Mostrar el menú principal después de cerrar el formulario
        }

        private void btnEquipo_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ocultar el menú principal
            using (EquipoForm equipoForm = new EquipoForm())
            {
                equipoForm.ShowDialog(); // Abre el formulario secundario como modal
            }
            this.Show(); // Mostrar el menú principal después de cerrar el formulario
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ocultar el menú principal
            using (HistorialForm historialForm = new HistorialForm())
            {
                historialForm.ShowDialog(); // Abre el formulario secundario como modal
            }
            this.Show(); // Mostrar el menú principal después de cerrar el formulario
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ocultar el menú principal
            using (BusquedaForm busquedaForm = new BusquedaForm())
            {
                busquedaForm.ShowDialog(); // Mostrar el formulario de búsqueda como modal
            }
            this.Show(); // Mostrar el menú principal nuevamente al cerrar el formulario de búsqueda
        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            if (RolUsuario == "Admin")
            {
                using (RegistroUsuarioForm registroUsuarioForm = new RegistroUsuarioForm())
                {
                    registroUsuarioForm.ShowDialog(); // Abre el formulario de registro de usuario como modal
                }
            }
            else
            {
                MessageBox.Show("Acceso denegado. Solo los administradores pueden agregar usuarios.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas salir?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Cierra toda la aplicación
            }
        }
    }
}



