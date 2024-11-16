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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            //Se debe usar el AuthService de la capa de BusinessLogic para autentificar
          /*  AuthService authService.Authenticate(username, password);
            bool isAuthenticated = authService.Authenticate(username, password); 
         

            if (isAuthenticated) {
                MessageBox.Show("Inicio de sesión exitoso");
                Close();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas");
            } */
        }
    } 
}
