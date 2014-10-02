//-------------------------------------------------------------------------------------
// <copyright file="Period.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

using rt.srz.model.srz.concepts;

namespace rt.srz.model.srz
{
  using System.Xml.Serialization;

  /// <summary>
  /// The Period.
  /// </summary>
  public partial class Period
  {
    [XmlIgnore]
    public virtual string Description
    {
      get
      {
        switch (Code.Id)
        {
          case PeriodCode.PeriodCode21:
            return string.Format("1-�� ������� {0} ����", Year.Year);
          case PeriodCode.PeriodCode22:
            return string.Format("2-�� ������� {0} ����", Year.Year);
          case PeriodCode.PeriodCode23:
            return string.Format("3-�� ������� {0} ����", Year.Year);
          case PeriodCode.PeriodCode24:
            return string.Format("4-�� ������� {0} ����", Year.Year);

          case PeriodCode.PeriodCode1:
            return "������";
          case PeriodCode.PeriodCode2:
            return "�������";
          case PeriodCode.PeriodCode3:
            return "����";
          case PeriodCode.PeriodCode4:
            return "������";
          case PeriodCode.PeriodCode5:
            return "���";
          case PeriodCode.PeriodCode6:
            return "����";
          case PeriodCode.PeriodCode7:
            return "����";
          case PeriodCode.PeriodCode8:
            return "������";
          case PeriodCode.PeriodCode9:
            return "��������";
          case PeriodCode.PeriodCode10:
            return "�������";
          case PeriodCode.PeriodCode11:
            return "������";
          case PeriodCode.PeriodCode12:
            return "�������";
          default: return null;
        }
      }
    }
  }
}