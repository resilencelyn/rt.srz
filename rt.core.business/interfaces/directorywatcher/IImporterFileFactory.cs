// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImporterFileFactory.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ������� ��� ���������� �������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.directorywatcher
{
  using System.IO;

  /// <summary>
  ///   ������� ��� ���������� �������
  /// </summary>
  public interface IImporterFileFactory
  {
    #region Public Methods and Operators

    /// <summary>
    /// ���������� ExporterBatchTyped �� ���������� ����.
    /// </summary>
    /// <param name="file">
    /// </param>
    /// <returns>
    /// ��������
    /// </returns>
    IImporterFile GetImporterFile(FileInfo file);

    #endregion
  }
}