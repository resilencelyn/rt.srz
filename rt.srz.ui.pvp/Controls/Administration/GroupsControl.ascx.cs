using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using StructureMap;
using rt.srz.ui.pvp.Enumerations;

namespace rt.srz.ui.pvp.Controls.Administration
{
  public partial class GroupsControl : System.Web.UI.UserControl
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
      lstGroups.DataSource = _securityService.GetGroupsByNameContains(searchByNameControl.NameValue);
      lstGroups.DataBind();
      contentUpdatePanel.Update();
    }

    void searchByNameControl_Clear()
    {
      RefreshData();
      contentUpdatePanel.Update();
    }

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
        //удаление
        UtilsHelper.PerformConfirmedAction(confirmDelete, DeleteGroup, Request);
        //двойной клик по списку
        UtilsHelper.PerformDoubleClickAction(lstGroups.UniqueID, Open, Request);
      }
    }

    public void RefreshData()
    {
      menu1.Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu];

      IList<Group> groups = _securityService.GetGroups();
      lstGroups.DataSource = groups;
      lstGroups.DataBind();
      if (lstGroups.SelectedIndex < 0 && groups.Count > 0)
      {
        lstGroups.SelectedIndex = 0;
      }
    }

    private void Open()
    {
      if (string.IsNullOrEmpty(lstGroups.SelectedValue))
      {
        return;
      }
      Response.Redirect(string.Format("~/Pages/Administrations/GroupEx.aspx?GroupId={0}", lstGroups.SelectedValue));
    }

    protected void menu_MenuItemClick(object sender, MenuEventArgs e)
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
          Response.Redirect(string.Format("~/Pages/Administrations/AssignUsersToGroup.aspx?GroupName={0}&GroupId={1}", lstGroups.SelectedItem.Text, lstGroups.SelectedItem.Value));
          break;
        case "AssignRoles":
          if (lstGroups.SelectedItem == null)
          {
            return;
          }
          Response.Redirect(string.Format("~/Pages/Administrations/AssignRolesToGroup.aspx?GroupName={0}&GroupId={1}", lstGroups.SelectedItem.Text, lstGroups.SelectedItem.Value));
          break;
      }
    }

    private void DeleteGroup()
    {
      if (string.IsNullOrEmpty(lstGroups.SelectedValue))
      {
        return;
      }
      _securityService.DeleteGroup(Guid.Parse(lstGroups.SelectedValue));
      lstGroups.Items.RemoveAt(lstGroups.SelectedIndex);
      contentUpdatePanel.Update();
    }

    protected void menu1_PreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(menu1);
    }

  }
}