// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Installation.ascx.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controls
{
  #region

  using System;
  using System.Web.UI;

  using rt.core.model;

  using StructureMap;

  using rt.atl.model.interfaces.Service;
  using rt.srz.ui.pvp.Enumerations;

  #endregion

  /// <summary>
  ///   The statements search.
  /// </summary>
  public partial class Installation : UserControl
  {
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
      if (!IsPostBack)
      {
      }
    }

    /// <summary>
    /// The btn complete the installation click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void BtnCompleteTheInstallationClick(object sender, EventArgs e)
    {
      // помечаем вс записи в przbuf как синхронизированными
      ObjectFactory.GetInstance<IAtlService>().FlagExportedPrzBuff();

      // Помечаем в конфиге, что устновка завершена
      SiteMode.CompleteTheInstallation();
      RedirectUtils.RedirectToMain(Response);
    }

    /// <summary>
    /// The btn load atlantika click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void BtnLoadAtlantikaClick(object sender, EventArgs e)
    {
      //FirstLoadingToPvpJobPool.Instance.CreateQueue();
    }

    protected void BtnStatisticClick(object sender, EventArgs e)
    {
      RedirectUtils.RedirectToStatisticInitialLoading(Response);
    }

    #endregion
  }
}