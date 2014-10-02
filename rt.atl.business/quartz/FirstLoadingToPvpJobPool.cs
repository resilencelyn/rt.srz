// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirstLoadingToPvpJobPool.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.quartz
{
  #region

  using System.Collections.Generic;

  using rt.atl.model.atl;
  using rt.core.business.nhibernate;
  using rt.srz.business.server;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The task calculating pool.
  /// </summary>
  public class FirstLoadingToPvpJobPool// : JobPoolDatabase<>
  {
    #region Constants

    /// <summary>
    ///   К-во записей для одной работы
    /// </summary>
    private const int JobRecordCount = 5000;

    #endregion

    #region Static Fields

    /// <summary>
    ///   The instance.
    /// </summary>
    private static FirstLoadingToPvpJobPool instance;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes static members of the <see cref="CalculateKeysPool" /> class.
    /// </summary>
    static FirstLoadingToPvpJobPool()
    {
      LockObject = "FirstLoadingToPvpJobPool";
    }

    /// <summary>
    ///   Prevents a default instance of the <see cref="CalculateKeysPool" /> class from being created.
    /// </summary>
    private FirstLoadingToPvpJobPool()
    {
      Queue = new Queue<FirstLoadingToPvpJobInfo>();
      ExecutingList = new List<FirstLoadingToPvpJobInfo>();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the instance.
    /// </summary>
    public static FirstLoadingToPvpJobPool Instance
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
    public List<FirstLoadingToPvpJobInfo> ExecutingList { get; private set; }

    /// <summary>
    ///   Gets the queue.
    /// </summary>
    public Queue<FirstLoadingToPvpJobInfo> Queue { get; private set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Init
    /// </summary>
    public void CreateQueue()
    {
      var sessionFactorySrz = ObjectFactory.GetInstance<IManagerSessionFactorys>().GetFactoryByName("NHibernateCfgAtl.xml");
      var peopleCount = 0;
      using (var sessionAtl = sessionFactorySrz.OpenStatelessSession())
      {
        // разбиение на несколько этапов
        peopleCount = sessionAtl.QueryOver<person>().Where(x => !x.IsExported).RowCount();
      }

      var jobCount = peopleCount / JobRecordCount;
      var divCount = peopleCount - jobCount * JobRecordCount;

      lock (LockObject)
      {
        using (var sessionAtl = sessionFactorySrz.OpenStatelessSession())
        {
          // разбиение на несколько этапов
          var min = 0;
          var max = 0;
          // Ставим задачи в очередь
          for (var jobIndex = 0; jobIndex <= jobCount; jobIndex++)
          {

            max = sessionAtl.CreateSQLQuery(string.Format("select MAX(id) from ( select top {0} ID from people where ID > :min and IsExported = 0 order by Id) t", JobRecordCount))
                        .SetTimeout(int.MaxValue)
                        .SetParameter("min", min)
                        .UniqueResult<int>();

            Queue.Enqueue(new FirstLoadingToPvpJobInfo { Min = min, Max = max });
            min = max;
          }
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The init.
    /// </summary>
    /// <returns>
    /// The <see cref="FirstLoadingToPvpJobPool"/>.
    /// </returns>
    private static FirstLoadingToPvpJobPool Init()
    {
      instance = new FirstLoadingToPvpJobPool();
      return instance;
    }

    #endregion
  }
}