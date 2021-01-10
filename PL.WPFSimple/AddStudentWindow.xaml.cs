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

namespace PL.SimpleWPF
{
    /// <summary>
    /// Interaction logic for AddStudentWindow.xaml
    /// al pi Eliezer ubeacharyuto
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        private PO.Student student = new PO.Student();

        public PO.Student Student { get => student; }
        public AddStudentWindow()
        {
            InitializeComponent();
            this.DataContext = student;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
