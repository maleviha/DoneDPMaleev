using System;
using System.Threading;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UchUch.Classes;
using Excel = Microsoft.Office.Interop.Excel;

namespace UchUch.Screen
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        DataContext db = new DataContext(Properties.Settings.Default.conn);
        string _role = "";
        public Admin(string role)
        {
            InitializeComponent();
            updateGridOrders();
            _role = role;
        }
        private void updateGridOrders() //получение таблицы работ из базы
        {
            DG_Job.ItemsSource = db.GetTable<Job>().Where(a => a.status == 1);



        }
       

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Sign w = new Sign();
            w.Show();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            var tempFIO = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(s => s.fio).ToArray()[0];

            Hide();
            AddWorker w = new AddWorker(_role);
            w.Show();
        }

     
        private void CBstatsByWorker_TextChanged(object sender, TextChangedEventArgs e)
        {
            BtnToExcel1.IsEnabled = true;
        }

        private void BtnToExcel1_Click(object sender, RoutedEventArgs e)
        {
            //создаем новый документ и открываем окно Excel
            Excel.Application application = new Excel.Application { Visible = true };
            Excel.Workbook wb = application.Workbooks.Add();
            Excel.Worksheet ws = wb.ActiveSheet;

            //делаем заголовок
            Excel.Range header = ws.get_Range("A1", "F1");
            header.Merge();
            header.Value = $"Список выполненных заявок {CBstatsByWorker.Text}";
            header.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            header.Interior.Color = Excel.XlRgbColor.rgbLightGray;
            header.Borders.Color = Excel.XlRgbColor.rgbBlack;

            //делаем заголовки колонок
            string[] columns = { "ID", "ФИО", "Должность", "Участок", "Дата", "Описание" };
            Excel.Range header2 = ws.get_Range("A3", "F3");
            header2.Value = columns;
            header2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            header2.Interior.Color = Excel.XlRgbColor.rgbLightGray;
            header2.Borders.Color = Excel.XlRgbColor.rgbBlack;

            //выводим список из базы
            List<Job> orders = db.GetTable<Job>().Where(o => o.status == 1 && o.fio == CBstatsByWorker.Text).ToList();
            for (int i = 0; i < orders.Count; i++)
            {
                Excel.Range rng = ws.get_Range($"A{i + 4}", $"F{i + 4}");
                Job order = orders[i];
                string[] item = { order.numjob.ToString(), order.fio, order.post, order.work.ToString(), order.date, order.description };
                rng.Value = item;
                rng.Borders.Color = Excel.XlRgbColor.rgbBlack;
            }



            //выводим текущую дату
            ws.Cells[orders.Count + 6, 1] = "Дата:";
            ws.Cells[orders.Count + 6, 2] = DateTime.Today;

            //автоширина колонок
            ws.Columns.AutoFit();
        }

        private void BtnToExcel3_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnStatsDay_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //создаем новый документ и открываем окно Excel
            Excel.Application application = new Excel.Application { Visible = true };
            Excel.Workbook wb = application.Workbooks.Add();
            Excel.Worksheet ws = wb.ActiveSheet;

            //делаем заголовок
            Excel.Range header = ws.get_Range("A1", "F1");
            header.Merge();
            header.Value = $"Список выполненных заявок за {BtnStatsDay.Text}";
            header.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            header.Interior.Color = Excel.XlRgbColor.rgbLightGray;
            header.Borders.Color = Excel.XlRgbColor.rgbBlack;

            //делаем заголовки колонок
            string[] columns = { "ID", "ФИО", "Должность", "Участок", "Дата", "Описание" };
            Excel.Range header2 = ws.get_Range("A3", "F3");
            header2.Value = columns;
            header2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            header2.Interior.Color = Excel.XlRgbColor.rgbLightGray;
            header2.Borders.Color = Excel.XlRgbColor.rgbBlack;

            //получаем список из базы за период времени

            List<Job> orders = db.GetTable<Job>().Where(o => o.status == 1 && o.date == BtnStatsDay.Text).ToList();
            //выводим список
            for (int i = 0; i < orders.Count; i++)
            {
                Excel.Range rng = ws.get_Range($"A{i + 4}", $"F{i + 4}");
                Job order = orders[i];
                string[] item = { order.numjob.ToString(), order.fio, order.post, order.work.ToString(), order.date, order.description };
                rng.Value = item;
                rng.Borders.Color = Excel.XlRgbColor.rgbBlack;
            }



            //выводим текущую дату
            ws.Cells[orders.Count + 6, 1] = "Дата:";
            ws.Cells[orders.Count + 6, 2] = DateTime.Today;

            //автоширина колонок
            ws.Columns.AutoFit();
        }
    }
    }
    

