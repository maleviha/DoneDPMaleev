using System;
using System.Collections.Generic;
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
using System.Data.Linq;
using UchUch.Classes;


namespace UchUch.Screen
{
    /// <summary>
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Main : Window
    {
        DataContext db = new DataContext(Properties.Settings.Default.conn);
        string _role = "";
        string tempFIO = "";
        public Main(string role)
        {
            InitializeComponent();
            _role = role;

                                                                                                                                    // Динамические данные авторизованного работника
            tempFIO = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(s => s.fio).ToArray()[0];
            TBfio.Text = tempFIO;

            var tempPOST = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(s => s.post).ToArray()[0];
            TBpost.Text = tempPOST;

            var tempWORK = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(s => s.work).ToArray()[0];
            TBwork.Text = tempWORK;


            TBdate.SelectedDate = DateTime.Now;
            updateGridOrders();
        }






        private void updateGridOrders() //получение таблицы работ из базы
        {
            DG_Work.ItemsSource = db.GetTable<Job>().Where(a => a.status == 1 && a.fio==tempFIO).ToList();

            

        }
    

        

        private void BtnAdd_Click(object sender, RoutedEventArgs e) //добавление работы
        {
            if (TBfio.Text != "" && TBpost.Text != "" && TBwork.Text != "" && TBdate.Text != "" && TBDescript.Text != "")
            {
                Table<Job> jbs = db.GetTable<Job>();
                Job jb = new Job { fio = TBfio.Text, post = TBpost.Text, work = TBwork.Text, date = TBdate.Text, description = TBDescript.Text, status = 1 };

                jbs.InsertOnSubmit(jb);
                db.SubmitChanges();
                updateGridOrders();


            }
            else MessageBox.Show("Заполните поля");
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var tempFIO = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(s => s.fio).ToArray()[0];

            Hide();
            
            Sign w = new Sign();
            w.Show();
            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void DG_Work_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Table<Job> jbs = db.GetTable<Job>();

          
        }
    }
}
