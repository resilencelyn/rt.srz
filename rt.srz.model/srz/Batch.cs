//-------------------------------------------------------------------------------------
// <copyright file="Batch.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System.Xml.Serialization;

  using rt.core.business.interfaces.exchange;
  using CodeConfirmEn = rt.srz.model.srz.concepts.CodeConfirm;

  /// <summary>
  /// The Batch.
  /// </summary>
  public partial class Batch : IBatch
  {
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
          default: return null;
        }
      }
    }
  }
}