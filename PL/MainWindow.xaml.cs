using PL.Engineer;
using PL.Task;
using System.Windows;
using System.Windows.Input.Manipulations;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private void BtnManeger_Click(object sender, RoutedEventArgs e)
    {
        string userInput = Microsoft.VisualBasic.Interaction.InputBox("Please enter your Id:", "Enter Id", "248728764");

        if (!string.IsNullOrEmpty(userInput))
        {
            new ManagerWindow().Show();
        }
    }

    private void BtnEngineer_Click(object sender, RoutedEventArgs e)
    {
        string userInput = Microsoft.VisualBasic.Interaction.InputBox("Please enter your Id:", "Enter Id", "165324683");

        if (!string.IsNullOrEmpty(userInput))
        {
            new EngineerWindow(int.Parse(userInput)).Show();
        }
    }

    public MainWindow()
    {
        InitializeComponent();
    }
}
