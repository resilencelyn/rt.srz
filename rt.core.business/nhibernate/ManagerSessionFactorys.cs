// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagerSessionFactorys.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The manager session factorys.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.nhibernate
{
  #region references

  using System;
  using System.Collections.Generic;

  using NHibernate;

  #endregion

  /// <summary>
  ///   The manager session factorys.
  /// </summary>
  public class ManagerSessionFactorys : IManagerSessionFactorys
  {
    #region Fields

    /// <summary>
    ///   The dictionary.
    /// </summary>
    private readonly Dictionary<string, ISessionFactory> dictionary;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ManagerSessionFactorys" /> class.
    /// </summary>
    public ManagerSessionFactorys()
    {
      dictionary = new Dictionary<string, ISessionFactory>(1);
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the get current factory.
    /// </summary>
    public ISessionFactory GetCurrentFactory
    {
      get
      {
        return dictionary["default"];
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The add session factory.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    /// <exception cref="Exception">
    /// </exception>
    public void AddSessionFactory(string name, ISessionFactory sessionFactory)
    {
      if ("default" == name)
      {
        throw new Exception("Для добавления фабрики по умолчанию используйте метод SetDefaultFactory.");
      }

      dictionary.Add(name, sessionFactory);
    }

    /// <summary>
    /// The get factory by name.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="ISessionFactory"/>.
    /// </returns>
    public ISessionFactory GetFactoryByName(string name)
    {
      ISessionFactory factory;
      return dictionary.TryGetValue(name, out factory) ? factory : null;
    }

    /// <summary>
    /// The set default factory.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    /// <exception cref="Exception">
    /// </exception>
    public void SetDefaultFactory(ISessionFactory sessionFactory)
    {
      if (dictionary.ContainsKey("default"))
      {
        throw new Exception("Фабрика по умолчанию уже создана.");
      }

      dictionary.Add("default", sessionFactory);
    }

    #endregion
  }
}