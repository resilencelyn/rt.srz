// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlHelper.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The xml helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.IO;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Xml;
  using System.Xml.Linq;
  using System.Xml.Serialization;
  using System.Xml.XPath;

  using rt.srz.model.HL7.commons.Enumerations;
  using rt.srz.model.HL7.dotNetX;

  #endregion

  /// <summary>
  ///   The xml helper.
  /// </summary>
  public static class XmlHelper
  {
    #region Constants

    /// <summary>
    ///   The schema location attribute.
    /// </summary>
    public const string SchemaLocationAttribute = "schemaLocation";

    /// <summary>
    ///   The xsd namespace.
    /// </summary>
    public const string XsdNamespace = "http://www.w3.org/2001/XMLSchema";

    /// <summary>
    ///   The xsi namespace.
    /// </summary>
    public const string XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";

    #endregion

    #region Static Fields

    /// <summary>
    ///   The regex xml invalid characters_1_0.
    /// </summary>
    private static readonly Regex regexXmlInvalidCharacters_1_0 =
      new Regex(@"[^\u0009,\u000A,\u000D,\u0020-\uD7FF,\uE000-\uFFFD,\u10000-\u10FFFF]", RegexOptions.Compiled);

    /// <summary>
    ///   The regex xml invalid characters_1_1.
    /// </summary>
    private static readonly Regex regexXmlInvalidCharacters_1_1 =
      new Regex(@"[^\u0001-\uD7FF,\uE000-\uFFFD,\u10000-\u10FFFF]", RegexOptions.Compiled);

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The apply x path.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <param name="xpath">
    /// The xpath.
    /// </param>
    /// <returns>
    /// The <see cref="XElement"/>.
    /// </returns>
    public static XElement ApplyXPath(this XElement xmlNode, string xpath)
    {
      if (xmlNode != null)
      {
        var enumerable = xpath.SplitXPath();
        if (enumerable == null)
        {
          return xmlNode;
        }

        foreach (var str in enumerable)
        {
          if (string.IsNullOrEmpty(str))
          {
            RetrieveRoot(ref xmlNode);
            continue;
          }

          var content = xmlNode.XPathSelectElement(str);
          if (content == null)
          {
            var index = str.IndexOf('[');
            if (index >= 0)
            {
              var startIndex = str.IndexOf('@', index) + 1;
              var num3 = str.IndexOf('=', startIndex);
              var none = QuotationMark.None;
              var num4 = str.SearchQuote(ref none, num3 + 1);
              var num5 = str.SearchQuote(ref none, ++num4);
              content = new XElement(str.Substring(0, index));
              content.Add(
                          new XAttribute(str.Substring(startIndex, num3 - startIndex), str.Substring(num4, num5 - num4)));
            }
            else
            {
              content = new XElement(str);
            }

            xmlNode.Add(content);
          }

          xmlNode = content;
        }
      }

      return xmlNode;
    }

    /// <summary>
    /// The conditional remove attribute.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <param name="attributeName">
    /// The attribute name.
    /// </param>
    /// <param name="expectedValue">
    /// The expected value.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool ConditionalRemoveAttribute(this XElement xmlNode, string attributeName, string expectedValue)
    {
      var attribute = xmlNode.Attribute(attributeName);
      if ((attribute != null) && (string.Compare(attribute.Value, expectedValue, StringComparison.Ordinal) == 0))
      {
        xmlNode.SetAttributeValue(attributeName, null);
        return true;
      }

      return false;
    }

    /// <summary>
    /// The contains invalid xml characters.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="version">
    /// The version.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool ContainsInvalidXmlCharacters(this string s, XmlVersion version = 0)
    {
      if (string.IsNullOrEmpty(s))
      {
        return false;
      }

      return DoRetreiveXmlInvalidCharactersSet(version).IsMatch(s);
    }

    /// <summary>
    /// The convert length value.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="attributeName">
    /// The attribute name.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int ConvertLengthValue(string value, string attributeName)
    {
      var num = Convert.ToInt32(value.Trim());
      if (num < 0)
      {
        ThrowCastException(attributeName, "Длина");
      }

      return num;
    }

    /// <summary>
    /// The format x path.
    /// </summary>
    /// <param name="nodes">
    /// The nodes.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatXPath(params string[] nodes)
    {
      return FormatXPath(null, nodes);
    }

    /// <summary>
    /// The format x path.
    /// </summary>
    /// <param name="resolver">
    /// The resolver.
    /// </param>
    /// <param name="nodes">
    /// The nodes.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string FormatXPath(IXmlNamespaceResolver resolver, params string[] nodes)
    {
      if (nodes == null)
      {
        return string.Empty;
      }

      StringBuilder builder = null;
      var leftString = RetrieveDefaultNamespacePrefix(resolver);
      foreach (var str2 in nodes)
      {
        if (TStringHelper.IsNullOrWhiteSpace(str2))
        {
          builder = TStringHelper.CombineStrings(builder, "/");
        }
        else if (str2.IndexOf(':') >= 0)
        {
          var delimiter = "/";
          builder = TStringHelper.CombineStrings(builder, str2, delimiter);
        }
        else
        {
          var str4 = "/";
          var str5 = ":";
          builder = TStringHelper.CombineStrings(builder, TStringHelper.CombineStrings(leftString, str2, str5), str4);
        }
      }

      return TStringHelper.StringToEmpty(builder);
    }

    /// <summary>
    /// The is default namespace attribute.
    /// </summary>
    /// <param name="attribute">
    /// The attribute.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsDefaultNamespaceAttribute(this XAttribute attribute)
    {
      return (string.Compare(attribute.Name.NamespaceName, XNamespace.None.NamespaceName, StringComparison.Ordinal) == 0)
             && (string.Compare(attribute.Name.LocalName, "xmlns", StringComparison.Ordinal) == 0);
    }

    /// <summary>
    /// The is namespace attribute.
    /// </summary>
    /// <param name="attribute">
    /// The attribute.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsNamespaceAttribute(this XAttribute attribute)
    {
      return (string.Compare(attribute.Name.NamespaceName, XNamespace.Xmlns.NamespaceName, StringComparison.Ordinal)
              == 0) || attribute.IsDefaultNamespaceAttribute();
    }

    /// <summary>
    /// The is well formed xml.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsWellFormedXml(TextReader reader)
    {
      try
      {
        return DoCheckAndCloseXmlReader(new XmlTextReader(reader));
      }
      catch
      {
      }

      return false;
    }

    /// <summary>
    /// The is well formed xml.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsWellFormedXml(string s)
    {
      try
      {
        return IsWellFormedXml(new StringReader(s));
      }
      catch
      {
      }

      return false;
    }

    /// <summary>
    /// The is well formed xml file.
    /// </summary>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsWellFormedXmlFile(string filePath)
    {
      try
      {
        return FileSystemPhysical.FileExists(filePath) && DoCheckAndCloseXmlReader(new XmlTextReader(filePath));
      }
      catch
      {
      }

      return false;
    }

    /// <summary>
    /// The is x path delimiter.
    /// </summary>
    /// <param name="ch">
    /// The ch.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsXPathDelimiter(this char ch)
    {
      if (ch != '/')
      {
        return ch == '\\';
      }

      return true;
    }

    /// <summary>
    /// The remove namespace attributes.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    public static void RemoveNamespaceAttributes(this XElement xmlNode)
    {
      var firstAttribute = xmlNode.FirstAttribute;
      while (firstAttribute != null)
      {
        var attribute2 = firstAttribute.IsNamespaceAttribute() ? firstAttribute : null;
        firstAttribute = firstAttribute.NextAttribute;
        if (attribute2 != null)
        {
          attribute2.Remove();
        }
      }
    }

    /// <summary>
    /// The replace invalid xml characters.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="version">
    /// The version.
    /// </param>
    /// <param name="replacement">
    /// The replacement.
    /// </param>
    /// <param name="allowNull">
    /// The allow null.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ReplaceInvalidXmlCharacters(
      this string s, 
      XmlVersion version = 0, 
      string replacement = " ", 
      bool allowNull = false)
    {
      if (!string.IsNullOrEmpty(s))
      {
        return DoRetreiveXmlInvalidCharactersSet(version).Replace(s, replacement);
      }

      if ((s == null) && allowNull)
      {
        return null;
      }

      return string.Empty;
    }

    /// <summary>
    /// The resolve namespaces for serializer.
    /// </summary>
    /// <param name="resolver">
    /// The resolver.
    /// </param>
    /// <param name="includeDefaultNamespace">
    /// The include default namespace.
    /// </param>
    /// <returns>
    /// The <see cref="XmlSerializerNamespaces"/>.
    /// </returns>
    public static XmlSerializerNamespaces ResolveNamespacesForSerializer(
      IXmlNamespaceResolver resolver, 
      bool includeDefaultNamespace = false)
    {
      if (resolver == null)
      {
        return null;
      }

      var namespaces = new XmlSerializerNamespaces();
      var namespacesInScope = resolver.GetNamespacesInScope(XmlNamespaceScope.All);
      if (namespacesInScope != null)
      {
        string str;
        if (includeDefaultNamespace)
        {
          str = null;
        }
        else if (!namespacesInScope.TryGetValue(string.Empty, out str))
        {
          str = null;
        }

        foreach (var pair in namespacesInScope)
        {
          var key = pair.Key;
          if ((string.Compare(key, "xml", StringComparison.Ordinal) != 0)
              && (string.Compare(key, "xmlns", StringComparison.Ordinal) != 0))
          {
            var strB = pair.Value;
            if ((str == null) || (string.Compare(str, strB) != 0))
            {
              namespaces.Add(key, pair.Value);
            }
          }
        }
      }

      return namespaces;
    }

    /// <summary>
    /// The retrieve attribute.
    /// </summary>
    /// <param name="attributeName">
    /// The attribute name.
    /// </param>
    /// <param name="xmlNodes">
    /// The xml nodes.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveAttribute(string attributeName, params XElement[] xmlNodes)
    {
      return RetrieveAttribute(attributeName, false, xmlNodes);
    }

    /// <summary>
    /// The retrieve attribute.
    /// </summary>
    /// <param name="attributeName">
    /// The attribute name.
    /// </param>
    /// <param name="trimToNull">
    /// The trim to null.
    /// </param>
    /// <param name="xmlNodes">
    /// The xml nodes.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveAttribute(string attributeName, bool trimToNull, params XElement[] xmlNodes)
    {
      if (xmlNodes != null)
      {
        foreach (var element in xmlNodes)
        {
          if (element != null)
          {
            var str = element.RetrieveAttribute(attributeName, trimToNull);
            if (str != null)
            {
              return str;
            }
          }
        }
      }

      return null;
    }

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
      if (attribute == null)
      {
        return null;
      }

      if (!trimToNull)
      {
        return attribute.Value;
      }

      var bTrimFirst = true;
      return TStringHelper.StringToNull(attribute.Value, bTrimFirst);
    }

    /// <summary>
    /// The retrieve bool.
    /// </summary>
    /// <param name="attribute">
    /// The attribute.
    /// </param>
    /// <param name="dflt">
    /// The dflt.
    /// </param>
    /// <param name="xmlNodes">
    /// The xml nodes.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool RetrieveBool(string attribute, bool dflt, params XElement[] xmlNodes)
    {
      var strValue = RetrieveAttribute(attribute, xmlNodes);
      if (strValue != null)
      {
        switch (ConversionHelper.StringToBool(strValue))
        {
          case BooleanFlag.False:
            return false;

          case BooleanFlag.True:
            return true;
        }

        ThrowCastException(attribute, "Булевский");
      }

      return dflt;
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
      var strValue = xmlNode.RetrieveAttribute(attribute, false);
      if (strValue != null)
      {
        switch (ConversionHelper.StringToBool(strValue))
        {
          case BooleanFlag.False:
            return false;

          case BooleanFlag.True:
            return true;
        }

        ThrowCastException(attribute, "Булевский");
      }

      return dflt;
    }

    /// <summary>
    /// The retrieve default namespace prefix.
    /// </summary>
    /// <param name="resolver">
    /// The resolver.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveDefaultNamespacePrefix(IXmlNamespaceResolver resolver)
    {
      if (resolver == null)
      {
        return null;
      }

      var str = resolver.LookupNamespace(string.Empty);
      if (string.IsNullOrEmpty(str))
      {
        return string.Empty;
      }

      var prefix = resolver.LookupPrefix(str);
      if (string.IsNullOrEmpty(prefix))
      {
        var namespacesInScope = resolver.GetNamespacesInScope(XmlNamespaceScope.All);
        if (namespacesInScope == null)
        {
          return prefix;
        }

        foreach (var pair in namespacesInScope)
        {
          var key = pair.Key;
          if (!string.IsNullOrEmpty(key) && (string.Compare(pair.Value, str, StringComparison.Ordinal) == 0))
          {
            return key;
          }
        }
      }

      return prefix;
    }

    /// <summary>
    /// The retrieve length.
    /// </summary>
    /// <param name="attributeName">
    /// The attribute name.
    /// </param>
    /// <param name="xmlNodes">
    /// The xml nodes.
    /// </param>
    /// <returns>
    /// The <see cref="int?"/>.
    /// </returns>
    public static int? RetrieveLength(string attributeName, params XElement[] xmlNodes)
    {
      var str = RetrieveLengthValue(attributeName, xmlNodes);
      if (str != null)
      {
        return ConvertLengthValue(str, attributeName);
      }

      return null;
    }

    /// <summary>
    /// The retrieve length.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <param name="attributeName">
    /// The attribute name.
    /// </param>
    /// <returns>
    /// The <see cref="int?"/>.
    /// </returns>
    public static int? RetrieveLength(this XElement xmlNode, string attributeName)
    {
      var str = xmlNode.RetrieveLengthValue(attributeName);
      if (str != null)
      {
        return ConvertLengthValue(str, attributeName);
      }

      return null;
    }

    /// <summary>
    /// The retrieve length value.
    /// </summary>
    /// <param name="attributeName">
    /// The attribute name.
    /// </param>
    /// <param name="xmlNodes">
    /// The xml nodes.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveLengthValue(string attributeName, params XElement[] xmlNodes)
    {
      var str = RetrieveAttribute(attributeName, xmlNodes);
      if (str == null)
      {
        return null;
      }

      if (str.Length < 1)
      {
        ThrowCastException(attributeName, "Длина");
      }

      return str;
    }

    /// <summary>
    /// The retrieve length value.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <param name="attributeName">
    /// The attribute name.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveLengthValue(this XElement xmlNode, string attributeName)
    {
      var str = xmlNode.RetrieveAttribute(attributeName, false);
      if (str == null)
      {
        return null;
      }

      if (str.Length < 1)
      {
        ThrowCastException(attributeName, "Длина");
      }

      return str;
    }

    /// <summary>
    /// The retrieve node name.
    /// </summary>
    /// <param name="xmlNodes">
    /// The xml nodes.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveNodeName(params XElement[] xmlNodes)
    {
      return RetrieveNodeName(false, xmlNodes);
    }

    /// <summary>
    /// The retrieve node name.
    /// </summary>
    /// <param name="includeNamespaces">
    /// The include namespaces.
    /// </param>
    /// <param name="xmlNodes">
    /// The xml nodes.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveNodeName(bool includeNamespaces, params XElement[] xmlNodes)
    {
      if (xmlNodes != null)
      {
        foreach (var element in xmlNodes)
        {
          var str = element.RetrieveNodeName(includeNamespaces);
          if (!string.IsNullOrEmpty(str))
          {
            return str;
          }
        }
      }

      return string.Empty;
    }

    /// <summary>
    /// The retrieve node name.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <param name="includeNamespaces">
    /// The include namespaces.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrieveNodeName(this XElement xmlNode, bool includeNamespaces = false)
    {
      if (xmlNode != null)
      {
        var name = xmlNode.Name;
        if (name != null)
        {
          if (includeNamespaces)
          {
            return name.ToString();
          }

          return name.LocalName;
        }
      }

      return string.Empty;
    }

    /// <summary>
    /// The retrieve path.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <param name="includeNamespaces">
    /// The include namespaces.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string RetrievePath(this XElement xmlNode, bool includeNamespaces = false)
    {
      if (xmlNode == null)
      {
        return string.Empty;
      }

      var builder = new StringBuilder(xmlNode.RetrieveNodeName(includeNamespaces));
      while (true)
      {
        xmlNode = xmlNode.Parent;
        if (xmlNode == null)
        {
          break;
        }

        builder.Insert(0, '/');
        builder.Insert(0, xmlNode.RetrieveNodeName(includeNamespaces));
      }

      return builder.ToString();
    }

    // public static string RetrievePath(this string xpath, string xroot = new string(), bool includeNamespaces = false)
    // {
    // if (string.IsNullOrEmpty(xpath))
    // {
    // return string.Empty;
    // }
    // if (includeNamespaces)
    // {
    // if (xpath[0].IsXPathDelimiter())
    // {
    // string delimiter = null;
    // return TStringHelper.CombineStrings(xroot, xpath, delimiter);
    // }
    // return xpath;
    // }
    // StringBuilder builder = new StringBuilder();
    // foreach (string str in xpath.SplitXPath())
    // {
    // if (xroot != null)
    // {
    // if (str.Length < 1)
    // {
    // builder.Append(xroot);
    // }
    // xroot = null;
    // }
    // int num = str.LastIndexOf(':');
    // string str2 = (num < 0) ? str : str.Substring(num + 1);
    // string str4 = "/";
    // TStringHelper.CombineStrings(builder, str2, str4);
    // }
    // return builder.ToString();
    // }

    /// <summary>
    /// The retrieve root.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    [CLSCompliant(false)]
    public static void RetrieveRoot(ref XElement xmlNode)
    {
      xmlNode = xmlNode.RetrieveRoot();
    }

    /// <summary>
    /// The retrieve root.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <returns>
    /// The <see cref="XElement"/>.
    /// </returns>
    public static XElement RetrieveRoot(this XElement xmlNode)
    {
      if (xmlNode == null)
      {
        return null;
      }

      while (xmlNode.Parent != null)
      {
        xmlNode = xmlNode.Parent;
      }

      return xmlNode;
    }

    /// <summary>
    /// The retrieve value.
    /// </summary>
    /// <param name="attribute">
    /// The attribute.
    /// </param>
    /// <param name="dflt">
    /// The dflt.
    /// </param>
    /// <param name="xmlNodes">
    /// The xml nodes.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T RetrieveValue<T>(string attribute, T dflt, params XElement[] xmlNodes)
    {
      var strValue = RetrieveAttribute(attribute, true, xmlNodes);
      if (strValue != null)
      {
        return ConversionHelper.StringToValue<T>(strValue);
      }

      return dflt;
    }

    /// <summary>
    /// The retrieve value.
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
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T RetrieveValue<T>(this XElement xmlNode, string attribute, T dflt)
    {
      var trimToNull = true;
      var strValue = xmlNode.RetrieveAttribute(attribute, trimToNull);
      if (strValue != null)
      {
        return ConversionHelper.StringToValue<T>(strValue);
      }

      return dflt;
    }

    /// <summary>
    /// The select by local name.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable"/>.
    /// </returns>
    public static IEnumerable<XElement> SelectByLocalName(this XElement xmlNode, string name)
    {
      if ((xmlNode != null) && !string.IsNullOrEmpty(name))
      {
        for (var iteratorVariable0 = xmlNode.FirstNode;
             iteratorVariable0 != null;
             iteratorVariable0 = iteratorVariable0.NextNode)
        {
          var iteratorVariable1 = iteratorVariable0 as XElement;
          if ((iteratorVariable1 != null)
              && (string.Compare(iteratorVariable1.Name.LocalName, name, StringComparison.Ordinal) == 0))
          {
            yield return iteratorVariable1;
          }
        }
      }
    }

    // public static void SerializeObject(Stream stream, object obj, IXmlNamespaceResolver resolver = new IXmlNamespaceResolver())
    // {
    // XmlSerializer serializer = new XmlSerializer(obj.GetType());
    // XmlSerializerNamespaces namespaces = ResolveNamespacesForSerializer(resolver, false);
    // if (namespaces != null)
    // {
    // serializer.Serialize(stream, obj, namespaces);
    // }
    // else
    // {
    // serializer.Serialize(stream, obj);
    // }
    // }

    // public static void SerializeObject(XmlWriter xmlWriter, object obj, IXmlNamespaceResolver resolver = new IXmlNamespaceResolver())
    // {
    // XmlSerializer serializer = new XmlSerializer(obj.GetType());
    // XmlSerializerNamespaces namespaces = ResolveNamespacesForSerializer(resolver, false);
    // if (namespaces != null)
    // {
    // serializer.Serialize(xmlWriter, obj, namespaces);
    // }
    // else
    // {
    // serializer.Serialize(xmlWriter, obj);
    // }
    // }

    /// <summary>
    /// The split x path.
    /// </summary>
    /// <param name="xpath">
    /// The xpath.
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// </exception>
    public static IEnumerable<string> SplitXPath(this string xpath)
    {
      List<string> list = null;
      char ch;
      var bTrimFirst = true;
      xpath = TStringHelper.StringToNull(xpath, bTrimFirst);
      if (xpath == null)
      {
        return list;
      }

      var length = xpath.Length;
      var startIndex = 0;
      var num3 = 0;
      var none = QuotationMark.None;
      Label_0023:
      ch = xpath[num3];
      var ch2 = ch;
      if (ch2 <= '\'')
      {
        switch (ch2)
        {
          case '"':
            switch (none)
            {
              case QuotationMark.None:
                none = QuotationMark.DoubleQuotes;
                goto Label_00D1;

              case QuotationMark.Apostrophe:
                goto Label_00D1;

              case QuotationMark.DoubleQuotes:
                none = QuotationMark.None;
                goto Label_00D1;
            }

            goto Label_00D1;

          case '\'':
            switch (none)
            {
              case QuotationMark.None:
                none = QuotationMark.Apostrophe;
                goto Label_00D1;

              case QuotationMark.Apostrophe:
                none = QuotationMark.None;
                goto Label_00D1;
            }

            goto Label_00D1;
        }
      }
      else if (((ch2 == '/') || (ch2 == '\\')) && (none == QuotationMark.None))
      {
        var str = xpath.Substring(startIndex, num3 - startIndex);
        if (string.IsNullOrEmpty(str) && (list != null))
        {
          throw new ArgumentException("пустой узел в x-path выражении");
        }

        if (list == null)
        {
          list = new List<string>();
        }

        list.Add(str);
        startIndex = num3 + 1;
      }

      Label_00D1:
      if (++num3 < length)
      {
        goto Label_0023;
      }

      if (none != QuotationMark.None)
      {
        throw new ArgumentException("незакрытая кавычка в x-path выражении");
      }

      if (list == null)
      {
        list = new List<string>();
      }

      list.Add(xpath.Substring(startIndex, num3 - startIndex));
      return list;
    }

    /// <summary>
    /// The throw cast exception.
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

    /// <summary>
    /// The try read attribute value.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <param name="attributeName">
    /// The attribute name.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    public static void TryReadAttributeValue(this XElement xmlNode, string attributeName, ref string value)
    {
      var str = xmlNode.RetrieveAttribute(attributeName, false);
      if (str != null)
      {
        value = str;
      }
    }

    /// <summary>
    /// The try read attribute value.
    /// </summary>
    /// <param name="xmlNode">
    /// The xml node.
    /// </param>
    /// <param name="attributeName">
    /// The attribute name.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    public static void TryReadAttributeValue(
      this XElement xmlNode, 
      string attributeName, 
      ref DateTime value, 
      string format)
    {
      var s = xmlNode.RetrieveAttribute(attributeName, false);
      if ((s != null)
          && !DateTime.TryParseExact(
                                     s, 
                                     format, 
                                     null, 
                                     DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, 
                                     out value))
      {
        ThrowCastException(attributeName, "Дата/Время");
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The do check and close xml reader.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool DoCheckAndCloseXmlReader(XmlReader reader)
    {
      if (reader != null)
      {
        try
        {
          while (reader.Read())
          {
          }

          return true;
        }
        finally
        {
          reader.Close();
        }
      }

      return false;
    }

    /// <summary>
    /// The do retreive xml invalid characters set.
    /// </summary>
    /// <param name="version">
    /// The version.
    /// </param>
    /// <returns>
    /// The <see cref="Regex"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// </exception>
    private static Regex DoRetreiveXmlInvalidCharactersSet(XmlVersion version)
    {
      switch (version)
      {
        case XmlVersion.Version_1_0:
          return regexXmlInvalidCharacters_1_0;

        case XmlVersion.Version_1_1:
          return regexXmlInvalidCharacters_1_1;
      }

      throw new ArgumentException(string.Format("Версия XML не поддерживается: {0}", version));
    }

    #endregion

    // [CompilerGenerated]
    // private sealed class <SelectByLocalName>d__0 : IEnumerable<XElement>, IEnumerable, IEnumerator<XElement>, IEnumerator, IDisposable
    // {
    // private int <>1__state;
    // private XElement <>2__current;
    // public string <>3__name;
    // public XElement <>3__xmlNode;
    // private int <>l__initialThreadId;
    // public XNode <child>5__1;
    // public XElement <xmlChild>5__2;
    // public string name;
    // public XElement xmlNode;

    // [DebuggerHidden]
    // public <SelectByLocalName>d__0(int <>1__state)
    // {
    // this.<>1__state = <>1__state;
    // this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
    // }

    // private bool MoveNext()
    // {
    // switch (this.<>1__state)
    // {
    // case 0:
    // this.<>1__state = -1;
    // if ((this.xmlNode == null) || string.IsNullOrEmpty(this.name))
    // {
    // goto Label_00B8;
    // }
    // this.<child>5__1 = this.xmlNode.FirstNode;
    // goto Label_00B0;

    // case 1:
    // this.<>1__state = -1;
    // goto Label_009F;

    // default:
    // goto Label_00B8;
    // }
    // Label_009F:
    // this.<child>5__1 = this.<child>5__1.NextNode;
    // Label_00B0:
    // if (this.<child>5__1 != null)
    // {
    // this.<xmlChild>5__2 = this.<child>5__1 as XElement;
    // if ((this.<xmlChild>5__2 != null) && (string.Compare(this.<xmlChild>5__2.Name.LocalName, this.name, StringComparison.Ordinal) == 0))
    // {
    // this.<>2__current = this.<xmlChild>5__2;
    // this.<>1__state = 1;
    // return true;
    // }
    // goto Label_009F;
    // }
    // Label_00B8:
    // return false;
    // }

    // [DebuggerHidden]
    // IEnumerator<XElement> IEnumerable<XElement>.GetEnumerator()
    // {
    // XmlHelper.<SelectByLocalName>d__0 d__;
    // if ((Thread.CurrentThread.ManagedThreadId == this.<>l__initialThreadId) && (this.<>1__state == -2))
    // {
    // this.<>1__state = 0;
    // d__ = this;
    // }
    // else
    // {
    // d__ = new XmlHelper.<SelectByLocalName>d__0(0);
    // }
    // d__.xmlNode = this.<>3__xmlNode;
    // d__.name = this.<>3__name;
    // return d__;
    // }

    // [DebuggerHidden]
    // IEnumerator IEnumerable.GetEnumerator()
    // {
    // return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement>.GetEnumerator();
    // }

    // [DebuggerHidden]
    // void IEnumerator.Reset()
    // {
    // throw new NotSupportedException();
    // }

    // void IDisposable.Dispose()
    // {
    // }

    // XElement IEnumerator<XElement>.Current
    // {
    // [DebuggerHidden]
    // get
    // {
    // return this.<>2__current;
    // }
    // }

    // object IEnumerator.Current
    // {
    // [DebuggerHidden]
    // get
    // {
    // return this.<>2__current;
    // }
    // }
    // }
  }
}