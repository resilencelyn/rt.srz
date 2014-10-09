// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Twins.aspx.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Pages.Twins
{
  using System;
  using System.Web.UI;
  using System.Web.UI.WebControls;

  using rt.core.model.interfaces;
  using rt.srz.model.dto;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;
  using rt.srz.ui.pvp.Controls.CustomPager;
  using rt.srz.ui.pvp.Controls.Twins;

  using StructureMap;

  /// <summary>
  /// The twins.
  /// </summary>
  public partial class Twins : Page
  {
    #region Fields

    /// <summary>
    /// The _interg pager.
    /// </summary>
    private IntegrationPager<SearchTwinCriteria, Twin> intergPager;

    /// <summary>
    /// The securityService.
    /// </summary>
    private ISecurityService securityService;

    /// <summary>
    /// The _service.
    /// </summary>
    private ITfomsService service;

    #endregion

    #region Methods

    /// <summary>
    /// The page_ init.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Init(object sender, EventArgs e)
    {
      service = ObjectFactory.GetInstance<ITfomsService>();
      securityService = ObjectFactory.GetInstance<ISecurityService>();
    }

    /// <summary>
    /// The page_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Load(object sender, EventArgs e)
    {
      intergPager = new IntegrationPager<SearchTwinCriteria, Twin>(
        grid, 
        custPager, 
        ViewState, 
        criteria => service.GetTwins(criteria));

      twinPerson1.AfterJoinTwins += DoAfterJoinTwins;
      twinPerson2.AfterJoinTwins += DoAfterJoinTwins;

      if (!IsPostBack)
      {
        var pdp = securityService.GetCurrentUser().PointDistributionPolicy();
        ViewState["fomId"] = pdp != null ? pdp.Parent.Parent.Id : Guid.Empty;

        var searchTypes = service.GetSearchKeyTypesByTFoms();
        foreach (var type in searchTypes)
        {
          ddlKeyTypes.Items.Add(new ListItem(type.Name, type.Id.ToString()));
        }

        intergPager.LoadPage();
      }
    }

    /// <summary>
    /// The render.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      foreach (GridViewRow r in grid.Rows)
      {
        if (r.RowType == DataControlRowType.DataRow)
        {
          r.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
          r.Attributes["onmouseout"] = "this.style.textDecoration='none';";
          r.ToolTip = @"Click to select row";
          r.Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(grid, "Select$" + r.RowIndex, true);
        }
      }

      base.Render(writer);
    }

    /// <summary>
    /// The btn delete twins_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void BtnDeleteTwinsClick(object sender, EventArgs e)
    {
      var keyId = Guid.Parse(ddlKeyTypes.SelectedValue);
      service.DeleteTwinsCalculatedOnlyByGivenKey(keyId);
      ReloadData();
    }

    /// <summary>
    /// The btn delete_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void BtnDeleteClick(object sender, EventArgs e)
    {
      if (grid.SelectedDataKey != null)
      {
        service.RemoveTwin((Guid)grid.SelectedDataKey.Value);
        ReloadData();
      }
    }

    /// <summary>
    /// The cust pager_ page index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void CustPagerPageIndexChanged(object sender, CustomPageChangeArgs e)
    {
      intergPager.DoPagerPageIndexChange(e);
      gridPanel.Update();
    }

    /// <summary>
    /// The cust pager_ page size changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void CustPagerPageSizeChanged(object sender, CustomPageChangeArgs e)
    {
      intergPager.DoPagerPageSizeChange(e);
      gridPanel.Update();
    }

    /// <summary>
    /// The ddl key types_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void DdlKeyTypesSelectedIndexChanged(object sender, EventArgs e)
    {
      var criter = intergPager.CurrentCriteria;
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
        var key = service.GetSearchKeyType((Guid)criter.KeyId);
        var fomId = (Guid)ViewState["fomId"];
        if (key != null && key.Tfoms != null && key.Tfoms.Id == fomId)
        {
          btnDeleteTwins.Enabled = true;
        }
      }

      ddlPanel.Update();
      ReloadData();
    }

    /// <summary>
    /// The grid_ deleting.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void GridDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    /// <summary>
    /// The grid_ selected index changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void GridSelectedIndexChanged(object sender, EventArgs e)
    {
      var twinId = (Guid)grid.SelectedDataKey.Value;
      var id1 = Guid.Parse(((Label)grid.Rows[grid.SelectedIndex].Cells[1].FindControl("lb1")).Text);
      var id2 = Guid.Parse(((Label)grid.Rows[grid.SelectedIndex].Cells[2].FindControl("lb2")).Text);

      // если не нашли заявления активного для одной из персон, то аннулируем дубликат и убираем его из списка в гриде
      if (twinPerson1.SetData(new TwinItem(twinId, 1, id1, id2)) == TwinPersonControl.ResultSet.NotFoundStatement)
      {
        service.AnnulateTwin(twinId);
        ReloadData();
        if (grid.Rows.Count > 0)
        {
          grid.SelectedIndex = 0;
          GridSelectedIndexChanged(grid, EventArgs.Empty);
        }

        return;
      }

      twinPerson2.SetData(new TwinItem(twinId, 2, id1, id2));

      var twin = service.GetTwin(twinId);
      tbTwinKeys.Text = twin.TwinKeyAsText;

      dataPanel.Update();
    }

    /// <summary>
    /// The do after join twins.
    /// </summary>
    private void DoAfterJoinTwins()
    {
      ReloadData();
      if (grid.Rows.Count > 0)
      {
        grid.SelectedIndex = 0;
        GridSelectedIndexChanged(grid, EventArgs.Empty);
      }
    }

    /// <summary>
    /// The reload data.
    /// </summary>
    private void ReloadData()
    {
      intergPager.RefreshData();
      gridPanel.Update();

      twinPerson1.Clear();
      twinPerson2.Clear();
      tbTwinKeys.Text = string.Empty;

      dataPanel.Update();
    }

    #endregion
  }
}