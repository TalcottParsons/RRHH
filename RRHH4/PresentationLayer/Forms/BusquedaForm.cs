using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RRHH
{
    public partial class BusquedaForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-RBBHGTO\\SQLEXPRESS;Initial Catalog=RRHH;User ID=sa;Password=wasawasa;TrustServerCertificate=True";

        public BusquedaForm()
        {
            InitializeComponent();
            CargarCriterios(); // Carga los criterios al iniciar el formulario
        }

        private void CargarCriterios()
        {
            cmbCriterio.Items.Clear();
            cmbCriterio.Items.AddRange(new object[]
            {
                "NombreCompleto",   // Nombre del colaborador
                "Departamento",     // Departamento del colaborador
                "Habilidad",        // Habilidades asociadas
                "Competencia",      // Competencias asociadas
                "Email",            // Dirección de correo electrónico
                "Telefono",         // Número de teléfono
                "FechaIngreso",     // Fecha de ingreso
                "EstadoActivo"      // Estado (activo/inactivo)
            });
            cmbCriterio.SelectedIndex = 0; // Selección predeterminada
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario de búsqueda
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = cmbCriterio.SelectedItem.ToString();
            string valor = txtBusqueda.Text.Trim();

            if (string.IsNullOrEmpty(valor))
            {
                MessageBox.Show("Por favor, ingrese un valor para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new ConexionBD().AbrirConexion())
            {
                string query = @"
            SELECT c.ColaboradorID AS ID, 
                   c.NombreCompleto AS Nombre, 
                   c.Departamento, 
                   c.Email, 
                   c.Telefono, 
                   c.FechaIngreso, 
                   c.EstadoActivo, 
                   c.Foto
            FROM Colaboradores c";

                // Agregar joins según el criterio seleccionado
                if (criterio == "Habilidad")
                {
                    query += @"
                INNER JOIN Habilidades h ON c.ColaboradorID = h.ColaboradorID
                WHERE h.Habilidad LIKE @Valor";
                }
                else if (criterio == "Competencia")
                {
                    query += @"
                INNER JOIN Competencias comp ON c.ColaboradorID = comp.ColaboradorID
                WHERE comp.Competencia LIKE @Valor";
                }
                else
                {
                    query += @"
                WHERE c." + criterio + " LIKE @Valor";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Valor", "%" + valor + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvResultados.DataSource = dt;

                // Ocultar la columna Foto en el DataGridView si existe
                if (dgvResultados.Columns.Contains("Foto"))
                {
                    dgvResultados.Columns["Foto"].Visible = false;
                }
            }
        }


        private void dgvResultados_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvResultados.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dgvResultados.SelectedRows[0];

                if (filaSeleccionada.Cells["Foto"].Value != DBNull.Value)
                {
                    byte[] fotoBytes = (byte[])filaSeleccionada.Cells["Foto"].Value;
                    using (MemoryStream ms = new MemoryStream(fotoBytes))
                    {
                        pbFoto.Image = Image.FromStream(ms); // Cargar la imagen en el PictureBox
                    }
                }
                else
                {
                    pbFoto.Image = null; // Limpiar el PictureBox si no hay foto
                }
            }
        }
    }
}
