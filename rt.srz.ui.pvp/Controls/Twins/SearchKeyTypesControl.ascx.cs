using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StructureMap;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Enumerations;

namespace rt.srz.ui.pvp.Controls.Twins
{
  public partial class SearchKeyTypesControl : System.Web.UI.UserControl
  {
    #region Constants
    private const string addMenuItemValue = "Add";
    private const string openMenuItemValue = "Open";
    private const string deleteMenuItemValue = "Delete";
    #endregion

    #region Fields
    private ITfomsService tfomsService = null;
    #endregion

    #region Events
    protected void Page_Init(object sender, EventArgs e)
    {
      tfomsService = ObjectFactory.GetInstance<ITfomsService>();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        SetButtonsEnable(false);
        LoadData();
      }
    }

    private void LoadData()
    {
      grid.DataSource = tfomsService.GetSearchKeyTypesByTFoms();
      grid.DataBind();
    }

    protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
      {
        return;
      }
      e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
      e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
      e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(grid, "Select$" + e.Row.RowIndex);
    }

    protected void grid_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (grid.SelectedDataKey == null)
      {
        SetButtonsEnable(false);
        return;
      }
      var fom = ((HiddenField)grid.SelectedRow.FindControl("hfom")).Value;
      SetButtonsEnable(grid.SelectedDataKey != null && !string.IsNullOrEmpty(fom));
    }

    private void SetButtonsEnable(bool value)
    {
      UtilsHelper.SetMenuButtonsEnable(value, MenuUpdatePanel, menu1);
    }
    
    protected void menu_MenuItemClick(object sender, MenuEventArgs e)
    {
      switch (e.Item.Value)
      {
        case "Add":
          Response.Redirect("~/Pages/Twins/SearchKeyTypeDetail.aspx");
          break;
        case "Open":
          if (grid.SelectedDataKey == null)
            return;
          Response.Redirect(string.Format("~/Pages/Twins/SearchKeyTypeDetail.aspx?SearchKeyTypeId={0}", grid.SelectedDataKey.Value));
          break;
        case "Delete":
          if (grid.SelectedDataKey == null)
            return;
          tfomsService.DeleteSearchKeyType((Guid)grid.SelectedDataKey.Value);
          grid.DeleteRow(grid.SelectedIndex);
          grid.SelectedIndex = -1;
          LoadData();
          contentUpdatePanel.Update();
          SetButtonsEnable(grid.SelectedDataKey != null);
					break;
      }
    }
    #endregion

    protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void menu1_PreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(menu1);
    }

  }
}