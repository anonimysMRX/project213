using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace project213
{
    public partial class Form4 : Form
    {


        public Form4()
        {
            InitializeComponent();


        }

        private void Form4_Load(object sender, EventArgs e)
        {
            

            SqlConnection con = new SqlConnection(@"Data Source=PCNG;Initial Catalog=register;Integrated Security=True");
            string Sql = $"select fio from Students where login = '{DataSend.text}'";
            SqlCommand scmd = new SqlCommand(Sql, con);
            con.Open();
            SqlDataReader sur = scmd.ExecuteReader();
            while (sur.Read())
            {
                string fio = sur["fio"].ToString();
                label1.Text = fio;
            }
            con.Close();

            info dbHelper = new info();
            string query = "SELECT * FROM Statu"; // Замените на ваш запрос
            DataTable data = dbHelper.GetData(query);

            dataGridView1.DataSource = data; // Предполагая, что вы добавили DataGridView на форму

        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form6 form6 = new Form6();

            form6.Show();

            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение выхода", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Завершает приложение
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=PCNG;Initial Catalog=register;Integrated Security=True"))
            {
                // Запрос на фио
                string Sql = $"select FIO from Students where login = '{DataSend.text}'";
                SqlCommand scmd = new SqlCommand(Sql, con);

                try
                {
                    con.Open();
                    SqlDataReader sur = scmd.ExecuteReader();

                    // читает и обновляет
                    if (sur.Read())
                    {
                        string fio = sur["FIO"].ToString();
                        label1.Text = fio;
                    }
                    sur.Close(); // закрывает датагридвиев
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }

            // обновляет датагридвиев
            info dbHelper = new info();
            string query = "SELECT * FROM Statu"; // Замените на ваш запрос
            DataTable data = dbHelper.GetData(query);

            dataGridView1.DataSource = data; // Обновляем DataGridView
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show(); // Открываем Form
            this.Hide();  // Скрываем Form
        }
    }
}




                

            
       
    

    


       
       