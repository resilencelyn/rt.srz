// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PoolNhibernateSection.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The Pool nhibernate section.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.configuration
{
  #region references

  using System.Configuration;

  #endregion

  /// <summary>
  ///   The Pool nhibernate section.
  /// </summary>
  public class PoolNhibernateSection : ConfigurationSection
  {
    #region Public Properties

    /// <summary>
    ///   Gets the file name configaration.
    /// </summary>
    [ConfigurationProperty("FileNameConfigaration")]
    public FileNameElementColection FileNameConfigaration
    {
      get
      {
        return base["FileNameConfigaration"] as FileNameElementColection;
      }
    }

    #endregion
  }
}