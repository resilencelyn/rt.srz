using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.srz.ui.pvp.Controllers;

namespace rt.srz.ui.pvp.Pages.Administrations
{
	public partial class SmoAddCancelPage : System.Web.UI.MasterPage
	{
		public event Action Save;
		public event Action Cancel;

		public string ActionTitle
		{
			get { return lbTitle.Text; }
			set { lbTitle.Text = value; }
		}

		//private string _postBackUrl;
		//public string PostBackUrl
		//{
		//	get { return _postBackUrl; }
		//	set { _postBackUrl = value; }
		//}

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				lbTitle.Text = ActionTitle;
				//btnSave.PostBackUrl = _postBackUrl;
				//btnCancel.PostBackUrl = _postBackUrl;
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Page.Validate();
			if (!Page.IsValid)
			{
				return;
			}
			if (Save != null)
			{
				Save();
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			if (Cancel != null)
			{
				Cancel();
			}
		}

		public void HideSaveButton()
		{
			btnSave.Visible = false;
		}

	}
}