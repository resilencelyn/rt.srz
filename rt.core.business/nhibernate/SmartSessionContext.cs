// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmartSessionContext.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The smart session context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.nhibernate
{
  #region references

  using System;
  using System.Collections;
  using System.ServiceModel;
  using System.Web;

  using NHibernate;
  using NHibernate.Context;
  using NHibernate.Engine;

  #endregion

  /// <summary>
  ///   The smart session context.
  /// </summary>
  public class SmartSessionContext : MapBasedSessionContext
  {
    #region Constants

    /// <summary>
    ///   The session factory map key.
    /// </summary>
    private const string SessionFactoryMapKey = "NHibernate.Context.WebSessionContext.SessionFactoryMapKey";

    #endregion

    #region Static Fields

    /// <summary>
    ///   The _session.
    /// </summary>
    [ThreadStatic]
    private static ISession _session;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SmartSessionContext"/> class.
    /// </summary>
    /// <param name="factory">
    /// The factory.
    /// </param>
    public SmartSessionContext(ISessionFactoryImplementor factory)
      : base(factory)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the session.
    /// </summary>
    protected override ISession Session
    {
      get
      {
        if (HttpContext.Current == null && OperationContext.Current == null)
        {
          return _session;
        }

        return base.Session;
      }

      set
      {
        if (HttpContext.Current == null && OperationContext.Current == null)
        {
          _session = value;
        }
        else
        {
          base.Session = value;
        }
      }
    }

    /// <summary>
    ///   Gets the wcf operation state.
    /// </summary>
    private static WcfStateExtension WcfOperationState
    {
      get
      {
        var extension = OperationContext.Current.Extensions.Find<WcfStateExtension>();

        if (extension == null)
        {
          extension = new WcfStateExtension();
          OperationContext.Current.Extensions.Add(extension);
        }

        return extension;
      }
    }

    #endregion

    #region Methods

    /// <summary>
    ///   The get map.
    /// </summary>
    /// <returns>
    ///   The <see cref="IDictionary" />.
    /// </returns>
    protected override IDictionary GetMap()
    {
      if (HttpContext.Current != null)
      {
        return ReflectiveHttpContext.HttpContextCurrentItems[SessionFactoryMapKey] as IDictionary;
      }

      if (ServiceSecurityContext.Current != null || OperationContext.Current != null)
      {
        return WcfOperationState.Map;
      }

      return null;
    }

    /// <summary>
    /// The set map.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    protected override void SetMap(IDictionary value)
    {
      if (HttpContext.Current != null)
      {
        ReflectiveHttpContext.HttpContextCurrentItems[SessionFactoryMapKey] = value;
        return;
      }

      if (ServiceSecurityContext.Current != null || OperationContext.Current != null)
      {
        WcfOperationState.Map = value;
      }
    }

    #endregion
  }
}