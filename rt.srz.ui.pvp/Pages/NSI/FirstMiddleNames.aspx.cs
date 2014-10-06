using rt.srz.model.dto;
using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Controls.CustomPager;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SortDirection = rt.core.model.dto.enumerations.SortDirection;

namespace rt.srz.ui.pvp.Pages.NSI
{
  using rt.core.model.interfaces;

  public partial class FirstMiddleNames : System.Web.UI.Page
  {
    private INsiService _service;
    private ISecurityService _sec;
    private IntegrationPager<SearchAutoCompleteCriteria, AutoComplete> _intergPager;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<INsiService>();
      _sec = ObjectFactory.GetInstance<ISecurityService>();
      searchByNameControl.Clear += searchByNameControl_Clear;
      searchByNameControl.Search += searchByNameControl_Search;
    }

    void searchByNameControl_Search()
    {
      custPager.ResetCurrentPage();

      SearchAutoCompleteCriteria newCriteria = new SearchAutoCompleteCriteria();
      newCriteria.Name = searchByNameControl.NameValue;
      _intergPager.SetNewCriteria(newCriteria);
      _intergPager.RefreshData();
      contentUpdatePanel.Update();
      //без этой строки при первом отображении списка смо, если набрать в поиске буквы и нажать ввод, 
      //то при попытке выбрать запись в гриде она не выбирается, пока не клыпнешь либо по другой колонке грида либо по другому контролу
      searchByNameControl.SetFocus();
    }

    void searchByNameControl_Clear()
    {
      SearchAutoCompleteCriteria newCriteria = new SearchAutoCompleteCriteria();
      _intergPager.SetNewCriteria(newCriteria);
      _intergPager.RefreshData();
      contentUpdatePanel.Update();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      _intergPager = new IntegrationPager<SearchAutoCompleteCriteria, AutoComplete>(
          grid, custPager, ViewState, (criteria) => _service.GetFirstMiddleNames(criteria));
      _intergPager.AfterRefreshData += () =>
        {
          SetButtonsEnable(grid.SelectedDataKey != null);
        };

      if (!IsPostBack)
      {
        searchByNameControl.NameTitle = "Название";
        SetButtonsEnable(false);
        _intergPager.LoadPage(20);
      }
    }

    protected void custPager_PageIndexChanged(object sender, CustomPageChangeArgs e)
    {
      _intergPager.DoPagerPageIndexChange(e);
    }

    protected void custPager_PageSizeChanged(object sender, CustomPageChangeArgs e)
    {
      _intergPager.DoPagerPageSizeChange(e);
    }


    protected void menu_MenuItemClick(object sender, MenuEventArgs e)
    {
      switch (e.Item.Value)
      {
        case "Add":
          Response.Redirect("~/Pages/NSI/FirstMiddleName.aspx");
          break;
        case "Open":
          if (grid.SelectedDataKey == null)
          {
            return;
          }
          Response.Redirect(string.Format("~/Pages/NSI/FirstMiddleName.aspx?Id={0}", grid.SelectedDataKey.Value.ToString()));
          break;
        case "Delete":
          if (grid.SelectedDataKey == null)
          {
            return;
          }
          _service.DeleteFirstMiddleName((Guid)grid.SelectedDataKey.Value);
          grid.SelectedIndex = -1;
          _intergPager.RefreshData(_intergPager.CurrentCriteria);
          contentUpdatePanel.Update();
          SetButtonsEnable(grid.SelectedDataKey != null);
          break;
      }
    }

    protected void grid_Sorting(object sender, GridViewSortEventArgs e)
    {
      var currentCriteria = _intergPager.CurrentCriteria;
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
        currentCriteria.SortDirection = UtilsHelper.ConvertSortDirection(e.SortDirection);
      }
      _intergPager.SetNewCriteria(currentCriteria);

      // Запускаем новый поиск
      _intergPager.RefreshData(currentCriteria);
      custPager.ReloadPager();
    }

    protected void grid_SelectedIndexChanged(object sender, EventArgs e)
    {
      SetButtonsEnable(grid.SelectedDataKey != null);
    }

    private void SetButtonsEnable(bool value)
    {
      UtilsHelper.SetMenuButtonsEnable(value, MenuUpdatePanel, menu1);

      //если не админские права то удалять записи нельзя
      if (ViewState["allowDelete"] == null)
      {
        ViewState["allowDelete"] = _sec.IsUserHasAdminPermissions(_sec.GetCurrentUser());
      }
      if (!(bool)ViewState["allowDelete"])
      {
        menu1.FindItem("Delete").Enabled = false;
        MenuUpdatePanel.Update();
      }
    }

    protected override void Render(HtmlTextWriter writer)
    {
      UtilsHelper.AddAttributesToGridRow(Page.ClientScript, grid);
      base.Render(writer);
    }

    protected void menu1_PreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(menu1);
    }

  }
}