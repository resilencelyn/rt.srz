using rt.srz.business.manager;
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
  public partial class PfrStatistic : System.Web.UI.Page
  {
    private ITFService _service;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<ITFService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        grid.DataSource = _service.GetPfrBatchesByUser();
        grid.DataBind();

        gridP.DataSource = _service.GetPfrPeriods();
        gridP.DataBind();
      }
    }

    protected override void Render(HtmlTextWriter writer)
    {
      AddAttributesToGridRow(grid);
      AddAttributesToGridRow(gridP);
      base.Render(writer);
    }

    private void AddAttributesToGridRow(GridView grid)
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
    }

    protected void grid_SelectedIndexChanged(object sender, EventArgs e)
    {
      Guid batchId = (Guid)grid.SelectedDataKey.Value;
      var pfrInfo = _service.GetPfrStatisticInfoByBatch(batchId);
      string batchDescription = ((Label)grid.Rows[grid.SelectedIndex].Cells[1].FindControl("lb1")).Text;
      lbStatisticBy.Text = string.Format("батчу - {0}", batchDescription);
      FillStatisticInfo(pfrInfo);
    }

    protected void gridP_SelectedIndexChanged(object sender, EventArgs e)
    {
      Guid periodId = (Guid)gridP.SelectedDataKey.Value;
      var pfrInfo = _service.GetPfrStatisticInfoByPeriod(periodId);
      string periodDescription = ((Label)gridP.Rows[gridP.SelectedIndex].Cells[1].FindControl("lb1")).Text;
      lbStatisticBy.Text = string.Format("периоду - {0}", periodDescription);
      FillStatisticInfo(pfrInfo);
    }

    private void FillStatisticInfo(PfrStatisticInfo info)
    {
      tbNotFoundRecordCount.Text = info.NotFoundRecordCount.ToString();
      tbTotalRecordCount.Text = info.TotalRecordCount.ToString();
      tbInsuredRecordCount.Text = info.InsuredRecordCount.ToString();
      tbEmployedRecordCount.Text = info.EmployedRecordCount.ToString();
      tbFoundByDataRecordCount.Text = info.FoundByDataRecordCount.ToString();
      tbFoundBySnilsRecordCount.Text = info.FoundBySnilsRecordCount.ToString();
    }
  }
}