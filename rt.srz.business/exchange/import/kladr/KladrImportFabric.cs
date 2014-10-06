// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KladrImportFabric.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The base import batch pfr.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.kladr
{
  using System;
  using System.Collections.Concurrent;
  using System.IO;
  using System.Linq.Expressions;
  using System.Threading.Tasks;

  using NLog;

  using Quartz;

  using rt.srz.business.manager;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  ///   The base import batch pfr.
  /// </summary>
  public class KladrImportFabric : BaseImportFabric<Kladr>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="KladrImportFabric"/> class. 
    ///   Initializes a new instance of the <see cref="BaseImporterFileQueryResponse{TXmlObj,TEnumerableItem}"/> class.
    /// </summary>
    public KladrImportFabric()
      : base(TypeSubject.Kladr)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The applies to.
    /// </summary>
    /// <param name="file">
    /// The file.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool AppliesTo(FileInfo file)
    {
      return file.Name.Equals("KLADR.DBF") || file.Name.Equals("STREET.DBF");
    }

    /// <summary>
    /// Обработка
    /// </summary>
    /// <param name="file">
    /// Путь к файлу загрузки
    /// </param>
    /// <param name="context">
    /// Контекст
    /// </param>
    /// <returns>
    /// был ли обработан пакет
    /// </returns>
    public override bool Processing(FileInfo file, IJobExecutionContext context)
    {
      try
      {
        var kladrCq = new ConcurrentQueue<Kladr>();
        using (var kladrCollection = Task.Factory.StartNew(() => ParseDbf.ReadDbf(file.FullName, kladrCq)))
        {
          var kladrManager = ObjectFactory.GetInstance<IKladrManager>();
          SaveToBase(kladrCq, kladrCollection, context);
        }

        // построение иерархии
        ObjectFactory.GetInstance<IExecuteStoredManager>().CalculateKladrLevelAndParrentId();

        return true;
      }
      catch (Exception ex)
      {
        LogManager.GetCurrentClassLogger().ErrorException("Ошибка загрузки файла", ex);
        return false;
      }
    }

    /// <summary>
    /// The undo batches.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    public override void UndoBatches(string fileName)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// The key value pair.
    /// </summary>
    /// <param name="computedEntity">
    /// The computed entity.
    /// </param>
    /// <returns>
    /// The <see cref="Expression"/>.
    /// </returns>
    protected override Expression<Func<Kladr, bool>> KeyValuePair(Kladr computedEntity)
    {
      return x => x.Code == computedEntity.Code;
    }

    /// <summary>
    /// The undo batch.
    /// </summary>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    protected override bool UndoBatch(Guid batch)
    {
      return true;
    }

    #endregion
  }
}