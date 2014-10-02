using rt.srz.model.interfaces.service;
using rt.srz.ui.pvp.Enumerations;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages
{
  public partial class Concepts : System.Web.UI.Page
  {
    private INsiService _service;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<INsiService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        grid.DataSource = _service.GetOids();
        grid.DataBind();
      }
    }

    protected override void Render(HtmlTextWriter writer)
    {
      UtilsHelper.AddAttributesToGridRow(Page.ClientScript, grid);
      base.Render(writer);
    }

    protected void grid_SelectedIndexChanged(object sender, EventArgs e)
    {
      gridConcept.DataSource = _service.GetConceptsByOid(grid.SelectedRow.Cells[1].Text);
      gridConcept.DataBind();
    }

  }
}