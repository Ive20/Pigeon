﻿#pragma checksum "C:\Users\Jack\Docs\2013 Spring\IMG\Pigeon SVN\trunk\src\Pigeon\Pigeon\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F5BC93FE91754016EFE98C435B54EFD8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
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


namespace Pigeon {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button btnLogin;
        
        internal System.Windows.Controls.Image imgReceived;
        
        internal System.Windows.Controls.TextBox txtUserName;
        
        internal System.Windows.Controls.Button btnRegister;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Pigeon;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.btnLogin = ((System.Windows.Controls.Button)(this.FindName("btnLogin")));
            this.imgReceived = ((System.Windows.Controls.Image)(this.FindName("imgReceived")));
            this.txtUserName = ((System.Windows.Controls.TextBox)(this.FindName("txtUserName")));
            this.btnRegister = ((System.Windows.Controls.Button)(this.FindName("btnRegister")));
        }
    }
}
