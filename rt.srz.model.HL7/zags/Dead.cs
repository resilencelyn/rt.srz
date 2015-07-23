// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dead.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Умерший
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.zags
{
  using rt.srz.model.Hl7.person.target;

  /// <summary>
  ///   Умерший
  /// </summary>
  public class Dead
  {
    #region Public Properties

    /// <summary>
    ///   Информация о умершем
    /// </summary>
    public MessagePid PidList { get; set; }

    #endregion
  }
}