// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XMLHelper.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The xml helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.srz.database.business.standard.helpers
{
  using System;
  using System.Xml.Linq;

  using rt.srz.database.business.standard.enums;

  /// <summary>
  /// помощник дл€ работы с XML
  /// </summary>
  public static class XmlHelper
  {
    /// <summary>
    /// The retrieve attribute.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <param name="attributeName">
    /// The attribute name.
    /// </param>
    /// <param name="trimToNull">
    /// The trim to null.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveAttribute(this XElement xmlNode, string attributeName, bool trimToNull = false)
    {
      var attribute = xmlNode.Attribute(attributeName);
      return attribute != null
               ? (trimToNull ? TStringHelper.StringToNull(attribute.Value, bTrimFirst: true) : attribute.Value)
               : null;
    }

    /// <summary>
    /// The retrieve bool.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <param name="attribute">
    /// The attribute.
    /// </param>
    /// <param name="dflt">
    /// The dflt.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool RetrieveBool(this XElement xmlNode, string attribute, bool dflt)
    {
      var attributeValue = RetrieveAttribute(xmlNode, attribute);
      if (attributeValue != null)
      {
        switch (ConversionHelper.StringToBool(attributeValue))
        {
          case BooleanFlag.True:
            return true;
          case BooleanFlag.False:
            return false;
          default:
            ThrowCastException(attribute, "Ѕулевский");
            break;
        }
      }

      return dflt;
    }

    /// <summary>
    /// выкинуть сообщение о невозможности преобразовани€ атрибута
    /// </summary>
    /// <param name="attribute">
    /// The attribute.
    /// </param>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <exception cref="ArgumentException">
    /// </exception>
    public static void ThrowCastException(string attribute, string type)
    {
      throw new ArgumentException(string.Format("Ќе удалось преобразовать значение атрибута '{0}' в тип {1}", attribute, 
                                                type));
    }
  }
}