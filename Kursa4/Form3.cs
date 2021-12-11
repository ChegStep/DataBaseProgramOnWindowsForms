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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public int id_zakaz;
        public int sum_zakaz;
        public static string connectionString = @"Data Source=DESKTOP-DBO4PFN;Initial Catalog=kursa4;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 fm4 = new Form4();
            fm4.Show();
            fm4.sum_zakaz = sum_zakaz;
            fm4.id_zakaz = id_zakaz;
            this.Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label2.Text = "Сумма заказа: " + sum_zakaz.ToString();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            DataSet k = new DataSet();
            string b = $"Select [id_заказа],[Наименование],[Цена],[Количество] from [Информация о заказе] where id_заказа={id_zakaz}";
            SqlDataAdapter s = new SqlDataAdapter(b, con);
            s.Fill(k);
            dataGridView1.DataSource = k.Tables[0];
            dataGridView1.Columns["id_заказа"].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*-------------------------- УДАЛЕНИЕ ТОВАРА ИЗ КОРЗИНЫ ---------------------*/
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            DataSet k = new DataSet();
            string b = $"Delete from [Информация о заказе] where id_заказа={id_zakaz} and Наименование=N'{dataGridView1[1,0].Value}'";
            SqlDataAdapter s = new SqlDataAdapter(b, con);
            s.Fill(k);
            sum_zakaz = sum_zakaz - Convert.ToInt32(dataGridView1[2, 0].Value) * Convert.ToInt32(dataGridView1[3, 0].Value);
            label2.Text = "Сумма заказа: " + sum_zakaz.ToString();
            MessageBox.Show("Товар: " + dataGridView1[1, 0].Value + " удалён из корзины");
            string del = $"Select [id_заказа],[Наименование],[Цена],[Количество] from [Информация о заказе] where id_заказа={id_zakaz}";
            SqlDataAdapter adp = new SqlDataAdapter(del, con);
            adp.Fill(k);
            dataGridView1.DataSource = k.Tables[0];
            dataGridView1.Columns["id_заказа"].Visible = false;
        }
    }
}
