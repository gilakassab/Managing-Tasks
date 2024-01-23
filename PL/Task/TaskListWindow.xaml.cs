using BO;
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
using System.Windows.Shapes;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public ObservableCollection<BO.Task> TaskList
        {
            get { return (ObservableCollection<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));
        public BO.EngineerExperience EngExperience { get; set; } = BO.EngineerExperience.None;
        public BO.Status Status { get; set; } = BO.Status.None;
        public BO.Roles Role { get; set; } = BO.Roles.None;

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new TaskWindow().Show();
            }
            catch(Exception ex) {
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
                
            }

        }

        private void cbSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = (IEnumerable<BO.Task>?)null;
            if (EngExperience != BO.EngineerExperience.None && Role != BO.Roles.None && Status != BO.Status.None)
                temp = s_bl?.Task.ReadAll(item => item.Role == Role && item.Level == EngExperience && item.Status == Status)!;
            if (EngExperience != BO.EngineerExperience.None && Role != BO.Roles.None && Status == BO.Status.None)
                temp = s_bl?.Task.ReadAll(item => item.Role == Role && item.Level == EngExperience)!;
            if (EngExperience != BO.EngineerExperience.None && Role == BO.Roles.None && Status != BO.Status.None)
                temp = s_bl?.Task.ReadAll(item => item.Level == EngExperience && item.Status == Status)!;
            if (EngExperience != BO.EngineerExperience.None && Role == BO.Roles.None && Status == BO.Status.None)
                temp = s_bl?.Task.ReadAll(item => item.Level == EngExperience)!;
            if (EngExperience == BO.EngineerExperience.None && Role != BO.Roles.None && Status != BO.Status.None)
                temp = s_bl?.Task.ReadAll(item => item.Role == Role && item.Status == Status)!;
            if (EngExperience == BO.EngineerExperience.None && Role != BO.Roles.None && Status == BO.Status.None)
                temp = s_bl?.Task.ReadAll(item => item.Role == Role)!;
            if (EngExperience == BO.EngineerExperience.None && Role == BO.Roles.None && Status != BO.Status.None)
                temp = s_bl?.Task.ReadAll(item => item.Status == Status)!;
            if (EngExperience == BO.EngineerExperience.None && Role == BO.Roles.None && Status == BO.Status.None)
                temp = s_bl?.Task.ReadAll()!;
            TaskList = temp == null ? new() : new(temp!);
        }

        //private void cbEngineerExperience_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Role = BO.Roles.None;
        //    var temp = (EngExperience == BO.EngineerExperience.None) ?
        //        s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Level == EngExperience)!;
        //    TaskList = temp == null ? new() : new(temp!);
        //}

        //private void cbRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var temp = (Role == BO.Roles.None) ?
        //        s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Role == Role)!;
        //    TaskList = temp == null ? new() : new(temp!);
        //}

        public TaskListWindow()
        {
            InitializeComponent();
            var temp = s_bl?.Task.ReadAll();
            TaskList = temp == null ? new() : new(temp!);
        }
        private void UpdateListAfterTaskWindowClosed()
        {
            var temp = s_bl?.Task.ReadAll();
            TaskList = temp == null ? new() : new(temp!);

        }
        private void gridUpdate_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.Task? task = (sender as ListView)?.SelectedItem as BO.Task;
                var taskWindow = new TaskWindow(task.Id);
                taskWindow.Closed += (sender, e) => UpdateListAfterTaskWindowClosed();
                taskWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
            }
        }
    }
}
