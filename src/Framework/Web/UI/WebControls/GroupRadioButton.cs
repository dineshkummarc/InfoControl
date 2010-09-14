using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace InfoControl.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for GroupRadioButton.
	/// </summary>
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:GroupRadioButton runat=server></{0}:GroupRadioButton>")]
    [ToolboxBitmap(typeof(GroupRadioButton))]
	public class GroupRadioButton : System.Web.UI.WebControls.RadioButton, IPostBackDataHandler
	{
		public GroupRadioButton() : base()
		{
		}
		
		
		[Bindable(true), 
		Category("Appearance"), 
		DefaultValue(""),
		Description("Propriedade nova para devolver o Value do Radio")] 
		private string Value
		{
			get
			{
				string val = Attributes["value"];
				if(val == null)
					val = UniqueID;
				else
					val = UniqueID + "_" + val;
				return val;
			}
		}

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			// Adiciono os atributos na m�o por causa do problema do NamingContainer
			output.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
			output.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
			output.AddAttribute(HtmlTextWriterAttribute.Name, GroupName);
			output.AddAttribute(HtmlTextWriterAttribute.Value, Value);
			
			// Verifico se o atributo pai Checked e Enabled pois essas bostas n�o tem par valor :-(
			if(Checked)
				output.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
			if(!Enabled)
				output.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
		
			// Aqui t� um macete para concatenar alguma fun��o 
			// javascript que queira adicionar sem perder o Evento Onclick no Server
			string onClick = Attributes["onclick"];
			if(AutoPostBack)
			{
				if(onClick != null)
					onClick = String.Empty;
				onClick += Page.ClientScript.GetPostBackEventReference(this, String.Empty);
				output.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick);
				output.AddAttribute("language", "javascript");
			}
			else
			{
				if(onClick != null)
					output.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick);
			}
			
			output.RenderBeginTag(HtmlTextWriterTag.Input);
			output.RenderEndTag();			
		}

		/// <summary>
		/// Seto o filho da puta do NamingContainer para null assim ele n�o me fode mais.
		/// </summary>
		public override Control NamingContainer
		{
			get
			{
				return null;
			}
		}



		#region Aqui ficam as implement���es do PostHandler pois sen�o algumas propriedades n�o vem"
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			OnCheckedChanged(EventArgs.Empty);
		}

		bool IPostBackDataHandler.LoadPostData(string postDataKey, 
			System.Collections.Specialized.NameValueCollection postCollection)
		{
			bool result = false;
			string value = postCollection[GroupName];
			if((value != null) && (value == Value))
			{
				if(!Checked)
				{
					Checked = true;
					result = true;
				}
			}
			else
			{
				if(Checked)
					Checked = false;
			}
			return result;
		}
		#endregion

	}
}
