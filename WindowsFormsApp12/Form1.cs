using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WindowsFormsApp12
{
    public partial class Form1 : Form
    {
        private string logFilePath = "user_activity_log.txt";
        private DateTime lastActivityTime;
        private System.Timers.Timer timer;
        private List<string> monitoredApplications = new List<string>(); // Список приложений для мониторинга
        private List<string> moderatedWords = new List<string>(); // Список слов для модерирования
        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
            //timerUserActivity = DateTime.Now;
            LogActivity("Application started.");
        }
        //C:\Users\Илья\source\repos\WindowsFormsApp12\Debug\Dll9.dll
        [DllImport(@"C:\Users\Илья\source\repos\WindowsFormsApp12\Debug\Dll9.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void TrackActivity(string activity);
        private void Track(string activity)
        {
            TrackActivity(activity);
        }
        private void LogActivity(string message)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(logFilePath))
                {
                    sw.WriteLine($"{DateTime.Now.ToString()} - {message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while logging activity: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeTimer()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 60000; // 1 minute interval
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            TimeSpan timeSinceLastActivity = DateTime.Now - lastActivityTime;
            if (timeSinceLastActivity.TotalMinutes >= 1)
            {
                LogActivity("No activity detected for the last minute.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 5000;
            timer1.Start();
        }
        //private void timerUserActivity_Tick(object sender, EventArgs e)
        //{
        //    // Получаем информацию о пользовательской активности и обновляем интерфейс
        //    //bool userActive = IsUserActive();
        //    labelUserActivity.Text = userActive ? "Пользователь активен" : "Пользователь неактивен";
        //}

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            lastActivityTime = DateTime.Now;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            lastActivityTime = DateTime.Now;
            if (checkBox1.Checked)
            {
                // Проверяем, если нажатая клавиша находится в списке слов для модерирования
                if (moderatedWords.Contains(e.KeyCode.ToString()))
                {
                    string moderatedReport = $"Moderated word '{e.KeyCode.ToString()}' typed at {DateTime.Now}.";
                    LogActivity(moderatedReport);
                    // Создаем файл отчета
                    LogKeyPress(e.KeyCode);
                    CreateModerationReport(moderatedReport);
                }
            }
        }
        private void LogKeyPress(Keys key)
        {
            try
            {
                using (StreamWriter sw = File.AppendText("key_log.txt"))
                {
                    sw.WriteLine($"{DateTime.Now.ToString()} - Key pressed: {key}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while logging key press: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            Track("Button Start clicked");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            InitializeTimer();
            LogActivity("Tracking started.");
            if (checkBox2.Checked)
            {
                StartMonitoringApplications();
            }
        }
        private void StartMonitoringApplications()
        {
            // Производим мониторинг процессов
            monitoredApplications.Clear();
            foreach (var process in System.Diagnostics.Process.GetProcesses())
            {
                monitoredApplications.Add(process.ProcessName);
            }
            // Создаем отчет о текущих запущенных процессах
            CreateApplicationMonitoringReport();
        }
        private void CreateApplicationMonitoringReport()
        {
            string report = $"Applications monitored at {DateTime.Now}:\n";
            foreach (var app in monitoredApplications)
            {
                report += app + "\n";
            }
            LogActivity(report);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 optionsForm = new Form2();
            optionsForm.ShowDialog();
            moderatedWords = optionsForm.GetModeratedWords();
        }
        private void CreateModerationReport(string report)
        {
            // Создаем файл отчета для модерирования
            string moderationReportFilePath = "moderation_report.txt";
            try
            {
                using (StreamWriter sw = File.AppendText(moderationReportFilePath))
                {
                    sw.WriteLine(report);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while creating moderation report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form3 reportForm = new Form3("user_activity_log.txt");
            reportForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Track("Button Start clicked");
        }
    }
}
