using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StructureMap;
using rt.srz.model.srz;
using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;
using rt.srz.ui.pvp.Enumerations;

namespace rt.srz.ui.pvp.Templates
{
  using System.Web.Security;

  using rt.core.model;
  using rt.core.model.core;
  using rt.core.model.interfaces;
  using rt.srz.business.extensions;

  using User = rt.core.model.core.User;

  public partial class AuthentificatedPage : System.Web.UI.MasterPage
  {
    #region Fields

    protected ISecurityService _securityService;

    #endregion

    #region events

    public event Action GotoMainPage;

    #endregion

    #region Event Handlers

    protected void Page_Init(object sender, EventArgs e)
    {
      _securityService = ObjectFactory.GetInstance<ISecurityService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      //контролу со всплываюищим окошком о том что время сессии истекло присваиваем ид (модал попапу аяксерму), чтобы можно было обратиться в javacript
      closeSession.SetBehaviorId("confirm");
      closeSession.SetCancelScript(string.Format("window.location.href = \"{0}\"", FormsAuthentication.LoginUrl));

      //перенесено в click
      //linkToMain.PostBackUrl = FormsAuthentication.DefaultUrl;

      //при использовании <%= в скрипте вылетает ошибка
      //The Controls collection cannot be modified because the control contains code blocks (i.e. <% ... %>).

      //поэтому используем <%# в скрипте
      //This changes the code block from a Response.Write code block to a databinding expression.
      //Since <%# ... %> databinding expressions aren't code blocks, the CLR won't complain. Then in the code for the master page, you'd add the following:
      Page.Header.DataBind();
      Page.ClientScript.RegisterStartupScript(this.GetType(), "onLoad", "DisplaySessionTimeout();", true);

      User currentUser = _securityService.GetCurrentUser();

      //проставляем видимость элементов меню в зависимости от наличия прав в базе
      SetMenuByPermission(currentUser);

      //Установка заголовка
      SetTitle(currentUser);

      // Установка времени синхрнизации с типовой СРЗ
      SetSyncTimes();

      if (Session[SessionConsts.CDisplayAdminMenu] == null)
      {
        Session[SessionConsts.CDisplayAdminMenu] = currentUser.IsAdmin ||
        _securityService.IsUserHasAdminPermissions(currentUser) ||
        _securityService.IsUserAdminSmo(currentUser.Id) ||
        _securityService.IsUserAdminTfoms(currentUser.Id);
      }

      //перенесено в метод который вызывается только для формы с заявлением
      //Menu.Enabled = !RedirectUtils.IsInStatementEditing();

      var valuePath = "Администрирование/Установка";

      // Use the FindItem method to get the Classical menu item using
      // its value path.
      var item = Menu.FindItem(valuePath);
      if (!SiteMode.BeingInstalled && item != null && item.Parent != null)
      {
        item.Parent.ChildItems.Remove(item);
      }
    }

    public void SetMenuAvailability()
    {
      Menu.Enabled = !RedirectUtils.IsInStatementEditing();
    }

    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
      Session.Clear();
    }

    #endregion

    #region Private methods

    private void SetMenuByPermission(User currentUser)
    {
      SetMenuByPermission(currentUser, SessionConsts.CMaian, PermissionCode.Statements);
      SetMenuByPermission(currentUser, SessionConsts.CExportSmoBatches, PermissionCode.ExportSmoBatches);
      SetMenuByPermission(currentUser, SessionConsts.CTwins, PermissionCode.Twins);
      SetMenuByPermission(currentUser, SessionConsts.CSearchKeyTypes, PermissionCode.SearchKeys);
      SetMenuByPermission(currentUser, SessionConsts.CPfrStatistic, PermissionCode.PfrStatistic);
      SetMenuByPermission(currentUser, SessionConsts.CErrorSynchronizationView, PermissionCode.ErrorSynchronizationView);
      SetMenuByPermission(currentUser, SessionConsts.CTfoms, PermissionCode.Tfoms);
      SetMenuByPermission(currentUser, SessionConsts.CSmos, PermissionCode.Smos);
      SetMenuByPermission(currentUser, SessionConsts.CMos, PermissionCode.Mos);
      SetMenuByPermission(currentUser, SessionConsts.CConcepts, PermissionCode.Concepts);
      SetMenuByPermission(currentUser, SessionConsts.CFirstMiddleNames, PermissionCode.FirstMiddleNames);
      SetMenuByPermission(currentUser, SessionConsts.CRangeNumbers, PermissionCode.RangeNumbers);
      SetMenuByPermission(currentUser, SessionConsts.CManageChecks, PermissionCode.ManageChecks);
      SetMenuByPermission(currentUser, SessionConsts.CUsers, PermissionCode.Users);
      SetMenuByPermission(currentUser, SessionConsts.CRoles, PermissionCode.Roles);
      SetMenuByPermission(currentUser, SessionConsts.CPermissions, PermissionCode.Permissions);
      SetMenuByPermission(currentUser, SessionConsts.CSchedulerTask, PermissionCode.SchedulerTask);
      SetMenuByPermission(currentUser, SessionConsts.CJobsProgress, PermissionCode.JobsProgress);
      SetMenuByPermission(currentUser, SessionConsts.CInstallation, PermissionCode.Installation);
      SetMenuByPermission(currentUser, SessionConsts.CTemplatesVs, PermissionCode.TemplatesVs);
      SetMenuByPermission(currentUser, SessionConsts.CJobsSettings, PermissionCode.JobsSettings);
      SetMenuByPermission(currentUser, SessionConsts.CAssignUecCertificates, PermissionCode.AssignUecCertificates);

      //скрываем все элементы у которых нет ни одного нажимаемого по ссылке элемента и т.д до родителя
      UtilsHelper.HideAllWithoutNavigatableChildren(Menu);
    }

    private void SetMenuByPermission(User currentUser, string menuItemValue, PermissionCode permissionCode)
    {
      UtilsHelper.SetMenuItemByPermission(Session, Menu, _securityService, menuItemValue, permissionCode);
    }

    protected void SetTitle(User currentUser)
    {
      try
      {
        if (currentUser != null && currentUser.PointDistributionPolicyId != null)
        {
          var pdp = currentUser.PointDistributionPolicy();
          if (pdp != null)
          {
            lblPDPTitle.Text = string.Format(
              "Пункт выдачи полисов {0}. Текущий пользователь {1}", pdp.ShortName, currentUser.Login);
            if (pdp.Parent != null)
            {
              lblSMOTitle.Text = string.Format("Страховая медицинская организация {0} ", pdp.Parent.FullName);
            }
          }
        }
      }
      catch (Exception)
      {
      }
    }

    protected void SetSyncTimes()
    { 
      // Вывод времени синхронизации из ПВП в СРЗ
      var syncTimePvp2Srz = ObjectFactory.GetInstance<IStatementService>().GetSetting("ExporterToSrz_Finish");
      DateTime? dtsyncTimePvp2Srz = null;
      DateTime dttemp;
      if (syncTimePvp2Srz != null && DateTime.TryParse(syncTimePvp2Srz.ValueString, out dttemp))
      {
        dtsyncTimePvp2Srz = dttemp;
      }

      if (dtsyncTimePvp2Srz.HasValue)
      {
        lblSyncPvp2Srz.Text = 
          string.Format("Синхронизация в типовую СРЗ из ПВП выполнена: Дата {0}, Время {1}", dtsyncTimePvp2Srz.Value.ToShortDateString(), dtsyncTimePvp2Srz.Value.ToShortTimeString());
      }
      else
      {
        lblSyncPvp2Srz.Text = string.Empty;
      }

      // Вывод времени синхронизации из СРЗ в ПВР
      var syncTimeSrz2Pvp = ObjectFactory.GetInstance<IStatementService>().GetSetting("ExporterToPvp_Finish");
      DateTime? dtsyncTimeSrz2Pvp = null;
      if (syncTimeSrz2Pvp != null && DateTime.TryParse(syncTimeSrz2Pvp.ValueString, out dttemp))
      {
        dtsyncTimeSrz2Pvp = dttemp;
      }

      lblSyncSrz2Pvp.Text = dtsyncTimeSrz2Pvp.HasValue ? 
        string.Format("Синхронизация из типовой СРЗ в ПВП выполнена: Дата {0}, Время {1}", dtsyncTimeSrz2Pvp.Value.ToShortDateString(), dtsyncTimeSrz2Pvp.Value.ToShortTimeString()) :
        string.Empty;
    }

    #endregion

    protected void linkToMain_Click(object sender, EventArgs e)
    {
      if (GotoMainPage != null)
      {
        GotoMainPage();
      }
      RedirectUtils.RedirectToMain(Response);
    }

  }
}