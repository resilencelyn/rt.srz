using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Controls.Common
{
	public partial class SearchByDatesControl : System.Web.UI.UserControl
	{
		public event Action Clear;
		public event Action Search;

		public DateTime? DateFrom
		{
			get { return tbDateFrom.Date; }
      set { tbDateFrom.Text = value.HasValue ? value.Value.ToShortDateString() : string.Empty; }
		}

    public DateTime? DateTo
    {
      get { return tbDateTo.Date; }
      set { tbDateTo.Text = value.HasValue ? value.Value.ToShortDateString() : string.Empty; }
    }

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void btnClear_Click(object sender, EventArgs e)
		{
			tbDateFrom.Text = string.Empty;
      tbDateTo.Text = string.Empty;
			if (Clear != null)
			{
				Clear();
			}
		}

		public void SetFocus()
		{
			tbDateFrom.Focus();
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