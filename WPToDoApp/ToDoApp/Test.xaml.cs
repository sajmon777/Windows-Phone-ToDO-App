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

namespace ToDoApp
{
    public partial class Test : PhoneApplicationPage
    {
        public Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ToDoServiceClient client = new ToDoServiceClient();
            client.ClientCredentials.UserName.UserName = "Simon777";
            client.ClientCredentials.UserName.Password = "Advantan"; 
            client.GetUserIdAsync("Simon777"); 
            client.GetUserIdCompleted+=new EventHandler<GetUserIdCompletedEventArgs>(client_GetUserIdCompleted);
        }

        void client_GetUserIdCompleted(object sender, GetUserIdCompletedEventArgs e)
        {
            MessageBox.Show("OK"); 
        }
    }
}