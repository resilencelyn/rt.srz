// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterFile.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.exchange.import
{
  #region

  using System;
  using System.IO;

  using NHibernate;

  using Quartz;

  using rt.core.business.interfaces.directorywatcher;

  using StructureMap;

  #endregion

  /// <summary>
  ///   ������������� ��������� �������
  /// </summary>
  public abstract class ImporterFile : IImporterFile
  {
    #region Fields

    /// <summary>
    ///   �������
    /// </summary>
    protected readonly ISessionFactory SessionFactory;

    /// <summary>
    ///   �������
    /// </summary>
    protected readonly int Subject;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ImporterFile"/> class. 
    ///   �����������
    /// </summary>
    /// <param name="subject">
    /// The subject. 
    /// </param>
    protected ImporterFile(int subject)
    {
      Subject = subject;
      SessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// �������� �� �������� ��� ������� ���� ���������?
    /// </summary>
    /// <param name="file">
    /// </param>
    /// <returns>
    /// true, ���� ��������, ����� false 
    /// </returns>
    public abstract bool AppliesTo(FileInfo file);

    /// <summary>
    ///   �������� ����� ���� ���������� ����� ����� ��������
    /// </summary>
    /// <returns> ����� </returns>
    public virtual string GetDirectoryToMove()
    {
      return string.Empty;
    }

    /// <summary>
    ///   �������� ����� ���� ���������� ����� � ���������� �������� ������� ��������� ������
    /// </summary>
    /// <returns> ����� </returns>
    public virtual string GetFailedDirectoryToMove()
    {
      return string.Empty;
    }

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
    public abstract bool Processing(FileInfo file, IJobExecutionContext context);

    /// <summary>
    /// The undo batches.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    public abstract void UndoBatches(string fileName);

    #endregion

    // ������ �������� ������
    #region Methods

    /// <summary>
    /// The undo batch.
    /// </summary>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    protected abstract bool UndoBatch(Guid batch);

    #endregion
  }
}