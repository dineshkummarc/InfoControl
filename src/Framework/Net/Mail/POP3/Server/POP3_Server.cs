using System;
using System.IO;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace InfoControl.Net.Mail.POP3
{
	#region Event delegates

	/// <summary>
	/// Represents the method that will handle the AuthUser event for Pop3Server.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A AuthUser_EventArgs that contains the event data.</param>
	public delegate void AuthUserEventHandler(object sender,AuthUser_EventArgs e);

	/// <summary>
	/// Represents the method that will handle the GetMessgesList event for Pop3Server.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A GetMessagesInfo_EventArgs that contains the event data.</param>
	public delegate void GetMessagesInfoHandler(object sender,GetMessagesInfo_EventArgs e);

	/// <summary>
	/// Represents the method that will handle the GetMessage,DeleteMessage,GetTopLines event for Pop3Server.
	/// </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A GetMessage_EventArgs that contains the event data.</param>
	public delegate void MessageHandler(object sender,Pop3Message_EventArgs e);

	#endregion

	/// <summary>
	/// Pop33 server component.
	/// </summary>
	public class Pop3Server : SocketServer
	{        				
		#region Event declarations

		/// <summary>
		/// Occurs when new computer connected to Pop33 server.
		/// </summary>
		public event ValidateIPHandler ValidateIPAddress = null;

		/// <summary>
		/// Occurs when connected user tryes to authenticate.
		/// </summary>
		public event AuthUserEventHandler AuthUser = null;

		/// <summary>
		/// Occurs user session ends. This is place for clean up.
		/// </summary>
		public event EventHandler SessionEnd = null;

		/// <summary>
		/// Occurs user session resetted. Messages marked for deletion are unmarked.
		/// </summary>
		public event EventHandler SessionResetted = null;

		/// <summary>
		/// Occurs when server needs to know logged in user's maibox messages.
		/// </summary>
		public event GetMessagesInfoHandler GetMessgesList = null;

		/// <summary>
		/// Occurs when user requests specified message.
		/// </summary>
		public event MessageHandler GetMessage = null;

		/// <summary>
		/// Occurs when user requests delete message.
		/// </summary>		
		public event MessageHandler DeleteMessage = null;

		/// <summary>
		/// Occurs when user requests specified message TOP lines.
		/// </summary>
		public event MessageHandler GetTopLines = null;

		/// <summary>
		/// Occurs when Pop33 session has finished and session log is available.
		/// </summary>
		public event LogEventHandler SessionLog = null;

		#endregion


		/// <summary>
		/// Defalut constructor.
		/// </summary>
		public Pop3Server() : base()
		{
			IPEndPoint = new IPEndPoint(IPAddress.Any,110);
		}


		#region override InitNewSession

		/// <summary>
		/// 
		/// </summary>
		/// <param name="socket"></param>
		protected override void InitNewSession(Socket socket)
		{
			SocketLogger logger = new SocketLogger(socket,this.SessionLog);
			Pop3Session session = new Pop3Session(socket,this,logger);
		}

		#endregion


		#region method IsUserLoggedIn

		/// <summary>
		/// Checks if user is logged in.
		/// </summary>
		/// <param name="userName">User name.</param>
		/// <returns></returns>
		internal bool IsUserLoggedIn(string userName)
		{			
			lock(m_pSessions){
				foreach(Pop3Session sess in m_pSessions.Values){
					if(sess.UserName.ToLower() == userName.ToLower()){
						return true;
					}
				}
			}
			
            return false;
		}

		#endregion


		#region Properties implementation
		
		#endregion

		#region Events Implementation

		#region function OnValidate_IpAddress

		/// <summary>
		/// Raises event ValidateIP event.
		/// </summary>
		/// <param name="localEndPoint">Server IP.</param>
		/// <param name="remoteEndPoint">Connected client IP.</param>
		/// <returns>Returns true if connection allowed.</returns>
		internal virtual bool OnValidate_IpAddress(IPEndPoint localEndPoint,IPEndPoint remoteEndPoint) 
		{			
			ValidateIP_EventArgs oArg = new ValidateIP_EventArgs(localEndPoint,remoteEndPoint);
			if(this.ValidateIPAddress != null){
				this.ValidateIPAddress(this, oArg);
			}

			return oArg.Validated;						
		}

		#endregion

		#region function OnAuthUser

		/// <summary>
		/// Authenticates user.
		/// </summary>
		/// <param name="session">Reference to current Pop33 session.</param>
		/// <param name="userName">User name.</param>
		/// <param name="passwData"></param>
		/// <param name="data"></param>
		/// <param name="authType"></param>
		/// <returns></returns>
		internal virtual AuthUser_EventArgs OnAuthUser(Pop3Session session,string userName,string passwData,string data,AuthType authType) 
		{				
			AuthUser_EventArgs oArg = new AuthUser_EventArgs(session,userName,passwData,data,authType);
			if(this.AuthUser != null){
				this.AuthUser(this,oArg);
			}
			
			return oArg;
		}

		#endregion


		#region function OnGetMessagesInfo

		/// <summary>
		/// Gest Pop33 messages info.
		/// </summary>
		/// <param name="session"></param>
		/// <param name="messages"></param>
		internal virtual void OnGetMessagesInfo(Pop3Session session,Pop3Messages messages) 
		{				
			GetMessagesInfo_EventArgs oArg = new GetMessagesInfo_EventArgs(session,messages,session.UserName);
			if(this.GetMessgesList != null){
				this.GetMessgesList(this, oArg);
			}
		}

		#endregion

		#region function OnGetMail

		/// <summary>
		/// Raises event get message.
		/// </summary>
		/// <param name="session"></param>
		/// <param name="message">Message which to get.</param>
		/// <param name="sessionSocket">Message which to get.</param>
		/// <returns></returns>
		internal virtual byte[] OnGetMail(Pop3Session session,Pop3Message message,Socket sessionSocket) 
		{			
			Pop3Message_EventArgs oArg = new Pop3Message_EventArgs(session,message,sessionSocket);
			if(this.GetMessage != null){
				this.GetMessage(this,oArg);
			}
			
			return oArg.MessageData;
		}

		#endregion

		#region function OnDeleteMessage

		/// <summary>
		/// Raises delete message event.
		/// </summary>
		/// <param name="session"></param>
		/// <param name="message">Message which to delete.</param>
		/// <returns></returns>
		internal virtual bool OnDeleteMessage(Pop3Session session,Pop3Message message) 
		{				
			Pop3Message_EventArgs oArg = new Pop3Message_EventArgs(session,message,null);
			if(this.DeleteMessage != null){
				this.DeleteMessage(this,oArg);
			}
			
			return true;
		}

		#endregion

		#region function OnGetTopLines

		/// <summary>
		/// Raises event GetTopLines.
		/// </summary>
		/// <param name="session"></param>
		/// <param name="message">Message wich top lines to get.</param>
		/// <param name="nLines">Header + number of body lines to get.</param>
		/// <returns></returns>
		internal byte[] OnGetTopLines(Pop3Session session,Pop3Message message,int nLines)
		{
			Pop3Message_EventArgs oArgs = new Pop3Message_EventArgs(session,message,null,nLines);
			if(this.GetTopLines != null){
				this.GetTopLines(this,oArgs);
			}
			return oArgs.MessageData;
		}

		#endregion

		
		#region function OnSessionEnd

		/// <summary>
		/// Raises SessionEnd event.
		/// </summary>
		/// <param name="session">Session which is ended.</param>
		internal void OnSessionEnd(object session)
		{
			if(this.SessionEnd != null){
				this.SessionEnd(session,new EventArgs());
			}
		}

		#endregion

		#region function OnSessionResetted

		/// <summary>
		/// Raises SessionResetted event.
		/// </summary>
		/// <param name="session">Session which is resetted.</param>
		internal void OnSessionResetted(object session)
		{
			if(this.SessionResetted != null){
				this.SessionResetted(session,new EventArgs());
			}
		}

		#endregion
	
		#endregion

	}
}