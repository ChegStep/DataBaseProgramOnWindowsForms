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
using System.Net.Mail;
using System.Net;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Kursa4
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        public static string connectionString = @"Data Source=DESKTOP-DBO4PFN;Initial Catalog=kursa4;Integrated Security=True";
        public int log;
        public int id_sotr;
        public int id_zakaz;
        public int id_dostavk;
        private void Form6_Load(object sender, EventArgs e)
        {
            if (log == 2)
            {
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage3);
                button3.Visible = false;
            }
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            DataSet k = new DataSet();
            string b = $"SELECT [id_заказа],[Имя заказчика],[id_доставки],[Адрес],[Телефон],[Дата],[Сумма],[Актуальность]FROM [dbo].[Заказ] where Актуальность=1";
            SqlDataAdapter s = new SqlDataAdapter(b, con);
            s.Fill(k);
            dataGridView1.DataSource = k.Tables[0];
            if (log == 2) button3.Visible = false;
            DataSet sotr = new DataSet();
            string selSotr = $"SELECT [id_должности],[Фамилия], [Имя], [Отчество] FROM [dbo].[Персонал]";
            SqlDataAdapter adpSotr = new SqlDataAdapter(selSotr, con);
            adpSotr.Fill(sotr);
            dataGridView2.DataSource = sotr.Tables[0];
            DataSet tovar = new DataSet();
            string selTov = $"SELECT * FROM [dbo].[Товар]";
            SqlDataAdapter adpTov = new SqlDataAdapter(selTov, con);
            adpTov.Fill(tovar);
            dataGridView3.DataSource = tovar.Tables[0];

        }

        public void button2_Click(object sender, EventArgs e)
        {
            id_zakaz = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            id_dostavk = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
            Form7 fm7 = new Form7();
            fm7.id_zakaz = id_zakaz;
            fm7.id_dostavk = id_dostavk;
            fm7.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*---------------- СНЯТИЕ ЗАКАЗА С АКТИВНОЙ ПОЗИЦИИ -------------------*/
            id_zakaz = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            SqlConnection con = new SqlConnection(connectionString);
            DataSet b = new DataSet();
            string ins = $"UPDATE [dbo].[Заказ] SET [id_сотрудника] = {id_sotr},[Актуальность] = 0 WHERE id_заказа={id_zakaz}";
            SqlDataAdapter adapter1 = new SqlDataAdapter(ins, con);
            adapter1.Fill(b);
            /*---------------- ОТПРАВКА ЭЛЕКТРОННОГО ПИСЬМА НА ПОЧТУ -------------------*/
            //MailAddress from = new MailAddress("stepanch601@gmail.com", "Мебельная компания");
            //MailAddress to = new MailAddress("stepan.cheg@mail.ru");
            //MailMessage m = new MailMessage(from, to);
            //m.Subject = "Ваш заказ готов и ожидает вас";
            //m.Body = "Здравствуйте, это мебельный магазин, ваш заказ готов и уже ожидает вас!";
            //m.IsBodyHtml = true;
            //SmtpClient smtp = new SmtpClient("smtp.gmail.ru", 587);
            //smtp.Credentials = new NetworkCredential("stepanch601@gmail.com", "stepan2709");
            //smtp.EnableSsl = true;
            // smtp.Send(m);
            ///*---------------- ОФОРМЛЕНИЕ ЧЕКА -------------------*/
            //string pathDocument = AppDomain.CurrentDomain.BaseDirectory + "Чек.docx";
            //DocX document = DocX.Create(pathDocument);
            //document.InsertParagraph("" +
            //    "ООО 'FURNITURE' " +
            //    "\nДобро пожаловать " +
            //    "\nККМ 00075411 #3969 " +
            //    "\nИНН 1087746942040 " +
            //    "\nЭКЛЗ 3851495566 " +
            //    "\nЧек № 1" +
            //    "\n" + DateTime.Now + " СИС.").
            //         Font("Times New Roman").
            //         FontSize(12).
            //         Color(Color.BlueViolet).
            //         Alignment = Alignment.left;
            //Table table = document.AddTable(4, 3);
            //table.Alignment = Alignment.left;
            //table.Design = TableDesign.TableGrid;
            //table.Rows[0].Cells[0].Paragraphs[0].Append("Наименование товара").
            //    Bold(). Color(Color.BlueViolet).FontSize(12).Alignment = Alignment.right;
            //table.Rows[0].Cells[1].Paragraphs[0].Append("Количество").
            //    Bold().Color(Color.BlueViolet).FontSize(12).Alignment = Alignment.right;
            //table.Rows[0].Cells[2].Paragraphs[0].Append("Цена").
            //    Bold().Color(Color.BlueViolet).FontSize(12). Alignment = Alignment.right;
            //table.Rows[1].Cells[0].Paragraphs[0].Append("Стол 'Кухонный'").
            //    FontSize(12).Color(Color.BlueViolet). Alignment = Alignment.right;
            //table.Rows[1].Cells[1].Paragraphs[0].Append("2").
            //    FontSize(12).Color(Color.BlueViolet).Alignment = Alignment.right;
            //table.Rows[1].Cells[2].Paragraphs[0].Append("1500").
            //    Color(Color.BlueViolet).FontSize(12).Alignment = Alignment.right;
            //table.Rows[2].Cells[0].Paragraphs[0].Append("Шкаф 'Большой'").
            //    FontSize(12).Color(Color.BlueViolet).Alignment = Alignment.right;
            //table.Rows[2].Cells[1].Paragraphs[0].Append("1").
            //    Color(Color.BlueViolet).FontSize(12). Alignment = Alignment.right;
            //table.Rows[2].Cells[2].Paragraphs[0].Append("3000").
            //    FontSize(12).Color(Color.BlueViolet). Alignment = Alignment.right;
            //table.Rows[3].Cells[0].Paragraphs[0].Append("Итого").
            //    Color(Color.BlueViolet). FontSize(12).Alignment = Alignment.right;
            //table.Rows[3].Cells[1].Paragraphs[0].Append("6000").
            //    FontSize(12).Color(Color.BlueViolet).Alignment = Alignment.right;
            //table.Rows[3].MergeCells(1, 2);
            //document.InsertParagraph().InsertTableAfterSelf(table);
            //document.InsertParagraph();
            //document.InsertParagraph("************************").
            //    Color(Color.BlueViolet).
            //    FontSize(12).
            //    Alignment = Alignment.left;
            //document.InsertParagraph("00003751#059705").
            //    Color(Color.BlueViolet).
            //    FontSize(12).
            //    Alignment = Alignment.left;
            //document.Save();
            MessageBox.Show("Заказ закрыт.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*---------------- УДАЛЕНИЕ ЗАКАЗА -------------------*/
            id_zakaz = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            DataSet k = new DataSet();
            string b = $"Delete from [Информация о заказе] where id_заказа={id_zakaz}";
            SqlDataAdapter s = new SqlDataAdapter(b, con);
            s.Fill(k);
            DataSet del = new DataSet();
            string del1 = $"Delete from [Заказ] where id_заказа={id_zakaz}";
            SqlDataAdapter adpdel = new SqlDataAdapter(del1, con);
            adpdel.Fill(del);
            Form6_Load(sender, e);
            MessageBox.Show("Заказ удалён!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form8 fm8 = new Form8();
            fm8.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form5 fm5 = new Form5();
            fm5.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form9 fm9 = new Form9();
            fm9.Show();
        }
    }
}
