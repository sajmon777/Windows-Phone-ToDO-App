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
    public partial class ToDoList : PhoneApplicationPage
    {


        #region Properties

        IDictionary<int, string> statusOptionValues;
        IDictionary<int, string> priorityOptionValues;
        Popup splachScreen;
        int numAsyncEvenst;

        private IList<ServiceToDo.ToDo> toDoListPrivate;
        public IList<ServiceToDo.ToDo> toDoList
        {
            get { return toDoListPrivate; }
            set
            {
                toDoListPrivate = value;
                ToDoListControl.ItemsSource = toDoListPrivate;
            }
        }

        #endregion
        #region Constructor
        public ToDoList()
        {
            InitializeComponent();
            InitializeSplashScreen();
            InitializeOptionValues();
        }
        #endregion
        #region Initialization
        private void InitializeOptionValues()
        {
            ToDoServiceClient toDoService = new ToDoServiceClient();
            toDoService.ClientCredentials.UserName.UserName = PhoneApplicationService.Current.State["UserName"].ToString();
            toDoService.ClientCredentials.UserName.Password = PhoneApplicationService.Current.State["Password"].ToString();
            PhoneApplicationService.Current.State["IsToDoListChange"] = false;

            toDoService.GetStatusValuesCompleted += new EventHandler<GetStatusValuesCompletedEventArgs>(toDoService_GetStatusValuesCompleted);
            toDoService.GetPriorityValuesCompleted += new EventHandler<GetPriorityValuesCompletedEventArgs>(toDoService_GetPriorityValuesCompleted);
            toDoService.GetToDosCompleted += new EventHandler<GetToDosCompletedEventArgs>(toDoService_GetToDosCompleted);

            try
            {
                toDoService.GetStatusValuesAsync();
                toDoService.GetPriorityValuesAsync();
                toDoService.GetToDosAsync();
                numAsyncEvenst += 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            toDoService.CloseAsync();

        }
        void InitializeSplashScreen()
        {
            splachScreen = new Popup();
            splachScreen.Child = new SplashScreenControl();
            numAsyncEvenst = 0;
            splachScreen.IsOpen = true;
        }
        #endregion

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);


            if (Convert.ToBoolean(PhoneApplicationService.Current.State["IsToDoListChange"].ToString()))
            {
                UpdateToDoList();
                PhoneApplicationService.Current.State["IsToDoListChange"] = false;
            }

        }

        void toDoService_GetToDosCompleted(object sender, GetToDosCompletedEventArgs e)
        {
            toDoList = e.Result;
            numAsyncEvenst -= 1;
            if (numAsyncEvenst <= 0)
                splachScreen.IsOpen = false;
        }


        void toDoService_GetStatusValuesCompleted(object sender, GetStatusValuesCompletedEventArgs e)
        {
            PhoneApplicationService.Current.State["StatusValues"] = e.Result;
            numAsyncEvenst -= 1;
            if (numAsyncEvenst == 0)
                splachScreen.IsOpen = false;
        }
        void toDoService_GetPriorityValuesCompleted(object sender, GetPriorityValuesCompletedEventArgs e)
        {
            PhoneApplicationService.Current.State["PriorityValues"] = e.Result;
            numAsyncEvenst -= 1;
            if (numAsyncEvenst == 0)
                splachScreen.IsOpen = false;
        }
        void toDoService_DeleteToDoCompleted(object sender, DeleteToDoCompletedEventArgs e)
        {
            bool result = e.Result;
            if (result)
                MessageBox.Show("ToDo successfully deleted");

            else
                MessageBox.Show("ToDo is not deleted");
            UpdateToDoList();
        }


        private void UpdateToDoList()
        {

            ToDoServiceClient toDoService = new ToDoServiceClient();
            toDoService.ClientCredentials.UserName.UserName = PhoneApplicationService.Current.State["UserName"].ToString();
            toDoService.ClientCredentials.UserName.Password = PhoneApplicationService.Current.State["Password"].ToString();
            toDoService.GetToDosCompleted += new EventHandler<GetToDosCompletedEventArgs>(toDoService_GetToDosCompleted);
            toDoService.GetToDosAsync();
            splachScreen.IsOpen = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ServiceToDo.ToDo toDo = (ServiceToDo.ToDo)button.DataContext;
            MessageBoxResult messageResult = MessageBox.Show("Do you really won't delete To Do", "Confirm", MessageBoxButton.OKCancel);

            if (messageResult == MessageBoxResult.OK)
            {
                ToDoServiceClient toDoService = new ToDoServiceClient();
                toDoService.ClientCredentials.UserName.UserName = PhoneApplicationService.Current.State["UserName"].ToString(); ;
                toDoService.ClientCredentials.UserName.Password = PhoneApplicationService.Current.State["Password"].ToString();

                toDoService.DeleteToDoAsync(toDo.Id);
                toDoService.DeleteToDoCompleted += new EventHandler<DeleteToDoCompletedEventArgs>(toDoService_DeleteToDoCompleted);
                splachScreen.IsOpen = true;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ServiceToDo.ToDo toDo = (ServiceToDo.ToDo)button.DataContext;
            PhoneApplicationService.Current.State["ToDoEdit"] = toDo;
            NavigationService.Navigate(new Uri("/Pages/EditToDo.xaml", UriKind.Relative));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/AddToDo.xaml", UriKind.Relative));
        }

        private void TextBlock_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            ServiceToDo.ToDo toDo = (ServiceToDo.ToDo)textBlock.DataContext;
            PhoneApplicationService.Current.State["ToDoDisplay"] = toDo;
            NavigationService.Navigate(new Uri("/Pages/DisplayToDo.xaml", UriKind.Relative));
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            toDoList = toDoList.OrderBy(x => x.Title).ToList();
        }
    }
}