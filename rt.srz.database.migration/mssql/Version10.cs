// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizeConcept.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Синхронизация таблицы Concept
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.mssql
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Globalization;
  using System.Linq;
  using System.Web;
  using System.Xml.Linq;

  using ECM7.Migrator.Framework;

  using NLog;

  /// <summary>
  ///   Синхронизация таблицы Concept
  /// </summary>
  [Migration(10)]
  public class Version10 : Migration
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The apply.
    /// </summary>
    public override void Apply()
    {

    }

    #endregion

    #region Methods

    #endregion
  }
}