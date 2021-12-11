using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursa4
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        public static string connectionString = @"Data Source=DESKTOP-DBO4PFN;Initial Catalog=kursa4;Integrated Security=True";
        public int log;
        public int id_sotr;
        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string pass = textBox2.Text;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmnd = new SqlCommand($"select * from Авторизация where Логин=N'{textBox1.Text}' and Пароль=N'{textBox2.Text}'", connection);
            cmnd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmnd);
            DataTable table = new DataTable("Авторизация");
            adapter.Fill(table);
            dataGridView1.DataSource = table.DefaultView;
            Form6 fm6 = new Form6();
            log = Convert.ToInt32(dataGridView1[2, 0].Value);
            id_sotr = Convert.ToInt32(dataGridView1[3, 0].Value);
            if ( log == 1)
            {
                MessageBox.Show("Авторизация успешна.");
                fm6.log = log;
                fm6.id_sotr = id_sotr;
                fm6.Show();
                this.Hide();
            }
            else if (log > 1)
            {
                MessageBox.Show("Авторизация успешна.");
                fm6.log = log;
                fm6.id_sotr = id_sotr;
                fm6.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Ошибка входа!");
            }
            connection.Close();
        }
    }
}
