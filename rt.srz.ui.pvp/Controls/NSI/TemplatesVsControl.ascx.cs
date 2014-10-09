using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Controls.NSI
{
  public partial class TemplatesVsControl : System.Web.UI.UserControl
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
      grid.DataSource = _service.GetTemplates();
      grid.DataBind();
    }

    protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    private void SetButtonsEnable(bool value)
    {
      var item = menu1.FindItem("Copy");
      if (item != null)
      {
        item.Enabled = value;
      }
      UtilsHelper.SetMenuButtonsEnable(value, MenuUpdatePanel, menu1);
    }

    protected void grid_SelectedIndexChanged(object sender, EventArgs e)
    {
      SetButtonsEnable(grid.SelectedDataKey != null);
    }

    protected void menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
      switch (e.Item.Value)
      {
        case "Add":
          Response.Redirect("~/Pages/NSI/TemplateVs.aspx");
          break;
        case "Copy":
          Template newTemplate = _service.CreateCopyOfTemplate((Guid)grid.SelectedDataKey.Value);
          RefreshData();
          grid.SelectedIndex = -1;
          SetButtonsEnable(grid.SelectedDataKey != null);
          //Response.Redirect(string.Format("~/Pages/NSI/TemplateVs.aspx?Id={0}", newTemplate.Id));
          break;
        case "Open":
          Open();
          break;
        case "Delete":
          if (grid.SelectedDataKey == null)
          {
            return;
          }
          //проверяем нет ли ссылок на шаблон, прежде чем удалить, если есть то не удаляем, а выдаём сообщение что удаление невозможно.
          var template = _service.GetTemplate((Guid)grid.SelectedDataKey.Value);
          if (template.RangeNumbers.Count > 0)
          {
            messageCantDeleteTemplate.Show();
            return;
          }
          _service.DeleteTemplateVs((Guid)grid.SelectedDataKey.Value);
          RefreshData();
          grid.SelectedIndex = -1;
          SetButtonsEnable(grid.SelectedDataKey != null);
          break;
      }
    }

    private void Open()
    {
      if (grid.SelectedDataKey == null)
      {
        return;
      }
      Response.Redirect(string.Format("~/Pages/NSI/TemplateVs.aspx?Id={0}", grid.SelectedDataKey.Value.ToString()));
    }

    public void RenderInPage()
    {
      UtilsHelper.AddAttributesToGridRow(Page.ClientScript, grid);
      UtilsHelper.AddDoubleClickAttributeToGrid(Page.ClientScript, grid);
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