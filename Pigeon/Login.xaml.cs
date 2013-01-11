using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Pigeon.Msg;

namespace Pigeon
{
    public partial class Login : PhoneApplicationPage
    {
        MessageHandler _handler;
        public Login()
        {
            InitializeComponent();
        }
       
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            _handler = MessageHandler.GetMessageHandler();
            _handler.Login(txtPhoneNumber.Text,txtPassword.Text, "cquapp.com");
            _handler.OnLogin += LoginSuccess;
            btnLogin.IsEnabled = false;   
        }
        private void LoginSuccess(object sender, Matrix.EventArgs e)
        {
        }
    }
}