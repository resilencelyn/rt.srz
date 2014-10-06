using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using rt.srz.model.enumerations;
using System.Web.UI.WebControls;
using System.Web.UI;
using SortDirection = rt.core.model.dto.enumerations.SortDirection;

using rt.srz.model.srz;
using System.Web.Security;
using DevExpress.Web.ASPxGridView;
using System.Web.SessionState;
using rt.srz.model.interfaces.service;
using rt.srz.ui.pvp.Controls.Common;

namespace rt.srz.ui.pvp.Enumerations
{
  using rt.core.model;
  using rt.core.model.interfaces;
  using rt.uec.model.enumerations;

  public static class UtilsHelper 
  {
    public const string c_IsOnline = "IsOnline";
    public const string c_TurnOn = "Включить";
    public const string c_TurnOff = "Выключить";

    /// <summary>
    /// Пререндер для дизабления пунктов меню к которым применяется css. Для меню в одну линию без уровней
    /// </summary>
    /// <param name="menu"></param>
    public static void MenuPreRender(Menu menu)
    {
      foreach (MenuItem item in menu.Items)
      {
        if ((!item.Enabled || !menu.Enabled) && !item.Text.Contains("<span style='color:LightGray'>"))
        {
          item.Text = "<span style='color:LightGray'>" + item.Text + "</span>";
        }
        else if (item.Enabled)
        {
          //удаляем span тэги
          item.Text = item.Text.Replace("<span style='color:LightGray'>", "").Replace("</span>", "");
        }
      }
    }

    /// <summary>
    /// The convert sort direction.
    /// </summary>
    /// <param name="sortDirection">
    /// The sort direction.
    /// </param>
    /// <returns>
    /// The <see cref="System.Web.UI.WebControls.SortDirection"/>.
    /// </returns>
    public static SortDirection ConvertSortDirection(System.Web.UI.WebControls.SortDirection sortDirection)
    {
      switch (sortDirection)
      {
        case System.Web.UI.WebControls.SortDirection.Ascending:
          return SortDirection.Ascending;
        case System.Web.UI.WebControls.SortDirection.Descending:
          return SortDirection.Descending;
        default:
          return SortDirection.Default;
      }
    }

    public static void ChangeTurn(StateBag viewState)
    {
      if (viewState[c_IsOnline] == null)
      {
        viewState[c_IsOnline] = true;
      }
      else
      {
        viewState[c_IsOnline] = !(bool)viewState[c_IsOnline];
      }
    }

    public static void SetTurnCaption(Button button, StateBag viewState)
    {
      if (viewState[c_IsOnline] == null)
      {
        button.Text = c_TurnOn;
        return;
      }
      if ((bool)viewState[c_IsOnline])
      {
        button.Text = c_TurnOff;
      }
      else
      {
        button.Text = c_TurnOn;
      }
    }

    public static void SetMenuButtonsEnable(bool value, UpdatePanel panel, Menu menu)
    {
      var item = menu.FindItem("Open");
      if (item != null)
      {
        item.Enabled = value;
      }
      item = menu.FindItem("Delete");
      if (item != null)
      {
        item.Enabled = value;
      }
      if (panel != null)
      {
        panel.Update();
      }
    }

    public static string GetLoadedSertificateText(TypeSertificate sertificateType, IList<SertificateUec> sertificates)
    {
      if (sertificates == null)
      {
        return null;
      }
      var sert = sertificates.Where(s => s.Type.Id == (int)sertificateType).FirstOrDefault();
      return sert != null ? string.Format("Сертификат установлен {0}", sert.InstallDate.ToShortDateString()) : null;
    }

    public static void AddAttributesToGridRow(ClientScriptManager manager, GridView grid)
    {
      foreach (GridViewRow r in grid.Rows)
      {
        if (r.RowType == DataControlRowType.DataRow)
        {
          r.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
          r.Attributes["onmouseout"] = "this.style.textDecoration='none';";
          r.ToolTip = "Click to select row";
          r.Attributes["onclick"] = manager.GetPostBackEventReference(grid, "Select$" + r.RowIndex, true);
        }
      }
    }

    /// <summary>
    /// добавление двойного клика. считаем что кнонпка с двойным кликом идёт первой в списке колонок грида
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="grid"></param>
    public static void AddDoubleClickAttributeToGrid(ClientScriptManager manager, GridView grid)
    {
      foreach (GridViewRow r in grid.Rows)
      {
        if (r.RowType == DataControlRowType.DataRow)
        {
          LinkButton dblClickButton = (LinkButton)r.Cells[0].Controls[0];
          string script = manager.GetPostBackClientHyperlink(dblClickButton, "");
          r.Attributes["ondblclick"] = script;
          manager.RegisterForEventValidation(r.UniqueID + "$ctl00");
        }
      }
    }

    public static void AddAttributesToGridDataBound(ClientScriptManager manager, GridView grid, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
      {
        return;
      }
      e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
      e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
      e.Row.Attributes["onclick"] = manager.GetPostBackEventReference(grid, "Select$" + e.Row.RowIndex);
    }

    public static void AddDoubleClickAttributeToGridDataBound(ClientScriptManager manager, GridView grid, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
      {
        return;
      }
      LinkButton dblClickButton = (LinkButton)e.Row.Cells[0].Controls[0];
      string script = manager.GetPostBackClientHyperlink(dblClickButton, "");
      e.Row.Attributes["ondblclick"] = script;
      manager.RegisterForEventValidation(e.Row.UniqueID + "$ctl00");
    }

    /// <summary>
    /// Нумерация поля в гриде порядковым номером в группе. Поле должно называться Number - FieldName="Number" для колонки с номером
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static void CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
    {
      if (e.Column.FieldName == "Number")
      {
        e.Value = e.ListSourceRowIndex + 1;
      }
    }

    /// <summary>
    /// Скрывает элементы меню для которых нету дочерних элементов с навигацией а для остальных элементов нету среди уже них дочерних элементов с навигаций. И так по цепочке
    /// </summary>
    /// <param name="menu"></param>
    public static void HideAllWithoutNavigatableChildren(Menu menu)
    {
      for (int i = menu.Items.Count - 1; i >= 0; i--)
      {
        HideAllWithoutNavigatableChildren(menu.Items[i]);
      }
      //удаляем верхний уровень меню если нету подменю
      for (int i = menu.Items.Count - 1; i >= 0; i--)
      {
        if (menu.Items[i].ChildItems.Count == 0)
        {
          menu.Items.Remove(menu.Items[i]);
        }
      }
    }

    private static void HideAllWithoutNavigatableChildren(MenuItem menuItem)
    {
      for (int i = menuItem.ChildItems.Count - 1; i >= 0; i--)
      {
        HideAllWithoutNavigatableChildren(menuItem.ChildItems[i]);
      }
      //если нету дочерних элементов с навигацией то удаляем элемент
      //сами элементы с навигацией удаляются через вызов SetMenuItemByPermission предварительно для всех необходимых пунктов, чтобы значть что осталось в итоге
      if (menuItem.ChildItems.Count == 0 && string.IsNullOrEmpty(menuItem.NavigateUrl))
      {
        //null будет у самого верхнего уровня меню
        if (menuItem.Parent != null)
        {
          menuItem.Parent.ChildItems.Remove(menuItem);
        }
      }
    }

    /// <summary>
    /// Ищет в меню элемент со значением menuItemValue и если нету прав на этот элемент, то скрывает(удаляет) его из меню
    /// </summary>
    /// <param name="session"></param>
    /// <param name="menu"></param>
    /// <param name="service"></param>
    /// <param name="menuItemValue"></param>
    /// <param name="permissionCode"></param>
    public static void SetMenuItemByPermission(HttpSessionState session, Menu menu, ISecurityService service, string menuItemValue, PermissionCode permissionCode)
    {
      //чтобы избежать пересечения в сессии значений например для пункта редактировать на разных формах, в качестве имени берём код разрешения
      string name = permissionCode.ToString();
      if (session[name] == null)
      {
        session[name] = service.GetIsCurrentUserAllowPermission(permissionCode);
      }
      SetMenuItemByValue(menu, menuItemValue, (bool)session[name]);
    }

    /// <summary>
    /// Ищет элемент меню по значению. Для корректного результата все подменю должны иметь разные значения
    /// </summary>
    /// <returns></returns>
    private static void SetMenuItemByValue(Menu menu, string menuItemValue, bool visibility)
    {
      foreach (MenuItem item in menu.Items)
      {
        if (item.Value == menuItemValue)
        {
          if (!visibility)
          {
            menu.Items.Remove(item);
          }
          return;
        }
      }
      foreach (MenuItem item in menu.Items)
      {
        var mi = FindMenuItemByValue(item, menuItemValue, visibility);
        if (mi != null)
        {
          return;
        }
      }
    }

    private static MenuItem FindMenuItemByValue(MenuItem item, string menuItemValue, bool visibility)
    {
      foreach (MenuItem child in item.ChildItems)
      {
        if (child.Value == menuItemValue)
        {
          if (!visibility)
          {
            item.ChildItems.Remove(child);
          }
          return item;
        }
      }
      foreach (MenuItem child in item.ChildItems)
      {
        var mi = FindMenuItemByValue(child, menuItemValue, visibility);
        if (mi != null)
        {
          return mi;
        }
      }
      return null;
    }

    public static void PerformConfirmedAction(ConfirmControl control, Action action, HttpRequest Request)
    {
      if (action == null)
      {
        return;
      }
      string eventTarget = Request["__EVENTTARGET"];
      string eventArgs = Request["__EVENTARGUMENT"];
      if (string.IsNullOrEmpty(eventTarget) || string.IsNullOrEmpty(eventArgs))
      {
        return;
      }
      if (eventArgs == control.ConfirmedArgumentUnique && eventTarget == control.ConfirmedTargetUnique)
      {
        action();
      }
    }

    public static void PerformDoubleClickAction(string target, Action doubleClick, HttpRequest Request)
    {
      if (doubleClick == null)
      {
        return;
      }
      string eventTarget = Request["__EVENTTARGET"];
      string eventArgs = Request["__EVENTARGUMENT"];
      if (string.IsNullOrEmpty(eventTarget) || string.IsNullOrEmpty(eventArgs))
      {
        return;
      }
      if (eventArgs == "doubleClick" && eventTarget == target)
      {
        doubleClick();
      }
    }

    public static void AddDoubleClick(ListBox listBox, ClientScriptManager manager)
    {
      listBox.Attributes.Add("ondblclick", manager.GetPostBackEventReference(listBox, "doubleClick"));
    }

  }

  public static class RedirectUtils
  {
    public static string AddUrlParam(string sourceUrl, string name, string value)
    {
      return sourceUrl.Contains("?") ? string.Format("{0}&{1}={2}", sourceUrl, name, value) :
        string.Format("{0}?{1}={2}", sourceUrl, name, value);
    }

    public static string GetUrlWithoutParams(string url)
    {
      int index = url.IndexOf("?");
      return index < 0 ? url : url.Substring(0, index);
    }

    public static void RedirectToAdministrationPermissions(HttpResponse response)
    {
      response.Redirect("~/Pages/Administrations/AdministrationPermissions.aspx?");
    }

    public static void RedirectToAdministrationRoles(HttpResponse response)
    {
      response.Redirect("~/Pages/Administrations/AdministrationRoles.aspx?");
    }

    public static void RedirectToAdministrationSmos(HttpResponse response)
    {
      response.Redirect("~/Pages/Administrations/AdministrationSmos.aspx?");
    }

    public static void RedirectToAdministrationMos(HttpResponse response)
    {
      response.Redirect("~/Pages/Administrations/AdministrationSmos.aspx?type=mo");
    }

    public static void RedirectToAdministrationUsers(HttpResponse response)
    {
      response.Redirect("~/Pages/Administrations/AdministrationUsers.aspx?");
    }

    public static void RedirectToMain(HttpResponse response)
    {
      response.Redirect(FormsAuthentication.DefaultUrl);
    }

    public static void RedirectToSeparate(HttpResponse response)
    {
      response.Redirect("~/Pages/SeparateNew.aspx");
    }

    public static void RedirectToSeparateOptions(HttpResponse response)
    {
      response.Redirect("~/Pages/SeparateOptions.aspx");
    }

    public static void RedirectToInsuranceHistory(HttpResponse response)
    {
      response.Redirect("~/Pages/InsuranceHistory.aspx");
    }

    public static void RedirectToTechnical(HttpResponse response)
    {
      response.Redirect("~/Pages/Administrations/SiteIsOffline.aspx");
    }

    public static void RedirectToFirstMiddleNames(HttpResponse response)
    {
      response.Redirect("~/Pages/NSI/FirstMiddleNames.aspx");
    }

    public static void RedirectToLogin(HttpResponse response)
    {
      response.Redirect(FormsAuthentication.LoginUrl);
    }

    public static void RedirectToPrintStatement(HttpResponse response)
    {
      response.Redirect("~/Pages/PrintStatement.aspx");
    }

    public static void RedirectToStatement(HttpResponse response)
    {
      HttpContext.Current.Session[SessionConsts.CInStatementEditing] = true;
      response.Redirect("~/Pages/NewStatementSelection.aspx");
    }

    public static void RedirectToIssueOfPolicy(HttpResponse response)
    {
      HttpContext.Current.Session[SessionConsts.CInStatementEditing] = true;
      response.Redirect("~/Pages/IssueOfPolicy.aspx");
    }

    public static void ClearInStatementEditing()
    {
      HttpContext.Current.Session[SessionConsts.CInStatementEditing] = false;
    }

    public static bool IsInStatementEditing()
    {
      return HttpContext.Current.Session[SessionConsts.CInStatementEditing] != null ?
        (bool)HttpContext.Current.Session[SessionConsts.CInStatementEditing] : false;
    }

    public static void RedirectToRangeNumbers(HttpResponse response)
    {
      response.Redirect("~/Pages/NSI/RangeNumbers.aspx");
    }

    public static void RedirectToPrintTemporaryCertificate(HttpResponse response)
    {
      response.Redirect("~/Pages/PrintTemporaryCertificate.aspx");
    }

    public static void RedirectToStatisticInitialLoading(HttpResponse response)
    {
      response.Redirect("~/Pages/StatisticInitialLoading.aspx");
    }

    public static void RedirectToTemplatesVs(HttpResponse response)
    {
      response.Redirect("~/Pages/NSI/TemplatesVs.aspx");
    }
  } 
}