using rt.srz.model.interfaces.service;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages.Administrations
{
  public partial class Tfoms : System.Web.UI.Page
  {
    private ISmoService _service;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<ISmoService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        grid.DataSource = _service.GetAllTfoms().OrderBy(x => x.Code);
        grid.DataBind();
      }
    }

    protected void btnTurn_Click(object sender, EventArgs e)
    {
      Button btn = (Button)sender;
      GridViewRow row = (GridViewRow)btn.NamingContainer;
      HiddenField Id = (HiddenField)row.FindControl("hId");
      HiddenField hIsOnline = (HiddenField)row.FindControl("hIsOnline");
      bool newIsOnline = !bool.Parse(hIsOnline.Value);
      hIsOnline.Value = newIsOnline.ToString();
      _service.SetTfomIsOnline(Guid.Parse(Id.Value), newIsOnline);
      if (newIsOnline)
      {
        btn.Text = UtilsHelper.c_TurnOff;
      }
      else
      {
        btn.Text = UtilsHelper.c_TurnOn;
      }
    }

  }
}