using BO;
using PL.Task;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        // מאפיינים של המהנדס
        public BO.Engineer Engineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        public static readonly DependencyProperty EngineerProperty =
            DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

        public BO.EngineerExperience EngExperience { get; set; }
        public BO.Roles Role { get; set; }

        private void ComboBoxEngExperience_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Engineer != null)
                Engineer.Level = EngExperience == BO.EngineerExperience.None ? Engineer.Level : EngExperience;
        }

        private void ComboBoxRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Engineer != null)
                Engineer.Role = Role == BO.Roles.None ? Engineer.Role : Role;
        }

        private void BtnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Engineer.Id == 0)
                {
                    s_bl.Engineer.Create(Engineer);
                    MessageBox.Show("addition successful", "Confirmation", MessageBoxButton.OK);
                }
                else
                {
                    s_bl.Engineer.Update(Engineer);
                    MessageBox.Show("updation successful", "Confirmation", MessageBoxButton.OK);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
            }
        }

        // בנאי החלון
        public EngineerWindow(int id = 0)
        {
            try
            {
                if (id != 0)
                    Engineer = s_bl!.Engineer.Read(id)!;
                else
                    Engineer = new BO.Engineer();
                EngExperience = Engineer.Level;
                Role = Engineer.Role;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
            InitializeComponent();
        }
    }
}
