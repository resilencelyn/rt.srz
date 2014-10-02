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

  /// <summary>
  /// Контрол для редактирования ролей
  /// </summary>
  public partial class RolesControl : System.Web.UI.UserControl
  {
    private ISecurityService _securityService;

    private int SelectedRoleId
    {
      get { return lstRoles.SelectedIndex >= 0 ? int.Parse(lstRoles.SelectedValue) : -1; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      _securityService = ObjectFactory.GetInstance<ISecurityService>();
      searchByNameControl.Clear += searchByNameControl_Clear;
      searchByNameControl.Search += searchByNameControl_Search;
    }

    void searchByNameControl_Search()
    {
      lstRoles.DataSource = _securityService.GetRolesByNameContains(searchByNameControl.NameValue);
      lstRoles.DataBind();
      contentUpdatePanel.Update();
    }

    void searchByNameControl_Clear()
    {
      RefreshData();
      contentUpdatePanel.Update();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      UtilsHelper.AddDoubleClick(lstRoles, Page.ClientScript);
      if (!IsPostBack)
      {
        RefreshData();
        menu1.FindItem(SessionConsts.CDelete).NavigateUrl = confirmDelete.ViewConfirmScript;
      }
      else
      {
        //удаление
        UtilsHelper.PerformConfirmedAction(confirmDelete, DeleteRole, Request);
        //двойной клик по списку
        UtilsHelper.PerformDoubleClickAction(lstRoles.UniqueID, OpenRole, Request);
      }
    }

    public void RefreshData()
    {
      if (Session[SessionConsts.CAdminPermission] == null)
      {
        Session[SessionConsts.CAdminPermission] = _securityService.IsUserHasAdminPermissions(_securityService.GetCurrentUser());
      }
      menu1.Enabled = (bool)Session[SessionConsts.CAdminPermission];

      IList<Role> roles = _securityService.GetRoles();
      lstRoles.DataSource = roles;
      lstRoles.DataBind();
      if (lstRoles.SelectedIndex < 0 && roles.Count > 0)
      {
        lstRoles.SelectedIndex = 0;
        lstRoles_SelectedIndexChanged(null, null);
      }
    }

    private void OpenRole()
    {
      if (string.IsNullOrEmpty(lstRoles.SelectedValue))
      {
        return;
      }
      Response.Redirect(string.Format("~/Pages/Administrations/RoleEx.aspx?RoleId={0}", lstRoles.SelectedValue));
    }

    protected void menuRoles_MenuItemClick(object sender, MenuEventArgs e)
    {
      switch (e.Item.Value)
      {
        case "Add":
          Response.Redirect("~/Pages/Administrations/Role.aspx");
          break;
        case "AddEx":
          Response.Redirect("~/Pages/Administrations/RoleEx.aspx");
          break;
        case "Open":
          OpenRole();
          break;
        case "Delete":
          DeleteRole();
          break;
        case "AssignPermission":
          if (lstRoles.SelectedItem == null)
          {
            return;
          }
          Response.Redirect(string.Format("~/Pages/Administrations/AssignPermissionsToRole.aspx?RoleName={0}&RoleId={1}", lstRoles.SelectedItem.Text, lstRoles.SelectedItem.Value));
          break;
      }
    }

    private void DeleteRole()
    {
      if (string.IsNullOrEmpty(lstRoles.SelectedValue))
      {
        return;
      }
      _securityService.DeleteRole(Guid.Parse(lstRoles.SelectedValue));
      lstRoles.Items.RemoveAt(lstRoles.SelectedIndex);
      contentUpdatePanel.Update();
    }

    protected void lstRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lstRoles.SelectedItem == null)
      {
        return;
      }
      //TODO: заменить на код Utils.C_AdminCode = 1
      if (lstRoles.SelectedItem.Text.ToLower() == "администратор")
      {
        menu1.FindItem("Delete").Enabled = false;
      }
      else
      {
        menu1.FindItem("Delete").Enabled = (bool)Session[SessionConsts.CAdminPermission];
      }
      MenuUpdatePanel.Update();
    }

    protected void menu1_PreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(menu1);
    }

  }
}