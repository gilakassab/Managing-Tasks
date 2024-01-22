using BO;
using DalTest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


        public BO.Task Task
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));

        public ObservableCollection<BO.Engineer> EngineersList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineersList", typeof(ObservableCollection<BO.Engineer>), typeof(TaskListWindow), new PropertyMetadata(null));

        public BO.EngineerExperience EngExperience { get; set; } = BO.EngineerExperience.None;
        public BO.Roles Role { get; set; } = BO.Roles.None;
        public BO.Status State { get; set; } = BO.Status.None;
        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Task.Id == 0)
            {
                try {  
                    s_bl.Task.Create(Task);
                    MessageBox.Show("addition successful", "Confirmation", MessageBoxButton.OK);
                    this.Close();
                }
                catch(Exception ex)
                { 
                    MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
                }
            }
            else
            { 
                try
                {
                    s_bl.Task.Update(Task);
                    MessageBox.Show("updation successful", "Confirmation", MessageBoxButton.OK);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
                  
                }
            }
        }

        public TaskWindow(int id = 0)
        {
            InitializeComponent();
            var temp = s_bl?.Engineer.ReadAll();
            EngineersList = temp == null ? new() : new(temp!);
            if (id != 0)
            {
                try
                {
                    Task = s_bl!.Task.Read(id)!;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex}");
                }
            }
            else
            {
                Task = new BO.Task();
            }
        }
    }
}
