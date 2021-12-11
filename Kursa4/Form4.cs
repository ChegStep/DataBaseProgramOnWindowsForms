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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        public int sum_zakaz;
        public int id_zakaz;
        public static string connectionString = @"Data Source=DESKTOP-DBO4PFN;Initial Catalog=kursa4;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {
            /*--------------------- ОФОРМЛЕНИЕ ЗАКАЗА -----------------------------*/
            string ins;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            DataSet b = new DataSet();
            if (comboBox1.SelectedIndex + 1 == 2)
            {
                sum_zakaz += 1500;
                ins = $"UPDATE [dbo].[Заказ] SET [Имя заказчика] = N'{textBox1.Text}',[id_доставки] = {comboBox1.SelectedIndex + 1},[Адрес] = N'{textBox3.Text}',[Телефон] = {textBox2.Text},[Дата] = N'{Convert.ToString(DateTime.Now)}',[Сумма] = {sum_zakaz},[Актуальность] = 1, [email] = N'{textBox4.Text}' WHERE id_заказа={id_zakaz}";
            }
            else {
                ins = $"UPDATE [dbo].[Заказ] SET [Имя заказчика] = N'{textBox1.Text}',[id_доставки] = {comboBox1.SelectedIndex + 1},[Телефон] = {textBox2.Text},[Дата] = N'{Convert.ToString(DateTime.Now)}',[Сумма] = {sum_zakaz},[Актуальность] = 1, [email] = N'{textBox4.Text}' WHERE id_заказа={id_zakaz}";
            }
            SqlDataAdapter adapter1 = new SqlDataAdapter(ins, con);
            adapter1.Fill(b);
            MessageBox.Show("Заказ принят. Как тольео заказ будет готов, вам на почту придёт письмо. Спасибо, что выбрали нас!");
            Form2 fm2 = new Form2();
            fm2.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursa4DataSet.Доставка". При необходимости она может быть перемещена или удалена.
            this.доставкаTableAdapter.Fill(this.kursa4DataSet.Доставка);

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex + 1 == 1)
            {
                label4.Visible = false;
                textBox3.Visible = false;
            }
            else
            {
                label4.Visible = true;
                textBox3.Visible = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 fm3 = new Form3();
            fm3.Show();
            this.Hide();
        }
    }
}
