// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobsSettings.aspx.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Pages.Administrations
{
  using System;
  using System.Web.UI;

  using rt.srz.ui.pvp.Enumerations;

  /// <summary>
  /// The jobs settings.
  /// </summary>
  public partial class JobsSettings : Page
  {
    #region Methods

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
      // if (!IsPostBack)
      // tbMaxJobCount.Text = FirstLoadingToPvpJob.MaxCountJob.ToString();
    }

    /// <summary>
    /// The btn cancel_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      RedirectUtils.RedirectToMain(Response);
    }

    /// <summary>
    /// The btn save statement_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void btnSaveStatement_Click(object sender, EventArgs e)
    {
      // int maxJobCount = 0;
      // if (int.TryParse(tbMaxJobCount.Text, out maxJobCount))
      // {
      // FirstLoadingToPvpJob.MaxCountJob = maxJobCount;
      // RedirectUtils.RedirectToMain(Response);
      // }
      // else
      // {
      // tbMaxJobCount.Text = FirstLoadingToPvpJob.MaxCountJob.ToString();
      // }
    }

    #endregion
  }
}