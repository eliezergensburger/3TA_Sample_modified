using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using BLAPI;

namespace PL.SimpleWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        ObservableCollection<BO.Student> ObserListOfStudents;
        public MainWindow()
        {
            InitializeComponent();
            ObserListOfStudents = new ObservableCollection<BO.Student>(bl.GetAllStudents());
    
            graduationComboBox.ItemsSource = Enum.GetValues(typeof(BO.StudentGraduate));
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.StudentStatus));
            personalStatusComboBox.ItemsSource = Enum.GetValues(typeof(BO.PersonalStatus));


            cbStudentID.DisplayMemberPath = "Name";//show only specific Property of object
            cbStudentID.SelectedValuePath = "ID";//selection return only specific Property of object
            cbStudentID.SelectedIndex = 0; //index of the object to be selected
            cbStudentID.DataContext = ObserListOfStudents;



        }

        private void cbStudentID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            BO.Student curStu = (cbStudentID.SelectedItem as BO.Student);
            
            gridOneStudent.DataContext = curStu;

            ObservableCollection<BO.StudentCourse> ObserListOfCourses = new ObservableCollection<BO.StudentCourse>();

            foreach (var item in curStu.ListOfCourses)
            {
                ObserListOfCourses.Add(item);
            }
            studentCourseDataGrid.DataContext = ObserListOfCourses; //curStu.ListOfCourses;//


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddStudentWindow wnd = new AddStudentWindow();
            bool? result = wnd.ShowDialog();
            if(result == true)
            {
                PO.Student student = wnd.Student;
                try
                {
                    bl.AddStudent(student.getStudentBO());
                    ObserListOfStudents = null;
                    ObserListOfStudents = new ObservableCollection<BO.Student>(bl.GetAllStudents());
                    this.DataContext = ObserListOfStudents;
                }
                catch (Exception ex)
                {

                }

            }
        }
    }
}
