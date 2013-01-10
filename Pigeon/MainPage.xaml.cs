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

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(33);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();
        }

        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                FrameworkDispatcher.Update();
            }
            catch
            {
            }

            if (true == _soundIsPlaying)
            {
                if (_soundInstance.State != SoundState.Playing)
                {
                    _soundIsPlaying = false;
                }
            }
        }

        MessageHandler _handler;

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            _handler = MessageHandler.GetMessageHandler();
            _handler.Login("cdffh", "123456", "cquapp.com");
            _handler.OnLogin += HandlerOnLogin;
            _handler.OnReceiveTextMessage += HandlerOnReceiveTextMessage;
            _handler.OnReceiveGPSMessage += HandlerOnReceiveGPSMessage;
            _handler.OnReceiveImageMessage += HandlerOnReceiveImageMessage;
            _handler.OnReceiveVoiceMessage += HandlerOnReceiveVoiceMessage;

            btnLogin.IsEnabled = false;
        }

        void HandlerOnLogin(object sender, Matrix.EventArgs e)
        {
            MessageBox.Show("Login Success");
        }

        void HandlerOnReceiveTextMessage(object sender, MessagingEventArgs e)
        {
            string message = (string)e.Body;
            MessageBox.Show(message);
        }

        void HandlerOnReceiveGPSMessage(object sender, MessagingEventArgs e)
        {
            GeoCoordinate coordinate = (GeoCoordinate)e.Body;
            MessageBox.Show("GPS = (" + coordinate.Latitude + ", " + coordinate.Longitude + ")");
        }

        void HandlerOnReceiveImageMessage(object sender, MessagingEventArgs e)
        {
            BitmapImage image = (BitmapImage)e.Body;
            imgReceived.Source = image;
        }

        void HandlerOnReceiveVoiceMessage(object sender, MessagingEventArgs e)
        {
            _voiceBytes = (byte[])e.Body;

            Thread soundThread = new Thread(new ThreadStart(PlaySound));
            soundThread.Start();
        }

        byte[] _voiceBytes;

        void PlaySound()
        {
            SoundEffect sound = new SoundEffect(_voiceBytes, _mic.SampleRate, AudioChannels.Mono);
            _soundInstance = sound.CreateInstance();
            _soundIsPlaying = true;
            _soundInstance.Play();
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