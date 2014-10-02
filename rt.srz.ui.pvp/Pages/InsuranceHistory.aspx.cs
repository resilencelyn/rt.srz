using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Controls.Twins;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages
{
  public partial class InsuranceHistory : System.Web.UI.Page
  {
    private IStatementService _statementService;

    protected void Page_Init(object sender, EventArgs e)
    {
      _statementService = ObjectFactory.GetInstance<IStatementService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        Guid statementId = (Guid)Session[SessionConsts.CGuidStatementId];
        Statement statement = _statementService.GetStatement(statementId);
        person1.SetData(new InsuranceHistoryItem(statement));
        person1.HideJoinButton();

        var stats = _statementService.GetAllByInsuredId(statement.InsuredPerson.Id);
        grid.DataSource = stats;
        grid.DataBind();
        grid.SelectedIndex = 0;
      }
    }

    protected void grid_SelectedIndexChanged(object sender, EventArgs e)
    {
      Guid statementId = (Guid)grid.SelectedDataKey.Value;
      Statement statement = _statementService.GetStatement(statementId);
      person1.SetData(new InsuranceHistoryItem(statement));
      gridPanel.Update();
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

    //protected void pdpsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //  if (e.Row.RowType != DataControlRowType.DataRow)
    //  {
    //    return;
    //  }
    //  e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
    //  e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
    //  e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(pdpsGridView, "Select$" + e.Row.RowIndex);
    //}



  }
}