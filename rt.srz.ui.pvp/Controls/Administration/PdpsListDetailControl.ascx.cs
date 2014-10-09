using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using StructureMap;
using rt.srz.ui.pvp.Enumerations;
using AjaxControlToolkit;

namespace rt.srz.ui.pvp.Controls.Administration
{
  //Список пунктов выдачи полисов сохраняется в сессии Session["pdpList"], поскольку пока не сохренены измененияс базой не происходит никаких действий
  //словарь соответсвующий рабочим станциям (пункт выдачи, список станций) сохраняется так же в сесии Session["workstationDict"]

  public partial class PdpsListDetailControl : System.Web.UI.UserControl
  {
    private IRegulatoryService regulatoryService;
    private Guid _smoId;

    private PdpGridRow SelectedPdp
    {
      get
      {
        IList<PdpGridRow> list = (IList<PdpGridRow>)Session[SessionConsts.CPdpList];
        return pdpsGridView.SelectedDataKey == null ? null : list.Where(r => r.Id == (Guid)this.pdpsGridView.SelectedDataKey.Value).FirstOrDefault();
      }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      regulatoryService = ObjectFactory.GetInstance<IRegulatoryService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Request.QueryString["SmoId"] == null)
      {
        _smoId = Guid.Empty;
      }
      else
      {
        _smoId = Guid.Parse(Request.QueryString["SmoId"]);
      }
      pdpDetailControl.BindParentList += BindParentList;
      pdpDetailControl.EnabledPdpGrid += EnablePdpGrid;
      if (!IsPostBack)
      {
        UtilsHelper.SetMenuButtonsEnable(SelectedPdp != null, menuPanel, menu1);
        pdpDetailControl.LoadPdps(_smoId, pdpsGridView);
      }
    }

    private void BindParentList(IList<PdpGridRow> list)
    {
      pdpsGridView.DataSource = list;
      pdpsGridView.DataBind();
    }

    protected void EnablePdpGrid(bool value)
    {
      pdpsGridView.Enabled = value;
      if (!value)
      {
        foreach (GridViewRow row in pdpsGridView.Rows)
        {
          row.Attributes.Remove("onmouseover");
          row.Attributes.Remove("onmouseout");
          row.Attributes.Remove("onclick");
          row.Attributes.Remove("ondblclick");
        }
      }
      else
      {
        UtilsHelper.AddAttributesToGridRow(Page.ClientScript, pdpsGridView);
        UtilsHelper.AddDoubleClickAttributeToGrid(Page.ClientScript, pdpsGridView);
      }

      contentUpdatePanel.Update();
      menu1.Enabled = value;
      menuPanel.Update();
    }

    protected void pdpsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      UtilsHelper.AddAttributesToGridDataBound(Page.ClientScript, pdpsGridView, e);
      UtilsHelper.AddDoubleClickAttributeToGridDataBound(Page.ClientScript, pdpsGridView, e);
    }

    protected void pdpsGridView_Deleting(Object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void menu_MenuItemClick(object sender, MenuEventArgs e)
    {

      switch (e.Item.Value)
      {
        case "Add":
          pdpDetailControl.AddPdp();
          CollapsiblePanelExtender1.ClientState = "false";
          CollapsiblePanelExtender1.Collapsed = false;
          contentUpdatePanel.Update();
          break;
        case "Open":
          OpenPdp();
          break;
        case "Delete":
          if (pdpsGridView.SelectedDataKey == null)
          {
            return;
          }
          pdpDetailControl.DeletePdp(SelectedPdp, pdpsGridView);
          contentUpdatePanel.Update();
          UtilsHelper.SetMenuButtonsEnable(SelectedPdp != null, menuPanel, menu1);
          break;
      }

    }

    private void OpenPdp()
    {
      if (pdpsGridView.SelectedDataKey == null)
      {
        return;
      }
      pdpDetailControl.OpenPdp(SelectedPdp);
      CollapsiblePanelExtender1.ClientState = "false";
      CollapsiblePanelExtender1.Collapsed = false;
      contentUpdatePanel.Update();
    }

    public void SaveChanges(Guid newSmoId)
    {
      pdpDetailControl.SavePdpGridList(_smoId != Guid.Empty ? _smoId : newSmoId);
    }

    protected void pdpsGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
      pdpDetailControl.ChangePdp(SelectedPdp, pdpsGridView);
      UtilsHelper.SetMenuButtonsEnable(SelectedPdp != null, menuPanel, menu1);
    }

    protected void menu1_PreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(menu1);
    }

    protected void pdpsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "DoubleClick":
          pdpsGridView.SelectedIndex = int.Parse(e.CommandArgument.ToString());
          pdpsGridView_SelectedIndexChanged(null, null);
          OpenPdp();
          break;
      }
    }
  }
}