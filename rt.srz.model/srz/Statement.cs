// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Statement.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The Statement.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using rt.srz.model.srz.concepts;
using System.Text;

namespace rt.srz.model.srz
{
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  /// <summary>
  ///   The Statement.
  /// </summary>
  public partial class Statement
  {
    /// <summary>
    /// Gets or sets the not check polis number.
    /// </summary>
    [XmlElement(Order = 50)]
    [DataMember(Order = 50)]
    public virtual bool? NotCheckPolisNumber { get; set; }

    /// <summary>
    /// Gets the type statement id.
    /// </summary>
    [XmlElement(Order = 51)]
    [DataMember(Order = 51)]
    public virtual int TypeStatementId
    {
      get { return CauseFiling != null ? GetTypeStatementId(CauseFiling.Id) : -1; }
    }

    [XmlElement(Order = 52)]
    [DataMember(Order = 52)]
    public virtual string NumberTemporaryCertificate
    {
      get
      {
        var insurance = MedicalInsurances != null ? MedicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.�) : null;
        if (insurance != null)
        {
          return insurance.PolisNumber;
        }
        return null;
      }
    }

    [XmlElement(Order = 53)]
    [DataMember(Order = 53)]
    public virtual DateTime? DateIssueTemporaryCertificate
    {
      get
      {
        var insurance = MedicalInsurances != null ? MedicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.�) : null;
        if (insurance != null)
        {
          return insurance.DateFrom;
        }
        return null;
      }
    }

    [XmlElement(Order = 54)]
    [DataMember(Order = 54)]
    public virtual string NumberPolisCertificate
    {
      get
      {
        var insurance = MedicalInsurances != null ? MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.� && x.IsActive) : null;
        if (insurance != null)
        {
          return insurance.PolisNumber;
        }
        return null;
      }
    }

    [XmlElement(Order = 55)]
    [DataMember(Order = 55)]
    public virtual DateTime? DateIssuePolisCertificate
    {
      get
      {
        var insurance = MedicalInsurances != null ? MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.� && x.IsActive) : null;
        if (insurance != null)
        {
          return insurance.DateFrom;
        }
        return null;
      }
    }

    [XmlElement(Order = 56)]
    [DataMember(Order = 56)]
    public virtual MedicalInsurance TempMedicalInsurance
    {
      get
      {
        return MedicalInsurances != null ? MedicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.� && x.IsActive) : null;
      }
    }

    [XmlElement(Order = 57)]
    [DataMember(Order = 57)]
    public virtual MedicalInsurance PolisMedicalInsurance
    {
      get
      {
        return MedicalInsurances != null ? MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.� && x.IsActive) : null;
      }
    }


    /// <summary>
    ///   Gets or sets the prz buff.
    /// </summary>
    [XmlIgnore]
    public virtual object PrzBuff { get; set; }

    /// <summary>
    ///   Gets or sets the prz buff.
    /// </summary>
    [XmlIgnore]
    public virtual object PrzBuffPolis { get; set; }

    /// <summary>
    /// ����� �����������
    /// </summary>
    [XmlIgnore]
    public virtual string AddressRegistrationStr
    {
      get { return GetAddressInStr(Address); }
    }

    /// <summary>
    /// ����� ����������
    /// </summary>
    [XmlIgnore]
    public virtual string AddressLiveStr
    {
      get { return GetAddressInStr(Address2); }
    }

    private string GetAddressInStr(address addr)
    {
      if (addr == null)
      {
        return string.Empty;
      }
      return addr.ToString();
      //StringBuilder sb = new StringBuilder();
      //sb.Append(addr.Postcode).Append(" ").
      //  Append(addr.Subject).Append(" ").
      //  Append(addr.Area).Append(" ").
      //  Append(addr.City).Append(" ").
      //  Append(addr.Town).Append(" ").
      //  Append(addr.Street).Append(" ").
      //  Append(addr.House).Append(" ").
      //  Append(addr.Housing).Append(" ").
      //  Append(addr.Room);
      //return sb.ToString();
    }

    /// <summary>
    /// The get type statement id.
    /// </summary>
    /// <param name="�auseFilingId">
    /// The �ause filing id.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int GetTypeStatementId(int �auseFilingId)
    {
      switch (�auseFilingId)
      {
        case CauseReinsurance.Choice:
        case CauseReinsurance.ReinsuranceAtWill:
        case CauseReinsurance.ReinsuranceWithTheMove:
        case CauseReinsurance.ReinsuranceStopFinance:
          {
            return TypeStatement.TypeStatement1;
          }

        case CauseReneval.RenevalChangePersonDetails:
        case CauseReneval.RenevalInaccuracy:
        case CauseReneval.RenevalExpiration:
          {
            return TypeStatement.TypeStatement2;
          }

        case CauseReneval.RenevalUnusable:
        case CauseReneval.RenevalLoss:
          {
            return TypeStatement.TypeStatement3;
          }

        case CauseReneval.Edit:
          {
            return TypeStatement.TypeStatement4;
          }
      }

      return 292;
    }

    /// <summary>
    /// The get cause filling by type.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable"/>.
    /// </returns>
    public static IEnumerable<int> GetCauseFillingByType(int type)
    {
      switch (type)
      {
        case TypeStatement.TypeStatement1:
          {
            return new[] { CauseReinsurance.Choice, CauseReinsurance.ReinsuranceAtWill, CauseReinsurance.ReinsuranceWithTheMove, CauseReinsurance.ReinsuranceStopFinance };
          }

        case TypeStatement.TypeStatement2:
          return new[] { CauseReneval.RenevalChangePersonDetails, CauseReneval.RenevalInaccuracy, CauseReneval.RenevalExpiration };
        case TypeStatement.TypeStatement3:
          return new[] { CauseReneval.RenevalUnusable, CauseReneval.RenevalLoss };

        case TypeStatement.TypeStatement4:
          return new[] { CauseReneval.Edit };
      }

      return new List<int>();
    }
  }
}