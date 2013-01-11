using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace people_hub
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
<<<<<<< HEAD
        }


        MessageHandler _handler;
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }  
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            _handler = MessageHandler.GetMessageHandler();
            string userName = txtUserName.Text;
            string password = "123456";
            _handler.Register(userName, password, "cquapp.com");
            _handler.OnRegister += HandlerOnRegister;
            _handler.OnRegisterError += HandlerOnRegisterError;
        }
        void HandlerOnRegister(object sender, EventArgs e)
        {
        }
        void HandlerOnRegisterError(object sender, Matrix.Xmpp.Client.IqEventArgs e)
        {
        }
=======

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {

        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
>>>>>>> developer
    }
}