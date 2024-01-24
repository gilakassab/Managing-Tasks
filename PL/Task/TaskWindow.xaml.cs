using BO;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Task;

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


    public BO.EngineerExperience EngExperience { get; set; }/* = BO.EngineerExperience.None;*/
    public int Engineer { get; set; } = 0;
    public int DepTask { get; set; } = 0;
    public BO.Roles Role { get; set; } /*= BO.Roles.None;*/
    public ObservableCollection<BO.Engineer> EngineersList
    {
        get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }

    public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("EngineersList", typeof(ObservableCollection<BO.Engineer>), typeof(TaskWindow), new PropertyMetadata(null));

    public ObservableCollection<BO.TaskInList> TasksList
    {
        get { return (ObservableCollection<BO.TaskInList>)GetValue(TasksListProperty); }
        set { SetValue(TasksListProperty, value); }
    }

    public static readonly DependencyProperty TasksListProperty =
        DependencyProperty.Register("TasksList", typeof(ObservableCollection<BO.TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));


    public int SelectedEngineer
    {
        get { return (int)GetValue(SelectedEngineerProperty); }
        set
        {
            MessageBox.Show($"{value}", "Confirmation", MessageBoxButton.OK);
            SetValue(SelectedEngineerProperty, value);
            try
            {
                if (value != null)
                {
                    Engineer eng = s_bl.Engineer.Read(value)!;
                    Task.Engineer = new BO.EngineerInTask() { Id = eng.Id, Name = eng.Name };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
            }
        }
    }

    public static readonly DependencyProperty SelectedEngineerProperty =
      DependencyProperty.Register("SelectedEngineer", typeof(int), typeof(TaskWindow), new PropertyMetadata(null));

    private void ComboBoxEngExperience_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Task != null && Task.Level != null)
        {
            Task.Level = EngExperience;
            LevelChanged(EngExperience);
        }
    }
    private void ComboBoxRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Task != null && Task.Role != null)
        {
            Task.Role = Role;
            RoleChanged(Role);
        }
    }
    private void ComboBoxEngineer_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (Engineer != 0)
            {
                Engineer eng = s_bl.Engineer.Read(Engineer)!;
                Task.Engineer = new BO.EngineerInTask() { Id = eng.Id, Name = eng.Name };
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
        }
    }
    private void ComboBoxDepTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (DepTask != 0)
            {
                BO.Task dep = s_bl.Task.Read(DepTask)!;
                Task.Dependencies.Add(new BO.TaskInList()
                {
                    Id = dep.Id,
                    Alias = dep.Alias,
                    Description = dep.Description,
                    Status = dep.Status
                });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
        }
    }

    private void DependenciesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is ListBox listBox && listBox.SelectedItem != null)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete the selected item?", "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Task.Dependencies!.Remove((BO.TaskInList)listBox.SelectedItem);
            }

            listBox.SelectedItem = null;
        }
    }

    private void RoleChanged(Roles value)
    {
        if (value == Roles.None)
            Task.Role = null; 
        else
            Task.Role = value;
        FindEngineers();
    }

    private void LevelChanged(EngineerExperience value)
    {
        if (value == EngineerExperience.None)
            Task.Level = null;
        else
            Task.Level = value;
        FindEngineers();
    }

    private void FindEngineers()
    {
        if (Task.Level != null && Task.Role != null)
        {
            var temp = s_bl?.Engineer.ReadAll(e => e.Role == Task.Role && e.Level >= Task.Level);
            EngineersList = temp == null ? new() : new(temp!);
        }
        else
            EngineersList = new();
    }

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
        if (id != 0)
        {
            try
            {
                Task = s_bl!.Task.Read(id)!;
                var temp = s_bl?.Task.ReadAll().Select(t => new BO.TaskInList()
                {
                    Id = t.Id,
                    Alias = t.Alias,
                    Description = t.Description,
                    Status = t.Status
                }).ToList();
                TasksList = temp == null ? new() : new(temp!);
                MessageBox.Show($"{TasksList[1]}", "Confirmation", MessageBoxButton.YesNo);
                Role = Task.Role == null ? Roles.None : Task.Role.Value;
                EngExperience = Task.Level == null ? EngineerExperience.None : Task.Level.Value;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }
        else
        {
            try
            {
                Task = new BO.Task();
                var temp = s_bl?.Task.ReadAll().Select(t => new BO.TaskInList()
                {
                    Id = t.Id,
                    Alias = t.Alias,
                    Description = t.Description,
                    Status = t.Status
                }).ToList();
                TasksList = temp == null ? new() : new(temp!);
                Role = Roles.None;
                EngExperience = EngineerExperience.None;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
         
        }
    }
}
