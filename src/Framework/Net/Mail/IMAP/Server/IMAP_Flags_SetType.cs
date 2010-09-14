using System;

namespace InfoControl.Net.Mail.IMAP.Server
{
	/// <summary>
	/// Summary description for IMAP_Flags_SetType.
	/// </summary>
	public enum IMAP_Flags_SetType
	{		
		/// <summary>
		/// Flags are added to existing ones.
		/// </summary>
		Add = 1,
		/// <summary>
		/// Flags are removed from existing ones.
		/// </summary>
		Remove = 3,
		/// <summary>
		/// Flags are replaced.
		/// </summary>
		Replace = 4,
	}
}
