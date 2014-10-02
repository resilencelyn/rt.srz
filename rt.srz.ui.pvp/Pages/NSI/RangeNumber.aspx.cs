using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Enumerations;
using rt.srz.ui.pvp.Pages.Administrations;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RangeNumberData = rt.srz.model.srz.RangeNumber;

namespace rt.srz.ui.pvp.Pages.NSI
{
  public partial class RangeNumber : System.Web.UI.Page
  {
    private BaseAddCancelPage MasterPage
    {
      get { return (BaseAddCancelPage)this.Master; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      MasterPage.Save += MasterPage_Save;
      MasterPage.Cancel += MasterPage_Cancel;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        MasterPage.ActionTitle = "";
      }
    }

    void MasterPage_Cancel()
    {
      RedirectUtils.RedirectToRangeNumbers(Response);
    }

    void MasterPage_Save()
    {
      rangeNumberControl.SaveChanges();
      RedirectUtils.RedirectToRangeNumbers(Response);
    }
  }
}