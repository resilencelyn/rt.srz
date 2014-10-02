using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rt.srz.model.HL7.enumerations
{
  /// <summary>
  /// Категория документа, выгруженная для СМО(Документ УДЛ, Документ подтверждающий регистрацию, Документ разрешающий проживание)
  /// </summary>
  public enum DocumentCategory
  {
    Unknown = 0,
    
    /// <summary>
    /// Документ УДЛ
    /// </summary>
    Udl = 1,

    /// <summary>
    /// Документ подтверждающий регистрацию
    /// </summary>
    Registration = 2,

    /// <summary>
    /// Документ разрешающий проживание
    /// </summary>
    Residency = 3
  }
}
