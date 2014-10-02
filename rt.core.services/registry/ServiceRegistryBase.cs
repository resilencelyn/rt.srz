// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceRegistryBase.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Linq;
using System.ServiceModel;
using NLog;
using StructureMap.Configuration.DSL;

using rt.core.services.aspects;

#endregion

namespace rt.core.services.registry
{
  using rt.core.model;

  /// <summary>
  /// Базовый класс регистрации
  /// </summary>
  /// <typeparam name="I">
  /// Интерфейс сервиса 
  /// </typeparam>
  /// <typeparam name="S">
  /// Сервис 
  /// </typeparam>
  /// <typeparam name="G">
  /// Шлюз 
  /// </typeparam>
  public abstract class ServiceRegistryBase<I, S, G> : Registry, IServiceRegistry
    where S : I
    where G : InterceptedBase, new()
  {
    #region Static Fields

    /// <summary>
    ///   Логгер
    /// </summary>
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    #endregion

    #region Fields

    /// <summary>
    ///   Дата начала старта
    /// </summary>
    private readonly DateTime begin;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ServiceRegistryBase{I,S,G}" /> class. 
    ///   Конструктор
    /// </summary>
    protected ServiceRegistryBase()
    {
      if (ServiceRegistryManager.ServiceHosts.Any(x => x.GetType() == GetType()))
      {
        logger.Info("Service alredy running.");
        return;
      }

      logger.Info("Starting WCF service [{0}]:[{1}]:[{2}]", typeof(S).Name, typeof(G).Name, typeof(I).Name);
      begin = DateTime.Now;
      if (Host != null)
      {
        logger.Info("Service alredy running. Shutting down first.");
        Host.Close();
      }

      Host = new ServiceHost(typeof(G));
      Host.Opened += HostOpened;
      Host.Open();
      ServiceRegistryManager.ServiceHosts.Add(this);
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Хост
    /// </summary>
    public ServiceHost Host { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// The shutdown.
    /// </summary>
    /// <param name="span">
    /// The span. 
    /// </param>
    public void Shutdown(TimeSpan span)
    {
      Host.Close(span);
    }

    /// <summary>
    /// Сервер создан
    /// </summary>
    /// <param name="sender">
    /// Сендер 
    /// </param>
    /// <param name="e">
    /// Параметры 
    /// </param>
    private void HostOpened(object sender, EventArgs e)
    {
      var end = DateTime.Now;
      logger.Info("WCF service [{0}]:[{1}]:[{2}]", typeof(S).Name, typeof(G).Name, typeof(I).Name, end - begin);
    }

    #endregion
  }
}