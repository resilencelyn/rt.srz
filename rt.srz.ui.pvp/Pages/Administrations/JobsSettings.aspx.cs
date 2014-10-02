using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.atl.business.quartz;
using rt.srz.ui.pvp.Enumerations;

namespace rt.srz.ui.pvp.Pages.Administration
{

  public partial class JobsSettings : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      //if (!IsPostBack)
      //  tbMaxJobCount.Text = FirstLoadingToPvpJob.MaxCountJob.ToString();
    }

    protected void btnSaveStatement_Click(object sender, EventArgs e)
    {
      //int maxJobCount = 0;
      //if (int.TryParse(tbMaxJobCount.Text, out maxJobCount))
      //{
      //  FirstLoadingToPvpJob.MaxCountJob = maxJobCount;
      //  RedirectUtils.RedirectToMain(Response);
      //}
      //else
      //{
      //  tbMaxJobCount.Text = FirstLoadingToPvpJob.MaxCountJob.ToString();
      //}
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      RedirectUtils.RedirectToMain(Response);
    }
  }
}