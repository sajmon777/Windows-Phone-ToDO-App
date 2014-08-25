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
    public partial class LogIn : PhoneApplicationPage
    {
        Popup splachScreen;
        public LogIn()
        {
            InitializeComponent();
            InitializeSplashScreen();
        }

        void InitializeSplashScreen()
        {
            splachScreen = new Popup();
            splachScreen.Child = new SplashScreenControl();
        }
        
        
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Register.xaml", UriKind.Relative));
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            ToDoServiceClient client = new ToDoServiceClient();
            client.ClientCredentials.UserName.UserName=UserNameTextBox.Text;
            client.ClientCredentials.UserName.Password = PasswordTextBox.Password;
            client.GetUserIdAsync("UserNameTextBox.Text");
            client.GetUserIdCompleted += new EventHandler<GetUserIdCompletedEventArgs>(client_GetUserIdCompleted);
            splachScreen.IsOpen = true; 
        }

        void client_GetUserIdCompleted(object sender, GetUserIdCompletedEventArgs e)
        {
            PhoneApplicationService.Current.State["UserName"] = UserNameTextBox.Text;
            PhoneApplicationService.Current.State["Password"] = PasswordTextBox.Password;
            splachScreen.IsOpen = false;  
            NavigationService.Navigate(new Uri("/Pages/ToDoList.xaml", UriKind.Relative));
        }
    }
}