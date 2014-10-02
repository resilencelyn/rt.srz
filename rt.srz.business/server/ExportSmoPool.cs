﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using NHibernate;
using NHibernate.Criterion;
using rt.srz.model.srz;

namespace rt.srz.business.server
{
  public class ExportSmoPool
  {
    #region Constants

    /// <summary>
    /// Максимальное к-во сообщений в одном батче
    /// </summary>
    private const int MaxCountMessageInBatch = 5000;

    #endregion

    #region Static Fields

    /// <summary>
    ///   The instance.
    /// </summary>
    private static ExportSmoPool instance;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes static members of the <see cref="ExportSmoPool" /> class.
    /// </summary>
    static ExportSmoPool()
    {
      LockObject = "ExportSmoPool";
    }

    /// <summary>
    ///   Prevents a default instance of the <see cref="ExportSmoPool" /> class from being created.
    /// </summary>
    private ExportSmoPool()
    {
      Queue = new Queue<ExportSmoJobInfo>();
      ExecutingList = new List<ExportSmoJobInfo>();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the instance.
    /// </summary>
    public static ExportSmoPool Instance
    {
      get
      {
        return instance ?? Init();
      }
    }

    /// <summary>
    ///   Gets the lock object.
    /// </summary>
    public static object LockObject { get; private set; }

    /// <summary>
    ///   Gets or sets the executing list.
    /// </summary>
    public List<ExportSmoJobInfo> ExecutingList { get; private set; }

    /// <summary>
    ///   Gets the queue.
    /// </summary>
    public Queue<ExportSmoJobInfo> Queue { get; private set; }

    #endregion

    #region Methods

    /// <summary>
    /// The init.
    /// </summary>
    /// <returns>
    /// The <see cref="ExportSmoPool"/>.
    /// </returns>
    private static ExportSmoPool Init()
    {
      instance = new ExportSmoPool();
      return instance;
    }

    #endregion
  }
}