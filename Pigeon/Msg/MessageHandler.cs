using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Device.Location;



using Matrix.Xmpp;
using Matrix.Xmpp.XData;
using Matrix.Xmpp.Sasl;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Register;
using Matrix.License;

namespace Pigeon.Msg
{
    public class MessagingEventArgs
    {
        object _body;
        public object Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }
    }

    class MessageHandler
    {
        public enum ChatType
        {
            normal,
            error,
            /// <summary>
            /// Use this for a person to person chat. 
            /// </summary>
            chat,
            /// <summary>
            /// Use this if you want to send the message to a chat room. 
            /// </summary>
            groupchat,
            headline
        }
        private XmppClient _xmppClient;
        private static MessageHandler _messageHandler;
        private Dictionary<string, string> MsgHeader = new Dictionary<string, string>(); 
        public EventHandler<Matrix.EventArgs> OnLogin;
        public EventHandler<Matrix.Xmpp.Sasl.SaslEventArgs> OnAuthError;
        public EventHandler<MessagingEventArgs> OnReceiveTextMessage;
        public EventHandler<MessagingEventArgs> OnReceiveGPSMessage;
        public EventHandler<MessagingEventArgs> OnReceiveImageMessage;
        public EventHandler<MessagingEventArgs> OnReceiveVoiceMessage;
        public EventHandler<RegisterEventArgs> OnReceiveErrorMessage;
        public EventHandler<EventArgs> OnRegister;
        public EventHandler<IqEventArgs> OnRegisterError;

        private MessageHandler()
        {
            _xmppClient = new XmppClient();
            MsgHeader.Add("Text", "_CUROTEXT@");
            MsgHeader.Add("Gps","_CUROGPS@");
            MsgHeader.Add("Img", "_CUROIMAGE@");
            MsgHeader.Add("VOi","_CUROVOICE@");
            MsgHeader.Add("Inv","_CUROInv@");
            _xmppClient.OnLogin += xmppClient_OnLogin;
            _xmppClient.OnMessage += xmppClient_OnMessage;
            _xmppClient.OnError += xmppClient_OnError;

            // Register the license. If you skip this, a warning messagebox 
            // would appear and your app will not be able to proceed. 
            const string license = @"eJxkkG9T4jAQxr+K49ubM21FC86asTSFgROOylBO3kW6V4NpUpukhfv0ouLfe7Ozz/72zzML12KNyuDRtpTKXB7z4qfRf23La7yQr+iYwqzWuVvbUU7n1uVCA/moQOq4ssLuqA/kPYfYGatLrClMeYk0abh03OoayIuGWJcVV7s3ILQ6OlgB8sYgKbmQ1HCJ5uqTs5N83/TK9s3vhxZVzi0m20rUyPYZDTy/4/leF8h/CEaGYamprd1+10HAc/w6f+oFXgjkG4C5KBS3rkbKdrzoGFzNeqzJ7s6svOGpDW00FRhsBumd+G3Xi+WP/mgjm7ZD2KDvN4P2wW9cWGZscrtQ0TaZJN1oFTbBqBvHD91qOIwYO++p3irl98Ms0XIe4Dis48zMbqp/60k1vl1s50FYSTz/82inbVspwX+5YqmXeNrp9zY6RNbiLpM2PkvdeByt0uISyIdvIId30ycB";
            LicenseManager.SetLicense(license);
        }

        public static MessageHandler GetMessageHandler()
        {
            if (_messageHandler == null)
            {
                _messageHandler = new MessageHandler();
            }
            return _messageHandler;
        }

        public void Register(string username, string password, string domain)
        {

            _xmppClient.OnRegister += xmppClient_OnRegister;
            _xmppClient.OnRegisterInformation += xmppClient_OnRegisterInformation;
            _xmppClient.OnRegisterError += xmppClient_OnRegisterError;
            _xmppClient.OnError += xmppClient_OnError;

            _xmppClient.SetUsername(username);
            _xmppClient.SetXmppDomain(domain);
            _xmppClient.Password = password;
            _xmppClient.RegisterNewAccount = true;

            _xmppClient.Open();
        }

        public void Login(string username, string password, string domain)
        {
            // Set up client username, password and the domain name of the server. 
            if(!_xmppClient.RegisterNewAccount)
            {
            _xmppClient.SetUsername(username);
            _xmppClient.SetXmppDomain(domain);
            _xmppClient.Password = password;

            // Way to go!
            _xmppClient.StartTls = false;
            _xmppClient.Open();
            }
        }

        public void SendTextMessage(string targetJID, string text, ChatType type)
        {
            Message toSend = new Message(targetJID);
            toSend.Type = MessageType.chat;
            switch (type)
            {
                case ChatType.chat:
                    toSend.Body = MsgHeader["Text"];
                    toSend.Body += text;
                    _xmppClient.Send(toSend);
                    break;
                case ChatType.error:
                    break;
                case ChatType.groupchat:
                    break;
                case ChatType.headline:
                    break;
                case ChatType.normal:
                    break;
            }
        }

        public void SendGpsMessage(string targetJID, GeoCoordinate GPS, ChatType type)
        {
            Message toSend = new Message(targetJID);
            toSend.Type = MessageType.chat;
            switch (type)
            {
                case ChatType.chat:
                    toSend.Body = MsgHeader["GPS"];
                    toSend.Body += MessageUtility.EncodeGPS(GPS);
                    _xmppClient.Send(toSend);
                    break;
                case ChatType.error:
                    break;
                case ChatType.groupchat:
                    break;
                case ChatType.headline:
                    break;
                case ChatType.normal:
                    break;
            }
        }

        public void SendImageMessage(string targetJID, BitmapImage bmpImage, ChatType type)
        {
            Message toSend = new Message(targetJID);
            toSend.Type = MessageType.chat;
            switch (type)
            {
                case ChatType.chat:
                    toSend.Body = MsgHeader["Img"];
                    toSend.Body += MessageUtility.EncodeImage(bmpImage);
                    _xmppClient.Send(toSend);
                    break;
                case ChatType.error:
                    break;
                case ChatType.groupchat:
                    break;
                case ChatType.headline:
                    break;
                case ChatType.normal:
                    break;
            }
        }

        public void SendVoiceMessage(string targetJID, MemoryStream stream, ChatType type)
        {
            Message toSend = new Message(targetJID);
            toSend.Type = MessageType.chat;
            switch (type)
            {

                case ChatType.chat:
                    toSend.Body = MsgHeader["Voi"];
                    toSend.Body += MessageUtility.EncodeVoice(stream);
                    _xmppClient.Send(toSend);
                    break;
                case ChatType.error:
                    break;
                case ChatType.groupchat:
                    break;
                case ChatType.headline:
                    break;
                case ChatType.normal:
                    break;
            }
        }

        void xmppClient_OnMessage(object sender, MessageEventArgs e)
        {
            string header = MessageUtility.GetHeader(e.Message.Body);
            string content = e.Message.Body.Substring(header.Length, e.Message.Body.Length - header.Length);

            MessagingEventArgs msgArg = new MessagingEventArgs();
          
            if (header == MsgHeader["Text"])
            {
                msgArg.Body = content;
                this.OnReceiveTextMessage(sender, msgArg);

            }
            else if (header == MsgHeader["Img"]) 
            {
                msgArg.Body = MessageUtility.DecodeGPS(content);
                this.OnReceiveGPSMessage(sender, msgArg);
            }
            else if (header == MsgHeader["Voi"])
            {
                msgArg.Body = MessageUtility.DecodeImage(content);
                this.OnReceiveImageMessage(sender, msgArg);
            }
            else if (header == MsgHeader["Gps"])
            {
                msgArg.Body = MessageUtility.DecodeVoice(content);
                this.OnReceiveVoiceMessage(sender, msgArg);
            }
        }

        void xmppClient_OnError(object sender, Matrix.ExceptionEventArgs e)
        {
        }

        void xmppClient_OnLogin(object sender, Matrix.EventArgs e)
        {
            this.OnLogin(sender, e);
        }

        private void xmppClient_OnRegisterInformation(object sender, RegisterEventArgs e)
        {
            e.Register.RemoveAll<Data>();

            e.Register.Username = _xmppClient.Username;
            e.Register.Password = _xmppClient.Password;
        }

        private void xmppClient_OnRegister(object sender, EventArgs e)
        {
            // registration was successful
            this.OnRegister(sender, e);
        }

        private void xmppClient_OnRegisterError(object sender, IqEventArgs e)
        {
            // registration failed.
            this.OnRegisterError(sender, e);
        }
    }
}
