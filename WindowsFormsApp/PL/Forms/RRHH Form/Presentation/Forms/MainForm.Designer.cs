namespace RRHH_Form
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEmployees = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnTeamView = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEmployees
            // 
            this.btnEmployees.Location = new System.Drawing.Point(50, 49);
            this.btnEmployees.Name = "btnEmployees";
            this.btnEmployees.Size = new System.Drawing.Size(140, 70);
            this.btnEmployees.TabIndex = 0;
            this.btnEmployees.Text = "Gestión de Empleados";
            this.btnEmployees.UseVisualStyleBackColor = true;
            this.btnEmployees.Click += new System.EventHandler(this.btnEmployees_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(218, 49);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(140, 70);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Búsqueda";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(50, 150);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(140, 70);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnTeamView
            // 
            this.btnTeamView.Location = new System.Drawing.Point(218, 150);
            this.btnTeamView.Name = "btnTeamView";
            this.btnTeamView.Size = new System.Drawing.Size(140, 70);
            this.btnTeamView.TabIndex = 3;
            this.btnTeamView.Text = "Equipo";
            this.btnTeamView.UseVisualStyleBackColor = true;
            this.btnTeamView.Click += new System.EventHandler(this.btnTeamView_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 288);
            this.Controls.Add(this.btnTeamView);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnEmployees);
            this.Name = "MainForm";
            this.Text = "Administador de CVs";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEmployees;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnTeamView;
    }
}

