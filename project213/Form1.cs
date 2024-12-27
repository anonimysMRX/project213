using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace project213
{
    public partial class Form1 : Form
    {



        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            password.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {


            var myForm = new Form2();
            myForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(login.Text) || string.IsNullOrWhiteSpace(password.Text))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль!");
                return; // Выход из метода

            }

            string connectionString = @"Data Source=PCNG;Initial Catalog=register;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Изменяем запрос, чтобы получить RoleId
                SqlCommand command = new SqlCommand("SELECT RoleId FROM Students WHERE login = @login AND pass = @pass", con);
                command.Parameters.Add("@login", SqlDbType.VarChar).Value = login.Text;
                command.Parameters.Add("@pass", SqlDbType.VarChar).Value = password.Text;

                // Получаем RoleId
                object roleIdObj = command.ExecuteScalar();

                if (roleIdObj != null)
                {
                    // Пользователь найден, открываем профиль в зависимости от роли
                    int roleId = Convert.ToInt32(roleIdObj);

                    // Теперь получаем название роли
                    SqlCommand roleCommand = new SqlCommand("SELECT RoleN FROM Roles WHERE RoleId = @RoleId", con);
                    roleCommand.Parameters.Add("@roleId", SqlDbType.Int).Value = roleId;

                    object roleNameObj = roleCommand.ExecuteScalar();

                    if (roleNameObj != null)
                    {
                        string roleName = roleNameObj.ToString();

                        switch (roleName)
                        {
                            case "Студент":
                                Form4 frmStudent = new Form4();
                                frmStudent.Show();
                                this.Hide();
                                break;

                            case "Админ":
                                FormAdmina frmAdmin = new FormAdmina();
                                frmAdmin.Show();
                                this.Hide();
                                break;

                            default:
                                MessageBox.Show("Неизвестная роль.");
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось определить роль пользователя.");
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            }
        }



        private void login_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
