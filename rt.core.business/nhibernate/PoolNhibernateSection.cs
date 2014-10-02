// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PoolNhibernateSection.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The Pool nhibernate section.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.nhibernate
{
  #region references

  using System.Configuration;

  using rt.core.business.nhibernate.target;

  #endregion

  /// <summary>
  ///   The Pool nhibernate section.
  /// </summary>
  public class PoolNhibernateSection : ConfigurationSection
  {
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
  }
}