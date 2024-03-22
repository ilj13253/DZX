using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp12
{
    public partial class Form2 : Form
    {

        private List<string> moderatedWords = new List<string>();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            moderatedWords.Clear();

            // Предполагается, что слова вводятся через запятую или новую строку
            string[] words = textBox1.Text.Split(new char[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Добавляем слова в список
            moderatedWords.AddRange(words);

            // Закрываем форму
            this.Close();
        }
        public List<string> GetModeratedWords()
        {
            return moderatedWords;
        }
    }
}
// Реализация формы для настройки приложения
// Здесь пользователь выбирает опции слежения