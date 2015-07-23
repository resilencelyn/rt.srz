// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExchangeSettings.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ��������� �������/��������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.configuration
{
  using System.Configuration;

  using rt.core.model.configuration.target;

  /// <summary>
  ///   ��������� �������/��������
  /// </summary>
  public class ExchangeSettings : ConfigurationSection
  {
    #region Public Properties

    /// <summary>
    ///   ����� � ����������� ������
    /// </summary>
    [ConfigurationProperty("BackupOutputFolder", IsRequired = true)]
    public string BackupOutputFolder
    {
      get
      {
        return (string)this["BackupOutputFolder"];
      }

      set
      {
        this["BackupOutputFolder"] = value;
      }
    }

    /// <summary>
    ///   ������� ������� ����� ��������� ��� �������
    /// </summary>
    [ConfigurationProperty("CountThreadImportFiles", IsRequired = false)]
    public int CountThreadImportFiles
    {
      get
      {
        return (int)this["CountThreadImportFiles"];
      }

      set
      {
        this["CountThreadImportFiles"] = value;
      }
    }

    /// <summary>
    ///   Gets the directory watch collection.
    /// </summary>
    [ConfigurationProperty("DirectoryWatchConfiguration")]
    public DirectoryWatchCollection DirectoryWatchCollection
    {
      get
      {
        return base["DirectoryWatchConfiguration"] as DirectoryWatchCollection;
      }
    }

    /// <summary>
    ///   ��������� �� ������������� �� ������
    /// </summary>
    [ConfigurationProperty("DisconnectUsers", IsRequired = false)]
    public bool DisconnectUsers
    {
      get
      {
        return (bool)this["DisconnectUsers"];
      }

      set
      {
        this["DisconnectUsers"] = value;
      }
    }

    /// <summary>
    ///   ���� �� ��������� ��� ������ � ���������� ��������� ������� ��������� ������
    /// </summary>
    [ConfigurationProperty("FailedPath", IsRequired = true)]
    public string FailedPath
    {
      get
      {
        return this["FailedPath"] as string;
      }
    }

    /// <summary>
    ///   ����� �������� ��������� (�������, �����)
    /// </summary>
    [ConfigurationProperty("ProcesingMode", IsRequired = false)]
    public string ProcesingMode
    {
      get
      {
        return (string)this["ProcesingMode"];
      }

      set
      {
        this["ProcesingMode"] = value;
      }
    }

    /// <summary>
    ///   ���� �� ��������� ��� ������� ������������ ������
    /// </summary>
    [ConfigurationProperty("ProcessedPath", IsRequired = true)]
    public string ProcessedPath
    {
      get
      {
        return this["ProcessedPath"] as string;
      }
    }

    /// <summary>
    ///   WorkingFolderExchange
    /// </summary>
    [ConfigurationProperty("WorkingFolderExchange", IsRequired = false)]
    public string WorkingFolderExchange
    {
      get
      {
        return (string)this["WorkingFolderExchange"];
      }

      set
      {
        this["WorkingFolderExchange"] = value;
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