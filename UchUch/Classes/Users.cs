using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using UchUch.Screen;


namespace UchUch.Classes
{
    [Table(Name = "worker")]
    class Users
    {
        [Column(Name = "numtab", IsPrimaryKey = true, IsDbGenerated = true)]
        public int numtab { get; set; }
        [Column(Name = "fio")]
        public string fio { get; set; }
        [Column(Name = "post")]
        public string post { get; set; }
        [Column(Name = "ad")]
        public string ad { get; set; }
        [Column(Name = "work")]
        public string work { get; set; }
        [Column(Name = "password")]
        public string password { get; set; }
        [Column(Name = "num")]
        public string num { get; set; }
        [Column(Name = "status")]
        public int status { get; set; }

        [Column(Name = "image")] 
        public string image { get; set; }

        static DataContext db = Sign.db;

        public static string LoginName; // Запоминает Логин пользователя что бы давать видимость в программе

    }
}
