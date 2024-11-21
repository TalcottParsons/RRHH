using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "SELECT ActividadID, UsuarioID, Accion, FechaActividad FROM HistorialActividades ORDER BY FechaActividad DESC";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                dgvHistorial.DataSource = dt;
            }
        }

        // Botón Volver
        private void btnVolver_Click(object sender, EventArgs e)
        {
            // Volver al menú principal
            this.Close(); // Cierra el formulario actual
        }
    }
}
