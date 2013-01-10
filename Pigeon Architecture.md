# 基本思路:
本项目底层功能采用模块化的开发思想，每类功能均封装为一个模块，模块主要用命名空间实现。在底层模块功能之上为逻辑层。在逻辑层之上为ui层

# 详细说明:
##底层功能:
## MsgHandler模块:
提供与服务器交互的所有功能。
### MsgHandler模块详细说明：
需引用的库
###类设计说明：
MsgHandler 单例维护一个MsgHandler对象。
###说明：MsgHandler类。
####成员变量设计:
	Private:
	MsgHandler _self
####函数设计：
	Public:
	delegate void callback();
	void Login(String username,String password,callback);
	void Register(String username,String password,String Email,calback);
	void SendTextMessage(revicer re,String text,callback);
	void SendGpsMessage(revicer re,Gps point,callback);
	void SendImgMessage(revicer re,img Img,callback);
	void SendVoiceMessage(revicer re ,voice voice,callback);
	void EncodeGPS(GeoCoordinate coordinate)
	void DecodeGPS(string coordinateString)
	Static GetMsghandler();
	Private:
	MsgHandler();
####事件设计：
	Public:	
	public EventHandler<Matrix.EventArgs> OnLogin;
	public EventHandler<Matrix.Xmpp.Sasl.SaslEventArgs> OnAuthError;
    public EventHandler<MessagingEventArgs> OnReceiveTextMessage;
    public EventHandler<MessagingEventArgs> OnReceiveGPSMessage;
    public EventHandler<MessagingEventArgs> OnReceiveImageMessage;
    public EventHandler<MessagingEventArgs> OnReceiveVoiceMessage;
    public EventHandler<MessagingEventArgs> OnReceiveErrorMessage;MsgTools:

所有的事件全部从e.body中取得相应的数据。

	String EncodeImg (Bitmap img)
	String EncodeVoice(Stream voi)
	Bitmap decodeImg(String strImg)
	Byte decodeVoice(string strVoi);

##ComDao模块：提供数据库的所有操作
业务逻辑：
##Map模块：提供与地图交与的所有功能。
###	Map模块详细说明。
##Ui：
