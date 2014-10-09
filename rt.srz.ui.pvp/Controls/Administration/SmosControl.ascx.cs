using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SortDirection = rt.core.model.dto.enumerations.SortDirection;
using rt.srz.model.dto;
using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Controls.CustomPager;
using StructureMap;
using rt.srz.ui.pvp.Enumerations;

namespace rt.srz.ui.pvp.Controls.Administration
{
  using rt.core.model;
  using rt.core.model.interfaces;

  public partial class SmosControl : System.Web.UI.UserControl
  {
    private IRegulatoryService regulatoryService;
    private ISecurityService _securityService;
    private IntegrationPager<SearchSmoCriteria, Organisation> _intergPager;

    //если в параметрах строки запроса type = "mo", то oid = Oid.Mo или пусто если вызываем форму с гридом смо
    private string _oid;

    protected void Page_Init(object sender, EventArgs e)
    {
      regulatoryService = ObjectFactory.GetInstance<IRegulatoryService>();
      _securityService = ObjectFactory.GetInstance<ISecurityService>();
      searchByNameControl.Clear += searchByNameControl_Clear;
      searchByNameControl.Search += searchByNameControl_Search;
    }

    void searchByNameControl_Search()
    {
      custPager.ResetCurrentPage();

      SearchSmoCriteria newCriteria = new SearchSmoCriteria();
      newCriteria.ShortName = searchByNameControl.NameValue;
      newCriteria.Oid = _oid;
      _intergPager.SetNewCriteria(newCriteria);
      _intergPager.RefreshData();
      contentUpdatePanel.Update();
      //без этой строки при первом отображении списка смо, если набрать в поиске буквы и нажать ввод, 
      //то при попытке выбрать запись в гриде она не выбирается, пока не клыпнешь либо по другой колонке грида либо по другому контролу
      searchByNameControl.SetFocus();
    }

    void searchByNameControl_Clear()
    {
      SearchSmoCriteria newCriteria = new SearchSmoCriteria();
      _intergPager.SetNewCriteria(newCriteria);
      _intergPager.RefreshData();
      contentUpdatePanel.Update();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
      if (Request.QueryString["type"] != null && Request.QueryString["type"] == "mo")
      {
        _oid = Oid.Mo;
        lbTitle.Text = "Медицинские организации";
      }
      else
      {
        _oid = Oid.Smo;
      }

      _intergPager = new IntegrationPager<SearchSmoCriteria, Organisation>(
          smosGridView, custPager, ViewState, (criteria) => { criteria.Oid = _oid; return regulatoryService.GetSmos(criteria); });
      _intergPager.AfterRefreshData += () =>
        {
          menu1.Enabled = _securityService.GetIsUserAllowPermission(_securityService.GetCurrentUser().Id, PermissionCode.EditSmos.GetHashCode());
          SetButtonsEnable(smosGridView.SelectedDataKey != null);
        };
      if (!IsPostBack)
      {
        searchByNameControl.NameTitle = "Краткое название";
        SetButtonsEnable(false);
        _intergPager.LoadPage();
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
      string prefix = "mo";
      if (_oid == Oid.Smo)
      {
        prefix = "smo";
      }
      switch (e.Item.Value)
      {
        //case "Add":
        //  Response.Redirect(string.Format("~/Pages/Administrations/SmoDetailEx.aspx?type={0}", prefix));
        //  break;
        case "Open":
          Open();
          break;
        //case "Delete":
        //  if (smosGridView.SelectedDataKey == null)
        //  {
        //    return;
        //  }
        //  regulatoryService.DeleteSmo((Guid)smosGridView.SelectedDataKey.Value);
        //  smosGridView.DeleteRow(smosGridView.SelectedIndex);
        //  smosGridView.SelectedIndex = -1;
        //  _intergPager.RefreshData(_intergPager.CurrentCriteria);
        //  contentUpdatePanel.Update();
        //  SetButtonsEnable(smosGridView.SelectedDataKey != null);
        //  break;
      }
    }

    private void Open()
    {
      string prefix = "mo";
      if (_oid == Oid.Smo)
      {
        prefix = "smo";
      }
      if (smosGridView.SelectedDataKey == null)
      {
        return;
      }
      Response.Redirect(string.Format("~/Pages/Administrations/SmoDetailEx.aspx?SmoId={0}&type={1}", smosGridView.SelectedDataKey.Value.ToString(), prefix));
    }

    protected void smoGridView_Deleting(Object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void smosGridView_Sorting(object sender, GridViewSortEventArgs e)
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

    protected void smosGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
      SetButtonsEnable(smosGridView.SelectedDataKey != null);
    }

    private void SetButtonsEnable(bool value)
    {
      UtilsHelper.SetMenuButtonsEnable(value, MenuUpdatePanel, menu1);
    }

    protected void menu1_PreRender(object sender, EventArgs e)
    {
      UtilsHelper.MenuPreRender(menu1);
    }

    public void RenderInPage()
    {
      UtilsHelper.AddAttributesToGridRow(Page.ClientScript, smosGridView);
      UtilsHelper.AddDoubleClickAttributeToGrid(Page.ClientScript, smosGridView);
    }

    protected void smosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      switch (e.CommandName)
      {
        case "DoubleClick":
          smosGridView.SelectedIndex = int.Parse(e.CommandArgument.ToString());
          Open();
          break;
      }
    }
  }
}