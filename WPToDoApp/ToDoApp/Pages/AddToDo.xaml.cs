using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using ToDoApp.ServiceToDo;
using Microsoft.Phone.Shell;
using System.Windows.Controls.Primitives;

namespace ToDoApp
{
    public partial class AddToDo : PhoneApplicationPage
    {
        ServiceToDo.ToDo toDo;
        Dictionary<int, string> statusValues;
        Dictionary<int, string> priorityValues;
        Popup splachScreen;
        public AddToDo()
        {
            InitializeComponent();
            statusValues = (Dictionary<int, string>)PhoneApplicationService.Current.State["StatusValues"];
            priorityValues = (Dictionary<int, string>)PhoneApplicationService.Current.State["PriorityValues"];
            StatusOption.ItemsSource = statusValues.Values.ToList();
            PriorityOption.ItemsSource = priorityValues.Values.ToList();
            InitializeToDo();
        }

        private void InitializeToDo()
        {
            toDo = new ServiceToDo.ToDo();
            toDo.DeadLine = DateTime.Now;
            toDo.Priority = priorityValues.FirstOrDefault().Value;
            toDo.Status = statusValues.FirstOrDefault().Value;
            DataContext = toDo;
            InitializeSplashScreen(); 
        }

        void InitializeSplashScreen()
        {
            splachScreen = new Popup();
            splachScreen.Child = new SplashScreenControl();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ToDoServiceClient toDoService = new ToDoServiceClient();
            toDoService.ClientCredentials.UserName.UserName = PhoneApplicationService.Current.State["UserName"].ToString(); ;
            toDoService.ClientCredentials.UserName.Password = PhoneApplicationService.Current.State["Password"].ToString();
            toDo.CreateDate = DateTime.Now; 
            toDoService.AddToDoAsync(toDo);
            toDoService.AddToDoCompleted += new EventHandler<AddToDoCompletedEventArgs>(toDoService_AddToDoCompleted);
            toDoService.CloseAsync();
            splachScreen.IsOpen = true;
        }

        void toDoService_AddToDoCompleted(object sender, AddToDoCompletedEventArgs e)
        {
            if (e.Result)
            {
                MessageBox.Show("To Do successfuly added");
                PhoneApplicationService.Current.State["IsToDoListChange"] = true;
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("To Do is not added");
            }
            splachScreen.IsOpen = false;
        }
    }
}