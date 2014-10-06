// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImporterFile.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ��������� ��������� �������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.directorywatcher
{
  using System.IO;

  using Quartz;

  /// <summary>
  ///   ��������� ��������� �������
  /// </summary>
  public interface IImporterFile
  {
    #region Public Methods and Operators

    /// <summary>
    /// �������� �� �������� ��� ������� ���� ���������?
    /// </summary>
    /// <param name="file">
    /// </param>
    /// <returns>
    /// true, ���� ��������, ����� false
    /// </returns>
    bool AppliesTo(FileInfo file);

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
    /// ���������
    /// </summary>
    /// <param name="file">
    /// ���� � ����� ��������
    /// </param>
    /// <param name="context">
    /// </param>
    /// <returns>
    /// ��� �� ��������� ����� 
    /// </returns>
    bool Processing(FileInfo file, IJobExecutionContext context);

    /// <summary>
    /// The undo batches.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    void UndoBatches(string fileName);

    #endregion
  }
}