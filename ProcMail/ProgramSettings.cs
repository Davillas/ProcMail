using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcMail.Properties;

namespace ProcMail
{
    public partial class ProgramSettings : Form
    {
        public ProgramSettings()
        {
            InitializeComponent();
            

        }

        //private string[] prevalues = new string[Properties.Settings.Default.Filter.Count];

        private void Settings_Load(object sender, EventArgs e)
        {
            
            Set();
            
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = fbd.SelectedPath;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbPath.Text = String.Empty;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            

            Properties.Settings.Default.Path = tbPath.Text;
            Properties.Settings.Default.SiteName = cbxSite.Text;
            Properties.Settings.Default.MailAdress = tbMail.Text;
            Properties.Settings.Default.MailPassword = tbMailPassword.Text;
            Properties.Settings.Default.Latency = Convert.ToInt32(cbxLatency.Text);
            Properties.Settings.Default.DeleteMails = cbDelete.Checked;
            Properties.Settings.Default.Filter.Clear();
            foreach (string item in lbFilter.Items)
            {
                Properties.Settings.Default.Filter.Add(item);
            }

            Properties.Settings.Default.Save();
            
            this.Hide();

        }
        
        

        

        void Set()
        {
            tbPath.Text = Properties.Settings.Default.Path;
            cbxSite.Text = Properties.Settings.Default.SiteName;
            tbMail.Text = Properties.Settings.Default.MailAdress;
            tbMailPassword.Text = Properties.Settings.Default.MailPassword;
            cbxLatency.Text = Properties.Settings.Default.Latency.ToString();
            cbDelete.Checked = Properties.Settings.Default.DeleteMails;
            //int i = 0;
            foreach(string s in Properties.Settings.Default.Filter)
            {
                if (!lbFilter.Items.Contains(s))
                {
                    lbFilter.Items.Add(s);
                    //prevalues[i] = lbFilter.Items[i].ToString();
                    //i++;
                }
                
            }
            //Properties.Settings.Default.Filter.Clear();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {

            //Set();
            tbPath.Text = Properties.Settings.Default.Path;
            cbxSite.Text = Properties.Settings.Default.SiteName;
            tbMail.Text = Properties.Settings.Default.MailAdress;
            tbMailPassword.Text = Properties.Settings.Default.MailPassword;
            cbxLatency.Text = Properties.Settings.Default.Latency.ToString();
            cbDelete.Checked = Properties.Settings.Default.DeleteMails;

            //foreach (string s in prevalues)
            //{
            //    try
            //    {
            //        if (!lbFilter.Items.Contains(s))
            //        {
            //            Properties.Settings.Default.Filter.Add(s);
            //        }
            //    }
            //    catch { }
            //}
            this.Close();
        }

       

       

        private void btnAdminka_Click(object sender, EventArgs e)
        {
            Adminka adm = new Adminka();
            adm.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbFilter.Text.Contains(".") && !lbFilter.Items.Contains(tbFilter.Text))
            {
                lbFilter.Items.Add(tbFilter.Text);
                tbFilter.Text = "";
            }
            else
                MessageBox.Show("Неверный формат файла. Возможно, вы не указали символ ТОЧКА. Либо элемент уже существует.", "Ошибка Указанного формата", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
           lbFilter.Items.Remove(lbFilter.SelectedItem);
            
        }

        private void lbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
