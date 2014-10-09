// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportSmoBatches.aspx.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Pages
{
  using System;
  using System.Globalization;
  using System.Linq;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.core.model.interfaces;
  using rt.srz.model.dto;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;
  using rt.srz.ui.pvp.Controls.CustomPager;

  using StructureMap;

  /// <summary>
  /// The export smo batches.
  /// </summary>
  public partial class ExportSmoBatches : Page
  {
    #region Fields

    /// <summary>
    ///   Сервис секурити
    /// </summary>
    private readonly ISecurityService securityService = ObjectFactory.GetInstance<ISecurityService>();

    /// <summary>
    ///   Сервис ТФ
    /// </summary>
    private readonly ITfomsService tfomsService = ObjectFactory.GetInstance<ITfomsService>();

    /// <summary>
    ///   Пэйджер
    /// </summary>
    private IntegrationPager<SearchExportSmoBatchCriteria, SearchBatchResult> integrPager;

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
      integrPager = new IntegrationPager<SearchExportSmoBatchCriteria, SearchBatchResult>(
        batchGrid, 
        custPager, 
        ViewState, 
        criteria => tfomsService.SearchExportSmoBatches(criteria));

      if (!IsPostBack)
      {
        var currentUser = securityService.GetCurrentUser();
        var searchCriteria = new SearchExportSmoBatchCriteria { BatchNumber = -1 };

        // Проверям права пользователей на просмотр батчей, в случае если текущий пользователь не глобальный админ
        if (!currentUser.IsAdmin)
        {
          // Определяем является ли текущий пользователь администратором ТФОМС
          if (securityService.IsUserAdminTF(currentUser.Id))
          {
            searchCriteria.SenderId = currentUser.GetTf().Id;
          }

          // Определяем, является ли текущий пользователь администратором СМО
          if (searchCriteria.SenderId == Guid.Empty)
          {
            if (securityService.IsUserAdminSmo(currentUser.Id))
            {
              searchCriteria.ReceiverId = currentUser.GetSmo().Id;
            }
          }
        }

        // Получаем список периодов
        var periodList = tfomsService.GetExportSmoBatchPeriodList(searchCriteria.SenderId, searchCriteria.ReceiverId);
        if (periodList.Any())
        {
          // Заполняем список периодов
          ddlPeriod.Items.AddRange(
                                   periodList.Select(
                                                     x =>
                                                     new ListItem(
                                                       x.Year.Year.ToString(CultureInfo.InvariantCulture) + " - " + x.Code.Code.ToString(CultureInfo.InvariantCulture), 
                                                       x.Id.ToString())).ToArray());
          ddlPeriod.SelectedIndex = 0;

          searchCriteria.PeriodId = new Guid(ddlPeriod.SelectedValue);
        }

        // Запускаем запрос по поиску батчей
        integrPager.LoadPage();
        integrPager.SetNewCriteria(searchCriteria);
        integrPager.RefreshData();
        upBatchGrid.Update();
      }
    }

    /// <summary>
    /// Кнпока очистки
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void BtnClearClick(object sender, EventArgs e)
    {
      tbBatchNumber.Text = string.Empty;
      if (ddlPeriod.Items.Count > 0)
      {
        ddlPeriod.SelectedIndex = 0;
      }

      upBatchGrid.Update();
    }

    /// <summary>
    /// The btn export_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void BtnExportClick(object sender, EventArgs e)
    {
      var btn = (Button)sender;
      var row = (GridViewRow)btn.NamingContainer;
      var hiddenField = (HiddenField)row.FindControl("hfId");

      // Меняем код батча
      tfomsService.MarkBatchAsUnexported(new Guid(hiddenField.Value));

      // Запускаем запрос по поиску батчей
      integrPager.RefreshData();
      upBatchGrid.Update();
    }

    /// <summary>
    /// Кнпока поиска
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void BtnSearchClick(object sender, EventArgs e)
    {
      // Поиск по периоду
      if (ddlPeriod.Items.Count > 0)
      {
        integrPager.CurrentCriteria.PeriodId = new Guid(ddlPeriod.SelectedValue);
      }

      // Поиск по номеру батча
      integrPager.CurrentCriteria.BatchNumber = -1;
      if (!string.IsNullOrEmpty(tbBatchNumber.Text))
      {
        var batchNumber = -1;
        integrPager.CurrentCriteria.BatchNumber = batchNumber;
        if (int.TryParse(tbBatchNumber.Text, out batchNumber))
        {
          integrPager.CurrentCriteria.BatchNumber = batchNumber;
        }
      }

      // Запускаем запрос по поиску батчей
      integrPager.RefreshData();
      upBatchGrid.Update();
    }

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
      integrPager.DoPagerPageIndexChange(e);
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
      integrPager.DoPagerPageSizeChange(e);
    }

    #endregion
  }
}