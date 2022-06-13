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

namespace UchUch.Screen
{
    /// <summary>
    /// Логика взаимодействия для Hub.xaml
    /// </summary>
    public partial class Hub : Window
    {
        string _role = "";
        public Hub(string role)
        {
            InitializeComponent();
            _role = role;

            if (role != "admin")
            {
                ButtonOpenAdm.Visibility = Visibility.Hidden;
            }
            ProfLogin.Content = $"Ваш табельный № : {Users.LoginName}";
            var tempPost = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(s => s.post).ToArray()[0];
            ProfPost.Content = $"Ваша должность: {tempPost}";
            var tempAdmin = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(s => s.ad).ToArray()[0];

            
          /*  if (tempPost == "Начальник")
            {
                ButtonOpenAdm.Visibility = Visibility;
            }
            */var tempFIO = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(s => s.fio).ToArray()[0];
            ProfFio.Content = $"Ваше ФИО: {tempFIO}";
            var tempWork = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(s => s.work).ToArray()[0];
            ProfWork.Content = $"Ваш участок: {tempWork}";
            var imgPuth = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(i => i.image).ToArray();
            ImageProfile.Source = new BitmapImage(new Uri("/UchUch;component/Assets/Image/Users/" + imgPuth[0], UriKind.Relative));

        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            Main w = new Main(_role);
            w.Show();
            Hide();
        }

        private void ButtonProfileExit_Click(object sender, RoutedEventArgs e)
        {
            var tempFIO = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(s => s.fio).ToArray()[0];

            Hide();
            MessageBox.Show($" {tempFIO}, вы вышли из аккаунта. До скорых встреч.");
            Sign w = new Sign();
            w.Show();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ButtonOpenAdm_Click(object sender, RoutedEventArgs e)
        {
            var tempFIO = Sign.db.GetTable<Users>().Where(l => l.num == Users.LoginName).Select(s => s.fio).ToArray()[0];

            Hide();
            MessageBox.Show($" {tempFIO}, вы вошли в панель Начальника.");
            Admin w = new Admin(_role);
            w.Show();

        }
    }
}
