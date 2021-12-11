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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int id_zakaz;
        public static string connectionString = @"Data Source=DESKTOP-DBO4PFN;Initial Catalog=kursa4;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            Random rnd = new Random();
            f2.id_zakaz = rnd.Next(1000,9999);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            DataSet b = new DataSet();
            string c = $"use kursa4 INSERT INTO [dbo].[Заказ]([id_заказа])VALUES({f2.id_zakaz})";
            SqlDataAdapter adapter = new SqlDataAdapter(c, con);
            adapter.Fill(b);
            f2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 fm5 = new Form5();
            fm5.Show();
            this.Hide();
        }
    }
}
