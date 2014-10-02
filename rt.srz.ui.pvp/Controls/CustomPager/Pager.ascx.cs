using System;

namespace rt.srz.ui.pvp.Controls.CustomPager
{
	public partial class Pager : System.Web.UI.UserControl
	{
		public event PageChangedEventHandler PageIndexChanged;

		public event PageChangedEventHandler PageSizeChanged;

    #region Constructor
    public Pager()
    {
      ReloadOnPostBack = true;
    }
    #endregion


    #region Properties

    public bool ReloadOnPostBack
    {
      get;
      set;
    }
    
    public int CurrentPageIndex
		{
			get { return ViewState["CurrentPageIndex"] != null ? (int)ViewState["CurrentPageIndex"] : -1 ;}
			set 
			{	
				ViewState["CurrentPageIndex"] = value;
				ddlPageNumber.SelectedValue = (value + 1).ToString();
			}
		}

		public int TotalPages
		{
			get { return ViewState["TotalPages"] != null ? (int)ViewState["TotalPages"] : -1; }
			set { ViewState["TotalPages"] = value; }
		}

		#endregion

		#region Methods

		public void SetPageSize(int pageSize)
		{
			int index = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue(pageSize.ToString()));
			if (index >= 0)
			{
				ddlPageSize.SelectedIndex = index;
			}
		}

		public void ResetCurrentPage()
		{
			if (ddlPageNumber.Items.Count > 0)
			{
				ViewState["CurrentPageIndex"] = 0;
				ddlPageNumber.Items[0].Selected = true;
				return;
			}
			ViewState["CurrentPageIndex"] = -1;
		}

		public void ReloadPager()
		{
			ddlPageNumber.Items.Clear();
			for (int count = 1; count <= this.TotalPages; ++count)
				ddlPageNumber.Items.Add(count.ToString());

			if (ddlPageNumber.Items.Count > 0)
				ddlPageNumber.Items[0].Selected = true;

			lblShowRecords.Text = string.Format(" {0} ", this.TotalPages.ToString());
		}
		#endregion
		
		#region Events
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack && ReloadOnPostBack)
			{
				ReloadPager();
			}
		}

		protected void ddlPageNumber_SelectedIndexChanged(object sender, EventArgs e)
		{
			CustomPageChangeArgs args = new CustomPageChangeArgs();
			args.CurrentPageSize = Convert.ToInt32(this.ddlPageSize.SelectedItem.Value);
			CurrentPageIndex = args.CurrentPageIndex = Convert.ToInt32(this.ddlPageNumber.SelectedItem.Text) - 1;
			args.TotalPages = Convert.ToInt32(this.lblShowRecords.Text);
			PageIndexChanged(this, args);
			lblShowRecords.Text = string.Format(" {0} ", args.TotalPages.ToString());
		}

		protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			CustomPageChangeArgs args = new CustomPageChangeArgs();
			args.CurrentPageSize = Convert.ToInt32(this.ddlPageSize.SelectedItem.Value);
			args.CurrentPageIndex = 0;
			args.TotalPages = Convert.ToInt32(this.lblShowRecords.Text);
			PageSizeChanged(this, args);

			ddlPageNumber.Items.Clear();
			for (int count = 1; count <= this.TotalPages; ++count)
				ddlPageNumber.Items.Add(count.ToString());
      if (ddlPageNumber.Items.Count > 0)
      {
        ddlPageNumber.Items[0].Selected = true;
      }
			lblShowRecords.Text = string.Format(" {0} ", this.TotalPages.ToString());
		}
		#endregion
	}
}