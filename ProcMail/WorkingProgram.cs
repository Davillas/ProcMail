using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcMail
{
    public partial class WorkingProgram : Form
    {
        public WorkingProgram()
        {
            InitializeComponent();

            notIconTray.Visible = true;
            this.notIconTray.MouseDoubleClick += new MouseEventHandler(notIconTray_MouseDoubleClick);
            this.Resize += new System.EventHandler(this.WorkingProgram_Resize);


            bwWorking.DoWork += bwWorking_DoWork;
            bwWorking.ProgressChanged += bwWorking_ProgressChanged;
            bwWorking.WorkerReportsProgress = true;
            bwWorking.WorkerSupportsCancellation = true;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            

                lblWorking.Text = "Программа выполняет свою работу";
            if (cbMinimize.Checked)
            {
                WindowState = FormWindowState.Minimized;
            }
            
            try
            {
                bwWorking.RunWorkerAsync();
                
            }
            catch { }
                
            
        }

        private void SetBalloonTip()
        {
            this.Click += new EventHandler(WorkingProgram_Click);
        }

        private void WorkingProgram_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void bwWorking_DoWork(object sender, DoWorkEventArgs e)
        {

            
                while (!bwWorking.CancellationPending)
                {
                    if (bwWorking.CancellationPending == true)
                    {
                        e.Cancel = true;
                        return;
                    }
                MainProgram.RunProgram();
                if(MainProgram.notification == true)
                {
                    notIconTray.ShowBalloonTip(30, "Уведомление", "Письмо было отклонено так как содержало потенциально вредоносные файлы.", ToolTipIcon.Warning);
                    MainProgram.notification = false;
                }
                Thread.Sleep(MainProgram.latency * 1000);
                
                }
            
        }

        private void bwWorking_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        

        

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void WorkingProgram_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnStop_Click_1(object sender, EventArgs e)
        {
            lblWorking.Text = "Программа остановлена, но не сломлена";
            bwWorking.CancelAsync();
        }

        private void WorkingProgram_Resize(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notIconTray.Visible = true;
            }
        }

        private void notIconTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notIconTray.Visible = false;
            this.ShowInTaskbar = true;

            WindowState = FormWindowState.Normal;
        }

        private void cbMinimize_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MinimizeInTray = cbMinimize.Checked;
            Properties.Settings.Default.Save();
        }

        private void WorkingProgram_Load(object sender, EventArgs e)
        {
            cbMinimize.Checked = Properties.Settings.Default.MinimizeInTray;
        }
    }
}
