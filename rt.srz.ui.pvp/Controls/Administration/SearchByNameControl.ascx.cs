using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Controls.Administration
{
	public partial class SearchByNameControl : System.Web.UI.UserControl
	{
		public event Action Clear;
		public event Action Search;

		public string NameValue
		{
			get { return tbName.Text; }
		}

		public string NameTitle
		{
			get { return lbName.Text; }
			set { lbName.Text = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnClear_Click(object sender, EventArgs e)
		{
			tbName.Text = string.Empty;
			if (Clear != null)
			{
				Clear();
			}
		}

		public void SetFocus()
		{
			tbName.Focus();
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			if (Search != null)
			{
				Search();
			}
		}
	}
}