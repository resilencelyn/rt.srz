using rt.srz.model.srz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Controls.Twins
{
  public partial class SeparateGridControl : System.Web.UI.UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetData(IList<Statement> statements)
    {
      grid.DataSource = statements;
      grid.DataBind();
    }

    public List<Guid> GetSelected()
    {
      var result = new List<Guid>();
      foreach(GridViewRow row in grid.Rows)
      {
        CheckBox cbCheck = row.FindControl("cbCheck") as CheckBox;
        if (cbCheck.Checked)
        {
          result.Add((Guid)grid.DataKeys[row.RowIndex].Value);
        }
      }
      return result;
    }
  }
}