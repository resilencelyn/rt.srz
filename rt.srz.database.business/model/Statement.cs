// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Statement.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The Statement.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.model
{
  using System;
  using System.Linq;
  using System.Xml.Linq;

  /// <summary>
  ///   The Statement.
  /// </summary>
  public partial class Statement
  {
    /// <summary>
    /// Конвертер
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Создает объект по xml
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static Statement FromXML(string xml)
    { 
      XDocument document = XDocument.Parse(xml);
      var statement = from st in document.Descendants("Dual")
                      select new Statement
                      {
                        RowId = new Guid(st.Element("RowId").Value),
                        DateFiling = st.Element("DateFiling") == null ? (DateTime?)null : DateTime.Parse(st.Element("DateFiling").Value),
                        HasPetition = st.Element("HasPetition") == null ? (bool?)null : BitStringValue2Boolean(st.Element("HasPetition").Value),
                        NumberPolicy = st.Element("NumberPolicy") == null ? null : st.Element("NumberPolicy").Value,
                        NumberTemporaryCertificate = st.Element("NumberTemporaryCertificate") == null ? null : st.Element("NumberTemporaryCertificate").Value,
                        DateIssueTemporaryCertificate = st.Element("DateIssueTemporaryCertificate") == null ? (DateTime?)null : DateTime.Parse(st.Element("DateIssueTemporaryCertificate").Value),
                        AbsentPrevPolicy = st.Element("AbsentPrevPolicy") == null ? (bool?)null : BitStringValue2Boolean(st.Element("AbsentPrevPolicy").Value),
                        IsActive = st.Element("IsActive") == null ? false : BitStringValue2Boolean(st.Element("IsActive").Value),
                        NumberPolisCertificate = st.Element("NumberPolisCertificate") == null ? null : st.Element("NumberPolisCertificate").Value,
                        DateIssuePolisCertificate = st.Element("DateIssuePolisCertificate") == null ? (DateTime?)null : DateTime.Parse(st.Element("DateIssuePolisCertificate").Value),
                        PolicyIsIssued = st.Element("PolicyIsIssued") == null ? (bool?)null : BitStringValue2Boolean(st.Element("PolicyIsIssued").Value),
                      };
      return statement.FirstOrDefault();
    }
  }
}