using BO;
using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Controls;

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

    private void BtnTaskListWindow_List(object sender, RoutedEventArgs e)
    {
       
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
            var temp = s_bl?.Task.ReadAll().Select(t => new BO.TaskInList()
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

   

    private void ComboBoxEngTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        /* try
            {
                if (DepTask != 0)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to add the selected item?", "Confirmation", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        BO.Task dep = s_bl.Task.Read(DepTask)!;
                        if (Task.Dependencies == null)
                            Task.Dependencies = new List<TaskInList>();

                        Task.Dependencies.Add(new BO.TaskInList()
                        {
                            Id = dep.Id,
                            Alias = dep.Alias,
                            Description = dep.Description,
                            Status = dep.Status
                        });
                        TaskDependencies = new ObservableCollection<BO.TaskInList>(Task.Dependencies);
                    }
                    DepTask = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
            }*/
        try
        {
            
                MessageBoxResult result = MessageBox.Show("Do you want to add the selected item?", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    var temp = s_bl.Task.ReadAll(t => t.Engineer == null ? false : t.Engineer.Id == Engineer.Id)!.Select(t => new BO.TaskInList()
                    {
                        Id = t.Id,
                        Alias = t.Alias,
                        Description = t.Description,
                        Status = t.Status
                    });
                EngineerTasks = temp != null ? new ObservableCollection<BO.TaskInList>(temp) : new ObservableCollection<BO.TaskInList>();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
        }
    }
}