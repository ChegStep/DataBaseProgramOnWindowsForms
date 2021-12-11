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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        public int id_zakaz;
        public int id_dostavk;
        public static string connectionString = @"Data Source=DESKTOP-DBO4PFN;Initial Catalog=kursa4;Integrated Security=True";
        private void Form7_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            DataSet k = new DataSet();
            string b = $"Select [id_заказа],[Наименование],[Цена],[Количество] from [Информация о заказе] where id_заказа={id_zakaz}";
            SqlDataAdapter s = new SqlDataAdapter(b, con);
            s.Fill(k);
            dataGridView1.DataSource = k.Tables[0];
            dataGridView1.Columns["id_заказа"].Visible = false;
            if (id_dostavk == 1) label1.Text = "Способ доставки: Самовывоз";
            else if(id_dostavk == 2) label1.Text = "Способ доставки: Грузчиками";
        }
    }
}
