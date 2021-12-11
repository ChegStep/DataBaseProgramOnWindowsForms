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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        public static string connectionString = @"Data Source=DESKTOP-DBO4PFN;Initial Catalog=kursa4;Integrated Security=True";
        public int id_tovara;
        private void button1_Click(object sender, EventArgs e)
        {
            int kolvo = Convert.ToInt32(textBox1.Text);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            DataSet b = new DataSet();
            string ins = $"INSERT INTO [dbo].[Приход]([id_товара],[Дата],[Количество])VALUES ({id_tovara},N'{DateTime.Now.ToString()}',{kolvo})";
            SqlDataAdapter adapter1 = new SqlDataAdapter(ins, con);
            adapter1.Fill(b);
            string upd = $"UPDATE [dbo].[Товар]SET [Количество] = Количество+{kolvo} WHERE id_товара={id_tovara}";
            SqlDataAdapter adapter2 = new SqlDataAdapter(upd, con);
            adapter2.Fill(b);
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            /*=============== КОМБОБОКС ДЛЯ ВЫБОРА ТИП МЕБЕЛИ -------------------*/
            string queryString1 = $"use kursa4 select * from [Тип товара]";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd1 = new SqlCommand(queryString1, connection);
                DataTable tbl1 = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(tbl1);
                this.comboBox1.DataSource = tbl1;
                this.comboBox1.DisplayMember = "Наименование";// столбец для отображения
                this.comboBox1.ValueMember = "id_типа";//столбец с id
                this.comboBox1.SelectedIndex = -1;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string queryString2 = $"use kursa4 select * from Товар where id_типа={this.comboBox1.SelectedIndex + 1}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd2 = new SqlCommand(queryString2, connection);
                DataTable tbl2 = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd2);
                da.Fill(tbl2);

                this.comboBox2.DataSource = tbl2;
                this.comboBox2.DisplayMember = "Наименование";// столбец для отображения
                this.comboBox2.ValueMember = "id_товара";//столбец с id
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*=============== КОМБОБОКС ДЛЯ ВЫБОРА МЕБЕЛИ -------------------*/
            string cmd = $"select [id_товара] from Товар where Наименование=N'{comboBox2.Text}'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand createCommand = new SqlCommand(cmd, connection);
                createCommand.ExecuteNonQuery();
                SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
                DataTable dt = new DataTable("Товар");
                dataAdp.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
                connection.Close();
                id_tovara = Convert.ToInt32(dataGridView1[0, 0].Value);
            }
        }
    }
}
