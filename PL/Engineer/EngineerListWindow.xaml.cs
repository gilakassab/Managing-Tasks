using BO;
using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Enginner
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        // רשימת המהנדסיפ המוצגת בחלון
        public ObservableCollection<BO.Engineer> EngineerList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

        public BO.EngineerExperience EngExperience { get; set; } = BO.EngineerExperience.None;

        public BO.Roles Role { get; set; } = BO.Roles.None;

        // פעולת התגובה ללחיצה על כפתור "הוספה"
        private void BtnAdd_ClickEng(object sender, RoutedEventArgs e)
        {
            try
            {
                var engineerWindow = new EngineerWindow();
                engineerWindow.Closed += (sender, e) => UpdateListAfterEnginnerWindowClosed();
                engineerWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
            }
        }

        // פעולת התגובה ללחיצה כפולה על אובייקט ברשימה לעדכון פרטיו
        private void GridUpdate_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.Engineer? engineer = (sender as ListView)?.SelectedItem as BO.Engineer;
                var engineerWindow = new EngineerWindow(engineer!.Id);
                engineerWindow.Closed += (sender, e) => UpdateListAfterEnginnerWindowClosed();
                engineerWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
            }
        }

        // עדכון רשימת המשימות לאחר סגירת חלון משימה
        private void UpdateListAfterEnginnerWindowClosed()
        {
            var temp = s_bl?.Engineer.ReadAll();
            EngineerList = temp == null ? new() : new(temp!);
        }

        // פעולת התגובה לשינויים בבחירת הערכים בקומבובוקסים
        private void CbSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = (IEnumerable<BO.Engineer>?)null;
            if (EngExperience != BO.EngineerExperience.None && Role != BO.Roles.None)
                temp = s_bl?.Engineer.ReadAll(item => item.Role == Role && item.Level == EngExperience)!;
            //if (EngExperience != BO.EngineerExperience.None && Role != BO.Roles.None)
            //    temp = s_bl?.Engineer.ReadAll(item => item.Role == Role && item.Level == EngExperience)!;
            if (EngExperience != BO.EngineerExperience.None && Role == BO.Roles.None)
                temp = s_bl?.Engineer.ReadAll(item => item.Level == EngExperience)!;
            //if (EngExperience != BO.EngineerExperience.None && Role == BO.Roles.None)
            //    temp = s_bl?.Engineer.ReadAll(item => item.Level == EngExperience)!;
            if (EngExperience == BO.EngineerExperience.None && Role != BO.Roles.None)
                temp = s_bl?.Engineer.ReadAll(item => item.Role == Role)!;
            //if (EngExperience == BO.EngineerExperience.None && Role != BO.Roles.None)
            //    temp = s_bl?.Engineer.ReadAll(item => item.Role == Role)!;
            //if (EngExperience == BO.EngineerExperience.None && Role == BO.Roles.None)
            //    temp = s_bl?.Engineer.ReadAll(item => item.Status == Status)!;
            if (EngExperience == BO.EngineerExperience.None && Role == BO.Roles.None/* && Status == BO.Status.None*/)
                temp = s_bl?.Engineer.ReadAll()!;
            EngineerList = temp == null ? new() : new(temp!);
        }

        // פעולת בנייה
        public EngineerListWindow()
        {
            //InitializeComponent();
            var temp = s_bl?.Engineer.ReadAll();
            EngineerList = temp == null ? new() : new(temp!);
        }
    }
}