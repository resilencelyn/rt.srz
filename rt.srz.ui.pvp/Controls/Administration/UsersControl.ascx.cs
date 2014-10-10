// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersControl.ascx.cs" company="РусБИТех">
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
  /// The users control.
  /// </summary>
  public partial class UsersControl : UserControl
  {
    #region Fields

    /// <summary>
    /// The _security service.
    /// </summary>
    private ISecurityService securityService;

    private ITfomsService tfomsService;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The refresh data.
    /// </summary>
    public void RefreshData()
    {
      foreach (MenuItem item in menu1.Items)
      {
        if (item.Value != @"AssignPdp")
        {
          item.Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu];
        }
      }

      var users = tfomsService.GetUsersByCurrent();
      lstUsers.DataSource = users;
      lstUsers.DataBind();
      if (users != null && (lstUsers.SelectedIndex < 0 && users.Count > 0))
      {
        lstUsers.SelectedIndex = 0;
        LstUsersSelectedIndexChanged(null, null);
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
      tfomsService = ObjectFactory.GetInstance<ITfomsService>();
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
      UtilsHelper.AddDoubleClick(lstUsers, Page.ClientScript);
      if (!IsPostBack)
      {
        RefreshData();
        menu1.FindItem(SessionConsts.CDelete).NavigateUrl = confirmDelete.ViewConfirmScript;
      }
      else
      {
        // удаление
        UtilsHelper.PerformConfirmedAction(confirmDelete, DeleteUser, Request);

        // двойной клик по списку
        UtilsHelper.PerformDoubleClickAction(lstUsers.UniqueID, Open, Request);
      }
    }

    /// <summary>
    /// The lst users_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void LstUsersSelectedIndexChanged(object sender, EventArgs e)
    {
      if (lstUsers.SelectedItem == null)
      {
        return;
      }

      if (lstUsers.SelectedItem.Text.ToLower() == "admin")
      {
        menu1.FindItem("Delete").Enabled = false;

        // назначать пдп для администратора может только он сам
        menu1.FindItem("AssignPdp").Enabled = securityService.GetCurrentUser().IsAdmin;
      }
      else
      {
        var allowAssignPdp = true;
        var currentUser = securityService.GetCurrentUser();

        // администратор смо не может назначать пункт выдачи для администратора территорального фонда
        if (!securityService.IsUserHasAdminPermissions(currentUser) && !securityService.IsUserAdminTfoms(currentUser.Id))
        {
          allowAssignPdp = !securityService.IsUserAdminTfoms(Guid.Parse(lstUsers.SelectedItem.Value));
        }

        menu1.FindItem("Delete").Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu];
        menu1.FindItem("AssignPdp").Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu] && allowAssignPdp;
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
    /// The menu_ menu item click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void MenuMenuItemClick(object sender, MenuEventArgs e)
    {
      switch (e.Item.Value)
      {
        case "Add":
          Response.Redirect("~/Pages/Administrations/User.aspx");
          break;
        case "AddEx":
          Response.Redirect("~/Pages/Administrations/UserEx.aspx");
          break;
        case "Open":
          Open();
          break;
        case "AssignPdp":
          if (string.IsNullOrEmpty(lstUsers.SelectedValue))
          {
            return;
          }

          Response.Redirect(
                            string.Format(
                                          "~/Pages/Administrations/AssignPdp.aspx?UserId={0}&UserName={1}", 
                                          lstUsers.SelectedValue, 
                                          lstUsers.SelectedItem.Text));
          break;
        case "Delete":
          DeleteUser();
          break;
        case "AssignGroups":
          if (lstUsers.SelectedItem == null)
          {
            return;
          }

          Response.Redirect(
                            string.Format(
                                          "~/Pages/Administrations/AssignGroupsToUser.aspx?UserName={0}&UserId={1}", 
                                          lstUsers.SelectedItem.Text, 
                                          lstUsers.SelectedItem.Value));
          break;
        case "AssignRoles":
          if (lstUsers.SelectedItem == null)
          {
            return;
          }

          Response.Redirect(
                            string.Format(
                                          "~/Pages/Administrations/AssignRolesToUser.aspx?UserName={0}&UserId={1}", 
                                          lstUsers.SelectedItem.Text, 
                                          lstUsers.SelectedItem.Value));
          break;
      }
    }

    /// <summary>
    /// The delete user.
    /// </summary>
    private void DeleteUser()
    {
      if (string.IsNullOrEmpty(lstUsers.SelectedValue))
      {
        return;
      }

      securityService.DeleteUser(Guid.Parse(lstUsers.SelectedValue));
      lstUsers.Items.RemoveAt(lstUsers.SelectedIndex);
      contentUpdatePanel.Update();
    }

    /// <summary>
    /// The open.
    /// </summary>
    private void Open()
    {
      if (string.IsNullOrEmpty(lstUsers.SelectedValue))
      {
        return;
      }

      Response.Redirect(string.Format("~/Pages/Administrations/UserEx.aspx?UserId={0}", lstUsers.SelectedValue));
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
      lstUsers.DataSource = securityService.GetUsersByNameContains(searchByNameControl.NameValue);
      lstUsers.DataBind();
      contentUpdatePanel.Update();
    }

    #endregion
  }
}