using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace RRHH
{
    public partial class Reporte : Form
    {
        private readonly dynamic _datosPersona;

        public Reporte(dynamic datosPersona)
        {
            InitializeComponent();
            _datosPersona = datosPersona;
        }

        private void Reporte_Load(object sender, EventArgs e)
        {
            try
            {
                // Configurar el ReportViewer
                reportViewer1.LocalReport.ReportEmbeddedResource = "RRHH.ReporteColaborador.rdlc";

                // Crear DataTable con los campos EXACTOS del RDLC
                DataTable dt = new DataTable();

                // Campos que existen en tu RDLC DataSet1
                dt.Columns.Add("ContactoID", typeof(string));
                dt.Columns.Add("NombreCompleto", typeof(string));
                dt.Columns.Add("TelefonoMovil", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Departamento", typeof(string));
                dt.Columns.Add("Objetivo", typeof(string));
                dt.Columns.Add("Foto", typeof(byte[]));

                // Agregar una sola fila con los datos
                dt.Rows.Add(
                    _datosPersona.ContactoID ?? "1",
                    _datosPersona.NombreCompleto ?? "Sin nombre",
                    _datosPersona.TelefonoMovil ?? "Sin teléfono",
                    _datosPersona.Email ?? "Sin email",
                    _datosPersona.Departamento ?? "Sin departamento",
                    _datosPersona.Objetivo ?? "Sin objetivo",
                    _datosPersona.Foto ?? new byte[0]
                );

                // Configurar el ReportViewer
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Agregar parámetros opcionales para información adicional
                ConfigurarParametrosAdicionales();

                // Refrescar el informe
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el reporte: {ex.Message}\n\nDetalles: {ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarParametrosAdicionales()
        {
            try
            {
                // Crear parámetros para mostrar información adicional
                var parametros = new ReportParameter[]
                {
                    new ReportParameter("FechaReporte", DateTime.Now.ToString("dd/MM/yyyy HH:mm")),
                    new ReportParameter("FechaIngreso", _datosPersona.FechaIngreso ?? "No especificada"),
                    new ReportParameter("EstadoActivo", _datosPersona.EstadoActivo ?? "No especificado"),
                    new ReportParameter("FormacionAcademica", _datosPersona.FormacionAcademica ?? "No especificada"),
                    new ReportParameter("ExperienciaProfesional", _datosPersona.ExperienciaProfesional ?? "No especificada"),
                    new ReportParameter("Habilidades", _datosPersona.Habilidades ?? "No especificadas"),
                    new ReportParameter("Competencias", _datosPersona.Competencias ?? "No especificadas"),
                    new ReportParameter("Referencias", _datosPersona.Referencias ?? "No especificadas")
                };

                reportViewer1.LocalReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {
                // Los parámetros son opcionales, solo mostrar en consola para debug
                System.Diagnostics.Debug.WriteLine($"No se pudieron configurar parámetros: {ex.Message}");
            }
        }

        // Método de debug para verificar los datos recibidos
        private void MostrarDatosDebug()
        {
            string debug = $@"
DATOS RECIBIDOS EN EL REPORTE:
================================
ContactoID: {_datosPersona.ContactoID}
NombreCompleto: {_datosPersona.NombreCompleto}
TelefonoMovil: {_datosPersona.TelefonoMovil}
Email: {_datosPersona.Email}
Departamento: {_datosPersona.Departamento}
Objetivo: {_datosPersona.Objetivo}
Foto: {(_datosPersona.Foto != null ? "Sí tiene foto" : "No tiene foto")}

INFORMACIÓN ADICIONAL:
=====================
FechaIngreso: {_datosPersona.FechaIngreso}
EstadoActivo: {_datosPersona.EstadoActivo}
FormacionAcademica: {_datosPersona.FormacionAcademica}
ExperienciaProfesional: {_datosPersona.ExperienciaProfesional}
Habilidades: {_datosPersona.Habilidades}
Competencias: {_datosPersona.Competencias}
Referencias: {_datosPersona.Referencias}";

            MessageBox.Show(debug, "Debug - Datos del Reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}