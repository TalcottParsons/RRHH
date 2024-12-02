using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;


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
                        pbFoto.Image = System.Drawing.Image.FromStream(ms); // Cargar la imagen en el PictureBox
                    }
                }
                else
                {
                    pbFoto.Image = null; // Limpiar el PictureBox si no hay foto
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvResultados.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow filaSeleccionada = dgvResultados.SelectedRows[0];
                    string nombre = filaSeleccionada.Cells["Nombre"].Value?.ToString() ?? "Sin nombre";
                    string departamento = filaSeleccionada.Cells["Departamento"].Value?.ToString() ?? "Sin departamento";
                    string email = filaSeleccionada.Cells["Email"].Value?.ToString() ?? "Sin email";
                    string telefono = filaSeleccionada.Cells["Telefono"].Value?.ToString() ?? "Sin teléfono";
                    string fechaIngreso = filaSeleccionada.Cells["FechaIngreso"].Value?.ToString() ?? "Sin fecha";
                    bool estadoActivo = filaSeleccionada.Cells["EstadoActivo"].Value != DBNull.Value &&
                                        Convert.ToBoolean(filaSeleccionada.Cells["EstadoActivo"].Value);

                    // Cambiar la ruta para que se guarde en Documentos
                    string carpetaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string archivoPDF = Path.Combine(carpetaDocumentos, $"{nombre.Replace(" ", "_")}_Registro.pdf");

                    using (var writer = new PdfWriter(archivoPDF))
                    {
                        using (var pdf = new PdfDocument(writer))
                        {
                            var document = new Document(pdf);

                            // Crear fuentes
                            var boldFont = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.TIMES_BOLD);
                            var normalFont = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);

                            // Título en negrita
                            document.Add(new Paragraph("Registro de Colaborador")
                                .SetFont(boldFont)
                                .SetFontSize(16));

                            // Detalles
                            document.Add(new Paragraph($"Nombre: {nombre}").SetFont(normalFont));
                            document.Add(new Paragraph($"Departamento: {departamento}").SetFont(normalFont));
                            document.Add(new Paragraph($"Email: {email}").SetFont(normalFont));
                            document.Add(new Paragraph($"Teléfono: {telefono}").SetFont(normalFont));
                            document.Add(new Paragraph($"Fecha de Ingreso: {fechaIngreso}").SetFont(normalFont));
                            document.Add(new Paragraph($"Estado Activo: {(estadoActivo ? "Sí" : "No")}").SetFont(normalFont));

                            // Imagen
                            if (filaSeleccionada.Cells["Foto"].Value != DBNull.Value)
                            {
                                byte[] fotoBytes = (byte[])filaSeleccionada.Cells["Foto"].Value;
                                using (MemoryStream ms = new MemoryStream(fotoBytes))
                                {
                                    var imageData = ImageDataFactory.Create(ms.ToArray());
                                    var image = new iText.Layout.Element.Image(imageData);
                                    document.Add(image);
                                }
                            }

                            document.Close();
                        }
                    }

                    MessageBox.Show($"El archivo PDF se ha guardado en la carpeta Documentos como: {archivoPDF}.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro para imprimir.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }
}
