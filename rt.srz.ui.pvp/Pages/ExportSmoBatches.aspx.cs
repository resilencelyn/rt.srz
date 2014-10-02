using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StructureMap;
using rt.srz.model.srz;
using rt.srz.model.dto;
using rt.srz.model.interfaces.service;
using rt.srz.ui.pvp.Controls.CustomPager;
using rt.srz.model.enumerations;

namespace rt.srz.ui.pvp.Pages
{
  public partial class ExportSmoBatches : System.Web.UI.Page
  {
    #region Private Fields
    
    /// <summary>
    /// Пэйджер
    /// </summary>
    private IntegrationPager<SearchExportSmoBatchCriteria, SearchBatchResult> integrPager;

    /// <summary>
    /// Сервис ТФ
    /// </summary>
    private ITFService tfService = ObjectFactory.GetInstance<ITFService>();

    /// <summary>
    /// Сервис секурити
    /// </summary>
    private ISecurityService securityService = ObjectFactory.GetInstance<ISecurityService>();
    
    #endregion

    #region Events
    protected void Page_Init(object sender, EventArgs e)
    {
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
      integrPager = new IntegrationPager<SearchExportSmoBatchCriteria, SearchBatchResult>(batchGrid, custPager, 
        ViewState, (criteria) => tfService.SearchExportSmoBatches(criteria));
      
      if (!IsPostBack)
      {
        User currentUser = securityService.GetCurrentUser();
        SearchExportSmoBatchCriteria searchCriteria = new SearchExportSmoBatchCriteria { BatchNumber = -1 };
        
        // Проверям права пользователей на просмотр батчей, в случае если текущий пользователь не глобальный админ
        if (!currentUser.IsAdmin)
        {
          // Определяем является ли текущий пользователь администратором ТФОМС
          if (securityService.IsUserAdminTF(currentUser.Id))
            searchCriteria.SenderId = currentUser.GetTf().Id;

          // Определяем, является ли текущий пользователь администратором СМО
          if (searchCriteria.SenderId == Guid.Empty)
          {
            if (securityService.IsUserAdminSmo(currentUser.Id))
              searchCriteria.ReceiverId = currentUser.GetSmo().Id;
          }
        }

        // Получаем список периодов
        var periodList = tfService.GetExportSmoBatchPeriodList(searchCriteria.SenderId, searchCriteria.ReceiverId);
        if (periodList.Any())
        {
          // Заполняем список периодов
          ddlPeriod.Items.AddRange(periodList.Select(x => new ListItem(x.Year.Year.ToString() + " - " + x.Code.Code.ToString(),
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

    protected void custPager_PageIndexChanged(object sender, CustomPageChangeArgs e)
    {
      integrPager.DoPagerPageIndexChange(e);
    }

    protected void custPager_PageSizeChanged(object sender, CustomPageChangeArgs e)
    {
      integrPager.DoPagerPageSizeChange(e);
    }

    /// <summary>
    /// Кнпока очистки
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
      tbBatchNumber.Text = string.Empty;
      if (ddlPeriod.Items.Count > 0)
        ddlPeriod.SelectedIndex = 0;
      upBatchGrid.Update();
    }

    /// <summary>
    /// Кнпока поиска
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
      // Поиск по периоду
      if (ddlPeriod.Items.Count > 0)
        integrPager.CurrentCriteria.PeriodId = new Guid(ddlPeriod.SelectedValue);

      // Поиск по номеру батча
      integrPager.CurrentCriteria.BatchNumber = -1;
      if (!string.IsNullOrEmpty(tbBatchNumber.Text))
      { 
        int batchNumber = -1;
        integrPager.CurrentCriteria.BatchNumber = batchNumber;
        if (int.TryParse(tbBatchNumber.Text, out batchNumber))
          integrPager.CurrentCriteria.BatchNumber = batchNumber;
      }
      
      // Запускаем запрос по поиску батчей
      integrPager.RefreshData();
      upBatchGrid.Update();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
      Button btn = (Button)sender;
      GridViewRow row = (GridViewRow)btn.NamingContainer;
      HiddenField hfId = (HiddenField)row.FindControl("hfId");

      //Меняем код батча
      tfService.MarkBatchAsUnexported(new Guid(hfId.Value));

      // Запускаем запрос по поиску батчей
      int pageIndex = integrPager.CurrentPageIndex;
      integrPager.RefreshData();
      upBatchGrid.Update();
    }
    #endregion
  }
}