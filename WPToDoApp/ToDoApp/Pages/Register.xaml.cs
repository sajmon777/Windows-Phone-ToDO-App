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
using System.Windows.Controls.Primitives;

namespace ToDoApp
{
    public partial class Register : PhoneApplicationPage
    {
        private User user;
        Popup splachScreen;
        public Register()
        {
            InitializeComponent();
            InitializeSplashScreen(); 
            user = new User();
            DataContext = user;
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

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            ServiceToDo.ToDoServiceClient todoService = new ServiceToDo.ToDoServiceClient();
            todoService.ClientCredentials.UserName.UserName = "Anonymous";
            todoService.ClientCredentials.UserName.Password = "";
            user.Password = PasswordTextBox.Password;
            todoService.RegisterUserCompleted += new EventHandler<ServiceToDo.RegisterUserCompletedEventArgs>(client_GetRegisterUserCompleted);
            todoService.RegisterUserAsync(user);
            RegisterButton.IsEnabled = false;
            splachScreen.IsOpen = true;
        }

        void client_GetRegisterUserCompleted(object sender, ServiceToDo.RegisterUserCompletedEventArgs e)
        {
            RegisterButton.IsEnabled = true;
            bool registerResult = e.Result;
            if (registerResult)
            {
                MessageBoxResult messageResult = MessageBox.Show("User succesfully registerd", "Confirm", MessageBoxButton.OK);
                if (messageResult == MessageBoxResult.OK)
                {
                    NavigationService.GoBack();
                }
            }
            else
            {
                MessageBoxResult messageResult = MessageBox.Show("User is already registerd", "Confirm", MessageBoxButton.OK); 

                if (messageResult == MessageBoxResult.OK)
                {
                    
                }
            }
            splachScreen.IsOpen = false;
        }
    }
}