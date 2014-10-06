// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Resurrect.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Воскресший
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.zags
{
  using rt.srz.model.HL7.person.target;

  /// <summary>
  ///   Воскресший
  /// </summary>
  public class Resurrect
  {
    #region Public Properties

    /// <summary>
    ///   Информация о умершем
    /// </summary>
    public MessagePid PidList { get; set; }

    #endregion
  }
}