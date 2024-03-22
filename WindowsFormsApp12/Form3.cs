using GSF.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp12
{
    public partial class Form3 : Form
    {
        private string reportFilePath = "user_activity_log.txt";
        public Form3(string filePath)
        {
            InitializeComponent();
            this.reportFilePath = filePath;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LoadReport();
        }
        private void LoadReport()
        {
            try
            {
                if (File.Exists(reportFilePath))
                {
                    // Считываем содержимое файла отчета
                    string reportContent = File.ReadAllText(reportFilePath);

                    // Отображаем содержимое в RichTextBox
                    richTextBox1.Text = reportContent;
                }
                else
                {
                    MessageBox.Show("Report file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while loading report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
// Реализация формы для просмотра информации о клавишах и процессах
// Загружает данные из файлов отчетов и представляет их пользователю