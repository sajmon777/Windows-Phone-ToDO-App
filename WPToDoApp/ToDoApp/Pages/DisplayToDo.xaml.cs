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

namespace ToDoApp
{
    public partial class DisplayToDo : PhoneApplicationPage
    {
        ServiceToDo.ToDo toDo;
        public DisplayToDo()
        {
            InitializeComponent();
            toDo = (ServiceToDo.ToDo)PhoneApplicationService.Current.State["ToDoDisplay"];
            DataContext = toDo;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}