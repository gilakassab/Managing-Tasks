using BO;
using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Engineer;
/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    bool isAdding = false;


    // מאפיינים של המהנדס
    public BO.Engineer Engineer
    {
        get { return (BO.Engineer)GetValue(EngineerProperty); }
        set { SetValue(EngineerProperty, value); }
    }


    public static readonly DependencyProperty EngineerProperty =
        DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

    public ObservableCollection<BO.TaskInList> EngineerTasks
    {
        get { return (ObservableCollection<BO.TaskInList>)GetValue(EngineerTasksProperty); }
        set { SetValue(EngineerTasksProperty, value); }
    }
    public static readonly DependencyProperty EngineerTasksProperty =
        DependencyProperty.Register("EngineerTasks", typeof(ObservableCollection<BO.TaskInList>), typeof(EngineerWindow), new PropertyMetadata(null));

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

    private void BtnTaskWindow_List(object sender, RoutedEventArgs e)
    {

        try
        {
            var taskWindow = new TaskWindow(Engineer.Task!.Id);

            taskWindow.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
        }
    }

    private void BtnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (isAdding)
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
            {
                Engineer = new BO.Engineer();
                isAdding = true;
            }
            EngExperience = Engineer.Level;
            Role = Engineer.Role;
            var temp = s_bl?.Task.ReadAll(t => t.Engineer == null ? false : t.Engineer.Id == Engineer.Id).Select(t => new BO.TaskInList()
            {
                Id = t.Id,
                Alias = t.Alias,
                Description = t.Description,
                Status = t.Status
            }).ToList();
            EngineerTasks = temp != null ? new ObservableCollection<BO.TaskInList>(temp) : new ObservableCollection<BO.TaskInList>();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
        }
        InitializeComponent();
    }

    private void TasksListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is ListBox listBox && listBox.SelectedItem != null)
        {
            try
            {
                if (Engineer.Task != null)
                {
                    BO.Task? task = s_bl!.Task.Read(Engineer.Task.Id);
                    if (task != null && task.Status != BO.Status.Completed)
                        MessageBox.Show("You can't start this task before finish the last one.", "Confirmation", MessageBoxButton.OK);
                    else
                        MessageBox.Show("After you will close the task window you will be able to start it.", "Confirmation", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("After you will close the task window you will be able to start it.", "Confirmation", MessageBoxButton.OK);

                int selectedTaskId = ((BO.TaskInList)listBox.SelectedItem).Id;
                var taskWindow = new TaskWindow(selectedTaskId);
                taskWindow.Closed += (sender, e) => ChangeTask(selectedTaskId);
                taskWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
            }
            listBox.SelectedItem = null;
        }
    }

    private void ChangeTask(int selectedTaskId)
    {
        if (Engineer.Task != null)
        {
            BO.Task? currentTask = s_bl!.Task.Read(Engineer.Task.Id);
            if (currentTask != null && currentTask.Status != BO.Status.Completed)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to start this Task?", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    BO.Task? task = s_bl!.Task.Read(selectedTaskId);
                    task!.Start = DateTime.Now;
                    s_bl!.Task.Update(task!);
                }
            }
        }

    }
}