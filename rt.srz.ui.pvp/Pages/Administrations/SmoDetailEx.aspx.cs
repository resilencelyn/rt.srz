using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.srz.ui.pvp.Controllers;
using rt.srz.ui.pvp.Enumerations;
using rt.srz.model.srz;

namespace rt.srz.ui.pvp.Pages.Administrations
{
  public partial class SmoDetailEx : System.Web.UI.Page
  {
    private string _oid;

    private SmoAddCancelPage MasterPage
    {
      get { return (SmoAddCancelPage)this.Master; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      MasterPage.Save += MasterPage_Save;
      MasterPage.Cancel += MasterPage_Cancel;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Request.QueryString["type"] != null && Request.QueryString["type"] == "mo")
      {
        _oid = Oid.Mo;
        pdpsListDetailControl.Visible = false;
      }
      else
      {
        _oid = Oid.Smo;
        pdpsListDetailControl.Visible = true;
      }
      if (!IsPostBack)
      {
        MasterPage.ActionTitle = "";//"Добавление группы";
      }
    }

    void MasterPage_Cancel()
    {
      Session.Remove("pdpList");
      Session.Remove("workstationDict");
      Redirect();
    }

    void MasterPage_Save()
    {
      var newSmoId = smoDetailControl.SaveChanges();
      if (_oid == Oid.Smo)
      {
        pdpsListDetailControl.SaveChanges(newSmoId);
      }
      Session.Remove("pdpList");
      Session.Remove("workstationDict");
      Redirect();
    }

    private void Redirect()
    {
      if (_oid == Oid.Mo)
      {
        RedirectUtils.RedirectToAdministrationMos(Response);
      }
      else
      {
        RedirectUtils.RedirectToAdministrationSmos(Response);
      }
    }

    /// <summary>
    /// Обслуживает Intellisense для ввода имени
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <param name="contextKey"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static List<string> GetFirstNameAutoComplete(string prefixText, int count)
    {
      return IntellisenseController.GetFirstNameAutoComplete(prefixText, count);
    }

    /// <summary>
    /// Обслуживает Intellisense для ввода отчества
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <param name="contextKey"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static List<string> GetMiddleNameAutoComplete(string prefixText, int count, string contextKey)
    {
      return IntellisenseController.GetMiddleNameAutoComplete(prefixText, count, contextKey);
    }

  }
}