﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportBatchType.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The export batch type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.exchange.export
{
  /// <summary>
  ///   The export batch type.
  /// </summary>
  public enum ExportBatchType
  {
    /// <summary>
    ///   The pfr.
    /// </summary>
    Pfr = 1, 

    /// <summary>
    ///   The SmoRec.
    /// </summary>
    SmoRec = 2, 

    /// <summary>
    ///   The SmoOp.
    /// </summary>
    SmoOp = 3, 

    /// <summary>
    ///   The SmoRep.
    /// </summary>
    SmoRep = 4, 

    /// <summary>
    ///   The SmoFlk.
    /// </summary>
    SmoFlk = 4, 
  }
}