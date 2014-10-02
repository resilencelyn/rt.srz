using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Controls.CustomPager
{
  using rt.core.model.dto;

  public delegate SearchResult<SearchResultItemType> GetData<SearchCriteriaType, SearchResultItemType>(SearchCriteriaType criteria);

  public class IntegrationPager<SearchCriteriaType, SearchResultItemType> where SearchCriteriaType : BaseSearchCriteria, new()
  {
    private readonly GridView _grid;
    private readonly Pager _pager;
    private readonly StateBag _viewState;
    private readonly GetData<SearchCriteriaType, SearchResultItemType> _getData;
    private readonly string _searchName;
    public event Action AfterRefreshData;

    public IntegrationPager(GridView grid, Pager pager, StateBag viewState, GetData<SearchCriteriaType, SearchResultItemType> getData)
    {
      _grid = grid;
      _pager = pager;
      _viewState = viewState;
      _getData = getData;
      _searchName = typeof(SearchCriteriaType).Name;
    }

    public SearchCriteriaType CurrentCriteria
    {
      get { return (SearchCriteriaType)_viewState[_searchName]; }
    }

    public int CurrentPageIndex
    {
      get { return _pager.CurrentPageIndex; }
      set { _pager.CurrentPageIndex = value; }
    }

    public void SetNewCriteria(SearchCriteriaType newCriteria)
    {
			_viewState[_searchName] = newCriteria;
    }

    public void DoPagerPageIndexChange(CustomPageChangeArgs e)
    {
      _grid.PageSize = e.CurrentPageSize;
      RefreshData((SearchCriteriaType)_viewState[_searchName]);
    }

    public void DoPagerPageSizeChange(CustomPageChangeArgs e)
    {
      _grid.PageSize = e.CurrentPageSize;
      _pager.CurrentPageIndex = 0;
      RefreshData((SearchCriteriaType)_viewState[_searchName]);
    }

    public void LoadPage(int initialCountPerPage)
    {
      _grid.PageIndex = 0;
      _grid.PageSize = initialCountPerPage;
      _pager.SetPageSize(initialCountPerPage);

      SearchCriteriaType newCriteria = new SearchCriteriaType();
      _viewState[_searchName] = newCriteria;
      RefreshData(newCriteria);
    }

    public void LoadPage()
    {
      LoadPage(10);
    }

    public void RefreshData()
    {
      if (_viewState[_searchName] == null)
      {
        SearchCriteriaType newCriteria = new SearchCriteriaType();
        _viewState[_searchName] = newCriteria;
      }
      RefreshData((SearchCriteriaType)_viewState[_searchName]);
      _pager.ReloadPager();
    }

    public void RefreshData(SearchCriteriaType criteria)
    {
      // поиск по базе
      criteria.Skip = _pager.CurrentPageIndex * _grid.PageSize;
      criteria.Take = _grid.PageSize;

      SearchResult<SearchResultItemType> result = _getData(criteria);

      IList<SearchResultItemType> list = result.Rows;

      _grid.DataSource = list;
      _grid.DataBind();
      _pager.Visible = true;
      _pager.TotalPages = result.Total % _grid.PageSize == 0
                               ? result.Total / _grid.PageSize
                               : (result.Total / _grid.PageSize) + 1;

      if (_grid.SelectedIndex >= 0)
      {
        _grid.SelectedIndex = -1;
      }

      if (AfterRefreshData != null)
      {
        AfterRefreshData();
      }
    }

  }
}