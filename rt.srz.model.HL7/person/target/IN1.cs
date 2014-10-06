// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IN1.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The i n 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The i n 1.
  /// </summary>
  [Serializable]
  public class IN1
  {
    #region Fields

    /// <summary>
    ///   The address list.
    /// </summary>
    [XmlElement(ElementName = "IN1.19", Order = 19)]
    public List<Address> AddressList;

    /// <summary>
    ///   The birth day.
    /// </summary>
    [XmlElement(ElementName = "IN1.18", Order = 18)]
    public string BirthDay;

    /// <summary>
    ///   The code of region.
    /// </summary>
    [XmlElement(ElementName = "IN1.15", Order = 15)]
    public string CodeOfRegion;

    /// <summary>
    ///   The company id.
    /// </summary>
    [XmlElement(ElementName = "IN1.3", Order = 3)]
    public CompanyId CompanyId;

    /// <summary>
    ///   The company name.
    /// </summary>
    [XmlElement(ElementName = "IN1.4", Order = 4)]
    public CompanyName CompanyName;

    /// <summary>
    ///   The date begin insurence.
    /// </summary>
    [XmlElement(ElementName = "IN1.12", Order = 12)]
    public string DateBeginInsurence = string.Empty;

    /// <summary>
    ///   The date end insurence.
    /// </summary>
    [XmlElement(ElementName = "IN1.13", Order = 13)]
    public string DateEndInsurence;

    /// <summary>
    ///   The employment.
    /// </summary>
    [XmlElement(ElementName = "IN1.42", Order = 42)]
    public Employment Employment;

    /// <summary>
    ///   The fio list.
    /// </summary>
    [XmlElement(ElementName = "IN1.16", Order = 16)]
    public List<Fio> FioList;

    /// <summary>
    ///   The id.
    /// </summary>
    [XmlElement(ElementName = "IN1.1", Order = 1)]
    public string Id;

    /// <summary>
    ///   The identificators list.
    /// </summary>
    [XmlElement(ElementName = "IN1.49", Order = 49)]
    public List<Identificators> IdentificatorsList;

    /// <summary>
    ///   The insurance ser num.
    /// </summary>
    [XmlElement(ElementName = "IN1.36", Order = 36)]
    public string InsuranceSerNum;

    /// <summary>
    ///   The insurance type.
    /// </summary>
    [XmlElement(ElementName = "IN1.35", Order = 35)]
    public string InsuranceType;

    /// <summary>
    ///   The place of birth.
    /// </summary>
    [XmlElement(ElementName = "IN1.52", Order = 52)]
    public string PlaceOfBirth;

    /// <summary>
    ///   The plan id.
    /// </summary>
    [XmlElement(ElementName = "IN1.2", Order = 2)]
    public PlanId PlanId;

    /// <summary>
    ///   The sex.
    /// </summary>
    [XmlElement(ElementName = "IN1.43", Order = 43)]
    public string Sex;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The has address list.
    /// </summary>
    /// <returns>
    ///   The <see cref="bool" />.
    /// </returns>
    public bool HasAddressList()
    {
      if (AddressList.Count == 0)
      {
        return false;
      }

      return true;
    }

    /// <summary>
    ///   The has birth day.
    /// </summary>
    /// <returns>
    ///   The <see cref="bool" />.
    /// </returns>
    public bool HasBirthDay()
    {
      if (string.IsNullOrEmpty(BirthDay))
      {
        return false;
      }

      return true;
    }

    /// <summary>
    ///   The has fio.
    /// </summary>
    /// <returns>
    ///   The <see cref="bool" />.
    /// </returns>
    public bool HasFio()
    {
      if (FioList.Count == 0)
      {
        return false;
      }

      return true;
    }

    /// <summary>
    ///   The has identificators.
    /// </summary>
    /// <returns>
    ///   The <see cref="bool" />.
    /// </returns>
    public bool HasIdentificators()
    {
      if (IdentificatorsList.Count == 0)
      {
        return false;
      }

      return true;
    }

    /// <summary>
    ///   The has place of birth.
    /// </summary>
    /// <returns>
    ///   The <see cref="bool" />.
    /// </returns>
    public bool HasPlaceOfBirth()
    {
      if (string.IsNullOrEmpty(PlaceOfBirth))
      {
        return false;
      }

      return true;
    }

    /// <summary>
    ///   The has sex.
    /// </summary>
    /// <returns>
    ///   The <see cref="bool" />.
    /// </returns>
    public bool HasSex()
    {
      if (string.IsNullOrEmpty(Sex))
      {
        return false;
      }

      return true;
    }

    #endregion
  }
}