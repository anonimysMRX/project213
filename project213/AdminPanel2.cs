using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project213
{
    public partial class AdminPanel2 : Form
    {
        public AdminPanel2()
        {
            InitializeComponent();

        }
        private string connectionString = @"Data Source=PCNG;Initial Catalog=register;Integrated Security=True";

        private void button5_Click(object sender, EventArgs e)
        {
            FormAdmina FormAdmina = new FormAdmina();

            FormAdmina.Show();

            this.Close();
        }
        private string GetStatusFromDatabase(string fio)
        {
            string status = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Stat FROM Statu WHERE FIO = @FIO";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FIO", fio); // Используйте FIO как параметр

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            status = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении статуса: " + ex.Message,
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return status;
        }


        private void button1_Click(object sender, EventArgs e)

        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Предполагаем, что у вас есть TextBox с ID записи
                    string query = "SELECT [Medical certificate], [Digital photo] FROM Users WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", textBoxId.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Загрузка первого изображения (Medical certificate)
                                if (!reader.IsDBNull(0))
                                {
                                    byte[] imageData1 = (byte[])reader[0];
                                    using (MemoryStream ms = new MemoryStream(imageData1))
                                    {
                                        pictureBox1.Image = Image.FromStream(ms);
                                    }
                                }

                                // Загрузка второго изображения (Digital photo)
                                if (!reader.IsDBNull(1))
                                {
                                    byte[] imageData2 = (byte[])reader[1];
                                    using (MemoryStream ms = new MemoryStream(imageData2))
                                    {
                                        pictureBox2.Image = Image.FromStream(ms);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Запись не найдена", "Информация",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке изображений: " + ex.Message,
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Получаем ID из textBoxId
            string id = textBoxId.Text;

            // Проверяем, что ID не пустой
            if (!string.IsNullOrWhiteSpace(id))
            {
                // Получаем статус из базы данных и обновляем label2
                string fio = GetFIOById(id);
                if (!string.IsNullOrEmpty(fio))
                {
                    string status = GetStatusFromDatabase(fio);
                    label2.Text = status; // Обновляем label2 с полученным статусом
                }
                else
                {
                    label2.Text = "Студент не найден";
                }
            }
            else
            {
                label2.Text = string.Empty; // Очищаем label2, если ID пустой
            }
        }
        
        private string GetFIOById(string id)
        {
            string fio = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT FIO FROM Users WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            fio = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении FIO: " + ex.Message,
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return fio;
        }

        private void AdminPanel2_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
    }

