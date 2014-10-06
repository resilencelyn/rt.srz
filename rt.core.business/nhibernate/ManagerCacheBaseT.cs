// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagerCacheBaseT.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Менеджер для кэшированных данных
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.nhibernate
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;
  using System.ServiceModel;
  using System.Web;

  using NHibernate;
  using NHibernate.Metadata;

  using StructureMap;

  #endregion

  /// <summary>
  /// Менеджер для кэшированных данных
  /// </summary>
  /// <typeparam name="TClass">
  /// тип базового объекта
  /// </typeparam>
  /// <typeparam name="TKey">
  /// Тип ключа
  /// </typeparam>
  public class ManagerCacheBaseT<TClass, TKey> : IManagerCacheBaseT<TClass, TKey>
    where TClass : class
  {
    #region Static Fields

    /// <summary>
    ///   The state.
    /// </summary>
    [ThreadStatic]
    private static StateExtension state;

    #endregion

    #region Fields

    /// <summary>
    ///   The repository.
    /// </summary>
    protected readonly IManagerBase<TClass, TKey> Repository;

    /// <summary>
    ///   The session factory.
    /// </summary>
    protected readonly ISessionFactory SessionFactory;

    /// <summary>
    ///   The class metadata.
    /// </summary>
    private readonly IClassMetadata classMetadata;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ManagerCacheBaseT{TClass,TKey}"/> class.
    /// </summary>
    /// <param name="repository">
    /// The repository.
    /// </param>
    public ManagerCacheBaseT(IManagerBase<TClass, TKey> repository)
    {
      Repository = repository;
      SessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
      TimeSpan = new TimeSpan(0, 0, 30, 0);
      classMetadata = SessionFactory.GetClassMetadata(typeof(TClass));
      TimeQueryDb = new DateTime(1900, 1, 1);
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the cache.
    /// </summary>
    protected IList<TClass> Cache
    {
      get
      {
        if ((DateTime.Now - TimeQueryDb) > TimeSpan || State.Cache == null)
        {
          Cache = Repository.GetAll(int.MaxValue);
          TimeQueryDb = DateTime.Now;
        }

        return State.Cache;
      }

      set
      {
        State.Cache = value;
      }
    }

    /// <summary>
    ///   Gets or sets the time query db.
    /// </summary>
    protected DateTime TimeQueryDb
    {
      get
      {
        return State.TimeQueryDb;
      }

      set
      {
        State.TimeQueryDb = value;
      }
    }

    /// <summary>
    ///   На сколько времени будем кэшировать данные из БД
    /// </summary>
    protected TimeSpan TimeSpan { get; set; }

    /// <summary>
    ///   Gets the state.
    /// </summary>
    private StateExtension State
    {
      get
      {
        // Работаем из http сессии
        if (HttpContext.Current != null)
        {
          var extension = (StateExtension)HttpContext.Current.Session[typeof(TClass).Name + ".State"];
          if (extension == null)
          {
            extension = new StateExtension();
            HttpContext.Current.Session[typeof(TClass).Name + ".State"] = extension;
          }

          return extension;
        }

        // Работаем из wcf сессии
        if (OperationContext.Current != null)
        {
          var extension = OperationContext.Current.Extensions.Find<StateExtension>();

          if (extension == null)
          {
            extension = new StateExtension();
            OperationContext.Current.Extensions.Add(extension);
          }

          return extension;
        }

        // работаем под обычным потоком
        return state ?? (state = new StateExtension());
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Получение всех по условию
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// список удовлетворяющих условию
    /// </returns>
    public virtual IList<TClass> GetBy(Expression<Func<TClass, bool>> expression)
    {
      return Cache.Where(expression.Compile()).ToList();
    }

    /// <summary>
    /// The get by id.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="TClass"/> .
    /// </returns>
    public TClass GetById(TKey id)
    {
      return GetBy(x => Equals(classMetadata.GetIdentifier(x, EntityMode.Poco), id)).FirstOrDefault();
    }

    /// <summary>
    ///   Сбросить кэш, чтобы при следующем обращении запрос пошел в базу а не к кэшу.
    /// </summary>
    public void Refresh()
    {
      TimeQueryDb = new DateTime(1900, 1, 1);
      if (Cache != null)
      {
        Cache.Clear();
      }
    }

    /// <summary>
    /// Получение одного по условию
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// список удовлетворяющих условию
    /// </returns>
    public TClass Single(Expression<Func<TClass, bool>> expression)
    {
      return GetBy(expression).Single();
    }

    /// <summary>
    /// Получение одного по условию
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// список удовлетворяющих условию
    /// </returns>
    public TClass SingleOrDefault(Expression<Func<TClass, bool>> expression)
    {
      return GetBy(expression).SingleOrDefault();
    }

    /// <summary>
    /// The unproxy.
    /// </summary>
    /// <param name="proxy">
    /// The proxy.
    /// </param>
    /// <returns>
    /// The <see cref="TClass"/>.
    /// </returns>
    public TClass Unproxy(TClass proxy)
    {
      return GetById((TKey)classMetadata.GetIdentifier(proxy, EntityMode.Poco));
    }

    #endregion

    /// <summary>
    ///   The state extension.
    /// </summary>
    public class StateExtension : IExtension<OperationContext>
    {
      #region Public Properties

      /// <summary>
      ///   The cache.
      /// </summary>
      public IList<TClass> Cache { get; set; }

      /// <summary>
      ///   The time query db.
      /// </summary>
      public DateTime TimeQueryDb { get; set; }

      #endregion

      // we don't really need implementations for these methods in this case
      #region Public Methods and Operators

      /// <summary>
      /// The attach.
      /// </summary>
      /// <param name="owner">
      /// The owner.
      /// </param>
      public void Attach(OperationContext owner)
      {
      }

      /// <summary>
      /// The detach.
      /// </summary>
      /// <param name="owner">
      /// The owner.
      /// </param>
      public void Detach(OperationContext owner)
      {
      }

      #endregion
    }
  }
}