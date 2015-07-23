// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Period.cs" company="јль€нс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The Period.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System.Xml.Serialization;

  using rt.srz.model.srz.concepts;

  /// <summary>
  ///   The Period.
  /// </summary>
  public partial class Period
  {
    #region Public Properties

    /// <summary>
    /// Gets the description.
    /// </summary>
    [XmlIgnore]
    public virtual string Description
    {
      get
      {
        switch (Code.Id)
        {
          case PeriodCode.PeriodCode21:
            return string.Format("1-ый квартал {0} года", Year.Year);
          case PeriodCode.PeriodCode22:
            return string.Format("2-ой квартал {0} года", Year.Year);
          case PeriodCode.PeriodCode23:
            return string.Format("3-ий квартал {0} года", Year.Year);
          case PeriodCode.PeriodCode24:
            return string.Format("4-ый квартал {0} года", Year.Year);

          case PeriodCode.PeriodCode1:
            return "€нварь";
          case PeriodCode.PeriodCode2:
            return "февраль";
          case PeriodCode.PeriodCode3:
            return "март";
          case PeriodCode.PeriodCode4:
            return "апрель";
          case PeriodCode.PeriodCode5:
            return "май";
          case PeriodCode.PeriodCode6:
            return "июнь";
          case PeriodCode.PeriodCode7:
            return "июль";
          case PeriodCode.PeriodCode8:
            return "август";
          case PeriodCode.PeriodCode9:
            return "сент€брь";
          case PeriodCode.PeriodCode10:
            return "окт€брь";
          case PeriodCode.PeriodCode11:
            return "но€брь";
          case PeriodCode.PeriodCode12:
            return "декабрь";
          default:
            return null;
        }
      }
    }

    #endregion
  }
}