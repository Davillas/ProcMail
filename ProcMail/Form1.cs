using System;
using System.Data;
using System.Windows.Forms;

using System.Data.SqlServerCe;

namespace ProcMail
{
    public partial class MainForm : Form
    {
        static int attempt = 5;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           
            
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            if(attempt == 0)
            {
                lblTrials.Text = "Вам не удалось взломать простейшую защиту этой программы. Свяжитесь с Администратором";
                return;
            }
            SqlCeConnection con = new SqlCeConnection(Properties.Settings.Default.DBConnection);
            SqlCeDataAdapter sda = new SqlCeDataAdapter("Select Role from AdminTest where Username='" + tbLogin.Text + "' and Password='" + tbPassword.Text + "' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                this.Hide();
                
                MailDistributor mb = new MailDistributor(dt.Rows[0][0].ToString());
                mb.Show();
            }
            else if (attempt == 5)
            {
                lblTrials.Text = "Первый блин комом. У вас осталось 4 попытки.";
                --attempt;
                tbLogin.Clear();
                tbPassword.Clear();
            }
            else if (attempt == 4)
            {
                lblTrials.Text = "Без паники. Сосредоточьтесь на пароле, проверьте язык и Caps Lock, и у вас все получится. У вас осталось 3 попытки";
                --attempt;
                tbLogin.Clear();
                tbPassword.Clear();
            }
            else if (attempt == 3)
            {
                lblTrials.Text = "Спорим, что вы не сможете открыть программу за оставшиеся пару попыток?";
                --attempt;
                tbLogin.Clear();
                tbPassword.Clear();
            }
            else if (attempt == 2)
            {
                lblTrials.Text = "Согласно статистике, люди, не набравшие верный пароль за три попытки, очень плохие хакеры.";
                --attempt;
                tbLogin.Clear();
                tbPassword.Clear();
            }
            else if (attempt == 1)
            {
                lblTrials.Text = "Официально разрешаю вам паниковать. Однако же предоставлю последнюю попытку";
                --attempt;
                tbLogin.Clear();
                tbPassword.Clear();
            }
        }
    }
}
