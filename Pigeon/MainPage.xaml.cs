using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Pigeon.Resources;

using Pigeon.Msg;
using System.Device.Location;
using System.Windows.Media.Imaging;
using System.Windows.Media;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.Threading;

using System.Windows.Threading;
using Microsoft.Phone.Tasks;

namespace Pigeon
{
    public partial class MainPage : PhoneApplicationPage
    {

        Microphone _mic = Microphone.Default;
        SoundEffectInstance _soundInstance;
        bool _soundIsPlaying = false;

        public MainPage()
        {
            InitializeComponent();
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
    }
}