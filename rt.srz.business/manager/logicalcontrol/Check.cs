// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Check.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The checkstatement.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol
{
  #region references

  using System;
  using System.Linq.Expressions;

  using NHibernate;

  using rt.srz.business.manager.cache;
  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces;
  using rt.srz.model.srz;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The checkstatement.
  /// </summary>
  public abstract class Check : ICheckStatement
  {
    #region Fields

    /// <summary>
    ///   The session factory.
    /// </summary>
    protected readonly ISessionFactory SessionFactory;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Check"/> class.
    ///   Initializes a new instance of the <see cref="CheckObject{T}"/> class.
    /// </summary>
    /// <param name="level">
    /// The level.
    /// </param>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    protected Check(CheckLevelEnum level, ISessionFactory sessionFactory)
    {
      SessionFactory = sessionFactory;
      Level = level;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Check"/> class.
    /// </summary>
    /// <param name="level">
    /// The level.
    /// </param>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    /// <param name="expression">
    /// The expression.
    /// </param>
    protected Check(
      CheckLevelEnum level, 
      ISessionFactory sessionFactory, 
      Expression<Func<Statement, object>> expression)
      : this(level, sessionFactory)
    {
      Expression = expression;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Можно ли включать\отключать свойство
    /// </summary>
    public virtual bool AllowChange
    {
      get
      {
        // если в таблице settings не нашли соответсвующую запись, то считаем что проверку можно включать/выключать 
        var setting =
          ObjectFactory.GetInstance<ICheckCacheManager>().GetByClassNameOnly(GetAllowChangeName(GetType().Name));
        if (setting == null)
        {
          return true;
        }

        if (setting.ValueString == "0")
        {
          return false;
        }

        return true;
      }
    }

    /// <summary>
    ///   Название для отображения проверки
    /// </summary>
    public abstract string Caption { get; }

    /// <summary>
    ///   Проверять или нет свойство
    /// </summary>
    public virtual bool CheckRequired
    {
      get
      {
        // если в таблице settings не нашли соответсвующую запись, то считаем что проверка включена, т.е. надо проверять свойство
        var setting = ObjectFactory.GetInstance<ICheckCacheManager>().GetByClassName(GetType().Name);
        if (setting == null)
        {
          return true;
        }

        if (setting.ValueString == "0")
        {
          return false;
        }

        return true;
      }
    }

    /// <summary>
    ///   для грида чтобы исползовать в кастве ключа
    /// </summary>
    public string ClassName
    {
      get
      {
        return GetType().Name;
      }
    }

    /// <summary>
    ///   Gets the expression.
    /// </summary>
    public Expression<Func<Statement, object>> Expression { get; private set; }

    /// <summary>
    ///   The level.
    /// </summary>
    public CheckLevelEnum Level { get; private set; }

    /// <summary>
    ///   The level description.
    /// </summary>
    public string LevelDescription
    {
      get
      {
        switch (Level)
        {
          case CheckLevelEnum.Simple:
            return Resource.LevelCaption_Simple;
          case CheckLevelEnum.Complex:
            return Resource.LevelCaption_Complex;
          case CheckLevelEnum.External:
            return Resource.LevelCaption_External;
          default:
            return Resource.LevelCaption_Simple;
        }
      }
    }

    /// <summary>
    ///   Номер записи
    /// </summary>
    public int RecordNumber { get; set; }

    /// <summary>
    ///   Видимость проверки в списке на странице ( не учитывается если есть права на отображение пункта меню установка - в
    ///   этом случае отображаются все)
    /// </summary>
    public virtual bool Visible
    {
      get
      {
        return false;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The get allow change name.
    /// </summary>
    /// <param name="validatorName">
    /// The validator name.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetAllowChangeName(string validatorName)
    {
      // в базе когда админ главный убирает галочку с AllowChange - 
      // возможность включать-выключать свойство (только он может это делать), в базу добавляется запись лдя валидатора с префиксом к имени AllowChange
      return string.Format("{0}_{1}", validatorName, "AllowChange");
    }

    /// <summary>
    /// The check.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public abstract void CheckObject(Statement statement);

    #endregion
  }
}