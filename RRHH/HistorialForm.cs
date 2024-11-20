using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RRHH
{
    public partial class HistorialForm : Form
    {
        public HistorialForm()
        {
            InitializeComponent();
            CargarHistorial(); // Carga inicial del historial de actividades
        }

        // Método para cargar el historial de actividades desde la base de datos
        private void CargarHistorial()
        {
            try
            {
                ConexionBD conexion = new ConexionBD();
                string query = @"
                    SELECT h.ActividadID AS ID, 
                           u.NombreUsuario AS Usuario, 
                           h.Accion AS Acción, 
                           h.FechaActividad AS Fecha
                    FROM HistorialActividades h
                    INNER JOIN Usuarios u ON h.UsuarioID = u.UsuarioID
                    ORDER BY h.FechaActividad DESC";

                DataTable historial = conexion.EjecutarConsulta(query);
                dgvHistorial.DataSource = historial;

                // Configuración visual del DataGridView
                dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvHistorial.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botón Volver
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario actual
        }
    }
}
