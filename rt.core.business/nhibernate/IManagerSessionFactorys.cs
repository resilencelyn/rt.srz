// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IManagerSessionFactorys.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ManagerSessionFactorys interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.nhibernate
{
  #region references

  using NHibernate;

  #endregion

  /// <summary>
  ///   The ManagerSessionFactorys interface.
  /// </summary>
  public interface IManagerSessionFactorys
  {
    #region Public Properties

    /// <summary>
    ///   Gets the get current factory.
    /// </summary>
    ISessionFactory GetCurrentFactory { get; }

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
    void AddSessionFactory(string name, ISessionFactory sessionFactory);

    /// <summary>
    /// The get factory by name.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="ISessionFactory"/>.
    /// </returns>
    ISessionFactory GetFactoryByName(string name);

    /// <summary>
    /// The set default factory.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    void SetDefaultFactory(ISessionFactory sessionFactory);

    #endregion
  }
}