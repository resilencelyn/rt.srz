using rt.srz.model.dto;
using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.ui.pvp.Controls.CustomPager;
using rt.srz.ui.pvp.Controls.Twins;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rt.srz.ui.pvp.Pages.Twins
{
  public partial class Twins : System.Web.UI.Page
  {
    private ITFService _service;
    private ISecurityService _sec;

    private IntegrationPager<SearchTwinCriteria, Twin> _intergPager;

    protected void Page_Init(object sender, EventArgs e)
    {
      _service = ObjectFactory.GetInstance<ITFService>();
      _sec = ObjectFactory.GetInstance<ISecurityService>();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      _intergPager = new IntegrationPager<SearchTwinCriteria, Twin>(
          grid, custPager, ViewState, (criteria) => _service.GetTwins(criteria));

      twinPerson1.AfterJoinTwins += DoAfterJoinTwins;
      twinPerson2.AfterJoinTwins += DoAfterJoinTwins;

      if (!IsPostBack)
      {
        var pdp = _sec.GetCurrentUser().PointDistributionPolicy;
        ViewState["fomId"] = pdp != null ? pdp.Parent.Parent.Id : Guid.Empty;

        var searchTypes = _service.GetSearchKeyTypesByTFoms();
        foreach(var type in searchTypes)
        {
          ddlKeyTypes.Items.Add(new ListItem(type.Name, type.Id.ToString()));
        }
        _intergPager.LoadPage();
      }
    }

    private void DoAfterJoinTwins()
    {
      ReloadData();
      if (grid.Rows.Count > 0)
      {
        grid.SelectedIndex = 0;
        grid_SelectedIndexChanged(grid, EventArgs.Empty);
      }
    }

    protected void grid_SelectedIndexChanged(object sender, EventArgs e)
    {
      Guid twinId = (Guid)grid.SelectedDataKey.Value;
      Guid Id1 = Guid.Parse(((Label)grid.Rows[grid.SelectedIndex].Cells[1].FindControl("lb1")).Text);
      Guid Id2 = Guid.Parse(((Label)grid.Rows[grid.SelectedIndex].Cells[2].FindControl("lb2")).Text);

      //если не нашли заявления активного для одной из персон, то аннулируем дубликат и убираем его из списка в гриде
      if (twinPerson1.SetData(new TwinItem(twinId, 1, Id1, Id2)) == TwinPersonControl.ResultSet.NotFoundStatement)
      {
        _service.AnnulateTwin(twinId);
        ReloadData();
        if (grid.Rows.Count > 0)
        {
          grid.SelectedIndex = 0;
          grid_SelectedIndexChanged(grid, EventArgs.Empty);
        }
        return;
      }

      twinPerson2.SetData(new TwinItem(twinId, 2, Id1, Id2));

      Twin twin = _service.GetTwin(twinId);
      tbTwinKeys.Text = twin.TwinKeyAsText;

      dataPanel.Update();
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

    protected void grid_Deleting(Object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
      if (grid.SelectedDataKey != null)
      {
        _service.RemoveTwin((Guid)grid.SelectedDataKey.Value);
        ReloadData();
      }
    }

    private void ReloadData()
    {
      _intergPager.RefreshData();
      gridPanel.Update();

      twinPerson1.Clear();
      twinPerson2.Clear();
      tbTwinKeys.Text = "";

      dataPanel.Update();
    }

    protected void custPager_PageIndexChanged(object sender, CustomPageChangeArgs e)
    {
      _intergPager.DoPagerPageIndexChange(e);
      gridPanel.Update();
    }

    protected void custPager_PageSizeChanged(object sender, CustomPageChangeArgs e)
    {
      _intergPager.DoPagerPageSizeChange(e);
      gridPanel.Update();
    }

    protected void ddlKeyTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
      var criter = _intergPager.CurrentCriteria;
      btnDeleteTwins.Enabled = false;
      if (ddlKeyTypes.SelectedValue == "All")
      {
        criter.KeyType = TwinKeyType.All;
      }
      else if (ddlKeyTypes.SelectedValue == "Standard")
      {
        criter.KeyType = TwinKeyType.Standard;
      }
      else
      {
        criter.KeyType = TwinKeyType.NonStandard;
        criter.KeyId = Guid.Parse(ddlKeyTypes.SelectedValue);
        var key = _service.GetSearchKeyType((Guid)criter.KeyId);
        Guid fomId = (Guid)ViewState["fomId"];
        if (key != null && key.Tfoms != null && key.Tfoms.Id == fomId)
        {
          btnDeleteTwins.Enabled = true;
        }
      }
      ddlPanel.Update();
      ReloadData();
    }

    protected void btnDeleteTwins_Click(object sender, EventArgs e)
    {
      Guid keyId = Guid.Parse(ddlKeyTypes.SelectedValue);
      _service.DeleteTwinsCalculatedOnlyByGivenKey(keyId);
      ReloadData();
    }

  }
}