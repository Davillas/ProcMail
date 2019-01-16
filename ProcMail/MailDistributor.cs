using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcMail
{

    public partial class MailDistributor : Form

    {
        

        public MailDistributor(string role)
        {
            InitializeComponent();
            lblStatus.Text = role;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(1);
        }

        private void MailDistributor_Load(object sender, EventArgs e)
        {
            if(Controls["lblStatus"].Text != "Admin")
            {
                //btnSettings.Visible = false;
                btnSettings.Enabled = false;
            }

            lblStatus.Text = "Вы авторизовались как : " + Controls["lblStatus"].Text;
        }

        
        private void btnSettings_Click(object sender, EventArgs e)
        {
            ProgramSettings st = new ProgramSettings();
            st.Show();
        }

        private void btnStartProgram_Click(object sender, EventArgs e)
        {
            this.Hide();
            WorkingProgram wp = new WorkingProgram();
            wp.Show();
            //MainProgram.RunProgram();
        }

        private void MailDistributor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
