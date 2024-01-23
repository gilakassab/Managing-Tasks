using BO;
using System;
using System.Collections.ObjectModel;
using System.Windows;
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

    public ObservableCollection<BO.Engineer> EngineersList
    {
        get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }

    public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("EngineersList", typeof(ObservableCollection<BO.Engineer>), typeof(TaskWindow), new PropertyMetadata(null));

    public BO.EngineerExperience TaskExperience
    {
        get { return (BO.EngineerExperience)GetValue(TaskExperienceProperty); }
        set
        {
            SetValue(TaskExperienceProperty, value);
            LevelChanged(value);
        }
    }

    public static readonly DependencyProperty TaskExperienceProperty =
      DependencyProperty.Register("TaskExperience", typeof(BO.EngineerExperience), typeof(TaskWindow), new PropertyMetadata(null));

    public BO.Roles TaskRole
    {
        get { return (BO.Roles)GetValue(TaskRoleProperty); }
        set
        {
            SetValue(TaskRoleProperty, value);
            RoleChanged(value);
        }
    }

    public static readonly DependencyProperty TaskRoleProperty =
      DependencyProperty.Register("TaskRole", typeof(BO.Roles), typeof(TaskWindow), new PropertyMetadata(null));

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

    private void RoleChanged(Roles value)
    {
        if (value == Roles.None)
            Task.Role = null; 
        else
            Task.Role = value;
        //FindEngineers();
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
                TaskRole = Task.Role == null ? Roles.None : Task.Role.Value;
                TaskExperience = Task.Level == null ? EngineerExperience.None : Task.Level.Value;
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
                TaskRole = Roles.None;
                TaskExperience = EngineerExperience.None;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
         
        }
    }
}
