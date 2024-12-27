using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project213
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            this.Text = "Соглашение на обработку персональных данных";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            RichTextBox richTextBox = new RichTextBox
            {
                Location = new Point(10, 10),
                Size = new Size(565, 400),
                ReadOnly = true
            };
            richTextBox.Text = @"СОГЛАСИЕ НА ОБРАБОТКУ ПЕРСОНАЛЬНЫХ ДАННЫХ

Я, субъект персональных данных, в соответствии с Федеральным законом от 27 июля 2006 года № 152 «О персональных данных» предоставляю согласие на обработку моих персональных данных.

Цель обработки персональных данных:
- идентификация стороны в рамках предоставляемых услуг;
- предоставление услуг надлежащего качества;
- улучшение качества услуг и обслуживания;
- проведение статистических и иных исследований.

Перечень персональных данных, на обработку которых дается согласие:
- Фамилия, имя, отчество
- Паспортные данные
- СНИЛС
- Данные аттестата
- Фотографии документов
- Медицинская справка

Перечень действий с персональными данными:
- Сбор, хранение, обработка и использование персональных данных
- Уточнение (обновление, изменение)
- Использование
- Передача
- Обезличивание
- Уничтожение

Настоящее согласие действует до момента его отзыва путем направления соответствующего уведомления.";

            Button closeButton = new Button
            {
                Location = new Point(485, 420),
                Size = new Size(90, 30),
                Text = "Закрыть"
            };
            closeButton.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { richTextBox, closeButton });
            InitializeComponent();

        }

        private void Form5_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    }



