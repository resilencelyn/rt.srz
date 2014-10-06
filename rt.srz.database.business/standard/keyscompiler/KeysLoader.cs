// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeysLoader.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The keys loader.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.keyscompiler
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Xml;
  using System.Xml.Linq;

  using rt.srz.database.business.standard.keyscompiler.Fields;

  /// <summary>
  /// The keys loader.
  /// </summary>
  public static class KeysLoader
  {
    #region Static Fields

    /// <summary>
    /// The field name resolver.
    /// </summary>
    public static readonly SearchFieldNameResolver FieldNameResolver;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The load keys.
    /// </summary>
    /// <param name="configFile">
    /// The config file.
    /// </param>
    /// <param name="subTypes">
    /// The sub types.
    /// </param>
    /// <param name="FieldNameResolver">
    /// The field name resolver.
    /// </param>
    /// <returns>
    /// The <see cref="List"/>.
    /// </returns>
    /// <exception cref="Exception">
    /// </exception>
    public static List<LoadedKey> LoadKeys(
      Stream configFile, 
      out Dictionary<string, string> subTypes, 
      SearchFieldNameResolver FieldNameResolver = null)
    {
      var keys = new List<LoadedKey>();
      subTypes = new Dictionary<string, string>();
      using (var reader = XmlReader.Create(configFile))
      {
        while (reader.Read())
        {
          if (reader.NodeType == XmlNodeType.Element)
          {
            try
            {
              switch (reader.Name)
              {
                case "Key":
                case "key":
                  var xKey = XElement.Load(reader.ReadSubtree());
                  var key = new LoadedKey
                            {
                              name = xKey.Attribute("name").Value, 
                              type = int.Parse(xKey.Attribute("type").Value), 
                              formula = GetFieldsTypes(xKey.Attribute("value").Value, FieldNameResolver)
                            };
                  if (xKey.Attribute("subtype") != null)
                  {
                    key.subtype = int.Parse(xKey.Attribute("subtype").Value);
                  }

                  keys.Add(key);
                  break;
                case "subtype":
                case "subType":
                  var xsubtype = XElement.Load(reader.ReadSubtree());
                  subTypes.Add(xsubtype.Attribute("name").Value, xsubtype.Attribute("value").Value);
                  break;
              }
            }
            catch (Exception ex)
            {
              throw new Exception("Ошибка при чтении ключей из конфигурационного файла: ", ex);
            }
          }
        }
      }

      return keys;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get fields types.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="FieldNameResolver">
    /// The field name resolver.
    /// </param>
    /// <returns>
    /// The <see cref="FieldTypes[]"/>.
    /// </returns>
    /// <exception cref="Exception">
    /// </exception>
    private static FieldTypes[] GetFieldsTypes(string value, SearchFieldNameResolver FieldNameResolver = null)
    {
      var fields = value.Split(new[] { '+' });
      var fieldTypes = new FieldTypes[fields.Length];
      try
      {
        for (var i = 0; i < fields.Length; i++)
        {
          fieldTypes[i] = FieldNameResolver(fields[i].Trim());
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Ошибка при чтении ключей из конфигурационного файла:", ex);
      }

      return fieldTypes;
    }

    #endregion
  }
}