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



namespace project213
{
    public partial class Form6 : Form
    {
        private string connectionString = "Server=PCNG;Database=register;Trusted_Connection=True;";
        private Dictionary<string, List<string>> collegeSpecialties = new Dictionary<string, List<string>>();

        public Form6()
        {
            InitializeComponent();
            // Заполняем данные
            collegeSpecialties.Add("РАНХиГС", new List<string> { "Журналистка", "Инноватика", "История", "Дизайн", "Государственное и муниципальное управление" });
            collegeSpecialties.Add("Энгельсский технологический институт им. Гагарина", new List<string> { "Технология машиностроения", "Информационные системы и программирование","Экономика и бухгалтерский учет", "Операционная деятельность в логистике" });

            // Заполняем комбобокс с колледжами
            comboBoxCollege.Items.AddRange(collegeSpecialties.Keys.ToArray());

            // Добавляем обработчик события
            comboBoxCollege.SelectedIndexChanged += ComboBoxCollege_SelectedIndexChanged;

        }
        private void ComboBoxCollege_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCollege = comboBoxCollege.SelectedItem.ToString();

            // Очищаем комбобокс специальностей
            comboBoxSpecialty.Items.Clear();

            // Заполняем специальностями выбранного колледжа
            if (collegeSpecialties.ContainsKey(selectedCollege))
            {
                comboBoxSpecialty.Items.AddRange(collegeSpecialties[selectedCollege].ToArray());
            }
        }

        private byte[] ImageToByteArray(PictureBox pictureBox)
        {
            if (pictureBox.Image == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                pictureBox.Image.Save(ms, pictureBox.Image.RawFormat);
                return ms.ToArray();
            }
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение выхода", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Завершает приложение

            }

        }

        private void button1_Click(object sender, EventArgs e)

        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                Medicalcertificate.Image == null ||
                Digitalphoto.Image == null ||
                comboBoxCollege.SelectedItem == null || 
                comboBoxSpecialty.SelectedItem == null) 

            {
                MessageBox.Show("Все поля должны быть заполнены и изображения загружены.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Получаем байтовые массивы из изображений
            byte[] medicalCertificateImage = ImageToByteArray(Medicalcertificate);
            byte[] digitalPhotoImage = ImageToByteArray(Digitalphoto);

            SaveData(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text,
        medicalCertificateImage, digitalPhotoImage,
        comboBoxCollege.SelectedItem.ToString(), // Получаем выбранное значение из comboBoxCollege
        comboBoxSpecialty.SelectedItem.ToString()); // Получаем выбранное значение из comboBoxSpecialty
        }

        void SaveData(string Passport, string Snils, string Attestat, string FIO,
     byte[] MedicalCertificate, byte[] DigitalPhoto, string College, string Specialty)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"INSERT INTO Users 
            (FIO, Passport, Snils, Attestat, [Medical certificate], [Digital photo], College, Specialty) 
            VALUES (@FIO, @Passport, @Snils, @Attestat, @MedicalCertificate, @DigitalPhoto, @College, @Specialty)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FIO", FIO);
                        command.Parameters.AddWithValue("@Passport", Passport);
                        command.Parameters.AddWithValue("@Snils", Snils);
                        command.Parameters.AddWithValue("@Attestat", Attestat);
                        command.Parameters.AddWithValue("@MedicalCertificate",
                            (object)MedicalCertificate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DigitalPhoto",
                            (object)DigitalPhoto ?? DBNull.Value);
                        command.Parameters.AddWithValue("@College", College); 
                        command.Parameters.AddWithValue("@Specialty", Specialty); 


                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Ваши данные были отправлены!", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при отправке данных.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }






        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show(); // Открываем Form
            this.Hide();  // Скрываем Form
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Medicalcertificate.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Digitalphoto.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

    
    





       