// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Statement.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The Statement.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  using rt.srz.model.srz.concepts;

  /// <summary>
  ///   The Statement.
  /// </summary>
  public partial class Statement
  {
    #region Public Properties

    /// <summary>
    ///   Àäðåñ ïðîæèâàíèÿ
    /// </summary>
    [XmlIgnore]
    public virtual string AddressLiveStr
    {
      get
      {
        return GetAddressInStr(Address2);
      }
    }

    /// <summary>
    ///   Àäðåñ ðåãèñòðàöèè
    /// </summary>
    [XmlIgnore]
    public virtual string AddressRegistrationStr
    {
      get
      {
        return GetAddressInStr(Address);
      }
    }

    /// <summary>
    /// Gets the date issue polis certificate.
    /// </summary>
    [XmlElement(Order = 55)]
    [DataMember(Order = 55)]
    public virtual DateTime? DateIssuePolisCertificate
    {
      get
      {
        var insurance = MedicalInsurances != null
                          ? MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.Â && x.IsActive)
                          : null;
        if (insurance != null)
        {
          return insurance.DateFrom;
        }

        return null;
      }
    }

    /// <summary>
    /// Gets the date issue temporary certificate.
    /// </summary>
    [XmlElement(Order = 53)]
    [DataMember(Order = 53)]
    public virtual DateTime? DateIssueTemporaryCertificate
    {
      get
      {
        var insurance = MedicalInsurances != null
                          ? MedicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.Â)
                          : null;
        if (insurance != null)
        {
          return insurance.DateFrom;
        }

        return null;
      }
    }

    /// <summary>
    ///   Gets or sets the not check polis number.
    /// </summary>
    [XmlElement(Order = 50)]
    [DataMember(Order = 50)]
    public virtual bool? NotCheckPolisNumber { get; set; }

    /// <summary>
    /// Gets the number polis certificate.
    /// </summary>
    [XmlElement(Order = 54)]
    [DataMember(Order = 54)]
    public virtual string NumberPolisCertificate
    {
      get
      {
        var insurance = MedicalInsurances != null
                          ? MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.Â && x.IsActive)
                          : null;
        if (insurance != null)
        {
          return insurance.PolisNumber;
        }

        return null;
      }
    }

    /// <summary>
    /// Gets the number temporary certificate.
    /// </summary>
    [XmlElement(Order = 52)]
    [DataMember(Order = 52)]
    public virtual string NumberTemporaryCertificate
    {
      get
      {
        var insurance = MedicalInsurances != null
                          ? MedicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.Â)
                          : null;
        if (insurance != null)
        {
          return insurance.PolisNumber;
        }

        return null;
      }
    }

    /// <summary>
    /// Gets the polis medical insurance.
    /// </summary>
    [XmlElement(Order = 57)]
    [DataMember(Order = 57)]
    public virtual MedicalInsurance PolisMedicalInsurance
    {
      get
      {
        return MedicalInsurances != null
                 ? MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.Â && x.IsActive)
                 : null;
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
    /// Gets the temp medical insurance.
    /// </summary>
    [XmlElement(Order = 56)]
    [DataMember(Order = 56)]
    public virtual MedicalInsurance TempMedicalInsurance
    {
      get
      {
        return MedicalInsurances != null
                 ? MedicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.Â && x.IsActive)
                 : null;
      }
    }

    /// <summary>
    ///   Gets the type statement id.
    /// </summary>
    [XmlElement(Order = 51)]
    [DataMember(Order = 51)]
    public virtual int TypeStatementId
    {
      get
      {
        return CauseFiling != null ? GetTypeStatementId(CauseFiling.Id) : -1;
      }
    }

    #endregion

    #region Public Methods and Operators

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
          return new[]
                 {
                   CauseReinsurance.Choice, CauseReinsurance.ReinsuranceAtWill, CauseReinsurance.ReinsuranceWithTheMove, 
                   CauseReinsurance.ReinsuranceStopFinance
                 };
        }

        case TypeStatement.TypeStatement2:
          return new[]
                 {
                   CauseReneval.RenevalChangePersonDetails, CauseReneval.RenevalInaccuracy, 
                   CauseReneval.RenevalExpiration
                 };
        case TypeStatement.TypeStatement3:
          return new[] { CauseReneval.RenevalUnusable, CauseReneval.RenevalLoss };

        case TypeStatement.TypeStatement4:
          return new[] { CauseReneval.Edit };
      }

      return new List<int>();
    }

    /// <summary>
    /// The get type statement id.
    /// </summary>
    /// <param name="ñauseFilingId">
    /// The ñause filing id.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int GetTypeStatementId(int ñauseFilingId)
    {
      switch (ñauseFilingId)
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

    #endregion

    #region Methods

    /// <summary>
    /// The get address in str.
    /// </summary>
    /// <param name="addr">
    /// The addr.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetAddressInStr(address addr)
    {
      if (addr == null)
      {
        return string.Empty;
      }

      return addr.ToString();

      // StringBuilder sb = new StringBuilder();
      // sb.Append(addr.Postcode).Append(" ").
      // Append(addr.Subject).Append(" ").
      // Append(addr.Area).Append(" ").
      // Append(addr.City).Append(" ").
      // Append(addr.Town).Append(" ").
      // Append(addr.Street).Append(" ").
      // Append(addr.House).Append(" ").
      // Append(addr.Housing).Append(" ").
      // Append(addr.Room);
      // return sb.ToString();
    }

    #endregion
  }
}