// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecialCalendarDayConfigurationCollection.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The special calendar day configuration collection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.configSection.productioncalendar
{
  using System.Configuration;

  /// <summary>
  /// The special calendar day configuration collection.
  /// </summary>
  public class SpecialCalendarDayConfigurationCollection : ConfigurationElementCollection
  {
    #region Public Indexers

    /// <summary>
    /// The this.
    /// </summary>
    /// <param name="index">
    /// The index.
    /// </param>
    /// <returns>
    /// The <see cref="SpecialCalendarDayConfigurationElement"/>.
    /// </returns>
    public SpecialCalendarDayConfigurationElement this[int index]
    {
      get
      {
        return this.BaseGet(index) as SpecialCalendarDayConfigurationElement;
      }

      set
      {
        if (this.BaseGet(index) != null)
        {
          this.BaseRemoveAt(index);
        }

        BaseAdd(index, value);
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The create new element.
    /// </summary>
    /// <returns>
    /// The <see cref="ConfigurationElement"/>.
    /// </returns>
    protected override ConfigurationElement CreateNewElement()
    {
      return new SpecialCalendarDayConfigurationElement();
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
      return ((SpecialCalendarDayConfigurationElement)element).Date;
    }

    #endregion
  }
}