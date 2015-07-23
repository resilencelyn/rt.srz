// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.fias.services
{
  using System;
  using System.Web;

  using rt.core.model;

  /// <summary>
  ///   The global.
  /// </summary>
  public class Global : HttpApplication
  {
    #region Methods

    /// <summary>
    /// The application_ acquire request state.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Application_AcquireRequestState(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// The application_ begin request.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Application_BeginRequest(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// The application_ end.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Application_End(object sender, EventArgs e)
    {
      Bootstrapper.Stop();
    }

    /// <summary>
    /// The application_ end request.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Application_EndRequest(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// The application_ error.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Application_Error(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// The application_ start.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Application_Start(object sender, EventArgs e)
    {
      Bootstrapper.Bootstrap();
    }

    /// <summary>
    /// The session_ end.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Session_End(object sender, EventArgs e)
    {
      // Code that runs when a session ends. 
      // Note: The Session_End event is raised only when the sessionstate mode
      // is set to InProc in the Web.config file. If session mode is set to StateServer 
      // or SQLServer, the event is not raised.
    }

    /// <summary>
    /// The session_ start.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Session_Start(object sender, EventArgs e)
    {
      // Code that runs when a new session is started
    }

    #endregion
  }
}