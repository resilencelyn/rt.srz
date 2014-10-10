// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterFileOrganisationBase.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The importer file registry base fabric.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.nsi
{
  using System;
  using System.Collections.Concurrent;
  using System.IO;
  using System.Linq.Expressions;
  using System.Threading.Tasks;

  using NHibernate;
  using NHibernate.Context;

  using Quartz;

  using rt.srz.business.configuration.algorithms.serialization;
  using rt.srz.business.manager;
  using rt.srz.model.Hl7.nsi;
  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  ///   The importer file registry base fabric.
  /// </summary>
  public abstract class ImporterFileOrganisationBase : BaseImportFabric<Organisation>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ImporterFileOrganisationBase"/> class.
    /// </summary>
    /// <param name="subject">
    /// The subject.
    /// </param>
    protected ImporterFileOrganisationBase(int subject)
      : base(subject)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the oid.
    /// </summary>
    protected abstract string Oid { get; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The processing.
    /// </summary>
    /// <param name="file">
    /// The file.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool Processing(FileInfo file, IJobExecutionContext context)
    {
      try
      {
        var oidManager = ObjectFactory.GetInstance<IOidManager>();
        var oid = oidManager.SingleOrDefault(x => x.Id == Oid);
        var queue = new ConcurrentQueue<Organisation>();
        using (var orgCollection = Task.Factory.StartNew(
                                                         () =>
                                                         {
                                                           var session =
                                                             ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
                                                           CurrentSessionContext.Bind(session);
                                                           var res = PrepairXml(file, queue, oid);
                                                           var sessionFactory =
                                                             ObjectFactory.GetInstance<ISessionFactory>();
                                                           session = CurrentSessionContext.Unbind(sessionFactory);

                                                           if (session != null)
                                                           {
                                                             if (session.Transaction != null
                                                                 && session.Transaction.IsActive)
                                                             {
                                                               session.Transaction.Dispose();
                                                             }

                                                             session.Flush();
                                                             session.Clear();
                                                             session.Close();
                                                             session.Dispose();
                                                           }

                                                           return res;
                                                         }))
        {
          return SaveToBase(queue, orgCollection, context);
        }
      }
      catch (Exception)
      {
        return false;
      }
    }

    /// <summary>
    /// Отмена загрузки пакетов
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
    protected override Expression<Func<Organisation, bool>> KeyValuePair(Organisation computedEntity)
    {
      return x => x.Oid.Id == Oid && x.Code == computedEntity.Code;
    }

    /// <summary>
    /// The prepair list.
    /// </summary>
    /// <param name="packet">
    /// The packet.
    /// </param>
    /// <param name="cqtasks">
    /// The cqtasks.
    /// </param>
    /// <param name="oid">
    /// The oid.
    /// </param>
    protected abstract void PrepairList(Packet packet, ConcurrentQueue<Organisation> cqtasks, Oid oid);

    /// <summary>
    /// Отмена загрузки пакета
    /// </summary>
    /// <param name="batch">
    /// Пакет
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    protected override bool UndoBatch(Guid batch)
    {
      return true;
    }

    /// <summary>
    /// The prepair xml.
    /// </summary>
    /// <param name="file">
    /// The file.
    /// </param>
    /// <param name="cqtasks">
    /// The cq tasks.
    /// </param>
    /// <param name="oid">
    /// The oid.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    private int PrepairXml(FileInfo file, ConcurrentQueue<Organisation> cqtasks, Oid oid)
    {
      var packet = new Packet();
      packet = (Packet)XmlSerializationHelper.Deserialize(file.FullName, packet);
      PrepairList(packet, cqtasks, oid);

      return cqtasks.Count;
    }

    #endregion
  }
}