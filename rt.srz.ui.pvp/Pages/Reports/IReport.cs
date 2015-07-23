// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReport.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Pages.Reports
{
  using rt.core.model.core;
  using rt.srz.model.srz;

  using User = rt.core.model.core.User;

  /// <summary>
  /// The Report interface.
  /// </summary>
  public interface IReport
  {
    #region Public Methods and Operators

    /// <summary>
    /// Заполняет данные в отчете
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="currentUser">
    /// The current User.
    /// </param>
    void FillReportData(Statement statement, User currentUser);

    #endregion
  }
}