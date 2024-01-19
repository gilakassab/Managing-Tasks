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

        public BO.EngineerExperience EngExperience { get; set; } = BO.EngineerExperience.None;
        public BO.Roles Role { get; set; } = BO.Roles.None;
        public BO.Status State { get; set; } = BO.Status.None;

        public TaskWindow(int id = 0)
        {
            InitializeComponent();
            //try
            //{
                var temp = s_bl?.Task.Read(id);
                Task = temp == null ? new() : temp!;
            //}catch (Exception ex)
            //{
            //    Task = new();

            //}
        
        }
    }
}
