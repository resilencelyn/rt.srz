// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupsControl.ascx.cs" company="РусБИТех">
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
  /// The groups control.
  /// </summary>
  public partial class GroupsControl : UserControl
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
      menu1.Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu];

      var groups = securityService.GetGroups();
      lstGroups.DataSource = groups;
      lstGroups.DataBind();
      if (lstGroups.SelectedIndex < 0 && groups.Count > 0)
      {
        lstGroups.SelectedIndex = 0;
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
      UtilsHelper.AddDoubleClick(lstGroups, Page.ClientScript);
      if (!IsPostBack)
      {
        RefreshData();
        menu1.FindItem(SessionConsts.CDelete).NavigateUrl = confirmDelete.ViewConfirmScript;
      }
      else
      {
        // удаление
        UtilsHelper.PerformConfirmedAction(confirmDelete, DeleteGroup, Request);

        // двойной клик по списку
        UtilsHelper.PerformDoubleClickAction(lstGroups.UniqueID, Open, Request);
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
          Response.Redirect("~/Pages/Administrations/Group.aspx");
          break;
        case "AddEx":
          Response.Redirect("~/Pages/Administrations/GroupEx.aspx");
          break;
        case "Open":
          Open();
          break;
        case "Delete":
          DeleteGroup();
          break;
        case "AssignUsersToGroup":
          if (lstGroups.SelectedItem == null)
          {
            return;
          }

          Response.Redirect(
                            string.Format(
                                          "~/Pages/Administrations/AssignUsersToGroup.aspx?GroupName={0}&GroupId={1}", 
                                          lstGroups.SelectedItem.Text, 
                                          lstGroups.SelectedItem.Value));
          break;
        case "AssignRoles":
          if (lstGroups.SelectedItem == null)
          {
            return;
          }

          Response.Redirect(
                            string.Format(
                                          "~/Pages/Administrations/AssignRolesToGroup.aspx?GroupName={0}&GroupId={1}", 
                                          lstGroups.SelectedItem.Text, 
                                          lstGroups.SelectedItem.Value));
          break;
      }
    }

    /// <summary>
    /// The delete group.
    /// </summary>
    private void DeleteGroup()
    {
      if (string.IsNullOrEmpty(lstGroups.SelectedValue))
      {
        return;
      }

      securityService.DeleteGroup(Guid.Parse(lstGroups.SelectedValue));
      lstGroups.Items.RemoveAt(lstGroups.SelectedIndex);
      contentUpdatePanel.Update();
    }

    /// <summary>
    /// The open.
    /// </summary>
    private void Open()
    {
      if (string.IsNullOrEmpty(lstGroups.SelectedValue))
      {
        return;
      }

      Response.Redirect(string.Format("~/Pages/Administrations/GroupEx.aspx?GroupId={0}", lstGroups.SelectedValue));
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
      lstGroups.DataSource = securityService.GetGroupsByNameContains(searchByNameControl.NameValue);
      lstGroups.DataBind();
      contentUpdatePanel.Update();
    }

    #endregion
  }
}