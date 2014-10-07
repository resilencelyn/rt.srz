// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingInterceptor.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Аспект для логирования
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.aspects
{
  #region references

  using System;
  using System.Diagnostics;

  using NLog;

  #endregion

  /// <summary>
  ///   Аспект для логирования
  /// </summary>
  public class LoggingInterceptor : IMethodInterceptor
  {
    #region Static Fields

    /// <summary>
    ///   Логгер
    /// </summary>
    protected static readonly Logger logger = LogManager.GetCurrentClassLogger();

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Инвокер
    /// </summary>
    /// <typeparam name="T">
    /// T
    /// </typeparam>
    /// <param name="invokeNext">
    /// Метод вызова
    /// </param>
    /// <param name="metod">
    /// Самый конечный метод
    /// </param>
    /// <returns>
    /// Результат выполнения
    /// </returns>
    public virtual T InvokeMethod<T>(Func<T> invokeNext, Func<T> metod)
    {
      var begin = DateTime.Now;
      var sessionId = Guid.NewGuid().ToString();
      try
      {
        logger.Info("Begin invoke SID: {1}: [{0}]", metod.Method.Name, sessionId);
        if (logger.IsInfoEnabled && metod.Target != null)
        {
          var fieldInfos = metod.Target.GetType().GetFields();
          foreach (var fieldInfo in fieldInfos)
          {
            try
            {
              logger.Info("Передан аргумент SID: " + sessionId + " " + fieldInfo.Name);
              var output = fieldInfo.GetValue(metod.Target);

              ////logger.Info("SID: " + sessionId + " " + ObjectDumper.WriteToString(output, 2));
              if (output != null)
              {
                var type = output.GetType();
                if (!type.IsInterface)
                {
                  logger.Info("SID: " + sessionId + " " + Dump.ObjectToXml(output));
                }
              }
            }
            catch (Exception)
            {
              // logger.Error("Не удалось получить значение поля. SID: " + sessionId + fieldInfo.Name, ex);
            }
          }
        }

        return invokeNext();
      }
      catch (Exception ex)
      {
        OnError(metod, sessionId, ex);
        throw;
      }
      finally
      {
        logger.Info("End invoke SID: {2}: [{0}]. Time: {1}", metod.Method.Name, DateTime.Now - begin, sessionId);
      }
    }

    /// <summary>
    /// Инвокер
    /// </summary>
    /// <param name="invokeNext">
    /// Метод вызова.
    /// </param>
    /// <param name="metod">
    /// Самый конечный метод.
    /// </param>
    [DebuggerStepThrough]
    public virtual void InvokeMethod(Action invokeNext, Action metod)
    {
      var begin = DateTime.Now;
      var sessionId = Guid.NewGuid().ToString();
      try
      {
        logger.Info("Begin invoke SID: {1}: [{0}]", metod.Method.Name, sessionId);

        if (logger.IsInfoEnabled && metod.Target != null)
        {
          var fieldInfos = metod.Target.GetType().GetFields();
          foreach (var fieldInfo in fieldInfos)
          {
            try
            {
              logger.Info("Передан аргумент SID: " + sessionId + " " + fieldInfo.Name);
              var output = fieldInfo.GetValue(metod.Target);

              //// logger.Info("SID: " + sessionId + " " + ObjectDumper.WriteToString(output, 2));
              if (output != null)
              {
                var type = output.GetType();
                if (!type.IsInterface)
                {
                  logger.Info("SID: " + sessionId + " " + Dump.ObjectToXml(output));
                }
              }
            }
            catch (Exception)
            {
              // logger.Error("Не удалось получить значение поля. SID: " + sessionId + fieldInfo.Name, ex);
            }
          }
        }

        invokeNext();
      }
      catch (Exception ex)
      {
        OnError(metod, sessionId, ex);
        throw;
      }
      finally
      {
        logger.Info("End invoke SID: {2}: [{0}]. Time: {1}", metod.Method.Name, DateTime.Now - begin, sessionId);
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The on error.
    /// </summary>
    /// <param name="target">
    /// The metod.
    /// </param>
    /// <param name="sessionId">
    /// The session id.
    /// </param>
    /// <param name="ex">
    /// The ex.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    protected virtual void OnError<T>(Func<T> target, string sessionId, Exception ex)
    {
      logger.Error("Ошибка исполнения метода SID: " + sessionId, ex);
      if (target.Target != null)
      {
        foreach (var fieldInfo in target.Target.GetType().GetFields())
        {
          try
          {
            logger.Error("Передан аргумент SID: " + sessionId + " " + fieldInfo.Name);

            ////logger.Error("SID: " + sessionId + " " + ObjectDumper.WriteToString(fieldInfo.GetValue(target.Target), 2));
            logger.Error("SID: " + sessionId + " " + Dump.ObjectToXml(fieldInfo.GetValue(target.Target)));
          }
          catch
          {
            logger.Error("Не удалось получить значение поля SID: " + sessionId + fieldInfo.Name);
          }
        }
      }
    }

    /// <summary>
    /// The on error.
    /// </summary>
    /// <param name="target">
    /// The metod.
    /// </param>
    /// <param name="sessionId">
    /// The session id.
    /// </param>
    /// <param name="ex">
    /// The ex.
    /// </param>
    protected virtual void OnError(Action target, string sessionId, Exception ex)
    {
      logger.Error("Ошибка исполнения метода SID: " + sessionId, ex);
      foreach (var fieldInfo in target.Target.GetType().GetFields())
      {
        try
        {
          logger.Error("Передан аргумент  SID: " + sessionId + " " + fieldInfo.Name);
          logger.Error("SID: " + sessionId + " " + Dump.ObjectToXml(fieldInfo.GetValue(target.Target)));
        }
        catch
        {
          logger.Error("Не удалось получить значение поля  SID: " + sessionId + " " + fieldInfo.Name);
        }
      }
    }

    #endregion
  }
}