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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public static string connectionString = @"Data Source=DESKTOP-DBO4PFN;Initial Catalog=kursa4;Integrated Security=True";
        public int id_zakaz;
        public int sum_zakaz = 0;
        public void Form2_Load(object sender, EventArgs e)
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
            string cmd = $"select * from Товар where Наименование=N'{comboBox2.Text}'";
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
            }
            label7.Text = dataGridView1[3, 0].Value + "";//Описание
            label9.Text = dataGridView1[5, 0].Value + "";//Цена
            label4.Text = dataGridView1.Rows[0].Cells[4].Value + "";//Количество
          //  string img = dataGridView1[6, 0].Value + "";//Изображение
            //pictureBox2.Image = Image.FromFile(img);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*=============== ДОБАВЛЕНИЕ ТОВАРА В ЗАКАЗ -------------------*/
            int kolvoVsego = Convert.ToInt32(dataGridView1[4, 0].Value);
            int kolvo = Convert.ToInt32(textBox2.Text);
            if (kolvo > kolvoVsego)
            {
                MessageBox.Show("Ошибка! Товара не хватает! Всего доступно: " + kolvoVsego + "!");
            }
            else
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                DataSet b = new DataSet();
                string ins = $"INSERT INTO [dbo].[Информация о заказе]([id_заказа],[id_товара],[Наименование],[Цена],[Количество])VALUES ({id_zakaz},{dataGridView1[0,0].Value},N'{dataGridView1[2, 0].Value}',{dataGridView1[5,0].Value},{kolvo})";
                SqlDataAdapter adapter1 = new SqlDataAdapter(ins, con);
                adapter1.Fill(b);
                string upd = $"UPDATE [dbo].[Товар]SET [Количество] = Количество-{kolvo} WHERE id_товара={dataGridView1[0, 0].Value}";
                SqlDataAdapter adapter2 = new SqlDataAdapter(upd, con);
                sum_zakaz = sum_zakaz + Convert.ToInt32(dataGridView1[5, 0].Value) * kolvo;
                adapter2.Fill(b);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 fm3 = new Form3();
            fm3.id_zakaz = id_zakaz;
            fm3.sum_zakaz = sum_zakaz;
            fm3.Show();
            this.Hide();
        }
    }
}
