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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        public static string connectionString = @"Data Source=DESKTOP-DBO4PFN;Initial Catalog=kursa4;Integrated Security=True";

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*------------------- ДОБАВЛЕНИЕ НОВОГО СОРУДНИКА -----------------------*/
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            DataSet b = new DataSet();
            string c = $"use kursa4 INSERT INTO [dbo].[Персонал]([id_должности],[Фамилия],[Имя],[Отчество])VALUES(2,N'{textBox1.Text}',N'{textBox2.Text}',N'{textBox3.Text}')";
            SqlDataAdapter adapter = new SqlDataAdapter(c, con);
            adapter.Fill(b);
            DataSet dt = new DataSet();
            string ins = $"use kursa4 INSERT INTO [dbo].[Авторизация]([Логин],[Пароль],[id_должности])VALUES(N'{textBox4.Text}',N'{textBox5.Text}',2)";
            SqlDataAdapter adp = new SqlDataAdapter(ins, con);
            adp.Fill(dt);
            MessageBox.Show("Сотрудник добавлен");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
