using System;
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

namespace UchUch.Screen
{
    /// <summary>
    /// Логика взаимодействия для Sign.xaml
    /// </summary>
    public partial class Sign : Window
    {
        public static DataContext db = new DataContext(Properties.Settings.Default.conn);
        string role = "";
        public Sign()
        {
            InitializeComponent();
            foreach (Window w in App.Current.Windows)
            {
                if (w != this) { w.Close(); }

            }
          




        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {


            if (TBlogin.Text != null && TBpass.Password != null)
            {
                int users = (db.GetTable<Users>().Where(u => u.num == TBlogin.Text && u.password == TBpass.Password)).Count();
                
                if (users == 1)
                {



                    Users.LoginName = TBlogin.Text;
                    var tempFIO = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(s => s.fio).ToArray()[0];


                    role = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(s => s.ad).ToArray()[0];


                    if (role == "admin")
                    {
                        MessageBox.Show($"Добро пожаловать, {tempFIO}");
                        Admin w = new Admin(role);
                        w.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show($"Добро пожаловать, {tempFIO}");
                        Main w = new Main(role);
                        w.Show();
                        Hide();
                    }


                    
                }
                else MessageBox.Show("Неверные данные");
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void TBlogin_DragEnter(object sender, DragEventArgs e)
        {
            TBlogin.Text = null;
        }

        public void RemoveText(object sender, EventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        public void AddText(object sender, EventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }
        public void RemoveTextPass(object sender, EventArgs e)
        {
            PasswordBox instance = (PasswordBox)sender;
            if (instance.Password == instance.Tag.ToString())
                instance.Password = "";
        }

        public void AddTextPass(object sender, EventArgs e)
        {
            PasswordBox instance = (PasswordBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Password))
                instance.Password = instance.Tag.ToString();
        }
    }
}
