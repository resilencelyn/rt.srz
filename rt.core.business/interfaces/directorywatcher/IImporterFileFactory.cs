// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImporterFileFactory.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
    /// <summary>
    /// ���������� ExportBatchTyped �� ���������� ����.
    /// </summary>
    /// <param name="file"> </param>
    /// <returns>
    /// �������� 
    /// </returns>
    IImporterFile GetImporterFile(FileInfo file);
  }
}