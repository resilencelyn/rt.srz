// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RolesControl.ascx.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls.Administration
{
  using System;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.core.model.interfaces;
  using rt.srz.model.interfaces.service;
  using rt.srz.ui.pvp.Enumerations;

  using StructureMap;

  /// <summary>
  ///   Контрол для редактирования ролей
  /// </summary>
  public partial class RolesControl : UserControl
  {
    #region Fields

    /// <summary>
    /// The _security service.
    /// </summary>
    private ISecurityService securityService;

    #endregion

    #region Properties

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The refresh data.
    /// </summary>
    public void RefreshData()
    {
      if (Session[SessionConsts.CAdminPermission] == null)
      {
        Session[SessionConsts.CAdminPermission] =
          securityService.IsUserHasAdminPermissions(securityService.GetCurrentUser());
      }

      menu1.Enabled = (bool)Session[SessionConsts.CAdminPermission];

      var roles = securityService.GetRoles();
      lstRoles.DataSource = roles;
      lstRoles.DataBind();
      if (lstRoles.SelectedIndex < 0 && roles.Count > 0)
      {
        lstRoles.SelectedIndex = 0;
        LstRolesSelectedIndexChanged(null, null);
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The page_ init.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Init(object sender, EventArgs e)
    {
      securityService = ObjectFactory.GetInstance<ISecurityService>();
      searchByNameControl.Clear += searchByNameControl_Clear;
      searchByNameControl.Search += searchByNameControl_Search;
    }

    /// <summary>
    /// The page_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
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
        // удаление
        UtilsHelper.PerformConfirmedAction(confirmDelete, DeleteRole, Request);

        // двойной клик по списку
        UtilsHelper.PerformDoubleClickAction(lstRoles.UniqueID, OpenRole, Request);
      }
    }

    /// <summary>
    /// The lst roles_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void LstRolesSelectedIndexChanged(object sender, EventArgs e)
    {
      if (lstRoles.SelectedItem == null)
      {
        return;
      }

      // TODO: заменить на код Utils.C_AdminCode = 1
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

    /// <summary>
    /// The menu 1_ pre render.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Menu1PreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(menu1);
    }

    /// <summary>
    /// The menu roles_ menu item click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
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

          Response.Redirect(
                            string.Format(
                                          "~/Pages/Administrations/AssignPermissionsToRole.aspx?RoleName={0}&RoleId={1}", 
                                          lstRoles.SelectedItem.Text, 
                                          lstRoles.SelectedItem.Value));
          break;
      }
    }

    /// <summary>
    /// The delete role.
    /// </summary>
    private void DeleteRole()
    {
      if (string.IsNullOrEmpty(lstRoles.SelectedValue))
      {
        return;
      }

      securityService.DeleteRole(Guid.Parse(lstRoles.SelectedValue));
      lstRoles.Items.RemoveAt(lstRoles.SelectedIndex);
      contentUpdatePanel.Update();
    }

    /// <summary>
    /// The open role.
    /// </summary>
    private void OpenRole()
    {
      if (string.IsNullOrEmpty(lstRoles.SelectedValue))
      {
        return;
      }

      Response.Redirect(string.Format("~/Pages/Administrations/RoleEx.aspx?RoleId={0}", lstRoles.SelectedValue));
    }

    /// <summary>
    /// The search by name control_ clear.
    /// </summary>
    private void searchByNameControl_Clear()
    {
      RefreshData();
      contentUpdatePanel.Update();
    }

    /// <summary>
    /// The search by name control_ search.
    /// </summary>
    private void searchByNameControl_Search()
    {
      lstRoles.DataSource = securityService.GetRolesByNameContains(searchByNameControl.NameValue);
      lstRoles.DataBind();
      contentUpdatePanel.Update();
    }

    #endregion
  }
}