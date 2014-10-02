using rt.atl.model.dto;
using rt.atl.model.interfaces.Service;
using rt.srz.ui.pvp.Controls.CustomPager;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages.Administrations
{
  public partial class ErrorSynchronizationView : System.Web.UI.Page
  {
    private IAtlService _service;
    private IntegrationPager<SearchErrorSinchronizationCriteria, ErrorSinchronizationInfoResult> _intergPager;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<IAtlService>();
      searchByDatesControl.Clear += searchByDatesControl_Clear;
      searchByDatesControl.Search += searchByDatesControl_Search;
    }

    void searchByDatesControl_Search()
    {
      custPager.ResetCurrentPage();

      SearchErrorSinchronizationCriteria newCriteria = new SearchErrorSinchronizationCriteria();
      newCriteria.DateFrom = searchByDatesControl.DateFrom;
      newCriteria.DateTo = searchByDatesControl.DateTo;
      _intergPager.SetNewCriteria(newCriteria);
      _intergPager.RefreshData();
      gridPanel.Update();
      //без этой строки при первом отображении списка смо, если набрать в поиске буквы и нажать ввод, 
      //то при попытке выбрать запись в гриде она не выбирается, пока не клыпнешь либо по другой колонке грида либо по другому контролу
      searchByDatesControl.SetFocus();
    }

    void searchByDatesControl_Clear()
    {
      SearchErrorSinchronizationCriteria newCriteria = new SearchErrorSinchronizationCriteria();
      _intergPager.SetNewCriteria(newCriteria);
      _intergPager.RefreshData();
      gridPanel.Update();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        searchByDatesControl.DateTo = DateTime.Now.Date;
        searchByDatesControl.DateFrom = DateTime.Now.Date.AddDays(-14);
        _intergPager = new IntegrationPager<SearchErrorSinchronizationCriteria, ErrorSinchronizationInfoResult>(
            grid, custPager, ViewState, (criteria) =>
              {
                criteria.DateTo = searchByDatesControl.DateTo;
                criteria.DateFrom = searchByDatesControl.DateFrom;
                return _service.GetErrorSinchronizationInfoList(criteria); 
              });

        _intergPager.LoadPage();
      }
      else
      {
        _intergPager = new IntegrationPager<SearchErrorSinchronizationCriteria, ErrorSinchronizationInfoResult>(
            grid, custPager, ViewState, (criteria) => _service.GetErrorSinchronizationInfoList(criteria));
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

    protected override void Render(HtmlTextWriter writer)
    {
      foreach (GridViewRow r in grid.Rows)
      {
        if (r.RowType == DataControlRowType.DataRow)
        {
          r.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
          r.Attributes["onmouseout"] = "this.style.textDecoration='none';";
          r.ToolTip = "Click to select row";
          r.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(grid, "Select$" + r.RowIndex, true);
        }
      }
      base.Render(writer);
    }

  }
}