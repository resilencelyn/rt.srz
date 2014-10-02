// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementsSearch.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.business.manager;
using rt.srz.business.manager.cache;

namespace rt.srz.ui.pvp.Controls
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.Globalization;
  using System.Linq;
  using System.Web.Security;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.atl.business.quartz;
  using rt.core.model.dto;
  using rt.core.model.security;
  using rt.srz.model.algorithms;
  using rt.srz.model.dto;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;
  using rt.srz.services;
  using rt.srz.ui.pvp.Controls.CustomPager;
  using rt.srz.ui.pvp.Enumerations;

  using StructureMap;

  using SortDirection = rt.core.model.dto.enumerations.SortDirection;
  using rt.srz.model.enumerations;
  using rt.srz.model.srz.concepts;
  using rt.srz.ui.pvp.Controls.Common;
  using System.Text;
  using NHibernate.Context;

  #endregion

  /// <summary>
  ///   The statements search.
  /// </summary>
  public partial class StatementsSearch : UserControl
  {
    #region Constants

    /// <summary>
    ///   The search criteria view state key.
    /// </summary>
    public const string SearchCriteriaViewStateKey = "SearchCriteria";

    //при изменении значения изменить также в ConfirmedArgument для <ConfirmDeleteControl ID="confirmDeath">
    private const string CArgumentDeleteDeathInfo = "DeleteDeathInfo";

    #endregion

    #region Fields

    /// <summary>
    ///   The auth service.
    /// </summary>
    protected IAuthService _authService;

    /// <summary>
    ///   The security service.
    /// </summary>
    protected ISecurityService _securityService;

    /// <summary>
    ///   The service.
    /// </summary>
    protected IStatementService _statementService;

    #endregion

    #region Properties

    private DateTime DefaultStartDateFilling
    {
      get { return DateTime.Today.AddDays(-30); }
    }

    private DateTime DefaultEndDateFilling
    {
      get { return DateTime.Today; }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The validate.
    /// </summary>
    /// <returns> The <see cref="bool" /> . </returns>
    public bool Validate()
    {
      var args = new ServerValidateEventArgs(string.Empty, true);

      ValidateSnils(tbSnils, args);
      if (!args.IsValid)
      {
        return false;
      }

      ValidateTemporaryCertificateNumber(tbTemporaryCertificateNumber, args);
      return args.IsValid;
    }

    #endregion

    #region Methods


    /// <summary>
    /// The menu_ menu item click.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void Menu_MenuItemClick(object sender, MenuEventArgs e)
    {
      StatementSearchMenuItem item;
      if (!Enum.TryParse(e.Item.Value, out item))
      {
        return;
      }

      Session[SessionConsts.COperation] = item;
      switch (item)
      {
        // Переход на страницу создания нового заявления
        case StatementSearchMenuItem.Reinsuranse:
          {
            var isownpeople = IsOwnSelectPeople();
            if (isownpeople.HasValue)
            {
              if (!isownpeople.Value)
              {
                FillExample();
              }
            }
            else
            {
              FillExample();
            }

            RedirectUtils.RedirectToStatement(Response);
          }
          break;

        case StatementSearchMenuItem.Reneval:
          {
            FillExample();
            RedirectUtils.RedirectToStatement(Response);
          }
          break;

        // Переход на страницу выдачи полиса
        case StatementSearchMenuItem.Issue:
          {
            if (SearchResultGridView.SelectedDataKey != null)
            {
              if (SearchResultGridView.SelectedDataKey.Value != null)
              {
                ////// Сохранение действий с ПД
                ////var userActionManager = new UserActionManager();
                ////userActionManager.LogAccessToPersonalData(_statementService.GetStatement((Guid)SearchResultGridView.SelectedDataKey.Value), "Просмотр заявления");

                // Переход
                Session[SessionConsts.CGuidStatementId] = SearchResultGridView.SelectedDataKey.Value;
                RedirectUtils.RedirectToIssueOfPolicy(Response);
              }
            }
          }
          break;


        // Переход на страницу редактирования заявления
        case StatementSearchMenuItem.Edit:
          {
            if (SearchResultGridView.SelectedDataKey != null)
            {
              if (SearchResultGridView.SelectedDataKey.Value != null)
              {
                ////// Сохранение действий с ПД
                ////var userActionManager = new UserActionManager();
                ////userActionManager.LogAccessToPersonalData(_statementService.GetStatement((Guid)SearchResultGridView.SelectedDataKey.Value), "Выдача полиса");

                // Переход
                //Session[SessionConsts.CGuidStatementId] = SearchResultGridView.SelectedDataKey.Value;
                Session[SessionConsts.CCurrentStatement] = _statementService.GetStatement((Guid)SearchResultGridView.SelectedDataKey.Value);
                RedirectUtils.RedirectToStatement(Response);
              }
            }
          }
          break;

        // Удаление заявления
        case StatementSearchMenuItem.Delete:
          CancelStatement();
          break;

        case StatementSearchMenuItem.Separate:
          if (SearchResultGridView.SelectedDataKey == null)
          {
            break;
          }

          // перенесено в проверки доступности пункта меню по выбору строки грида с заявлением
          // if (!_statementService.InsuredInJoined(_statementService.GetStatement((Guid)SearchResultGridView.SelectedDataKey.Value).InsuredPerson.Id))
          // {
          // //todo: call message
          // break;
          // }
          Session[SessionConsts.CGuidStatementId] = SearchResultGridView.SelectedDataKey.Value;
          RedirectUtils.RedirectToSeparate(Response);
          break;
        case StatementSearchMenuItem.InsuranceHistory:
          if (SearchResultGridView.SelectedDataKey == null)
          {
            break;
          }

          Session[SessionConsts.CGuidStatementId] = SearchResultGridView.SelectedDataKey.Value;
          RedirectUtils.RedirectToInsuranceHistory(Response);
          break;
        case StatementSearchMenuItem.DeleteDeathInfo:
          DeleteDeathInfo();
          break;
      }
    }

    private void DeleteDeathInfo()
    {
      if (SearchResultGridView.SelectedDataKey == null)
      {
        return;
      }
      _statementService.DeleteDeathInfo((Guid)SearchResultGridView.SelectedDataKey.Value);
      RefreshCurrentSearchGridPage();
    }

    private void CancelStatement()
    {
      if (SearchResultGridView.SelectedDataKey == null || SearchResultGridView.SelectedDataKey.Value == null)
      {
        return;
      }
      Session[SessionConsts.CGuidStatementId] = SearchResultGridView.SelectedDataKey.Value;
      Guid id;
      if (Guid.TryParse(SearchResultGridView.SelectedDataKey.Value.ToString(), out id))
      {
        _statementService.CanceledStatement(id);
      }
      RefreshCurrentSearchGridPage();
    }

    private void RefreshCurrentSearchGridPage()
    {
      var currentCriteria = Session[SearchCriteriaViewStateKey] as SearchStatementCriteria;
      if (currentCriteria == null)
      {
        return;
      }
      currentCriteria.SearchResult = null;
      Session[SearchCriteriaViewStateKey] = currentCriteria;
      MakeStatementSearch(currentCriteria);
    }

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
      _statementService = ObjectFactory.GetInstance<IStatementService>();
      _securityService = ObjectFactory.GetInstance<ISecurityService>();
      _authService = ObjectFactory.GetInstance<IAuthService>();
      if (!IsPostBack)
      {
        FillStatusStatement();
        FillTypeStatement();
        FillDocumentType();
        FillGender();
        documentUDL.SetSimpleMode();
      }
    }

    private void SetMenuByPermission(User currentUser)
    {
      SetMenuByPermission(currentUser, SessionConsts.CReinsuranse, PermissionCode.Search_Create);
      SetMenuByPermission(currentUser, SessionConsts.CReneval, PermissionCode.Search_Reneval);
      SetMenuByPermission(currentUser, SessionConsts.CEdit, PermissionCode.Search_Edit);
      SetMenuByPermission(currentUser, SessionConsts.CDelete, PermissionCode.Search_Delete);
      SetMenuByPermission(currentUser, SessionConsts.CReadUEC, PermissionCode.Search_ReadUec);
      SetMenuByPermission(currentUser, SessionConsts.CWriteUEC, PermissionCode.Search_WriteUec);
      SetMenuByPermission(currentUser, SessionConsts.CReadSmartCard, PermissionCode.Search_ReadSmartCard);
      SetMenuByPermission(currentUser, SessionConsts.CSeparate, PermissionCode.Search_Separate);
      SetMenuByPermission(currentUser, SessionConsts.CInsuranceHistory, PermissionCode.Search_InsuranceHistory);
      SetMenuByPermission(currentUser, SessionConsts.CDeleteDeathInfo, PermissionCode.CancelDeath);
      SetMenuByPermission(currentUser, SessionConsts.CIssue, PermissionCode.Search_GiveOut);
    }

    private void SetMenuByPermission(User currentUser, string menuItemValue, PermissionCode permissionCode)
    {
      UtilsHelper.SetMenuItemByPermission(Session, Menu, _securityService, menuItemValue, permissionCode);
    }

    /// <summary>
    /// Обновляет краткую инфу по фильтру в уголке, которая отображается при свёрнутом фильтре
    /// </summary>
    private void UpdateBriefFilterInfoInCorner()
    {
      //Отображаются последние заявления
      lbLastStatementsF.Visible = cbReturnLastStatement.Checked;

      //даты подачи заявления
      SetTextToBriefFilter(lbDatesF, "Дата подачи заявления", chbUseDateFilling.Checked ? string.Format("{0} - {1}", tbDateFillingFrom.Text, tbDateFillingTo.Text) : "");

      //удл
      SetTextToBriefFilter(lbUdlF, "Документ УДЛ", documentUDL.DocumentTypeStr == "Выберите вид документа" ? "" : documentUDL.DocumentTypeStr,
        documentUDL.DocumentSeries, documentUDL.DocumentNumber,
        documentUDL.DocumentIssuingAuthority, documentUDL.DocumentIssueDate.HasValue ? documentUDL.DocumentIssueDate.Value.ToShortDateString() : "");

      // ФИО
      SetTextToBriefFilter(lbFioF, "ФИО", tbLastName.Text, tbFirstName.Text, tbMiddleName.Text);

      // Дата рождения
      SetTextToBriefFilter(lbDatebirthF, "Дата рождения", tbBirthDate.Text);

      // СНИЛС
      if (!string.IsNullOrEmpty(tbSnils.Text.Replace("-", "").Replace(" ", "").Replace("_", "")))
      {
        SetTextToBriefFilter(lbSnilsF, "СНИЛС", tbSnils.Text);
      }
      else
      {
        SetTextToBriefFilter(lbSnilsF, "СНИЛС", "");
      }

      // Место рождения
      SetTextToBriefFilter(lbBirthPlaceF, "Место рождения", tbBirthPlace.Text);

      //текст ошибки
      //0 индекс соответсвует тому когда данные не выбраны
      SetTextToBriefFilter(lbErrorF, "Причина отклонения", (hSelectedError.Value == "Данные не выбраны" || hSelectedError.Value == "-1") ? "" : hSelectedError.Value);
    }

    private void SetTextToBriefFilter(Label label, string baseText, params string[] valuesText)
    {
      string resultValue = string.Join(" ", valuesText).Trim();
      if (string.IsNullOrEmpty(resultValue))
      {
        label.Visible = false;
        return;
      }
      else
      {
        label.Visible = true;
      }
      StringBuilder sb = new StringBuilder();
      sb.Append("<span><b>").Append(baseText).Append(": </b></span>").Append(resultValue).Append("; ");
      label.Text = sb.ToString();
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
      if (!IsPostBack)
      {
        ViewState["OpenNotOwnSmo"] = _securityService.GetIsCurrentUserAllowPermission(PermissionCode.Search_OpenNotOwnSmo);

        User currentUser = _securityService.GetCurrentUser();
        Menu.FindItem(SessionConsts.CDelete).NavigateUrl = confirmDelete.ViewConfirmScript;
        ////Menu.FindItem(SessionConsts.CDeleteDeathInfo).NavigateUrl = confirmDeath.ViewConfirmScript;
        SetMenuByPermission(currentUser);
        DisableMenuItems();

        var searchCriteria = (SearchStatementCriteria)Session[SearchCriteriaViewStateKey];
        if (searchCriteria != null)
        {
          SetSearchCriteria(searchCriteria);
          SearchResultGridView.PageSize = searchCriteria.Take;
          MakeStatementSearch(searchCriteria);
          custPager.ReloadPager();
          custPager.CurrentPageIndex = searchCriteria.CurrentPageIndex;
          custPager.SetPageSize(SearchResultGridView.PageSize);
          custPager.ReloadOnPostBack = false;
        }
        else
        {
          // Начальное состояние дата с
          tbDateFillingFrom.Text = DefaultStartDateFilling.ToShortDateString();
          tbDateFillingFrom.Enabled = false;

          // Начальное состояние дата по
          tbDateFillingTo.Text = DefaultEndDateFilling.ToShortDateString();
          tbDateFillingTo.Enabled = false;

          ddlErrors.Style.Value = "display: none";
          lbErrors.Style.Value = "display: none";

          // формирование пустого грида
          BindEmptyData();

          // Установка кнпоки по умолчанию
          Page.Form.DefaultButton = btnSearch.UniqueID;

          hfSearchResultGVSelectedRowIndex.Value = "-1";

          // Очистка веб сессии
          Session.Clear();
        }
      }
      else
      {
        tbDateFillingFrom.Style.Value = "display: none";
        tbDateFillingTo.Style.Value = "display: none";
        lblDateFilling.Style.Value = "display: none";
        Label8.Style.Value = "display: none";

        ddlErrors.Style.Value = "display: none";
        lbErrors.Style.Value = "display: none";
        tbDateFillingTo.Enabled = false;
        tbDateFillingFrom.Enabled = false;
        ddlErrors.Enabled = false;
        if (chbUseDateFilling.Checked)
        {
          //если выбран статус заявления отклонено или вообще не выбран, то только в этом случае отображаем список с ошибками
          if (ddlCertificateStatus.SelectedValue == ((int)StatusStatement.Declined).ToString() || ddlCertificateStatus.SelectedValue == "-1")
          {
            ddlErrors.Enabled = true;
            ddlErrors.Style.Value = "display: block";
            lbErrors.Style.Value = "display: block";
          }
          tbDateFillingTo.Enabled = true;
          tbDateFillingFrom.Enabled = true;

          tbDateFillingFrom.Style.Value = "display: block";
          tbDateFillingTo.Style.Value = "display: block";
          lblDateFilling.Style.Value = "display: block";
          Label8.Style.Value = "display: block";
       }

        //Отмена заявления
        if (Request.Form[Page.postEventSourceID] == confirmDelete.ConfirmedTargetUnique &&
          Request.Form[Page.postEventArgumentID] == confirmDelete.ConfirmedArgumentUnique)
        {
          CancelStatement();
          return;
        }
        //Отмена смерти
        if (Request.Form[Page.postEventSourceID] == confirmDeath.ConfirmedTargetUnique &&
          Request.Form[Page.postEventArgumentID] == confirmDeath.ConfirmedArgumentUnique)
        {
          DeleteDeathInfo();
          return;
        }
      }

      //обновляем инфу в уголке по краткому содержанию фильтра
      UpdateBriefFilterInfoInCorner();
    }

    private void DisableMenuItems()
    {
      SetMenuItemEnable(SessionConsts.CReneval, false);
      SetMenuItemEnable(SessionConsts.CIssue, false);
      SetMenuItemEnable(SessionConsts.CEdit, false);
      SetMenuItemEnable(SessionConsts.CDelete, false);
      SetMenuItemEnable(SessionConsts.CWriteUEC, false);
      SetMenuItemEnable(SessionConsts.CSeparate, false);
      SetMenuItemEnable(SessionConsts.CDeleteDeathInfo, false);
      SetMenuItemEnable(SessionConsts.CInsuranceHistory, false);
      MenuUpdatePanel.Update();
    }

    private void SetMenuItemEnable(string itemName, bool value)
    {
      //все элементы на одном уровне
      var item = Menu.FindItem(itemName);
      if (item != null)
      {
        item.Enabled = value;
      }
    }

    private void SetMenuItemText(string itemName, string text)
    {
      //все элементы на одном уровне
      var item = Menu.FindItem(itemName);
      if (item != null)
      {
        item.Text = text;
      }
    }

    protected string GetUrlUecGate()
    {
      var url = Request.Url.AbsoluteUri.Replace("Pages/Main.aspx", "UecGate.svc");
      return url;
    }

    /// <summary>
    /// The search result grid view_ row data bound.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void SearchResultGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
      {
        return;
      }

      e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
      e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
      e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(
        SearchResultGridView, "Select$" + e.Row.RowIndex);


      // Get the LinkButton control in the second cell
      LinkButton _doubleClickButton = (LinkButton)e.Row.Cells[12].Controls[0];
      // Get the javascript which is assigned to this LinkButton
      string _jsDouble = Page.ClientScript.GetPostBackClientHyperlink(_doubleClickButton, "");
      // Add this JavaScript to the ondblclick Attribute of the row
      e.Row.Attributes["ondblclick"] = _jsDouble;


      var strFromCurrentSmo = e.Row.Cells[0].Text;
      bool fromCurrentSmo;
      if (!bool.TryParse(strFromCurrentSmo, out fromCurrentSmo))
      {
        return;
      }

      if (!fromCurrentSmo)
      {
        return;
      }

      e.Row.ForeColor = Color.Black; //Color.FromArgb(0, 145, 175); //Color.Green;
    }

    /// <summary>
    /// The search result grid view_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void SearchResultGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
      hfSearchResultGVSelectedRowIndex.Value = SearchResultGridView.SelectedIndex.ToString(CultureInfo.InvariantCulture);
      if (SearchResultGridView.SelectedIndex == -1)
      {
        return;
      }

      Guid id;
      Statement statement = null;
      var user = _securityService.GetCurrentUser();
      if (SearchResultGridView.SelectedDataKey != null && Guid.TryParse(SearchResultGridView.SelectedDataKey.Value.ToString(), out id))
      {
        statement = _statementService.GetStatement(id);
      }

      bool fromCurrentSmo;
#if(IgnoreSmo)
      fromCurrentSmo = true;
#else
      fromCurrentSmo = statement != null
        && user.PointDistributionPolicy != null
        && user.PointDistributionPolicy.Parent != null
        && statement.PointDistributionPolicy != null
        && statement.PointDistributionPolicy.Parent != null
        && statement.PointDistributionPolicy.Parent.Id == user.PointDistributionPolicy.Parent.Id;
#endif

      SetMenuItemEnable(SessionConsts.CReinsuranse, statement != null && statement.IsActive && statement.Status.Id == StatusStatement.Exercised);
      SetMenuItemEnable(SessionConsts.CReneval, statement != null && statement.IsActive && fromCurrentSmo && statement.Status.Id == StatusStatement.Exercised);

      //для отклонённых и отменённых заявлений кнопка выдать полис должна быть недоступна
      SetMenuItemEnable(SessionConsts.CIssue, fromCurrentSmo && (!statement.PolicyIsIssued.HasValue || !statement.PolicyIsIssued.Value) &&
        statement.Status.Id != StatusStatement.Declined && statement.Status.Id != StatusStatement.Cancelled);

      SetMenuItemEnable(SessionConsts.CEdit, fromCurrentSmo || (bool)ViewState["OpenNotOwnSmo"]);

      SetMenuItemText(SessionConsts.CReinsuranse, fromCurrentSmo ? "Создать" : "Перестраховать");

      var statusForDelete = statement != null && (statement.Status.Id == StatusStatement.Declined || statement.Status.Id == StatusStatement.New);
      SetMenuItemEnable(SessionConsts.CDelete, fromCurrentSmo && statusForDelete);
      SetMenuItemEnable(SessionConsts.CWriteUEC, fromCurrentSmo);
      SetMenuItemEnable(SessionConsts.CInsuranceHistory, fromCurrentSmo);
      var insuredInJoined = _statementService.InsuredInJoined(_statementService.GetStatement((Guid)SearchResultGridView.SelectedDataKey.Value).InsuredPerson.Id);
      SetMenuItemEnable(SessionConsts.CSeparate, insuredInJoined && fromCurrentSmo);

      SetMenuItemEnable(SessionConsts.CDeleteDeathInfo, statement != null && statement.InsuredPerson.Status.Id == StatusPerson.Dead);

      SetMenuItemEnable(SessionConsts.CInsuranceHistory, statement != null);

      MenuUpdatePanel.Update();
    }

    /// <summary>
    /// The search result grid view_ sorting.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void SearchResultGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
      var currentCriteria = Session[SearchCriteriaViewStateKey] as SearchStatementCriteria;
      if (currentCriteria != null)
      {
        if (!string.IsNullOrEmpty(currentCriteria.SortExpression) && currentCriteria.SortExpression == e.SortExpression)
        {
          // Меняем направление сортировки
          switch (currentCriteria.SortDirection)
          {
            case SortDirection.Ascending:
              currentCriteria.SortDirection = SortDirection.Descending;
              break;
            case SortDirection.Descending:
              currentCriteria.SortDirection = SortDirection.Ascending;
              break;
          }
        }
        else
        {
          // Добавляем сортировку
          currentCriteria.SortExpression = e.SortExpression;
          currentCriteria.SortDirection = ConvertSortDirection(e.SortDirection);
        }
      }

      //очищаем чтобы вызвался поиск
      currentCriteria.SearchResult = null;
      Session[SearchCriteriaViewStateKey] = currentCriteria;

      // Запускаем новый поиск
      MakeStatementSearch(currentCriteria);
      custPager.ReloadPager();
    }

    protected void SearchResultGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        //case "SingleClick":
        //  SearchResultGridView.SelectedIndex = int.Parse(e.CommandArgument.ToString());
        //  SearchResultGridView_SelectedIndexChanged(SearchResultGridView, new EventArgs());
        //  break;
        case "DoubleClick":
          SearchResultGridView.SelectedIndex = int.Parse(e.CommandArgument.ToString());
          Menu_MenuItemClick(Menu, new MenuEventArgs(Menu.Items[(int)StatementSearchMenuItem.Edit]));
          break;
      }
    }

    /// <summary>
    /// The validate snils.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateSnils(object source, ServerValidateEventArgs args)
    {
      if (!cbNotCheckSnils.Checked)
      {
        var st = new Statement();
        st.InsuredPersonData = new InsuredPersonDatum();
        st.InsuredPersonData.Snils = SnilsChecker.SsToShort(tbSnils.Text.Replace("_", string.Empty));
        args.IsValid = _statementService.TryCheckProperty(st, Utils.GetExpressionNode(x => x.InsuredPersonData.Snils));
        cbNotCheckSnils.Visible = !args.IsValid;
        return;
      }

      args.IsValid = true;
    }

    /// <summary>
    /// The validate temporary certificate number.
    /// </summary>
    /// <param name="source">
    /// The source. 
    /// </param>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected void ValidateTemporaryCertificateNumber(object source, ServerValidateEventArgs args)
    {
      if (!string.IsNullOrEmpty(tbTemporaryCertificateNumber.Text))
      {
        var st = new Statement
        {
          MedicalInsurances = new List<MedicalInsurance>{new MedicalInsurance
             {
                PolisType = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(PolisType.В),
                PolisNumber = tbTemporaryCertificateNumber.Text
             }}
        };
        try
        {
          ObjectFactory.GetInstance<IStatementService>().CheckPropertyStatement(
            st, Utils.GetExpressionNode(x => x.MedicalInsurances[0].PolisNumber));
        }
        catch (LogicalControlException e)
        {
          args.IsValid = false;
          cvTemporaryCertificateNumber.Text = e.GetAllMessages();
        }
      }
    }

    /// <summary>
    /// The btn clear_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
      Session[SearchCriteriaViewStateKey] = null;
      Response.Redirect("~/Pages/Main.aspx");
    }

    /// <summary>
    /// The btn search_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
      if (!Validate())
      {
        return;
      }

      var searchCriteria = GetSearchCriteria();
      //очищаем чтобы вызвался поиск
      searchCriteria.SearchResult = null;
      Session[SearchCriteriaViewStateKey] = searchCriteria;
      MakeStatementSearch(searchCriteria);
      custPager.ReloadPager();
    }

    /////// <summary>
    /////// The chb is look insured person_ on checked changed.
    /////// </summary>
    /////// <param name="sender">
    /////// The sender. 
    /////// </param>
    /////// <param name="e">
    /////// The e. 
    /////// </param>
    ////protected void chbIsLookInsuredPerson_OnCheckedChanged(object sender, EventArgs e)
    ////{
    ////  divAdvancedSearch.Style.Value = chbIsLookInsuredPerson.Checked ? "display: block" : "display: none";
    ////}

    /// <summary>
    /// The cust pager_ page index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void custPager_PageIndexChanged(object sender, CustomPageChangeArgs e)
    {
      // SearchResultGridView.PageIndex = 1;
      SearchResultGridView.PageSize = e.CurrentPageSize;

      var searchCriteria = Session[SearchCriteriaViewStateKey] as SearchStatementCriteria;
      //очищаем чтобы вызвался поиск
      searchCriteria.SearchResult = null;
      Session[SearchCriteriaViewStateKey] = searchCriteria;

      MakeStatementSearch(searchCriteria);
    }

    /// <summary>
    /// The cust pager_ page size changed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected void custPager_PageSizeChanged(object sender, CustomPageChangeArgs e)
    {
      // SearchResultGridView.PageIndex = 0;
      SearchResultGridView.PageSize = e.CurrentPageSize;
      custPager.CurrentPageIndex = 0;

      var searchCriteria = Session[SearchCriteriaViewStateKey] as SearchStatementCriteria;
      //очищаем чтобы вызвался поиск
      searchCriteria.SearchResult = null;
      Session[SearchCriteriaViewStateKey] = searchCriteria;

      MakeStatementSearch(searchCriteria);
    }

    /// <summary>
    ///   The bind empty data.
    /// </summary>
    private void BindEmptyData()
    {
      // Инициализация пустого грида
      // IList<SearchStatementResult> emptyList = new List<SearchStatementResult>();
      // SearchResultGridView.DataSource = emptyList;
      // SearchResultGridView.DataBind();

      // Инициализация пэйджера
      SearchResultGridView.PageIndex = 0;
      SearchResultGridView.PageSize = 5;
      custPager.Visible = false;
    }

    /// <summary>
    /// The convert sort direction.
    /// </summary>
    /// <param name="sortDirection">
    /// The sort direction. 
    /// </param>
    /// <returns>
    /// The <see cref="System.Web.UI.WebControls.SortDirection"/> . 
    /// </returns>
    private SortDirection ConvertSortDirection(System.Web.UI.WebControls.SortDirection sortDirection)
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

    /// <summary>
    ///   The fill document type.
    /// </summary>
    private void FillDocumentType()
    {
      var documentList = new List<ListItem>();
      documentList.Insert(0, new ListItem("Выберите вид документа", "-1"));
      documentList.AddRange(
        _statementService.GetNsiRecords(Oid.ДокументУдл).Select(
          x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray());
      documentUDL.FillDocumentTypeDdl(documentList.ToArray(), "0");
    }

    /// <summary>
    ///   The fill example.
    /// </summary>
    private void FillExample()
    {
      Session.Remove(SessionConsts.CGuidStatementId);
      Session.Remove(SessionConsts.CCurrentStatement);

      Statement example;
      if (SearchResultGridView.SelectedDataKey != null && SearchResultGridView.SelectedDataKey.Value != null)
      {
        var exampleId = (Guid)SearchResultGridView.SelectedDataKey.Value;
        example = _statementService.GetStatement(exampleId);
        example = _statementService.CreateFromExample(example);
        Session[SessionConsts.CPreviosStatementId] = exampleId;
      }
      else
      {
        example = new Statement();
        example.Status = _statementService.GetConcept(StatusStatement.New);
        example.AbsentPrevPolicy = false;
        example.DocumentUdl = new Document();
        example.DocumentUdl.DocumentType = _statementService.GetConcept(documentUDL.DocumentType);
        example.DocumentUdl.Series = documentUDL.DocumentSeries;
        example.DocumentUdl.Number = documentUDL.DocumentNumber;
        example.DocumentUdl.IssuingAuthority = documentUDL.DocumentIssuingAuthority;
        example.DocumentUdl.DateIssue = documentUDL.DocumentIssueDate;
        example.DocumentUdl.DateExp = documentUDL.DocumentExpDate;

        example.InsuredPersonData = new InsuredPersonDatum();
        example.InsuredPersonData.LastName = tbLastName.Text;
        example.InsuredPersonData.FirstName = tbFirstName.Text;
        example.InsuredPersonData.MiddleName = tbMiddleName.Text;
        DateTime dt;
        if (DateTime.TryParse(tbBirthDate.Text, out dt))
        {
          example.InsuredPersonData.BirthdayType = (int)BirthdayType.Full;
          example.InsuredPersonData.Birthday = dt;
        }

        example.InsuredPersonData.Snils = SnilsChecker.SsToShort(tbSnils.Text.Replace("_", string.Empty));
        example.InsuredPersonData.Birthplace = tbBirthPlace.Text;
        example.NumberPolicy = tbPolicyNumber.Text;
        example.InsuredPersonData.Gender = _statementService.GetConcept(int.Parse(ddlGender.SelectedValue));
      }

      Session[SessionConsts.CExampleStatement] = example;
    }

    /// <summary>
    ///   The fill gender.
    /// </summary>
    private void FillGender()
    {
      ddlGender.Items.AddRange(
        _statementService.GetNsiRecords(Oid.Пол).Select(
          x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray());
    }

    /// <summary>
    ///   The fill status statement.
    /// </summary>
    private void FillStatusStatement()
    {
      ddlCertificateStatus.Items.AddRange(
        _statementService.GetNsiRecords(Oid.СтатусызаявлениянавыдачуполисаОмс).Select(
          x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray());

      ddlCertificateStatus.Items.Add(new ListItem("Коллизия документов УДЛ", "9000"));
      ddlCertificateStatus.Items.Add(new ListItem("Коллизия СНИЛС", "9001"));
    }

    /// <summary>
    ///   The fill type statement.
    /// </summary>
    private void FillTypeStatement()
    {
      ddlCertificateType.Items.AddRange(
        _statementService.GetNsiRecords(Oid.Кодтипазаявления).Select(
          x => new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture))).ToArray());
    }

    private void SetSearchCriteria(SearchStatementCriteria criteria)
    {
      // Показывать последние заявления ЗЛ
      cbReturnLastStatement.Checked = criteria.ReturnLastStatement;

      // Учитывать дату подачи заявления
      chbUseDateFilling.Checked = criteria.UseDateFiling;

      //ошибки
      if (criteria.UseDateFiling)
      {
        hSelectedError.Value = criteria.Error;
      }

      // даты подачи заявления
      tbDateFillingFrom.Text = criteria.DateFilingFrom.HasValue ? criteria.DateFilingFrom.Value.ToString("dd.MM.yyyy") : DefaultStartDateFilling.ToShortDateString();
      tbDateFillingTo.Text = criteria.DateFilingTo.HasValue ? criteria.DateFilingTo.Value.ToString("dd.MM.yyyy") : DefaultEndDateFilling.ToShortDateString();

      // Статус заявления
      ddlCertificateStatus.SelectedValue = criteria.StatementStatus.ToString(CultureInfo.InvariantCulture);

      // Тип заявления
      ddlCertificateType.SelectedValue = criteria.StatementType.ToString(CultureInfo.InvariantCulture);

      // Документ УДЛ
      documentUDL.DocumentType = criteria.DocumentTypeId;
      documentUDL.DocumentSeries = criteria.DocumentSeries;
      documentUDL.DocumentNumber = criteria.DocumentNumber;
      documentUDL.DocumentIssuingAuthority = criteria.DocumentIssuingAuthority;
      documentUDL.DocumentIssueDate = criteria.DocumentIssueDate;

      // ФИО
      tbFirstName.Text = criteria.FirstName;
      tbLastName.Text = criteria.LastName;
      tbMiddleName.Text = criteria.MiddleName;

      // Дата рождения
      tbBirthDate.Text = criteria.BirthDate.HasValue ? criteria.BirthDate.Value.ToString("dd.MM.yyyy") : string.Empty;

      // СНИЛС
      tbSnils.Text = SnilsChecker.SsToLong(criteria.SNILS);
      cbNotCheckSnils.Checked = criteria.NotCheckSnils;

      // Место рождения
      tbBirthPlace.Text = criteria.BirthPlace;

      // Пол
      ddlGender.SelectedValue = criteria.Gender;

      // Номер ВС 
      tbTemporaryCertificateNumber.Text = criteria.CertificateNumber;

      // Полис
      tbPolicyNumber.Text = criteria.PolicyNumber;
      tbDatePolicyFrom.Text = criteria.DatePolicyFrom.HasValue ? criteria.DatePolicyFrom.Value.ToString("dd.MM.yyyy") : string.Empty;
      tbDatePolicyTo.Text = criteria.DatePolicyTo.HasValue ? criteria.DatePolicyTo.Value.ToString("dd.MM.yyyy") : string.Empty;

      // СМО
      tbSmo.Text = criteria.Smo;

      // ТФ
      tbTfoms.Text = criteria.Tfoms;
    }

    /// <summary>
    ///   Формирует новый поисковый критерий с данными из полей фильтра
    /// </summary>
    /// <returns> The <see cref="SearchStatementCriteria" /> . </returns>
    private SearchStatementCriteria GetSearchCriteria()
    {
      var criteria = new SearchStatementCriteria();

      // Показывать последние заявления ЗЛ
      criteria.ReturnLastStatement = cbReturnLastStatement.Checked;

      // Учитывать дату подачи заявления
      criteria.UseDateFiling = chbUseDateFilling.Checked;

      // даты подачи заявления

      criteria.DateFilingFrom = !chbUseDateFilling.Checked || string.IsNullOrEmpty(tbDateFillingFrom.Text)
                                  ? null
                                  : (DateTime?)DateTime.Parse(tbDateFillingFrom.Text);
      criteria.DateFilingTo = !chbUseDateFilling.Checked || string.IsNullOrEmpty(tbDateFillingTo.Text)
                                ? null
                                : (DateTime?)DateTime.Parse(tbDateFillingTo.Text);

      // Статус заявления
      criteria.StatementStatus = int.Parse(ddlCertificateStatus.SelectedValue);

      // Тип заявления
      criteria.StatementType = int.Parse(ddlCertificateType.SelectedValue);

      // Документ УДЛ
      criteria.DocumentTypeId = documentUDL.DocumentType;
      criteria.DocumentSeries = documentUDL.DocumentSeries;
      criteria.DocumentNumber = documentUDL.DocumentNumber;
      criteria.DocumentIssuingAuthority = documentUDL.DocumentIssuingAuthority;
      criteria.DocumentIssueDate = documentUDL.DocumentIssueDate;

      // ФИО
      criteria.FirstName = tbFirstName.Text;
      criteria.LastName = tbLastName.Text;
      criteria.MiddleName = tbMiddleName.Text;

      // Дата рождения
      criteria.BirthDate = string.IsNullOrEmpty(tbBirthDate.Text) ? null : (DateTime?)DateTime.Parse(tbBirthDate.Text);

      // СНИЛС
      criteria.SNILS = SnilsChecker.SsToShort(tbSnils.Text.Replace("_", string.Empty));
      criteria.NotCheckSnils = cbNotCheckSnils.Checked;

      // Место рождения
      criteria.BirthPlace = tbBirthPlace.Text;

      // Пол
      criteria.Gender = ddlGender.SelectedValue;

      // Номер ВС 
      criteria.CertificateNumber = tbTemporaryCertificateNumber.Text;

      // Полис
      criteria.PolicyNumber = tbPolicyNumber.Text;
      criteria.DatePolicyFrom = string.IsNullOrEmpty(tbDatePolicyFrom.Text)
                                  ? null
                                  : (DateTime?)DateTime.Parse(tbDatePolicyFrom.Text);
      criteria.DatePolicyTo = string.IsNullOrEmpty(tbDatePolicyTo.Text)
                                ? null
                                : (DateTime?)DateTime.Parse(tbDatePolicyTo.Text);

      // СМО
      criteria.Smo = tbSmo.Text;

      // ТФ
      criteria.Tfoms = tbTfoms.Text;

      // ошибки
      if (criteria.UseDateFiling)
      {
        criteria.Error = hSelectedError.Value;
      }

      // Сортировка
      criteria.SortExpression = "DateFiling";
      criteria.SortDirection = SortDirection.Descending;

      return criteria;
    }

    /// <summary>
    ///   The is own select people.
    /// </summary>
    /// <returns> The <see cref="bool?" /> . </returns>
    private bool? IsOwnSelectPeople()
    {
      if (SearchResultGridView.SelectedIndex == -1)
      {
        return null;
      }

      var strFromCurrentSmo = SearchResultGridView.SelectedRow.Cells[0].Text;
      bool fromCurrentSmo;
      return bool.TryParse(strFromCurrentSmo, out fromCurrentSmo) && fromCurrentSmo;
    }

    /// <summary>
    /// The make statement search.
    /// </summary>
    /// <param name="criteria">
    /// The criteria. 
    /// </param>
    private void MakeStatementSearch(SearchStatementCriteria criteria)
    {
      if (criteria.SearchResult == null)
      {
        // поиск по базе
        criteria.CurrentPageIndex = custPager.CurrentPageIndex >= 0 ? custPager.CurrentPageIndex : 0;
        criteria.Skip = criteria.CurrentPageIndex * SearchResultGridView.PageSize;
        criteria.Take = SearchResultGridView.PageSize;

        var searchResult = new SearchResult<SearchStatementResult> { Rows = new List<SearchStatementResult>() };
        try
        {
          searchResult = _statementService.Search(criteria);
          lbSeachError.Text = string.Empty;
        }
        catch (LogicalControlException exception)
        {
          // ошибка ФЛК
          lbSeachError.Text = exception.GetAllMessages();
        }
        catch (SearchTimeoutException exception)
        {
          // ошибка таймаута
          lbSeachError.Text = exception.Message;
        }
        catch (ArgumentException exception)
        {
          lbSeachError.Text = exception.Message;
        }
        catch (Exception exception)
        {
          // ошибка таймаута
          lbSeachError.Text = string.Format("Не обработаная ошибка при поиске ({0})", exception.Message);
        }

        criteria.SearchResult = searchResult;
      }

      var statementList = criteria.SearchResult.Rows;
      var statementCount = statementList.Count;
      if (statementCount == 0)
      {
        notFoundData.Visible = true;
        scrollArea.Visible = false;
        BindEmptyData();
      }
      else
      {
        scrollArea.Visible = true;
        notFoundData.Visible = false;

        // Биндинг данных
        SearchResultGridView.DataSource = statementList;
        SearchResultGridView.DataBind();

        // Пересчет пэйджера
        custPager.Visible = true;
        custPager.TotalPages = criteria.SearchResult.Total % SearchResultGridView.PageSize == 0
                                 ? criteria.SearchResult.Total / SearchResultGridView.PageSize
                                 : (criteria.SearchResult.Total / SearchResultGridView.PageSize) + 1;
      }

      // сброс селектированной строки
      if (SearchResultGridView.SelectedIndex >= 0)
      {
        SearchResultGridView.SelectedIndex = -1;
      }
      DisableMenuItems();

      if (hfSearchResultGVSelectedRowIndex.Value != null && hfSearchResultGVSelectedRowIndex.Value != "-1")
      {
        hfSearchResultGVSelectedRowIndex.Value = "-1";
      }

      GridUpdatePanel.Update();
    }

    #endregion

    protected void Menu_PreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(Menu);
    }

  }
}