// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImporterFile.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   ��������� ��������� �������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.directorywatcher
{
  using Quartz;
using System.IO;

  /// <summary>
  ///   ��������� ��������� �������
  /// </summary>
  public interface IImporterFile
  {
    /// <summary>
    ///   ���������
    /// </summary>
    /// <param name="file">
    /// ���� � ����� ��������
    /// </param>
    /// <param name="context"> </param>
    /// <returns> ��� �� ��������� ����� </returns>
    bool Processing(FileInfo file, IJobExecutionContext context);

    /// <summary>
    ///   �������� ����� ���� ���������� ����� ����� ��������
    /// </summary>
    /// <returns> ����� </returns>
    string GetDirectoryToMove();

    /// <summary>
    ///   �������� ����� ���� ���������� ����� � ���������� �������� ������� ��������� ������
    /// </summary>
    /// <returns> ����� </returns>
    string GetFailedDirectoryToMove();

    /// <summary>
    /// �������� �� �������� ��� ������� ���� ���������?
    /// </summary>
    /// <param name="file"> </param>
    /// <returns>
    /// true, ���� ��������, ����� false 
    /// </returns>
    bool AppliesTo(FileInfo file);

    /// <summary>
    /// The undo batches.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    void UndoBatches(string fileName);
  }
}