using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project213
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();


        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            password.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var myForm = new Form1();
            myForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkBox2.Checked)
            {
                MessageBox.Show("Необходимо согласиться с обработкой персональных данных!",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(fio.Text) ||
                string.IsNullOrWhiteSpace(login.Text) ||
                string.IsNullOrWhiteSpace(password.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!");
                return;
            }

            using (SqlConnection con = new SqlConnection(@"Data Source=PCNG;Initial Catalog=register;Integrated Security=True"))
            {
                try
                {
                    con.Open();

                    // Проверка существующего логина, ФИО и пароля
                    string checkQuery = "SELECT COUNT(*) FROM Students WHERE login = @login OR fio = @fio OR pass = @pass";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, con))
                    {
                        checkCommand.Parameters.Add("@login", SqlDbType.VarChar).Value = login.Text;
                        checkCommand.Parameters.Add("@fio", SqlDbType.VarChar).Value = fio.Text;
                        checkCommand.Parameters.Add("@pass", SqlDbType.VarChar).Value = password.Text;

                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("Пользователь с таким логином, ФИО или паролем уже существует!");
                            return;
                        }
                    }

                    // Получаем RoleId для роли "Студент"
                    int roleId;
                    using (SqlCommand roleCommand = new SqlCommand("SELECT RoleId FROM Roles WHERE RoleN = @RoleN", con))
                    {
                        roleCommand.Parameters.Add("@RoleN", SqlDbType.VarChar).Value = "Студент";
                        object result = roleCommand.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("Роль 'Студент' не найдена в базе данных.");
                            return;
                        }

                        roleId = Convert.ToInt32(result);
                    }

                    // Вставляем нового пользователя с RoleId
                    using (SqlCommand command = new SqlCommand("INSERT INTO Students (login, pass, fio, RoleId) VALUES (@login, @pass, @fio, @RoleId)", con))
                    {
                        command.Parameters.Add("@login", SqlDbType.VarChar).Value = login.Text;
                        command.Parameters.Add("@pass", SqlDbType.VarChar).Value = password.Text;
                        command.Parameters.Add("@fio", SqlDbType.VarChar).Value = fio.Text;
                        command.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleId;

                        command.ExecuteNonQuery();
                        MessageBox.Show("Новый пользователь добавлен!");

                        Form4 frm = new Form4();
                        frm.Show();
                        this.Hide();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при работе с базой данных: {ex.Message}");
                }
            }
        }
            
        
   
   
            
        

        private void password_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Location = new Point(10, 420);
            Size = new Size(400, 20);
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var agreementForm = new Form5())
            {
                agreementForm.ShowDialog();
            }
        }
    }
    }

