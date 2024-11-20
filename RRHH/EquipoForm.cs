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
    public partial class EquipoForm : Form
    {
        public EquipoForm()
        {
            InitializeComponent();
            CargarEquipo(); // Carga la información del equipo al DataGridView
        }

        // Método para cargar los datos del equipo
        private void CargarEquipo()
        {
            // Crear una tabla en memoria para mostrar los datos
            DataTable dt = new DataTable();
            dt.Columns.Add("Nombre Completo", typeof(string));
            dt.Columns.Add("Carnet", typeof(string));

            // Agregar filas con los integrantes del equipo
            dt.Rows.Add("Fernando Adrian Palacios Ascencio", "PA100323");
            dt.Rows.Add("Michael Anderson Mancía Hernández", "MH100223");
            dt.Rows.Add("Wilmer Henrry Salazar Martínez", "SM100223");
            dt.Rows.Add("Mauricio Alejandro Chavez Hernandez", "CH100423");

            // Asignar la tabla al DataGridView
            dgvEquipo.DataSource = dt;
        }

        // Botón Volver
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario actual
        }
    }
}
