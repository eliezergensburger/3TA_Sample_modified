using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL.SimpleWPF;

namespace PO
{
    public class StudentCourse : BindableBase
    {
        public int ID { get; set; }
        public int Number
        {
            get;
            set;
        }
        public string Name 
        { 
            get; set; 
        }
        public int? Grade 
        { 
            get;
            set;
        }
        public int Year 
        { 
            get; 
            set; 
        }
        public BO.Semester Semester 
        { 
            get; 
            set;
        }

    }
}
