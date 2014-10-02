using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate;
using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using StructureMap;
using rt.srz.ui.pvp.Controls.Common;
using rt.srz.ui.pvp.Enumerations;

namespace rt.srz.ui.pvp.Controls.Administration
{
  public partial class UsersControl : System.Web.UI.UserControl
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
      lstUsers.DataSource = _securityService.GetUsersByNameContains(searchByNameControl.NameValue);
      lstUsers.DataBind();
      contentUpdatePanel.Update();
    }

    void searchByNameControl_Clear()
    {
      RefreshData();
      contentUpdatePanel.Update();
    }


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
        //удаление
        UtilsHelper.PerformConfirmedAction(confirmDelete, DeleteUser, Request);
        //двойной клик по списку
        UtilsHelper.PerformDoubleClickAction(lstUsers.UniqueID, Open, Request);
      }
    }

    public void RefreshData()
    {
      User currentUser = _securityService.GetCurrentUser();
      foreach (MenuItem item in menu1.Items)
      {
        if (item.Value != "AssignPdp")
        {
          item.Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu];
        }
      }

      IList<User> users = _securityService.GetUsersByCurrent();
      lstUsers.DataSource = users;
      lstUsers.DataBind();
      if (users != null && (lstUsers.SelectedIndex < 0 && users.Count > 0))
      {
        lstUsers.SelectedIndex = 0;
        lstUsers_SelectedIndexChanged(null, null);
      }
    }

    private void Open()
    {
      if (string.IsNullOrEmpty(lstUsers.SelectedValue))
      {
        return;
      }
      Response.Redirect(string.Format("~/Pages/Administrations/UserEx.aspx?UserId={0}", lstUsers.SelectedValue));
    }

    protected void menu_MenuItemClick(object sender, MenuEventArgs e)
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
          Response.Redirect(string.Format("~/Pages/Administrations/AssignPdp.aspx?UserId={0}&UserName={1}", lstUsers.SelectedValue, lstUsers.SelectedItem.Text));
          break;
        case "Delete":
          DeleteUser();
          break;
        case "AssignGroups":
          if (lstUsers.SelectedItem == null)
          {
            return;
          }
          Response.Redirect(string.Format("~/Pages/Administrations/AssignGroupsToUser.aspx?UserName={0}&UserId={1}", lstUsers.SelectedItem.Text, lstUsers.SelectedItem.Value));
          break;
        case "AssignRoles":
          if (lstUsers.SelectedItem == null)
          {
            return;
          }
          Response.Redirect(string.Format("~/Pages/Administrations/AssignRolesToUser.aspx?UserName={0}&UserId={1}", lstUsers.SelectedItem.Text, lstUsers.SelectedItem.Value));
          break;
      }
    }

    private void DeleteUser()
    {
      if (string.IsNullOrEmpty(lstUsers.SelectedValue))
      {
        return;
      }
      _securityService.DeleteUser(Guid.Parse(lstUsers.SelectedValue));
      lstUsers.Items.RemoveAt(lstUsers.SelectedIndex);
      contentUpdatePanel.Update();
    }

    protected void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lstUsers.SelectedItem == null)
      {
        return;
      }
      if (lstUsers.SelectedItem.Text.ToLower() == "admin")
      {
        menu1.FindItem("Delete").Enabled = false;
        //назначать пдп для администратора может только он сам
        menu1.FindItem("AssignPdp").Enabled = _securityService.GetCurrentUser().IsAdmin;
      }
      else
      {
        bool allowAssignPdp = true;
        User currentUser = _securityService.GetCurrentUser();
        //администратор смо не может назначать пункт выдачи для администратора территорального фонда
        if (!_securityService.IsUserHasAdminPermissions(currentUser) &&
          !_securityService.IsUserAdminTF(currentUser.Id))
        {
          allowAssignPdp = !_securityService.IsUserAdminTF(Guid.Parse(lstUsers.SelectedItem.Value));
        }
        menu1.FindItem("Delete").Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu];
        menu1.FindItem("AssignPdp").Enabled = (bool)Session[SessionConsts.CDisplayAdminMenu] && allowAssignPdp;
      }
      MenuUpdatePanel.Update();
    }

    protected void menu1_PreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(menu1);
    }

  }
}