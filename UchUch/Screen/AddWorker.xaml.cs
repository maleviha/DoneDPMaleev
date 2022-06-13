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
using UchUch.Screen;

namespace UchUch.Screen
{

    /// <summary>
    /// Логика взаимодействия для AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Window
    {

        DataContext db = new DataContext(Properties.Settings.Default.conn);
        string _role = "";
        public AddWorker(string role)
        {
            
            InitializeComponent();
            updateGridOrders();
            _role = role;
        }
        public void updateGridOrders() //получение таблицы работ из базы
        {
            DG_Worker.ItemsSource = Sign.db.GetTable<Users>().Where(a => a.status == 1);
            
           
           

        }
        public void ClearTablePole()
        {
            TBfio.Clear();
            TBpost.Clear();
            TBwork.Clear();
            TBnumtab.Clear();
            TBpass.Clear();
            

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TBfio.Text != "" && TBpost.Text != "" && TBwork.Text != "" && TBnumtab.Text != "" && TBpass.Text !="")
            {
                Table<Users> jbs = db.GetTable<Users>();
                Users jbn = new Users { fio = TBfio.Text, post = TBpost.Text, work = TBwork.Text, num = TBnumtab.Text, password = TBpass.Text, ad = "0", image="profile.png", status = 1 };

                jbs.InsertOnSubmit(jbn);
                db.SubmitChanges();
                updateGridOrders();


            }
            else MessageBox.Show("Заполните поля");
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Hide();
           
            Admin w = new Admin(_role);
            w.Show();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CanvasEditProduct.Visibility = Visibility.Visible;
                proded = (Users)DG_Worker.SelectedItem;
                TBfio.Text = proded.fio;
                TBnumtab.Text = proded.num;
                TBpost.Text = proded.post;
                TBwork.Text = proded.work;
                TBpass.Text = proded.password;
            }
            catch (Exception)
            {
                MessageBox.Show("Выделите элемент");
            }
        }
    
        Users proded;
        private void ButtonProductsOk_Click(object sender, RoutedEventArgs e)
        {
            if (TBfio.Text != "" && TBnumtab.Text != "" && TBpost.Text != "" && TBwork.Text != "" && TBpass.Text != "")
            {
                proded.fio = TBfio.Text;
                proded.num = TBnumtab.Text;
                proded.post = TBpost.Text;
                proded.work = TBwork.Text;
                proded.password = TBpass.Text;
                Sign.db.SubmitChanges();
                MessageBox.Show("Работник изменен");
                ClearTablePole();
                updateGridOrders();
                

                CanvasEditProduct.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonProductsConsal_Click(object sender, RoutedEventArgs e)
        {
            ClearTablePole();
            CanvasEditProduct.Visibility = Visibility.Collapsed;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Users prod = (Users)DG_Worker.SelectedItem;
                dynamic temp = Sign.db.GetTable<Users>().Where(p => p.numtab == prod.numtab).ToArray();
                temp[0].status = 0;
                Sign.db.SubmitChanges();
                updateGridOrders();
                MessageBox.Show("Работник удалён");

            }
            catch (Exception)
            {
                MessageBox.Show("Выберите работника");
            }
        }
    }
}
