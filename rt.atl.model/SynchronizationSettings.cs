// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationSettings.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ��������� �������/��������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.model
{
  using System.Configuration;

  /// <summary>
  ///   ��������� �������/��������
  /// </summary>
  public class SynchronizationSettings : ConfigurationSection
  {
    #region Public Properties

    /// <summary>
    ///   WorkingFolderExchange
    /// </summary>
    [ConfigurationProperty("SynchronizationNsi", IsRequired = true)]
    public bool SynchronizationNsi
    {
      get
      {
        return (bool)this["SynchronizationNsi"];
      }

      set
      {
        this["SynchronizationNsi"] = value;
      }
    }

    /// <summary>
    ///   ����� �������� ��������� (�������, �����)
    /// </summary>
    [ConfigurationProperty("SynchronizationToPvp", IsRequired = true)]
    public bool SynchronizationToPvp
    {
      get
      {
        return (bool)this["SynchronizationToPvp"];
      }

      set
      {
        this["SynchronizationToPvp"] = value;
      }
    }

    /// <summary>
    ///   ����� �������� ��������� (�������, �����)
    /// </summary>
    [ConfigurationProperty("SynchronizationToSrz", IsRequired = true)]
    public bool SynchronizationToSrz
    {
      get
      {
        return (bool)this["SynchronizationToSrz"];
      }

      set
      {
        this["SynchronizationToSrz"] = value;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only.
    /// </summary>
    /// <returns> true if the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only; otherwise, false. </returns>
    public override bool IsReadOnly()
    {
      return false;
    }

    #endregion
  }
}