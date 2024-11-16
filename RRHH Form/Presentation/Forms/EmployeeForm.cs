using RRHH_Form.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RRHH_Form.Presentation.Forms
{
    public partial class EmployeeForm : Form
    {
        private CvRepository _repository = new CvRepository();
        public EmployeeForm()
        {
            InitializeComponent();
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
                Foto = pbFoto.ImageLocation // Si tienes una imagen, puedes usar la ruta del archivo
            };

            _repository.AgregarCv(cv);
            CargarDatos(); // Recargar la lista
        }

        private void btnModificar_Click(object sender, EventArgs e)
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
                Foto = pbFoto.ImageLocation
            };

            _repository.ActualizarCv(cv);
            CargarDatos(); // Recargar la lista

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text; // El nombre puede ser el identificador
            _repository.EliminarCv(nombre);
            CargarDatos(); // Recargar la lista

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void CargarDatos()
        {
            var cvs = _repository.ObtenerTodosCvs();
            dataGridView1.DataSource = cvs;
        }
    }
}
