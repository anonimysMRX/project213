using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project213
{
    public partial class FormAdmina : Form
    {

        public FormAdmina()
        {


            InitializeComponent();

            using (SqlConnection con = new SqlConnection(@"Data Source=PCNG;Initial Catalog=register;Integrated Security=True"))
            {
                
                string query = "SELECT Stat, FIO FROM Statu";

              
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                   
                    con.Open();

                    
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();

                   
                    da.Fill(dataTable);

                   
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                   
                    MessageBox.Show("Ошибка: " + ex.Message);

                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void FormAdmina_Load(object sender, EventArgs e)

        {

            LoadData(); 
        }
        private string connectionString = @"Data Source=PCNG;Initial Catalog=register;Integrated Security=True";
        private SqlDataAdapter adapter;
        private DataTable table;


        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT Id, FIO, Passport, Snils, Attestat, College, Specialty,
                           CASE WHEN [Medical certificate] IS NOT NULL 
                                THEN 'Да' ELSE 'Нет' END AS HasMedicalCertificate,
                           CASE WHEN [Digital photo] IS NOT NULL 
                                THEN 'Да' ELSE 'Нет' END AS HasDigitalPhoto 
                           FROM Users";

                    adapter = new SqlDataAdapter(query, connection);
                    table = new DataTable();
                    adapter.Fill(table);

                    dataGridView2.DataSource = table;
                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    dataGridView2.Columns["Id"].HeaderText = "ID";
                    dataGridView2.Columns["FIO"].HeaderText = "ФИО";
                    dataGridView2.Columns["Passport"].HeaderText = "Паспорт";
                    dataGridView2.Columns["Snils"].HeaderText = "СНИЛС";
                    dataGridView2.Columns["Attestat"].HeaderText = "Аттестат";
                    dataGridView2.Columns["HasMedicalCertificate"].HeaderText = "Мед. справка";
                    dataGridView2.Columns["HasDigitalPhoto"].HeaderText = "Фото";
                    dataGridView2.Columns["College"].HeaderText = "Колледж";
                    dataGridView2.Columns["Specialty"].HeaderText = "Специальность";


                    // ID только для чтения
                    dataGridView2.Columns["Id"].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            string connectionString = @"Data Source=PCNG;Initial Catalog=register;Integrated Security=True";

           
            string query = "SELECT Id, FIO, Passport, Snils, Attestat FROM Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                   
                    DataTable table = new DataTable();

                   
                    adapter.Fill(table);

                    
                    dataGridView2.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение выхода", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            try
            {
                string connectionString = @"Data Source=PCNG;Initial Catalog=register;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                   
                    string query = "SELECT Stat, FIO FROM Statu";

                  
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        
                        DataTable dataTable = new DataTable();

                       
                        adapter.Fill(dataTable);

                       
                        dataGridView1.DataSource = dataTable;
                    }
                }

                MessageBox.Show("Данные успешно обновлены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
            }
        }
    
            
        
        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show(); 
            this.Hide();  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=PCNG;Initial Catalog=register;Integrated Security=True"))
                {
                    connection.Open();

                   
                    string deleteQuery = "DELETE FROM Statu";
                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.ExecuteNonQuery();
                    }

                    
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        
                        if (!row.IsNewRow)
                        {
                            string stat = row.Cells["Stat"].Value?.ToString() ?? "";
                            string fio = row.Cells["FIO"].Value?.ToString() ?? "";

                            string insertQuery = "INSERT INTO Statu (Stat, FIO) VALUES (@Stat, @FIO)";
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@Stat", stat);
                                insertCommand.Parameters.AddWithValue("@FIO", fio);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show("Данные успешно сохранены!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении данных: " + ex.Message);
            }
        }







        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



        }



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow newRow = table.NewRow();
                table.Rows.Add(newRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении записи: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.CurrentRow != null)

                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                     
                        DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить эту запись?",
                            "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            int id = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
                            string connectionString = @"Data Source=PCNG;Initial Catalog=register;Integrated Security=True";

                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                string query = "DELETE FROM Users WHERE ID = @ID";

                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@ID", id);
                                    command.ExecuteNonQuery();
                                }
                            }

                            dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[0].Index);
                            MessageBox.Show("Запись успешно удалена!");



                        }
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите строку для удаления.");
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)

        {
            using (SqlConnection con = new SqlConnection(@"Data Source=PCNG;Initial Catalog=register;Integrated Security=True"))
            {
                string sql = @"SELECT Id, FIO, Passport, Snils, Attestat, College, Specialty,
                   CASE WHEN [Medical certificate] IS NOT NULL THEN 'Да' ELSE 'Нет' END AS MedicalCertificate,
                   CASE WHEN [Digital photo] IS NOT NULL THEN 'Да' ELSE 'Нет' END AS DigitalPhoto 
                   FROM Users";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    adapter.Fill(dt);
                    dataGridView2.DataSource = dt;

              
                    dataGridView2.Columns["Id"].HeaderText = "ID";
                    dataGridView2.Columns["FIO"].HeaderText = "ФИО";
                    dataGridView2.Columns["Passport"].HeaderText = "Паспорт";
                    dataGridView2.Columns["Snils"].HeaderText = "СНИЛС";
                    dataGridView2.Columns["Attestat"].HeaderText = "Аттестат";
                    dataGridView2.Columns["MedicalCertificate"].HeaderText = "Мед. справка";
                    dataGridView2.Columns["DigitalPhoto"].HeaderText = "Фото";
                    dataGridView2.Columns["College"].HeaderText = "Колледж";
                    dataGridView2.Columns["Specialty"].HeaderText = "Специальность";

                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                  
                    dataGridView2.Columns["MedicalCertificate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView2.Columns["DigitalPhoto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    MessageBox.Show("Данные успешно обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    string connectionString = @"Data Source=PCNG;Initial Catalog=register;Integrated Security=True";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            string stat = row.Cells["Stat"].Value.ToString();
                            string fio = row.Cells["FIO"].Value.ToString();

                            string query = "DELETE FROM Statu WHERE Stat = @Stat AND FIO = @FIO";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Stat", stat);
                                command.Parameters.AddWithValue("@FIO", fio);
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    // удаляем эт строки из датагрид
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows.Remove(row);
                    }

                    MessageBox.Show("Выбранные записи успешно удалены!");
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строки для удаления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            AdminPanel2 AdminPanel2 = new AdminPanel2();

            AdminPanel2.Show();

            this.Close();
        }
    }
}
        

    

                







