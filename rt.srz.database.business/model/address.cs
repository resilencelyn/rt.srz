// --------------------------------------------------------------------------------------------------------------------
// <copyright file="address.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The address.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.model
{
  using System;
  using System.Linq;
  using System.Xml.Linq;

  /// <summary>
  ///   The address.
  /// </summary>
  public partial class address
  {
    #region Public Methods and Operators

    /// <summary>
    /// The from xml.
    /// </summary>
    /// <param name="xml">
    /// The xml.
    /// </param>
    /// <returns>
    /// The <see cref="address"/>.
    /// </returns>
    public static address FromXML(string xml)
    {
      var document = XDocument.Parse(xml);
      var adr = from a in document.Descendants("Dual")
                select
                  new address
                  {
                    RowId = new Guid(a.Element("RowId").Value), 
                    Street = a.Element("Street") == null ? null : a.Element("Street").Value, 
                    House = a.Element("House") == null ? null : a.Element("House").Value, 
                    Room = a.Element("Room") == null ? (short?)null : short.Parse(a.Element("Room").Value), 
                    Okato = a.Element("Okato") == null ? null : a.Element("Okato").Value, 
                  };

      address result = null;
      try
      {
        result = adr.FirstOrDefault();
      }
      catch (Exception e)
      {
      }

      return result;
    }

    #endregion
  }
}