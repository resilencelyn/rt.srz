// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolSettingsElement.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The protocol settings element.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.uec.model.protocol
{
  #region references

  using System;
  using System.Configuration;

  using rt.uec.model.enumerations;

  #endregion

  /// <summary>
  ///   The protocol settings element.
  /// </summary>
  public class ProtocolSettingsElement : ConfigurationElement
  {
    #region Public Properties

    /// <summary>
    ///   Тип
    /// </summary>
    [ConfigurationProperty("Type", IsRequired = true)]
    public ProtocolSettingsEnum Type
    {
      get
      {
        return (ProtocolSettingsEnum)Enum.Parse(typeof(ProtocolSettingsEnum), this["Type"].ToString());
      }
    }

    /// <summary>
    ///   Значение
    /// </summary>
    [ConfigurationProperty("Value", IsRequired = true)]
    public string Value
    {
      get
      {
        return this["Value"].ToString();
      }
    }

    #endregion
  }
}