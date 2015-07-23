// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XMLHelper.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   помощник для работы с XML
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.helpers
{
  using System;
  using System.Xml.Linq;

  using rt.srz.database.business.standard.enums;

  /// <summary>
  ///   помощник для работы с XML
  /// </summary>
  public static class XmlHelper
  {
    #region Public Methods and Operators

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
               ? (trimToNull ? TStringHelper.StringToNull(attribute.Value, true) : attribute.Value)
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
            ThrowCastException(attribute, "Булевский");
            break;
        }
      }

      return dflt;
    }

    /// <summary>
    /// выкинуть сообщение о невозможности преобразования атрибута
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
      throw new ArgumentException(
        string.Format("Не удалось преобразовать значение атрибута '{0}' в тип {1}", attribute, type));
    }

    #endregion
  }
}