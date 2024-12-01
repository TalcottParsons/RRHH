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
using System.IO;



namespace RRHH
{
    public partial class ColaboradoresForm : Form
    {
        // Constructor
        public ColaboradoresForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(ColaboradoresForm_Load);
            dgvColaboradores.SelectionChanged += new EventHandler(dgvColaboradores_SelectionChanged);
        }

        // Evento que se ejecuta al cargar el formulario
        private void ColaboradoresForm_Load(object sender, EventArgs e)
        {
            CargarColaboradores();
        }


        // Método para cargar colaboradores al DataGridView
        private void CargarColaboradores()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "SELECT ColaboradorID, NombreCompleto, Telefono, Email, Departamento, EstadoActivo, Objetivo FROM Colaboradores";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                dgvColaboradores.DataSource = dt;
            }
        }

        private void btnBuscarColaborador_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores ingresados en los campos
                string nombre = txtNombreCompleto.Text.Trim().ToLower();
                string telefono = txtTelefono.Text.Trim().ToLower();
                string email = txtEmail.Text.Trim().ToLower();
                string departamento = txtDepartamento.Text.Trim().ToLower();
                string objetivo = txtObjetivo.Text.Trim().ToLower();

                // Crear una lista de filas que coincidan con los criterios
                List<DataGridViewRow> resultados = dgvColaboradores.Rows.Cast<DataGridViewRow>()
                    .Where(fila =>
                        (string.IsNullOrEmpty(nombre) || fila.Cells["NombreCompleto"].Value.ToString().ToLower().Contains(nombre)) &&
                        (string.IsNullOrEmpty(telefono) || fila.Cells["Telefono"].Value.ToString().ToLower().Contains(telefono)) &&
                        (string.IsNullOrEmpty(email) || fila.Cells["Email"].Value.ToString().ToLower().Contains(email)) &&
                        (string.IsNullOrEmpty(departamento) || fila.Cells["Departamento"].Value.ToString().ToLower().Contains(departamento)) &&
                        (string.IsNullOrEmpty(objetivo) || fila.Cells["Objetivo"].Value.ToString().ToLower().Contains(objetivo))
                    ).ToList();

                // Verificar si hay resultados
                if (resultados.Count > 0)
                {
                    // Limpia la selección actual
                    dgvColaboradores.ClearSelection();

                    // Resaltar las filas encontradas
                    foreach (var fila in resultados)
                    {
                        fila.Selected = true;
                        dgvColaboradores.FirstDisplayedScrollingRowIndex = fila.Index;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron colaboradores que coincidan con los criterios de búsqueda.",
                                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error durante la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Botón Agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación de campos vacíos
                if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Por favor, complete los campos obligatorios (Nombre Completo y Email).",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar si el colaborador ya existe
                Colaborador colaborador = new Colaborador();
                if (colaborador.ExisteColaborador(txtNombreCompleto.Text.Trim(), txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Ya existe un colaborador con este nombre o correo electrónico.",
                                    "Duplicado detectado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear el nuevo colaborador
                colaborador.NombreCompleto = txtNombreCompleto.Text.Trim();
                colaborador.Telefono = txtTelefono.Text.Trim();
                colaborador.Email = txtEmail.Text.Trim();
                colaborador.Departamento = txtDepartamento.Text.Trim();
                colaborador.Objetivo = txtObjetivo.Text.Trim();
                colaborador.EstadoActivo = true;

                // Convertir la imagen en el PictureBox a un arreglo de bytes
                if (pictureBoxFoto.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBoxFoto.Image.Save(ms, pictureBoxFoto.Image.RawFormat);
                        colaborador.Foto = ms.ToArray();
                    }
                }

                // Agregar el colaborador
                colaborador.AgregarColaborador();
                MessageBox.Show("Colaborador agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar la lista de colaboradores
                CargarColaboradores();

                // Limpiar los campos del formulario
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para limpiar los campos del formulario
        private void LimpiarCampos()
        {
            // Limpia los campos de entrada
            txtNombreCompleto.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDepartamento.Clear();
            txtObjetivo.Clear();

            // Limpia la imagen en el PictureBox
            pictureBoxFoto.Image = null;

            // Deselecciona cualquier fila en el DataGridView
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                dgvColaboradores.ClearSelection();
            }
        }



        // Botón Actualizar
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                try
                {
                    Colaborador colaborador = new Colaborador
                    {
                        ColaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value),
                        NombreCompleto = txtNombreCompleto.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Departamento = txtDepartamento.Text.Trim(),
                        Objetivo = txtObjetivo.Text.Trim(),
                    };

                    // Convierte la imagen en el PictureBox a un arreglo de bytes
                    if (pictureBoxFoto.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pictureBoxFoto.Image.Save(ms, pictureBoxFoto.Image.RawFormat);
                            colaborador.Foto = ms.ToArray();
                        }
                    }

                    colaborador.ActualizarColaborador();
                    MessageBox.Show("Colaborador actualizado exitosamente.");
                    CargarColaboradores();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un colaborador para actualizar.");
            }
        }



        // Botón Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                try
                {
                    Colaborador colaborador = new Colaborador
                    {
                        ColaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value)
                    };

                    colaborador.EliminarColaborador();
                    MessageBox.Show("Colaborador marcado como inactivo.");
                    CargarColaboradores();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un colaborador para eliminar.");
            }
        }


        private void btnSeleccionarFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Seleccionar una foto";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Cargar la imagen seleccionada en el PictureBox
                        this.pictureBoxFoto.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
                        this.pictureBoxFoto.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar la imagen: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        // Botón Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtNombreCompleto.Text.Trim();
            if (!string.IsNullOrEmpty(criterio))
            {
                using (SqlConnection conexion = new ConexionBD().AbrirConexion())
                {
                    string query = "SELECT ColaboradorID, NombreCompleto, Telefono, Email, Departamento, EstadoActivo FROM Colaboradores WHERE NombreCompleto LIKE @Criterio + '%'";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@Criterio", criterio);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);
                    dgvColaboradores.DataSource = dt;
                }
            }
            else
            {
                MessageBox.Show("Ingrese un criterio de búsqueda.");
            }
        }

        // Método para cargar formación académica
        private void CargarFormaciones()
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                int colaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value);

                using (SqlConnection conexion = new ConexionBD().AbrirConexion())
                {
                    string query = "SELECT Institucion, Titulo, AñoInicio, AñoFin FROM FormacionAcademica WHERE ColaboradorID = @ColaboradorID";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@ColaboradorID", colaboradorID);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvFormacionAcademica.DataSource = dt;
                }
            }
            else
            {
                dgvFormacionAcademica.DataSource = null;
            }
        }

        private void btnAgregarFormacion_Click(object sender, EventArgs e)
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                try
                {
                    FormacionAcademica formacion = new FormacionAcademica
                    {
                        ColaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value),
                        Institucion = txtInstitucion.Text.Trim(),
                        Titulo = txtTitulo.Text.Trim(),
                        AñoInicio = (int)numAñoInicio.Value,
                        AñoFin = (int)numAñoFin.Value
                    };

                    formacion.AgregarFormacion();
                    MessageBox.Show("Formación académica agregada exitosamente.");
                    CargarFormaciones();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar la formación académica: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un colaborador antes de agregar formación académica.");
            }
        }


        private void btnActualizarFormacion_Click(object sender, EventArgs e)
        {
            if (dgvFormacionAcademica.SelectedRows.Count > 0)
            {
                FormacionAcademica formacion = new FormacionAcademica
                {
                    FormacionID = Convert.ToInt32(dgvFormacionAcademica.SelectedRows[0].Cells["FormacionID"].Value),
                    Institucion = txtInstitucion.Text.Trim(),
                    Titulo = txtTitulo.Text.Trim(),
                    AñoInicio = (int)numAñoInicio.Value,
                    AñoFin = (int)numAñoFin.Value
                };

                formacion.ActualizarFormacion();
                MessageBox.Show("Formación actualizada exitosamente.");
                CargarFormaciones();
            }
            else
            {
                MessageBox.Show("Seleccione una formación para actualizar.");
            }
        }

        private void btnEliminarFormacion_Click(object sender, EventArgs e)
        {
            if (dgvFormacionAcademica.SelectedRows.Count > 0)
            {
                FormacionAcademica formacion = new FormacionAcademica
                {
                    FormacionID = Convert.ToInt32(dgvFormacionAcademica.SelectedRows[0].Cells["FormacionID"].Value)
                };

                formacion.EliminarFormacion();
                MessageBox.Show("Formación eliminada exitosamente.");
                CargarFormaciones();
            }
            else
            {
                MessageBox.Show("Seleccione una formación para eliminar.");
            }
        }

        // Método para cargar experiencia profesional
        private void CargarExperiencias()
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                int colaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value);

                using (SqlConnection conexion = new ConexionBD().AbrirConexion())
                {
                    string query = "SELECT Empresa, Puesto, AñoInicio, AñoFin FROM ExperienciaProfesional WHERE ColaboradorID = @ColaboradorID";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@ColaboradorID", colaboradorID);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvExperienciaProfesional.DataSource = dt;
                }
            }
            else
            {
                dgvExperienciaProfesional.DataSource = null;
            }
        }

        private void btnAgregarExperiencia_Click(object sender, EventArgs e)
        {
            try
            {
                ExperienciaProfesional experiencia = new ExperienciaProfesional
                {
                    ColaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value),
                    Empresa = txtEmpresa.Text.Trim(),
                    Puesto = txtPuesto.Text.Trim(),
                    AñoInicio = (int)numAñoInicioExperiencia.Value,
                    AñoFin = (int)numAñoFinExperiencia.Value
                };

                experiencia.AgregarExperiencia();
                MessageBox.Show("Experiencia agregada exitosamente.");
                CargarExperiencias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnActualizarExperiencia_Click(object sender, EventArgs e)
        {
            if (dgvExperienciaProfesional.SelectedRows.Count > 0)
            {
                ExperienciaProfesional experiencia = new ExperienciaProfesional
                {
                    ExperienciaID = Convert.ToInt32(dgvExperienciaProfesional.SelectedRows[0].Cells["ExperienciaID"].Value),
                    Empresa = txtEmpresa.Text.Trim(),
                    Puesto = txtPuesto.Text.Trim(),
                    AñoInicio = (int)numAñoInicioExperiencia.Value,
                    AñoFin = (int)numAñoFinExperiencia.Value
                };

                experiencia.ActualizarExperiencia();
                MessageBox.Show("Experiencia actualizada exitosamente.");
                CargarExperiencias();
            }
            else
            {
                MessageBox.Show("Seleccione una experiencia para actualizar.");
            }
        }

        private void btnEliminarExperiencia_Click(object sender, EventArgs e)
        {
            if (dgvExperienciaProfesional.SelectedRows.Count > 0)
            {
                ExperienciaProfesional experiencia = new ExperienciaProfesional
                {
                    ExperienciaID = Convert.ToInt32(dgvExperienciaProfesional.SelectedRows[0].Cells["ExperienciaID"].Value)
                };

                experiencia.EliminarExperiencia();
                MessageBox.Show("Experiencia eliminada exitosamente.");
                CargarExperiencias();
            }
            else
            {
                MessageBox.Show("Seleccione una experiencia para eliminar.");
            }
        }

        private void CargarHabilidades()
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                int colaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value);

                dgvHabilidades.DataSource = Habilidad.ObtenerHabilidades(colaboradorID);
            }
            else
            {
                dgvHabilidades.DataSource = null;
            }
        }

        private void btnAgregarHabilidad_Click(object sender, EventArgs e)
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                try
                {
                    Habilidad habilidad = new Habilidad
                    {
                        ColaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value),
                        HabilidadNombre = txtHabilidad.Text.Trim()
                    };

                    habilidad.AgregarHabilidad();
                    MessageBox.Show("Habilidad agregada exitosamente.");
                    CargarHabilidades();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar habilidad: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un colaborador antes de agregar una habilidad.");
            }
        }

        private void btnActualizarHabilidad_Click(object sender, EventArgs e)
        {
            if (dgvHabilidades.SelectedRows.Count > 0)
            {
                try
                {
                    Habilidad habilidad = new Habilidad
                    {
                        HabilidadID = Convert.ToInt32(dgvHabilidades.SelectedRows[0].Cells["HabilidadID"].Value),
                        HabilidadNombre = txtHabilidad.Text.Trim()
                    };

                    habilidad.ActualizarHabilidad();
                    MessageBox.Show("Habilidad actualizada exitosamente.");
                    CargarHabilidades();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar habilidad: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una habilidad para actualizar.");
            }
        }

        private void btnEliminarHabilidad_Click(object sender, EventArgs e)
        {
            if (dgvHabilidades.SelectedRows.Count > 0)
            {
                try
                {
                    Habilidad habilidad = new Habilidad
                    {
                        HabilidadID = Convert.ToInt32(dgvHabilidades.SelectedRows[0].Cells["HabilidadID"].Value)
                    };

                    habilidad.EliminarHabilidad();
                    MessageBox.Show("Habilidad eliminada exitosamente.");
                    CargarHabilidades();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar habilidad: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una habilidad para eliminar.");
            }
        }

        private void CargarCompetencias()
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                int colaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value);
                dgvCompetencias.DataSource = Competencia.ObtenerCompetencias(colaboradorID);
            }
        }

        private void btnAgregarCompetencia_Click(object sender, EventArgs e)
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                try
                {
                    Competencia competencia = new Competencia
                    {
                        ColaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value),
                        CompetenciaNombre = txtCompetencia.Text.Trim(),
                        Dominio = cmbDominio.SelectedItem.ToString()
                    };

                    competencia.AgregarCompetencia();
                    MessageBox.Show("Competencia agregada exitosamente.");
                    CargarCompetencias();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar la competencia: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un colaborador antes de agregar una competencia.");
            }
        }

        private void btnActualizarCompetencia_Click(object sender, EventArgs e)
        {
            if (dgvCompetencias.SelectedRows.Count > 0)
            {
                try
                {
                    Competencia competencia = new Competencia
                    {
                        CompetenciaID = Convert.ToInt32(dgvCompetencias.SelectedRows[0].Cells["CompetenciaID"].Value),
                        CompetenciaNombre = txtCompetencia.Text.Trim(),
                        Dominio = cmbDominio.SelectedItem.ToString()
                    };

                    competencia.ActualizarCompetencia();
                    MessageBox.Show("Competencia actualizada exitosamente.");
                    CargarCompetencias();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la competencia: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una competencia para actualizar.");
            }
        }

        private void btnEliminarCompetencia_Click(object sender, EventArgs e)
        {
            if (dgvCompetencias.SelectedRows.Count > 0)
            {
                try
                {
                    Competencia competencia = new Competencia
                    {
                        CompetenciaID = Convert.ToInt32(dgvCompetencias.SelectedRows[0].Cells["CompetenciaID"].Value)
                    };

                    competencia.EliminarCompetencia();
                    MessageBox.Show("Competencia eliminada exitosamente.");
                    CargarCompetencias();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la competencia: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una competencia para eliminar.");
            }
        }

        private void btnAgregarReferencia_Click(object sender, EventArgs e)
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                try
                {
                    Referencia referencia = new Referencia
                    {
                        ColaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value),
                        TipoReferencia = cmbTipoReferencia.SelectedItem.ToString(),
                        Nombre = txtNombreReferencia.Text.Trim(),
                        Telefono = txtTelefonoReferencia.Text.Trim()
                    };

                    referencia.AgregarReferencia();
                    MessageBox.Show("Referencia agregada exitosamente.");
                    CargarReferencias();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar la referencia: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un colaborador antes de agregar una referencia.");
            }
        }

        private void btnActualizarReferencia_Click(object sender, EventArgs e)
        {
            if (dgvReferencias.SelectedRows.Count > 0)
            {
                try
                {
                    Referencia referencia = new Referencia
                    {
                        ReferenciaID = Convert.ToInt32(dgvReferencias.SelectedRows[0].Cells["ReferenciaID"].Value),
                        TipoReferencia = cmbTipoReferencia.SelectedItem.ToString(),
                        Nombre = txtNombreReferencia.Text.Trim(),
                        Telefono = txtTelefonoReferencia.Text.Trim()
                    };

                    referencia.ActualizarReferencia();
                    MessageBox.Show("Referencia actualizada exitosamente.");
                    CargarReferencias();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la referencia: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Selecciona una referencia para actualizar.");
            }
        }

        private void btnEliminarReferencia_Click(object sender, EventArgs e)
        {
            if (dgvReferencias.SelectedRows.Count > 0)
            {
                try
                {
                    Referencia referencia = new Referencia
                    {
                        ReferenciaID = Convert.ToInt32(dgvReferencias.SelectedRows[0].Cells["ReferenciaID"].Value)
                    };

                    referencia.EliminarReferencia();
                    MessageBox.Show("Referencia eliminada exitosamente.");
                    CargarReferencias();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la referencia: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Selecciona una referencia para eliminar.");
            }
        }

        private void CargarReferencias()
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                int colaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value);
                dgvReferencias.DataSource = Referencia.ObtenerReferencias(colaboradorID);
            }
        }



        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario actual
        }


        // Evento para actualizar las formaciones cuando se selecciona un colaborador
        private void dgvColaboradores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvColaboradores.SelectedRows[0];
                int colaboradorID = Convert.ToInt32(row.Cells["ColaboradorID"].Value);

                using (SqlConnection conexion = new ConexionBD().AbrirConexion())
                {
                    string query = "SELECT Foto FROM Colaboradores WHERE ColaboradorID = @ColaboradorID";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@ColaboradorID", colaboradorID);

                    object fotoObj = comando.ExecuteScalar();
                    if (fotoObj != null && fotoObj != DBNull.Value)
                    {
                        byte[] fotoBytes = (byte[])fotoObj;
                        using (var ms = new MemoryStream(fotoBytes))
                        {
                            pictureBoxFoto.Image = Image.FromStream(ms);
                            pictureBoxFoto.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                    else
                    {
                        pictureBoxFoto.Image = null; // Si no hay foto, limpiar el PictureBox
                    }
                }
            }
            CargarFormaciones();
            CargarExperiencias();
            CargarHabilidades();
            CargarCompetencias();
            CargarReferencias();
        }

        // Método para limpiar los campos
       
    }
}
