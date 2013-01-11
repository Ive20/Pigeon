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
using Pigeon.Maps;

namespace Pigeon
{
    public partial class Navigation : PhoneApplicationPage
    {

        private MessageHandler _handler;

        private MapManipulator _map;

        public Navigation()
        {
            InitializeComponent();

            _handler = MessageHandler.GetMessageHandler();
            _map = new MapManipulator(mainMap);

        }
    }
}