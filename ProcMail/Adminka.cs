using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Windows.Forms;

namespace ProcMail
{
    public partial class Adminka : Form
    {
        
        
        
        public Adminka()
        {
            InitializeComponent();
        }

        SqlCeConnection con;
        BindingSource bindS = new BindingSource();
        

        private void Adminka_Load(object sender, EventArgs e)
        {
            

            SqlCeConnection con = new SqlCeConnection(Properties.Settings.Default.DBConnection);
            string query = "SELECT * FROM AdminTest";
            adap = new SqlCeDataAdapter(query, con);
            SqlCeCommandBuilder cBuilder = new SqlCeCommandBuilder(adap);
            adap.Fill(dataT);

            bindS.DataSource = dataT;
            dataGridView1.DataSource = bindS;
            


        }


        SqlCeDataAdapter adap;
        DataTable dataT = new DataTable();

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlCeConnection(Properties.Settings.Default.DBConnection);
                string query = "SELECT * FROM AdminTest";
                SqlCeDataAdapter adap = new SqlCeDataAdapter(query, con);

                SqlCeCommandBuilder cb = new SqlCeCommandBuilder(adap);


                adap.Update(dataT);
                MessageBox.Show("Информация о пользователях была успешно обновлена", "Обновление Данных", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch { MessageBox.Show("Вы оставили одно из полей пустым, не делайте так!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
        }

        private void btnAdminOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
