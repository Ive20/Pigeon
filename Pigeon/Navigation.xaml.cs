using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

using Pigeon.Msg;
using Pigeon.Maps;

namespace Pigeon
{
    public partial class Navigation : PhoneApplicationPage
    {

        private MessageHandler _handler;

        private MapManipulator _map;
        private Brush brush;
        public Navigation()
        {
            InitializeComponent();
            _handler = MessageHandler.GetMessageHandler();
            _map = new MapManipulator(mainMap);
            _handler.OnReceiveGPSMessage+= OnReceiveGps;
            _handler.OnReceiveTextMessage += OnReceiveText;
            _handler.OnReceiveVoiceMessage += OnReceiveVoice;
            _handler.OnReceiveImageMessage += OnReceiveImage;
            brush = new SolidColorBrush(Colors.Red);
            _map.PinpointCurrentUserLocation(brush);
        }
        private void OnReceiveGps(object sender, MessagingEventArgs arg)
        {
            Brush bluebrush=new SolidColorBrush(Colors.Blue);
            _map.Pinpoint(MessageUtility.DecodeGPS(arg.Body.ToString()), bluebrush);
        }
         private void OnReceiveText(object sender, MessagingEventArgs arg)
        {

        }
         private void OnReceiveImage(object sender, MessagingEventArgs arg)
        {

        }
         private void OnReceiveVoice(object sender, MessagingEventArgs arg)
        {
         
        }
        private void OnReceiveError(object sender, MessagingEventArgs arg)
        {           
        }
        }
       
}