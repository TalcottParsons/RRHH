using RRHH_Form.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RRHH_Form.Presentation.Forms
{
    public partial class EmployeeForm : Form
    {

        private CvRepository _repository = new CvRepository();

        public EmployeeForm()
        {
            InitializeComponent();
            CargarDatos(); // Cargar datos al inicializar
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CurriculumVitae cv = new CurriculumVitae
            {
                Nombre = txtNombre.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text,
                Objetivo = txtObjetivo.Text,
                Departamento = comboBox1.Text,
                Titulo = txtTitulo.Text,
                Institucion = txtInstitucion.Text,
                Desde = dtpFADesde.Value,
                Hasta = dtpFAHasta.Value,
                Cargo = txtCargo.Text,
                Entidad = txtEntidad.Text,
                Competencias = txtCompetencias.Text,
                RLNombre = txtRLNombre.Text,
                RLPhone = txtRLPhone.Text,
                RPNombre = txtRPNombre.Text,
                RPPhone = txtRPPhone.Text,
                Foto = pbFoto.ImageLocation // Ruta de la imagen si es cargada
            };

            _repository.AgregarCv(cv);
            CargarDatos(); // Actualizar la lista
            LimpiarFormulario(); // Limpiar los campos
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener el Id del CV seleccionado
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                // Crear un objeto CurriculumVitae con los datos del formulario
                CurriculumVitae cv = new CurriculumVitae
                {
                    Id = id, // Asignar el Id del CV seleccionado
                    Nombre = txtNombre.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text,
                    Objetivo = txtObjetivo.Text,
                    Departamento = comboBox1.Text,
                    Titulo = txtTitulo.Text,
                    Institucion = txtInstitucion.Text,
                    Desde = dtpFADesde.Value,
                    Hasta = dtpFAHasta.Value,
                    Cargo = txtCargo.Text,
                    Entidad = txtEntidad.Text,
                    Competencias = txtCompetencias.Text,
                    RLNombre = txtRLNombre.Text,
                    RLPhone = txtRLPhone.Text,
                    RPNombre = txtRPNombre.Text,
                    RPPhone = txtRPPhone.Text,
                    Foto = pbFoto.ImageLocation
                };

                // Actualizar el CV en la base de datos
                _repository.ActualizarCv(cv);
                CargarDatos(); // Recargar la lista de CVs
                LimpiarFormulario(); // Limpiar el formulario
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un CV para modificar.");
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener el Id del CV seleccionado
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                // Eliminar el CV
                _repository.EliminarCv(id);
                CargarDatos(); // Recargar la lista de CVs
                LimpiarFormulario(); // Limpiar el formulario
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un CV para eliminar.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void CargarDatos()
        {
            var cvs = _repository.ObtenerTodosCvs();
            dataGridView1.DataSource = cvs;

            // Configuración opcional para ocultar columnas
            if (dataGridView1.Columns.Contains("Foto"))
                dataGridView1.Columns["Foto"].Visible = false; // Ejemplo para ocultar una columna
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtObjetivo.Clear();
            comboBox1.SelectedIndex = -1; // Desmarcar selección
            txtTitulo.Clear();
            txtInstitucion.Clear();
            dtpFADesde.CustomFormat = " "; // Ocultar la fecha
            dtpFADesde.Format = DateTimePickerFormat.Custom;
            dtpFAHasta.CustomFormat = " "; // Ocultar la fecha
            dtpFAHasta.Format = DateTimePickerFormat.Custom;
            txtCargo.Clear();
            txtEntidad.Clear();
            txtCompetencias.Clear();
            txtRLNombre.Clear();
            txtRLPhone.Clear();
            txtRPNombre.Clear();
            txtRPPhone.Clear();
            pbFoto.ImageLocation = null; // Limpiar la imagen
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verificar que se seleccionó una fila válida
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtNombre.Text = row.Cells["Nombre"].Value?.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
                txtObjetivo.Text = row.Cells["Objetivo"].Value?.ToString();
                comboBox1.Text = row.Cells["Departamento"].Value?.ToString();
                txtTitulo.Text = row.Cells["Titulo"].Value?.ToString();
                txtInstitucion.Text = row.Cells["Institucion"].Value?.ToString();
                dtpFADesde.Value = Convert.ToDateTime(row.Cells["Desde"].Value);
                dtpFAHasta.Value = Convert.ToDateTime(row.Cells["Hasta"].Value);
                txtCargo.Text = row.Cells["Cargo"].Value?.ToString();
                txtEntidad.Text = row.Cells["Entidad"].Value?.ToString();
                txtCompetencias.Text = row.Cells["Competencias"].Value?.ToString();
                txtRLNombre.Text = row.Cells["RLNombre"].Value?.ToString();
                txtRLPhone.Text = row.Cells["RLPhone"].Value?.ToString();
                txtRPNombre.Text = row.Cells["RPNombre"].Value?.ToString();
                txtRPPhone.Text = row.Cells["RPPhone"].Value?.ToString();
                pbFoto.ImageLocation = row.Cells["Foto"].Value?.ToString(); // Cargar imagen si está disponible
            }
        }
    }
}
