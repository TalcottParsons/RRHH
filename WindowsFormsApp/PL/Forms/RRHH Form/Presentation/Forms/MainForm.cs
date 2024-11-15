using RRHH_Form.Presentation.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RRHH_Form
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent(); 
        }

        //Eventos click para conectar los formularios al main form
        private void btnEmployees_Click(object sender, EventArgs e)
        {
            EmployeeForm employeeForm = new EmployeeForm();
            employeeForm.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm();
            searchForm.ShowDialog();
        }

        private void btnTeamView_Click(object sender, EventArgs e)
        {
            TeamForm teamForm = new TeamForm();
            teamForm.ShowDialog();
        }
    }

}

