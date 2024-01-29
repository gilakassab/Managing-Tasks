using DalTest;
using PL.Task;
using PL.Engineer;
using System.Windows;
namespace PL;

/// <summary>
/// Interaction logic for ManagerWindow.xaml
/// </summary>
public partial class ManagerWindow : Window
{
    private void BtnTaskList_Click(object sender, RoutedEventArgs e)
    {
        new TaskListWindow().Show();
    }
    private void BtnEngineerList_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }

    private void BtnInitialization_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Would you like to create Initial data?", "Confirmation", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            // User clicked Yes
            Initialization.Do();
        }
    }

    public ManagerWindow()
    {
        InitializeComponent();
    }
}
