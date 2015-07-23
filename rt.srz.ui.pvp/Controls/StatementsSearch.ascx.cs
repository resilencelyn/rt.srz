// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementsSearch.ascx.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.Globalization;
  using System.Linq;
  using System.Text;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.core.model;
  using rt.core.model.dto;
  using rt.core.model.interfaces;
  using rt.core.model.security;
  using rt.srz.business.extensions;
  using rt.srz.model.algorithms;
  using rt.srz.model.dto;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;
  using rt.srz.services;
  using rt.srz.ui.pvp.Controls.CustomPager;
  using rt.srz.ui.pvp.Enumerations;

  using StructureMap;

  using SortDirection = rt.core.model.dto.enumerations.SortDirection;

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

    #endregion

    #region Fields

    /// <summary>
    ///   The security service.
    /// </summary>
    protected ISecurityService SecurityService;

    /// <summary>
    ///   The service.
    /// </summary>
    protected IStatementService StatementService;

    /// <summary>
    /// The auth service.
    /// </summary>
    protected IAuthService AuthService;

    protected IRegulatoryService RegulatoryService;

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the default end date filling.
    /// </summary>
    private DateTime DefaultEndDateFilling
    {
      get
      {
        return DateTime.Today;
      }
    }

    /// <summary>
    ///   Gets the default start date filling.
    /// </summary>
    private DateTime DefaultStartDateFilling
    {
      get
      {
        return DateTime.Today.AddDays(-30);
      }
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
    ///   The get url uec gate.
    /// </summary>
    /// <returns>
    ///   The <see cref="string" />.
    /// </returns>
    protected string GetUrlUecGate()
    {
      var url = Request.Url.AbsoluteUri.Replace("Pages/Main.aspx", "UecGate.svc");
      return url;
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
                ////userActionManager.LogAccessToPersonalData(_StatementService.GetStatement((Guid)SearchResultGridView.SelectedDataKey.Value), "Просмотр заявления");

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
                ////userActionManager.LogAccessToPersonalData(_StatementService.GetStatement((Guid)SearchResultGridView.SelectedDataKey.Value), "Выдача полиса");

                // Переход
                // Session[SessionConsts.CGuidStatementId] = SearchResultGridView.SelectedDataKey.Value;
                Session[SessionConsts.CCurrentStatement] =
                  StatementService.GetStatement((Guid)SearchResultGridView.SelectedDataKey.Value);
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
          // if (!_StatementService.InsuredInJoined(_StatementService.GetStatement((Guid)SearchResultGridView.SelectedDataKey.Value).InsuredPerson.Id))
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

    /// <summary>
    /// The menu_ pre render.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void MenuPreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(Menu);
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
      StatementService = ObjectFactory.GetInstance<IStatementService>();
      SecurityService = ObjectFactory.GetInstance<ISecurityService>();
      AuthService = ObjectFactory.GetInstance<IAuthService>();
      RegulatoryService = ObjectFactory.GetInstance<IRegulatoryService>();

      if (!IsPostBack)
      {
        FillStatusStatement();
        FillTypeStatement();
        FillDocumentType();
        FillGender();
        documentUDL.SetSimpleMode();
      }
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
      confirmDelete.Hide();
      confirmDeath.Hide();

      if (!IsPostBack)
      {
        ViewState["OpenNotOwnSmo"] = SecurityService.GetIsCurrentUserAllowPermission(PermissionCode.Search_OpenNotOwnSmo);

        var currentUser = SecurityService.GetCurrentUser();
        Menu.FindItem(SessionConsts.CDelete).NavigateUrl = confirmDelete.ViewConfirmScript;

        ////Menu.FindItem(SessionConsts.CDeleteDeathInfo).NavigateUrl = confirmDeath.ViewConfirmScript;
        SetMenuByPermission();
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
          // если выбран статус заявления отклонено или вообще не выбран, то только в этом случае отображаем список с ошибками
          if (ddlCertificateStatus.SelectedValue == StatusStatement.Declined.ToString(CultureInfo.InvariantCulture)
              || ddlCertificateStatus.SelectedValue == "-1")
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

        // Отмена заявления
        if (Request.Form[Page.postEventSourceID] == confirmDelete.ConfirmedTargetUnique
            && Request.Form[Page.postEventArgumentID] == confirmDelete.ConfirmedArgumentUnique)
        {
          CancelStatement();
          return;
        }

        // Отмена смерти
        if (Request.Form[Page.postEventSourceID] == confirmDeath.ConfirmedTargetUnique
            && Request.Form[Page.postEventArgumentID] == confirmDeath.ConfirmedArgumentUnique)
        {
          DeleteDeathInfo();
          return;
        }
      }

      // обновляем инфу в уголке по краткому содержанию фильтра
      UpdateBriefFilterInfoInCorner();
      }

    /// <summary>
    /// The search result grid view_ row command.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void SearchResultGridViewRowCommand(object sender, GridViewCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        // case "SingleClick":
        // SearchResultGridView.SelectedIndex = int.Parse(e.CommandArgument.ToString());
        // SearchResultGridView_SelectedIndexChanged(SearchResultGridView, new EventArgs());
        // break;
        case "DoubleClick":
          SearchResultGridView.SelectedIndex = int.Parse(e.CommandArgument.ToString());
          MenuMenuItemClick(Menu, new MenuEventArgs(Menu.Items[(int)StatementSearchMenuItem.Edit]));
          break;
      }
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
    protected void SearchResultGridViewRowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
      {
        return;
      }

      e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
      e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
      e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(
                                                                                SearchResultGridView,
                                                                                "Select$" + e.Row.RowIndex);

      // Get the LinkButton control in the second cell
      var doubleClickButton = (LinkButton)e.Row.Cells[12].Controls[0];

      // Get the javascript which is assigned to this LinkButton
      var jsdouble = Page.ClientScript.GetPostBackClientHyperlink(doubleClickButton, string.Empty);

      // Add this JavaScript to the ondblclick Attribute of the row
      e.Row.Attributes["ondblclick"] = jsdouble;

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

      e.Row.ForeColor = Color.Black; // Color.FromArgb(0, 145, 175); //Color.Green;
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
    protected void SearchResultGridViewSelectedIndexChanged(object sender, EventArgs e)
    {
      hfSearchResultGVSelectedRowIndex.Value = SearchResultGridView.SelectedIndex.ToString(CultureInfo.InvariantCulture);
      if (SearchResultGridView.SelectedIndex == -1)
      {
        return;
      }

      Guid id;
      Statement statement = null;
      var user = SecurityService.GetCurrentUser();
      if (SearchResultGridView.SelectedDataKey != null
          && Guid.TryParse(SearchResultGridView.SelectedDataKey.Value.ToString(), out id))
      {
        statement = StatementService.GetStatement(id);
      }

      bool fromCurrentSmo;
#if(IgnoreSmo)
      fromCurrentSmo = true;
#else

      fromCurrentSmo = statement != null && user.HasSmo() && statement.PointDistributionPolicy != null
                       && statement.PointDistributionPolicy.Parent != null
                       && statement.PointDistributionPolicy.Parent.Id == user.GetSmo().Id;
#endif

      var value = statement != null && statement.IsActive && statement.Status.Id == StatusStatement.Exercised;
      SetMenuItemEnable(SessionConsts.CReinsuranse, value);
      SetMenuItemEnable(SessionConsts.CReneval, value && fromCurrentSmo);

      // для отклонённых и отменённых заявлений кнопка выдать полис должна быть недоступна
      SetMenuItemEnable(
                        SessionConsts.CIssue,
                        fromCurrentSmo
                        && (!statement.PolicyIsIssued.HasValue || !statement.PolicyIsIssued.Value)
                        && statement.Status.Id != StatusStatement.Declined
                        && statement.Status.Id != StatusStatement.Cancelled);

      SetMenuItemEnable(SessionConsts.CEdit, fromCurrentSmo || (bool)ViewState["OpenNotOwnSmo"]);

      SetMenuItemText(SessionConsts.CReinsuranse, fromCurrentSmo ? "Создать" : "Перестраховать");

      var statusForDelete = statement != null
                            && (statement.Status.Id == StatusStatement.Declined
                                || statement.Status.Id == StatusStatement.New);
      SetMenuItemEnable(SessionConsts.CDelete, fromCurrentSmo && statusForDelete);
      SetMenuItemEnable(SessionConsts.CWriteUEC, fromCurrentSmo);
      SetMenuItemEnable(SessionConsts.CInsuranceHistory, fromCurrentSmo);
      var insuredInJoined =
        StatementService.InsuredInJoined(StatementService.GetStatement((Guid)SearchResultGridView.SelectedDataKey.Value).InsuredPerson.Id);
      SetMenuItemEnable(SessionConsts.CSeparate, insuredInJoined && fromCurrentSmo);

      SetMenuItemEnable(
                        SessionConsts.CDeleteDeathInfo,
                        statement != null && statement.InsuredPerson.Status.Id == StatusPerson.Dead);

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
    protected void SearchResultGridViewSorting(object sender, GridViewSortEventArgs e)
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

      // очищаем чтобы вызвался поиск
      if (currentCriteria != null)
      {
        currentCriteria.SearchResult = null;
        Session[SearchCriteriaViewStateKey] = currentCriteria;

        // Запускаем новый поиск
        MakeStatementSearch(currentCriteria);
      }

      custPager.ReloadPager();
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
        args.IsValid = StatementService.TryCheckProperty(st, Utils.GetExpressionNode(x => x.InsuredPersonData.Snils));
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
                   MedicalInsurances =
                     new List<MedicalInsurance>
                     {
                       new MedicalInsurance
                       {
                         PolisType = RegulatoryService.GetConcept(PolisType.В), 
                         PolisNumber = tbTemporaryCertificateNumber.Text
                       }
                     }
                 };
        try
        {
          ObjectFactory.GetInstance<IStatementService>()
                       .CheckPropertyStatement(st, Utils.GetExpressionNode(x => x.MedicalInsurances[0].PolisNumber));
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
    protected void BtnClearClick(object sender, EventArgs e)
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
    protected void BtnSearchClick(object sender, EventArgs e)
    {
      if (!Validate())
      {
        return;
      }

      var searchCriteria = GetSearchCriteria();

      // очищаем чтобы вызвался поиск
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
    protected void CustPagerPageIndexChanged(object sender, CustomPageChangeArgs e)
    {
      // SearchResultGridView.PageIndex = 1;
      SearchResultGridView.PageSize = e.CurrentPageSize;

      var searchCriteria = Session[SearchCriteriaViewStateKey] as SearchStatementCriteria;

      // очищаем чтобы вызвался поиск
      if (searchCriteria != null)
      {
        searchCriteria.SearchResult = null;
        Session[SearchCriteriaViewStateKey] = searchCriteria;

        MakeStatementSearch(searchCriteria);
      }
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
    protected void CustPagerPageSizeChanged(object sender, CustomPageChangeArgs e)
    {
      // SearchResultGridView.PageIndex = 0;
      SearchResultGridView.PageSize = e.CurrentPageSize;
      custPager.CurrentPageIndex = 0;

      var searchCriteria = Session[SearchCriteriaViewStateKey] as SearchStatementCriteria;

      // очищаем чтобы вызвался поиск
      if (searchCriteria != null)
      {
        searchCriteria.SearchResult = null;
        Session[SearchCriteriaViewStateKey] = searchCriteria;

        MakeStatementSearch(searchCriteria);
      }
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
    ///   The cancel statement.
    /// </summary>
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
        StatementService.CanceledStatement(id);
      }

      RefreshCurrentSearchGridPage();
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
    ///   The delete death info.
    /// </summary>
    private void DeleteDeathInfo()
    {
      if (SearchResultGridView.SelectedDataKey == null)
      {
        return;
      }

      StatementService.DeleteDeathInfo((Guid)SearchResultGridView.SelectedDataKey.Value);
      RefreshCurrentSearchGridPage();
    }

    /// <summary>
    ///   The disable menu items.
    /// </summary>
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

    /// <summary>
    ///   The fill document type.
    /// </summary>
    private void FillDocumentType()
    {
      var documentList = new List<ListItem>();
      documentList.Insert(0, new ListItem("Выберите вид документа", "-1"));
      documentList.AddRange(
                            RegulatoryService.GetNsiRecords(Oid.ДокументУдл)
                                            .Select(
                                                    x =>
                                                    new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture)))
                                            .ToArray());
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
        example = StatementService.GetStatement(exampleId);
        example = StatementService.CreateFromExample(example);
        Session[SessionConsts.CPreviosStatementId] = exampleId;
      }
      else
      {
        example = new Statement();
        example.Status = RegulatoryService.GetConcept(StatusStatement.New);
        example.AbsentPrevPolicy = false;
        example.DocumentUdl = new Document();
        example.DocumentUdl.DocumentType = RegulatoryService.GetConcept(documentUDL.DocumentType);
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
        example.InsuredPersonData.Gender = RegulatoryService.GetConcept(int.Parse(ddlGender.SelectedValue));
      }

      Session[SessionConsts.CExampleStatement] = example;
    }

    /// <summary>
    ///   The fill gender.
    /// </summary>
    private void FillGender()
    {
      ddlGender.Items.AddRange(
                               RegulatoryService.GetNsiRecords(Oid.Пол)
                                               .Select(
                                                       x =>
                                                       new ListItem(x.Name, x.Id.ToString(CultureInfo.InvariantCulture)))
                                               .ToArray());
    }

    /// <summary>
    ///   The fill status statement.
    /// </summary>
    private void FillStatusStatement()
    {
      ddlCertificateStatus.Items.AddRange(
                                          RegulatoryService.GetNsiRecords(Oid.СтатусызаявлениянавыдачуполисаОмс)
                                                          .Select(
                                                                  x =>
                                                                  new ListItem(
                                                                    x.Name,
                                                                    x.Id.ToString(CultureInfo.InvariantCulture)))
                                                          .ToArray());

      ddlCertificateStatus.Items.Add(new ListItem("Коллизия документов УДЛ", "9000"));
      ddlCertificateStatus.Items.Add(new ListItem("Коллизия СНИЛС", "9001"));
    }

    /// <summary>
    ///   The fill type statement.
    /// </summary>
    private void FillTypeStatement()
    {
      ddlCertificateType.Items.AddRange(
                                        RegulatoryService.GetNsiRecords(Oid.Кодтипазаявления)
                                                        .Select(
                                                                x =>
                                                                new ListItem(
                                                                  x.Name,
                                                                  x.Id.ToString(CultureInfo.InvariantCulture)))
                                                        .ToArray());
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
          searchResult = StatementService.Search(criteria);
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

    /// <summary>
    ///   The refresh current search grid page.
    /// </summary>
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
    /// The set menu by permission.
    /// </summary>
    private void SetMenuByPermission()
    {
      SetMenuByPermission(SessionConsts.CReinsuranse, PermissionCode.Search_Create);
      SetMenuByPermission(SessionConsts.CReneval, PermissionCode.Search_Reneval);
      SetMenuByPermission(SessionConsts.CEdit, PermissionCode.Search_Edit);
      SetMenuByPermission(SessionConsts.CDelete, PermissionCode.Search_Delete);
      SetMenuByPermission(SessionConsts.CReadUEC, PermissionCode.Search_ReadUec);
      SetMenuByPermission(SessionConsts.CWriteUEC, PermissionCode.Search_WriteUec);
      SetMenuByPermission(SessionConsts.CReadSmartCard, PermissionCode.Search_ReadSmartCard);
      SetMenuByPermission(SessionConsts.CSeparate, PermissionCode.Search_Separate);
      SetMenuByPermission(SessionConsts.CInsuranceHistory, PermissionCode.Search_InsuranceHistory);
      SetMenuByPermission(SessionConsts.CDeleteDeathInfo, PermissionCode.CancelDeath);
      SetMenuByPermission(SessionConsts.CIssue, PermissionCode.Search_GiveOut);
    }

    /// <summary>
    /// The set menu by permission.
    /// </summary>
    /// <param name="menuItemValue">
    /// The menu item value.
    /// </param>
    /// <param name="permissionCode">
    /// The permission code.
    /// </param>
    private void SetMenuByPermission(string menuItemValue, PermissionCode permissionCode)
    {
      UtilsHelper.SetMenuItemByPermission(Session, Menu, SecurityService, menuItemValue, permissionCode);
    }

    /// <summary>
    /// The set menu item enable.
    /// </summary>
    /// <param name="itemName">
    /// The item name.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    private void SetMenuItemEnable(string itemName, bool value)
    {
      // все элементы на одном уровне
      var item = Menu.FindItem(itemName);
      if (item != null)
      {
        item.Enabled = value;
      }
    }

    /// <summary>
    /// The set menu item text.
    /// </summary>
    /// <param name="itemName">
    /// The item name.
    /// </param>
    /// <param name="text">
    /// The text.
    /// </param>
    private void SetMenuItemText(string itemName, string text)
    {
      // все элементы на одном уровне
      var item = Menu.FindItem(itemName);
      if (item != null)
      {
        item.Text = text;
      }
    }

    /// <summary>
    /// The set search criteria.
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    private void SetSearchCriteria(SearchStatementCriteria criteria)
    {
      // Показывать последние заявления ЗЛ
      cbReturnLastStatement.Checked = criteria.ReturnLastStatement;

      // Учитывать дату подачи заявления
      chbUseDateFilling.Checked = criteria.UseDateFiling;

      // ошибки
      if (criteria.UseDateFiling)
      {
        hSelectedError.Value = criteria.Error;
      }

      // даты подачи заявления
      tbDateFillingFrom.Text = criteria.DateFilingFrom.HasValue
                                 ? criteria.DateFilingFrom.Value.ToString("dd.MM.yyyy")
                                 : DefaultStartDateFilling.ToShortDateString();
      tbDateFillingTo.Text = criteria.DateFilingTo.HasValue
                               ? criteria.DateFilingTo.Value.ToString("dd.MM.yyyy")
                               : DefaultEndDateFilling.ToShortDateString();

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
      tbDatePolicyFrom.Text = criteria.DatePolicyFrom.HasValue
                                ? criteria.DatePolicyFrom.Value.ToString("dd.MM.yyyy")
                                : string.Empty;
      tbDatePolicyTo.Text = criteria.DatePolicyTo.HasValue
                              ? criteria.DatePolicyTo.Value.ToString("dd.MM.yyyy")
                              : string.Empty;

      // СМО
      tbSmo.Text = criteria.Smo;

      // ТФ
      tbTfoms.Text = criteria.Tfoms;
    }

    /// <summary>
    /// The set text to brief filter.
    /// </summary>
    /// <param name="label">
    /// The label.
    /// </param>
    /// <param name="baseText">
    /// The base text.
    /// </param>
    /// <param name="valuesText">
    /// The values text.
    /// </param>
    private void SetTextToBriefFilter(Label label, string baseText, params string[] valuesText)
    {
      var resultValue = string.Join(" ", valuesText).Trim();
      if (string.IsNullOrEmpty(resultValue))
      {
        label.Visible = false;
        return;
      }

      label.Visible = true;
      var sb = new StringBuilder();
      sb.Append("<span><b>").Append(baseText).Append(": </b></span>").Append(resultValue).Append("; ");
      label.Text = sb.ToString();
    }

    /// <summary>
    ///   Обновляет краткую инфу по фильтру в уголке, которая отображается при свёрнутом фильтре
    /// </summary>
    private void UpdateBriefFilterInfoInCorner()
    {
      // Отображаются последние заявления
      lbLastStatementsF.Visible = cbReturnLastStatement.Checked;

      // даты подачи заявления
      var valuesText = chbUseDateFilling.Checked ? string.Format("{0} - {1}", tbDateFillingFrom.Text, tbDateFillingTo.Text) : string.Empty;
      SetTextToBriefFilter(lbDatesF, "Дата подачи заявления", valuesText);

      // удл
      var text = documentUDL.DocumentTypeStr == "Выберите вид документа"
                   ? string.Empty
                   : documentUDL.DocumentTypeStr;
      var s = documentUDL.DocumentIssueDate.HasValue
                ? documentUDL.DocumentIssueDate.Value.ToShortDateString()
                : string.Empty;
      SetTextToBriefFilter(lbUdlF, "Документ УДЛ", text, documentUDL.DocumentSeries, documentUDL.DocumentNumber, documentUDL.DocumentIssuingAuthority, s);

      // ФИО
      SetTextToBriefFilter(lbFioF, "ФИО", tbLastName.Text, tbFirstName.Text, tbMiddleName.Text);

      // Дата рождения
      SetTextToBriefFilter(lbDatebirthF, "Дата рождения", tbBirthDate.Text);

      // СНИЛС
      if (
        !string.IsNullOrEmpty(
                              tbSnils.Text.Replace("-", string.Empty)
                                     .Replace(" ", string.Empty)
                                     .Replace("_", string.Empty)))
      {
        SetTextToBriefFilter(lbSnilsF, "СНИЛС", tbSnils.Text);
      }
      else
      {
        SetTextToBriefFilter(lbSnilsF, "СНИЛС", string.Empty);
      }

      // Место рождения
      SetTextToBriefFilter(lbBirthPlaceF, "Место рождения", tbBirthPlace.Text);

      // текст ошибки
      // 0 индекс соответсвует тому когда данные не выбраны
      var s1 = (hSelectedError.Value == "Данные не выбраны" || hSelectedError.Value == "-1")
                          ? string.Empty
                          : hSelectedError.Value;
      SetTextToBriefFilter(lbErrorF, "Причина отклонения", s1);
    }

    #endregion
  }
}