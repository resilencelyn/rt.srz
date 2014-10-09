using rt.srz.model.interfaces.service;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages.NSI
{
  public partial class RangeNumbers : System.Web.UI.Page
  {
    private IRegulatoryService _service;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<IRegulatoryService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        SetButtonsEnable(false);
        RefreshData();
      }
    }

    private void RefreshData()
    {
      grid.DataSource = _service.GetRangeNumbers();
      grid.DataBind();
    }

    protected void menu_MenuItemClick(object sender, MenuEventArgs e)
    {
      switch (e.Item.Value)
      {
        //case "Add":
        //  Response.Redirect("~/Pages/NSI/RangeNumber.aspx");
        //  break;
        case "Open":
          Open();
          break;
        //case "Delete":
        //  if (grid.SelectedDataKey == null)
        //  {
        //    return;
        //  }
        //  _service.DeleteRangeNumber((Guid)grid.SelectedDataKey.Value);
        //  grid.SelectedIndex = -1;
        //  RefreshData();
        //  contentUpdatePanel.Update();
        //  SetButtonsEnable(grid.SelectedDataKey != null);
        //  break;
      }
    }

    private void Open()
    {
      if (grid.SelectedDataKey == null)
      {
        return;
      }
      Response.Redirect(string.Format("~/Pages/NSI/RangeNumber.aspx?Id={0}", grid.SelectedDataKey.Value.ToString()));
    }

    protected void grid_SelectedIndexChanged(object sender, EventArgs e)
    {
      SetButtonsEnable(grid.SelectedDataKey != null);
    }

    private void SetButtonsEnable(bool value)
    {
      UtilsHelper.SetMenuButtonsEnable(value, MenuUpdatePanel, menu1);
    }

    protected override void Render(HtmlTextWriter writer)
    {
      UtilsHelper.AddAttributesToGridRow(Page.ClientScript, grid);
      UtilsHelper.AddDoubleClickAttributeToGrid(Page.ClientScript, grid);
      base.Render(writer);
    }

    protected void menu1_PreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(menu1);
    }

    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "DoubleClick":
          grid.SelectedIndex = int.Parse(e.CommandArgument.ToString());
          grid_SelectedIndexChanged(null, null);
          Open();
          break;
      }
    }

  }
}