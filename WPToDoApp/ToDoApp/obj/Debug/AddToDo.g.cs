﻿#pragma checksum "C:\Users\Simon\Documents\Visual Studio 2010\Projects\WPToDoApp\ToDoApp\AddToDo.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "98799EFD5F262C23F59D0021A591886D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace ToDoApp {
    
    
    public partial class AddToDo : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox TitleTextBox;
        
        internal System.Windows.Controls.TextBox DescriptionTextBox;
        
        internal System.Windows.Controls.TextBox DeadLineTextBox;
        
        internal Microsoft.Phone.Controls.ListPicker StatusOption;
        
        internal Microsoft.Phone.Controls.ListPicker PriorityOption;
        
        internal System.Windows.Controls.Button AddButton;
        
        internal System.Windows.Controls.Button CancelButton;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/ToDoApp;component/AddToDo.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.TitleTextBox = ((System.Windows.Controls.TextBox)(this.FindName("TitleTextBox")));
            this.DescriptionTextBox = ((System.Windows.Controls.TextBox)(this.FindName("DescriptionTextBox")));
            this.DeadLineTextBox = ((System.Windows.Controls.TextBox)(this.FindName("DeadLineTextBox")));
            this.StatusOption = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("StatusOption")));
            this.PriorityOption = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("PriorityOption")));
            this.AddButton = ((System.Windows.Controls.Button)(this.FindName("AddButton")));
            this.CancelButton = ((System.Windows.Controls.Button)(this.FindName("CancelButton")));
        }
    }
}

