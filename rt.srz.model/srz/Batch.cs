// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Batch.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The Batch.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System.Xml.Serialization;

  using rt.core.model.interfaces;

  using CodeConfirmEn = rt.srz.model.srz.concepts.CodeConfirm;

  /// <summary>
  ///   The Batch.
  /// </summary>
  public partial class Batch : IBatch
  {
    #region Public Properties

    /// <summary>
    /// Gets the status description.
    /// </summary>
    [XmlIgnore]
    public virtual string StatusDescription
    {
      get
      {
        if (CodeConfirm == null)
        {
          return null;
        }

        switch (CodeConfirm.Id)
        {
          case CodeConfirmEn.AA:
            return "Обработан";
          case CodeConfirmEn.AE:
            return "Ошибка обработки";
          default:
            return null;
        }
      }
    }

    #endregion
  }
}