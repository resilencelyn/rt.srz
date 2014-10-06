using rt.atl.model.interfaces.Service;
using rt.srz.model.dto;
using rt.srz.model.interfaces.service;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages
{
  public partial class StatisticInitialLoading : System.Web.UI.Page
  {
    private IAtlService _service;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<IAtlService>();
    }

    private void RefreshData()
    {
      grid.DataSource = _service.GetStatisticInitialLoading();
      grid.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        //RefreshData();
      }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
      RefreshData();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
      RefreshData();
    }

  }
}