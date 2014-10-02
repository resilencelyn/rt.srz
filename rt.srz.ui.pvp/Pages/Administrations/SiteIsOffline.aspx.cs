using rt.srz.ui.pvp.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages.Administrations
{
  using rt.core.model;

  public partial class SiteIsOffline : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        lb.Text = string.Format(lb.Text, SiteMode.OnLineDateTime);
      }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
      if (SiteMode.IsOnline)
      {
        RedirectUtils.RedirectToLogin(Response);
      }
    }
  }
}