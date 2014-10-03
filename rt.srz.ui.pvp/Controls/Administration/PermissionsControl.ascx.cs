// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionsControl.ascx.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls.Administration
{
  using System;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.srz.model.interfaces.service;
  using rt.srz.ui.pvp.Enumerations;

  using StructureMap;

  /// <summary>
  /// The permissions control.
  /// </summary>
  public partial class PermissionsControl : UserControl
  {
    #region Fields

    /// <summary>
    /// The _security service.
    /// </summary>
    private ISecurityService securityService;

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

      var list = securityService.GetPermissions();
      lstPermission.DataSource = list;
      lstPermission.DataBind();
      if (lstPermission.SelectedIndex < 0 && list.Count > 0)
      {
        lstPermission.SelectedIndex = 0;
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
      searchByNameControl.Clear += SearchByNameControlClear;
      searchByNameControl.Search += SearchByNameControlSearch;
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
      UtilsHelper.AddDoubleClick(lstPermission, Page.ClientScript);
      if (!IsPostBack)
      {
        RefreshData();
        menu1.FindItem(SessionConsts.CDelete).NavigateUrl = confirmDelete.ViewConfirmScript;
      }
      else
      {
        // удаление
        UtilsHelper.PerformConfirmedAction(confirmDelete, DeletePermission, Request);

        // двойной клик по списку
        UtilsHelper.PerformDoubleClickAction(lstPermission.UniqueID, Open, Request);
      }
    }

    /// <summary>
    /// The menu 1_ menu item click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Menu1MenuItemClick(object sender, MenuEventArgs e)
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

          Response.Redirect(
                            string.Format(
                                          "~/Pages/Administrations/AssignRolesForPermission.aspx?permissionId={0}&permissionName={1}", 
                                          lstPermission.SelectedItem.Value, 
                                          lstPermission.SelectedItem.Text));
          break;
      }
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
    /// The delete permission.
    /// </summary>
    private void DeletePermission()
    {
      if (string.IsNullOrEmpty(lstPermission.SelectedValue))
      {
        return;
      }

      securityService.DeletePermission(Guid.Parse(lstPermission.SelectedValue));
      lstPermission.Items.RemoveAt(lstPermission.SelectedIndex);
      contentUpdatePanel.Update();
    }

    /// <summary>
    /// The open.
    /// </summary>
    private void Open()
    {
      if (string.IsNullOrEmpty(lstPermission.SelectedValue))
      {
        return;
      }

      Response.Redirect(
                        string.Format(
                                      "~/Pages/Administrations/PermissionEx.aspx?PermissionId={0}", 
                                      lstPermission.SelectedValue));
    }

    /// <summary>
    /// The search by name control_ clear.
    /// </summary>
    private void SearchByNameControlClear()
    {
      RefreshData();
      contentUpdatePanel.Update();
    }

    /// <summary>
    /// The search by name control_ search.
    /// </summary>
    private void SearchByNameControlSearch()
    {
      lstPermission.DataSource = securityService.GetPermissionsByNameContains(searchByNameControl.NameValue);
      lstPermission.DataBind();
      contentUpdatePanel.Update();
    }

    #endregion
  }
}