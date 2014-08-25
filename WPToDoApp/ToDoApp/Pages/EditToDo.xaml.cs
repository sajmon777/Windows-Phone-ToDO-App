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
using Microsoft.Phone.Shell;
using ToDoApp.ServiceToDo;
using System.Windows.Controls.Primitives;

namespace ToDoApp
{
    public partial class EditToDo : PhoneApplicationPage
    {
        ServiceToDo.ToDo toDo;
        Dictionary<int, string> statusValues;
        Dictionary<int, string> priorityValues;
        Popup splachScreen;

        public EditToDo()
        {
            InitializeComponent();
            statusValues = (Dictionary<int, string>)PhoneApplicationService.Current.State["StatusValues"];
            priorityValues = (Dictionary<int, string>)PhoneApplicationService.Current.State["PriorityValues"];
            toDo = (ServiceToDo.ToDo)PhoneApplicationService.Current.State["ToDoEdit"];
            StatusOption.ItemsSource = statusValues.Values.ToList();
            PriorityOption.ItemsSource = priorityValues.Values.ToList();
            InitializeToDo();
            InitializeSplashScreen();
        } 
        
        
        private void InitializeToDo()
        {
            DataContext = toDo;
        }

        void InitializeSplashScreen()
        {
            splachScreen = new Popup();
            splachScreen.Child = new SplashScreenControl();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ToDoServiceClient toDoService = new ToDoServiceClient();
            toDoService.ClientCredentials.UserName.UserName = PhoneApplicationService.Current.State["UserName"].ToString(); ;
            toDoService.ClientCredentials.UserName.Password = PhoneApplicationService.Current.State["Password"].ToString();
            toDoService.EditToDoCompleted += new EventHandler<EditToDoCompletedEventArgs>(toDoService_EditToDoCompleted);
            toDoService.EditToDoAsync(toDo);
            toDoService.CloseAsync();
            splachScreen.IsOpen = true; 
        }

        void toDoService_EditToDoCompleted(object sender, EditToDoCompletedEventArgs e)
        {
            if (e.Result)
            {
                MessageBox.Show("To Do successfuly edit");
                PhoneApplicationService.Current.State["IsToDoListChange"] = true;
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("To Do is not edit");
            }
            splachScreen.IsOpen = false; 
        }
        
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();  
        }
    }
}