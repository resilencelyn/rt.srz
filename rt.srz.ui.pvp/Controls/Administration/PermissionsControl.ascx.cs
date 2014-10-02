namespace rt.srz.ui.pvp.Controls.Administration
{
  using System;
  using System.Collections.Generic;
  using System.Web.UI.WebControls;

  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using StructureMap;
  using rt.srz.ui.pvp.Controls.Common;
  using rt.srz.ui.pvp.Enumerations;
  using System.Web.UI;

  public partial class PermissionsControl : System.Web.UI.UserControl
  {
    private ISecurityService _securityService;

    protected void Page_Init(object sender, EventArgs e)
    {
      _securityService = ObjectFactory.GetInstance<ISecurityService>();
      searchByNameControl.Clear += searchByNameControl_Clear;
      searchByNameControl.Search += searchByNameControl_Search;
    }

    void searchByNameControl_Search()
    {
      lstPermission.DataSource = _securityService.GetPermissionsByNameContains(searchByNameControl.NameValue);
      lstPermission.DataBind();
      contentUpdatePanel.Update();
    }

    void searchByNameControl_Clear()
    {
      RefreshData();
      contentUpdatePanel.Update();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      UtilsHelper.AddDoubleClick(lstPermission, Page.ClientScript);
      if (!IsPostBack)
      {
        RefreshData();
        menu1.FindItem(SessionConsts.CDelete).NavigateUrl = confirmDelete.ViewConfirmScript;
      }
      else
      {
        //удаление
        UtilsHelper.PerformConfirmedAction(confirmDelete, DeletePermission, Request);
        //двойной клик по списку
        UtilsHelper.PerformDoubleClickAction(lstPermission.UniqueID, Open, Request);
      }
    }

    public void RefreshData()
    {
      if (Session[SessionConsts.CAdminPermission] == null)
      {
        Session[SessionConsts.CAdminPermission] = _securityService.IsUserHasAdminPermissions(_securityService.GetCurrentUser());
      }
      menu1.Enabled = (bool)Session[SessionConsts.CAdminPermission];

      IList<Permission> list = _securityService.GetPermissions();
      lstPermission.DataSource = list;
      lstPermission.DataBind();
      if (lstPermission.SelectedIndex < 0 && list.Count > 0)
      {
        lstPermission.SelectedIndex = 0;
      }
    }

    private void Open()
    {
      if (string.IsNullOrEmpty(lstPermission.SelectedValue))
      {
        return;
      }
      Response.Redirect(string.Format("~/Pages/Administrations/PermissionEx.aspx?PermissionId={0}", lstPermission.SelectedValue));    
    }

    protected void menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
      switch (e.Item.Value)
      {
        case "Add":
          Response.Redirect("~/Pages/Administrations/Permission.aspx");
          break;
        case "AddEx":
          Response.Redirect("~/Pages/Administrations/PermissionEx.aspx");
          break;
        case "Open":
          Open();
          break;
        case "Delete":
          DeletePermission();
          break;
        case "AssignRoles":
          if (string.IsNullOrEmpty(lstPermission.SelectedValue))
          {
            return;
          }
          Response.Redirect(string.Format("~/Pages/Administrations/AssignRolesForPermission.aspx?permissionId={0}&permissionName={1}",
            lstPermission.SelectedItem.Value, lstPermission.SelectedItem.Text));
          break;
      }
    }

    private void DeletePermission()
    {
      if (string.IsNullOrEmpty(lstPermission.SelectedValue))
      {
        return;
      }
      _securityService.DeletePermission(Guid.Parse(lstPermission.SelectedValue));
      lstPermission.Items.RemoveAt(lstPermission.SelectedIndex);
      contentUpdatePanel.Update();
    }

    protected void menu1_PreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(menu1);
    }

  }
}