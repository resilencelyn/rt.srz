// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp
{
  using System;
  using System.Linq;
  using System.Web;
  using System.Web.Management;
  using System.Web.Security;

  using NHibernate;
  using NHibernate.Context;

  using NLog;

  using rt.core.model;
  using rt.core.model.core;
  using rt.core.model.interfaces;
  using rt.srz.model.interfaces.service;
  using rt.srz.ui.pvp.Enumerations;

  using StructureMap;

  using User = rt.core.model.core.User;

  /// <summary>
  ///   The global.
  /// </summary>
  public class Global : HttpApplication
  {
    #region Fields

    /// <summary>
    /// The _redirect to offline.
    /// </summary>
    private bool redirectToOffline;

    #endregion

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
      User currentUser = null;
      try
      {
        currentUser = ObjectFactory.GetInstance<ISecurityService>().GetCurrentUser();
      }
      catch (Exception)
      {
      }

      if (currentUser == null)
      {
        return;
      }

      // Получаем время окончания последней синхронизации
      var syncTimeSrz2Pvp = ObjectFactory.GetInstance<IStatementService>().GetSettingCurrentUser("ExporterToPvp_Finish");

      DateTime? syncTime = null;
      if (syncTimeSrz2Pvp != null)
      {
        DateTime temp;
        if (DateTime.TryParse(syncTimeSrz2Pvp, out temp))
        {
          syncTime = temp;
        }
      }

      // Если время логина меньше времени окончания синхронизации из СРЗ в ПВП,
      // то делаем логофф текущего пользователя
      if (syncTime != null && currentUser.LastLoginDate < syncTime)
      {
        if (HttpContext.Current.Session != null)
        {
          HttpContext.Current.Session.Abandon();
        }

        FormsAuthentication.SignOut();
        HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl);
        HttpContext.Current.Response.End();
      }
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
      // Открываем сессию к базе
      var session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
      CurrentSessionContext.Bind(session);

      // Проверяем, не находится ли сайт на обслуживании
      ProcessOffline();
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
      if (ProcessOffline())
      {
        return;
      }

      ////if (!SiteMode.IsOnline)
      ////{
      ////  если сайт на тех обслуживании то перенаправляем с формы логина на соотвествующую страницу
      ////  if (!string.IsNullOrEmpty(Response.RedirectLocation) && Response.RedirectLocation.Contains("/Account/Login.aspx"))
      ////  {
      ////    RedirectUtils.RedirectToTechnical(Response);
      ////  }
      ////  return;
      ////}
      var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
      var session = CurrentSessionContext.Unbind(sessionFactory);
      if (session != null)
      {
        if (session.Transaction != null && session.Transaction.IsActive)
        {
          session.Transaction.Rollback();
        }

        session.Flush();
        session.Clear();
        session.Close();
        session.Dispose();
      }
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
      var context = HttpContext.Current;
      var ex = context.Server.GetLastError();
      LogManager.GetCurrentClassLogger().Fatal("Application_Error", ex);

      LogManager.GetCurrentClassLogger().Fatal("Server.GetLastError()", Server.GetLastError());
      LogManager.GetCurrentClassLogger()
                .Fatal("Server.GetLastError().GetBaseException()", Server.GetLastError().GetBaseException());

      // Ошибка генерируемая при загрузке слишком большого по размеру файла
      // при загрузке файла большего размера чем положено параметром httpRuntime - maxRequestLength надо скрыть сообщение ие. 
      // Своё отображается клиентским скриптом OnClientUploadError="onUploadError" у AsyncFileUpload
      if (ex.InnerException != null && ex.InnerException.GetType() == typeof(HttpException)
          && ((HttpException)ex.InnerException).WebEventCode == WebEventCodes.RuntimeErrorPostTooLarge
          && Request.Url.Query.Contains("AsyncFileUploadID="))
      {
        Response.Close();
      }
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
    /// The process offline.
    /// </summary>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private bool ProcessOffline()
    {
      if (!SiteMode.IsOnline)
      {
        if (!redirectToOffline)
        {
          redirectToOffline = true;
          RedirectUtils.RedirectToTechnical(Response);
        }

        // если мы открываем страницу ещё одну - например любой справочник, 
        // то если режим офлайн надо редиректить на техническую страницу независимо от того что на неё ранее был сделан редирект
        if (!Request.Url.AbsoluteUri.Contains("SiteIsOffline.aspx") && !Request.Url.AbsoluteUri.Contains("settings.png")
            && !Request.Url.AbsoluteUri.Contains(".css"))
        {
          RedirectUtils.RedirectToTechnical(Response);
        }

        return true;
      }

      redirectToOffline = false;
      return false;
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