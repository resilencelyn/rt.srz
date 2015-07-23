// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchKeyType.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The SearchKeyType.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.model
{
  using System;
  using System.Linq;
  using System.Xml.Linq;

  /// <summary>
  ///   The SearchKeyType.
  /// </summary>
  public partial class SearchKeyType
  {
    #region Public Methods and Operators

    /// <summary>
    /// Создает объект по xml
    /// </summary>
    /// <param name="xml">
    /// </param>
    /// <returns>
    /// The <see cref="SearchKeyType"/>.
    /// </returns>
    public static SearchKeyType FromXML(string xml)
    {
      var document = XDocument.Parse(xml);
      var keyType = from kt in document.Descendants("SearchKeyType")
                    select
                      new SearchKeyType
                      {
                        RowId = new Guid(kt.Element("RowId").Value), 
                        FirstName = BitStringValue2Boolean(kt.Element("FirstName").Value), 
                        LastName = BitStringValue2Boolean(kt.Element("LastName").Value), 
                        MiddleName = BitStringValue2Boolean(kt.Element("MiddleName").Value), 
                        Birthday = BitStringValue2Boolean(kt.Element("Birthday").Value), 
                        Birthplace = BitStringValue2Boolean(kt.Element("Birthplace").Value), 
                        Snils = BitStringValue2Boolean(kt.Element("Snils").Value), 
                        DocumentType = BitStringValue2Boolean(kt.Element("DocumentType").Value), 
                        DocumentSeries = BitStringValue2Boolean(kt.Element("DocumentSeries").Value), 
                        DocumentNumber = BitStringValue2Boolean(kt.Element("DocumentNumber").Value), 
                        Okato = BitStringValue2Boolean(kt.Element("Okato").Value), 
                        PolisType = BitStringValue2Boolean(kt.Element("PolisType").Value), 
                        PolisSeria = BitStringValue2Boolean(kt.Element("PolisSeria").Value), 
                        PolisNumber = BitStringValue2Boolean(kt.Element("PolisNumber").Value), 
                        FirstNameLength = short.Parse(kt.Element("FirstNameLength").Value), 
                        LastNameLength = short.Parse(kt.Element("LastNameLength").Value), 
                        MiddleNameLength = short.Parse(kt.Element("MiddleNameLength").Value), 
                        BirthdayLength = short.Parse(kt.Element("BirthdayLength").Value), 
                        AddressStreet = BitStringValue2Boolean(kt.Element("AddressStreet").Value), 
                        AddressStreetLength = short.Parse(kt.Element("AddressStreetLength").Value), 
                        AddressHouse = BitStringValue2Boolean(kt.Element("AddressHouse").Value), 
                        AddressRoom = BitStringValue2Boolean(kt.Element("AddressRoom").Value), 
                        AddressStreet2 = BitStringValue2Boolean(kt.Element("AddressStreet2").Value), 
                        AddressStreetLength2 = short.Parse(kt.Element("AddressStreetLength2").Value), 
                        AddressHouse2 = BitStringValue2Boolean(kt.Element("AddressHouse2").Value), 
                        AddressRoom2 = BitStringValue2Boolean(kt.Element("AddressRoom2").Value), 
                        IdenticalLetters = kt.Element("IdenticalLetters").Value
                      };
      return keyType.FirstOrDefault();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Конвертер
    /// </summary>
    /// <param name="value">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool BitStringValue2Boolean(string value)
    {
      switch (value)
      {
        case "0":
          return false;
        case "1":
          return true;
      }

      return false;
    }

    #endregion
  }
}