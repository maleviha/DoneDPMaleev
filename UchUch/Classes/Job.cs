using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace UchUch.Classes
{
    [Table(Name = "job")]
        class Job
    {
        [Column(Name = "numjob", IsPrimaryKey = true, IsDbGenerated = true)]
        public int numjob { get; set; }
        [Column(Name = "fio")]
        public string fio { get; set; }
        [Column(Name = "post")]
        public string post { get; set; }

        [Column(Name = "work")]
        public string work { get; set; }

        [Column(Name = "description")]
        public string description { get; set; }

        [Column(Name = "date")]
        public string date { get; set; }
        [Column(Name = "status")]
        public int status { get; set; }

    }
}
