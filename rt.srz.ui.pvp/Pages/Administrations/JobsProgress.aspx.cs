using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using Quartz;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages.Administration
{
  public partial class JobsProgress : System.Web.UI.Page
  {
    private void LoadControlsData()
    {
      ISchedulerFactory schedulerFactory = ObjectFactory.GetInstance<ISchedulerFactory>();
      var scheduler = schedulerFactory.GetScheduler();
      var jobs = scheduler.GetCurrentlyExecutingJobs();

      List<JobData> jobList = new List<JobData>();
      jobList.AddRange(jobs.Select(x => new JobData()
      {
        GroupName = x.Trigger.Key.Name,
        Name = x.JobDetail.Key.ToString(),
        Datails = (x.JobDetail.JobDataMap["datails"] ?? string.Empty).ToString(),
        Position = x.JobDetail.JobDataMap["progress"] != null ?
          (int)x.JobDetail.JobDataMap["progress"] : 0
      }));

      if (jobList.Count == 0)
      {
        lbNoDataText.Visible = true;
        griddiv.Visible = false;
      }
      else
      {
        lbNoDataText.Visible = false;
        griddiv.Visible = true;
      }

      GridGroupHelper<JobData> helper = new GridGroupHelper<JobData>(x => x.GroupName);
      grid.DataSource = helper.GetDataList(grid, x => x.RecordNumber, jobList);
      grid.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      LoadControlsData();
    }

    protected void dxgrid_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
    {
      UtilsHelper.CustomUnboundColumnData(sender, e);
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
    }

    public class JobData
    {
      public string GroupName { get; set; }
      public string Name { get; set; }
      public string Datails { get; set; }
      public int Position { get; set; }
      public int RecordNumber { get; set; }
    }

    protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        HiddenField progressValueField = (HiddenField)e.Row.FindControl("progressValue");
        HtmlContainerControl progressDiv = (HtmlContainerControl)e.Row.FindControl("progressdiv");
        if (progressValueField.Value == string.Empty)
        {
          progressDiv.Style.Value = "width:1px;height:20px;background-color:lightgreen";
        }
        else
        {
          progressDiv.Style.Value = string.Format("width:{0}%;height:20px;background-color:lightgreen", progressValueField.Value);
        }
      }
    }
  }
}