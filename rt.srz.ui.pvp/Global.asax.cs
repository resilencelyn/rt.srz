// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The global.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Web;
using NHibernate;
using NHibernate.Context;
using StructureMap;

#endregion

namespace rt.srz.ui.pvp
{
  using System.Web.Security;
  using System.Linq;

  using rt.core.model;
  using rt.srz.ui.pvp.Enumerations;
  using rt.srz.model.interfaces.service;
  using rt.srz.business.manager;
  using rt.atl.business.exchange.impl;

  /// <summary>
  /// The global.
  /// </summary>
  public class Global : HttpApplication
  {
    protected void Application_AcquireRequestState(object sender, EventArgs e)
    {
      model.srz.User currentUser = null;
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
      var syncTimeSrz2Pvp = ObjectFactory.GetInstance<ISettingManager>()
        .GetBy(x => x.Name == typeof(ExporterToPvp).FullName)
        .FirstOrDefault();

      DateTime? syncTime = null;
      if (syncTimeSrz2Pvp != null)
      {
        DateTime temp;
        if (DateTime.TryParse(syncTimeSrz2Pvp.ValueString, out temp))
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
      // Проверяем, не находится ли сайт на обслуживании
      if (ProcessOffline())
      {
        return;
      }

      // Открываем сессию к базе
      var session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
      CurrentSessionContext.Bind(session);
    }

    bool _redirectToOffline = false;

    private bool ProcessOffline()
    {
      if (!SiteMode.IsOnline)
      {
        if (!_redirectToOffline)
        {
          _redirectToOffline = true;
          RedirectUtils.RedirectToTechnical(Response);
        }
        //если мы открываем страницу ещё одну - например любой справочник, 
        //то если режим офлайн надо редиректить на техническую страницу независимо от того что на неё ранее был сделан редирект
        if (!Request.Url.AbsoluteUri.Contains("SiteIsOffline.aspx") && !Request.Url.AbsoluteUri.Contains("settings.png") &&
          !Request.Url.AbsoluteUri.Contains(".css"))
        {
          RedirectUtils.RedirectToTechnical(Response);
        }
        return true;
      }
      
      _redirectToOffline = false;
      return false;
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
      HttpContext context = HttpContext.Current;
      Exception ex = context.Server.GetLastError();
      NLog.LogManager.GetCurrentClassLogger().Fatal("Application_Error", ex);

      NLog.LogManager.GetCurrentClassLogger().Fatal("Server.GetLastError()", Server.GetLastError());
      NLog.LogManager.GetCurrentClassLogger().Fatal("Server.GetLastError().GetBaseException()", Server.GetLastError().GetBaseException());

      //Ошибка генерируемая при загрузке слишком большого по размеру файла
      //при загрузке файла большего размера чем положено параметром httpRuntime - maxRequestLength надо скрыть сообщение ие. 
      //Своё отображается клиентским скриптом OnClientUploadError="onUploadError" у AsyncFileUpload
      if (ex.InnerException != null && ex.InnerException.GetType() == typeof(HttpException) && 
        ((HttpException)ex.InnerException).WebEventCode == System.Web.Management.WebEventCodes.RuntimeErrorPostTooLarge && 
        Request.Url.Query.Contains("AsyncFileUploadID="))
      {
        Response.Close(); 
      }
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
  }
}