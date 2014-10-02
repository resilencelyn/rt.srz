// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolSettingsCollection.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The protocol settings collection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.uec.model.protocol
{
  #region references

  using System.Configuration;

  #endregion

  /// <summary>
  ///   The protocol settings collection.
  /// </summary>
  public class ProtocolSettingsCollection : ConfigurationElementCollection
  {
    #region Public Indexers

    /// <summary>
    /// The this.
    /// </summary>
    /// <param name="index">
    /// The index.
    /// </param>
    /// <returns>
    /// The <see cref="ProtocolSettingsElement"/>.
    /// </returns>
    public ProtocolSettingsElement this[int index]
    {
      get
      {
        return BaseGet(index) as ProtocolSettingsElement;
      }

      set
      {
        if (BaseGet(index) != null)
        {
          BaseRemoveAt(index);
        }

        BaseAdd(index, value);
      }
    }

    #endregion

    #region Methods

    /// <summary>
    ///   The create new element.
    /// </summary>
    /// <returns>
    ///   The <see cref="System.Configuration.ConfigurationElement" />.
    /// </returns>
    protected override ConfigurationElement CreateNewElement()
    {
      return new ProtocolSettingsElement();
    }

    /// <summary>
    /// The get element key.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((ProtocolSettingsElement)element).Type;
    }

    #endregion
  }
}