// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseImportFabric.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The base import fabric.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import
{
  using System;
  using System.Collections.Concurrent;
  using System.IO;
  using System.Linq;
  using System.Linq.Expressions;
  using System.Text;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Xml.Serialization;

  using NHibernate;

  using Quartz;

  using rt.core.business.server.exchange.import;
  using rt.core.model;

  using StructureMap;

  /// <summary>
  /// The base import fabric.
  /// </summary>
  /// <typeparam name="T">
  /// Тип
  /// </typeparam>
  public abstract class BaseImportFabric<T> : ImporterFile
    where T : BusinessBase<Guid>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseImportFabric{T}"/> class.
    /// </summary>
    /// <param name="subject">
    /// The subject.
    /// </param>
    protected BaseImportFabric(int subject)
      : base(subject)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// The deserialize.
    /// </summary>
    /// <param name="encoding">
    /// The encoding.
    /// </param>
    /// <param name="queueElement">
    /// The queue element.
    /// </param>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    protected T Deserialize(Encoding encoding, string queueElement)
    {
      var ser = new XmlSerializer(typeof(T));
      Stream stream = new MemoryStream(encoding.GetBytes(queueElement));
      stream.Position = 0;
      return (T)ser.Deserialize(stream);
    }

    /// <summary>
    /// The key value pair.
    /// </summary>
    /// <param name="computedEntity">
    /// The computed entity.
    /// </param>
    /// <returns>
    /// The <see cref="Expression"/>.
    /// </returns>
    protected abstract Expression<Func<T, bool>> KeyValuePair(T computedEntity);

    /// <summary>
    /// The save to base.
    /// </summary>
    /// <param name="entities">
    /// The entities.
    /// </param>
    /// <param name="prepairXml">
    /// The prepair xml.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    protected bool SaveToBase(ConcurrentQueue<T> entities, Task<int> prepairXml, IJobExecutionContext context)
    {
      try
      {
        var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

        for (var i = 1; entities.Count > 0 || !prepairXml.IsCompleted; i++)
        {
          T computedEntity;
          if (!entities.TryDequeue(out computedEntity))
          {
            Thread.Sleep(1000);
            i--;
            continue;
          }

          var expression = KeyValuePair(computedEntity);
          var savedEntities = session.QueryOver<T>().Where(expression).Take(1).List().SingleOrDefault();
          computedEntity.Id = savedEntities != null ? savedEntities.Id : Guid.NewGuid();
          session.Replicate(typeof(T).Name, computedEntity, ReplicationMode.Overwrite);

          if (i % 10000 == 0)
          {
            session.Flush();
          }

          context.JobDetail.JobDataMap["progress"] = (int)(i / (double)(entities.Count() + i) * 100);
        }

        session.Flush();
        return true;
      }
      catch
      {
        return false;
      }
    }

    #endregion
  }
}