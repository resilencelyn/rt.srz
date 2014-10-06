// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LifeService.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The life service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Threading;

  using rt.srz.model.HL7.commons.Delegates;
  using rt.srz.model.HL7.commons.Enumerations;

  #endregion

  /// <summary>
  ///   The life service.
  /// </summary>
  public static class LifeService
  {
    // private static AfterFatalErrorHandler AfterFatalError;
    // private static BeforeFatalErrorHandler BeforeFatalError;
    // private static VoidHandler ConfigReadEvent;
    #region Static Fields

    /// <summary>
    ///   The running events.
    /// </summary>
    private static readonly Dictionary<GlobalEvent, RunningEventData> runningEvents =
      new Dictionary<GlobalEvent, RunningEventData>();

    /// <summary>
    ///   The fatal restart interval.
    /// </summary>
    private static TimeSpan fatalRestartInterval = TimeSpan.FromSeconds(10.0);

    // private static GlobalEventStagingHandler GlobalEventStaging;
    // private static HandleFatalErrorHander HandleFatalError;
    /// <summary>
    ///   The live state.
    /// </summary>
    private static LiveState liveState = LiveState.Normal;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes static members of the <see cref="LifeService" /> class.
    /// </summary>
    static LifeService()
    {
      // SystemEvents.TimeChanged += new EventHandler(LifeService.SystemEvents_TimeChanged);
    }

    #endregion

    #region Delegates

    /// <summary>
    ///   The after fatal error handler.
    /// </summary>
    /// <param name="fatalErrorType">
    ///   The fatal error type.
    /// </param>
    /// <param name="handled">
    ///   The handled.
    /// </param>
    public delegate void AfterFatalErrorHandler(FatalErrorType fatalErrorType, bool handled);

    /// <summary>
    ///   The before fatal error handler.
    /// </summary>
    /// <param name="fatalErrorType">
    ///   The fatal error type.
    /// </param>
    /// <param name="cancel">
    ///   The cancel.
    /// </param>
    public delegate void BeforeFatalErrorHandler(FatalErrorType fatalErrorType, ref bool cancel);

    /// <summary>
    ///   The global event staging handler.
    /// </summary>
    /// <param name="globalEvent">
    ///   The global event.
    /// </param>
    /// <param name="stage">
    ///   The stage.
    /// </param>
    public delegate void GlobalEventStagingHandler(GlobalEvent globalEvent, SimplePrecedence stage);

    /// <summary>
    ///   The handle fatal error hander.
    /// </summary>
    /// <param name="fatalErrorType">
    ///   The fatal error type.
    /// </param>
    /// <param name="handled">
    ///   The handled.
    /// </param>
    public delegate void HandleFatalErrorHander(FatalErrorType fatalErrorType, ref bool handled);

    /// <summary>
    ///   The raise global event_ create new event.
    /// </summary>
    /// <param name="globalEvent">
    ///   The global event.
    /// </param>
    private delegate RunningEventData RaiseGlobalEvent_CreateNewEvent(GlobalEvent globalEvent);

    /// <summary>
    ///   The raise global event_ queue next event.
    /// </summary>
    /// <param name="runningEventData">
    ///   The running event data.
    /// </param>
    private delegate void RaiseGlobalEvent_QueueNextEvent(RunningEventData runningEventData);

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the fatal restart interval.
    /// </summary>
    public static TimeSpan FatalRestartInterval
    {
      get
      {
        return fatalRestartInterval;
      }
    }

    /// <summary>
    ///   Gets a value indicating whether has steady state.
    /// </summary>
    public static bool HasSteadyState
    {
      get
      {
        return liveState == LiveState.Normal;
      }
    }

    /// <summary>
    ///   Gets a value indicating whether has unload flag.
    /// </summary>
    public static bool HasUnloadFlag
    {
      get
      {
        switch (liveState)
        {
          case LiveState.ExitExpected:
          case LiveState.RestartExpected:
          case LiveState.FatalRestartExpected:
            return true;
        }

        return false;
      }
    }

    /// <summary>
    ///   Gets the live state.
    /// </summary>
    public static LiveState LiveState
    {
      get
      {
        return liveState;
      }
    }

    #endregion

    // private static void CallFatalError(FatalErrorType fatalErrorType)
    // {
    // CallGlobalEventStaging(GlobalEvent.FatalErrorHandle, SimplePrecedence.Before);
    // try
    // {
    // try
    // {
    // if (BeforeFatalError != null)
    // {
    // bool cancel = false;
    // BeforeFatalError(fatalErrorType, ref cancel);
    // if (cancel)
    // {
    // return;
    // }
    // }
    // }
    // catch (Exception exception)
    // {
    // FomsLogger.WriteError(exception, null);
    // }
    // bool handled = false;
    // try
    // {
    // if (HandleFatalError != null)
    // {
    // HandleFatalError(fatalErrorType, ref handled);
    // }
    // }
    // catch (Exception exception2)
    // {
    // FomsLogger.WriteError(exception2, null);
    // }
    // try
    // {
    // if (AfterFatalError != null)
    // {
    // AfterFatalError(fatalErrorType, handled);
    // }
    // }
    // catch (Exception exception3)
    // {
    // FomsLogger.WriteError(exception3, null);
    // }
    // }
    // finally
    // {
    // CallGlobalEventStaging(GlobalEvent.FatalErrorHandle, SimplePrecedence.After);
    // }
    // }

    // private static void GlobalEventAction(RunningEventData runningEventData)
    // {
    // GlobalEventAction(runningEventData.globalEvent, runningEventData);
    // }

    // private static bool GlobalEventAction(GlobalEvent globalEvent, RunningEventData runningEventData = new RunningEventData())
    // {
    // switch (globalEvent)
    // {
    // case GlobalEvent.LiveStateChanged:
    // if (LiveStateChanged == null)
    // {
    // break;
    // }
    // if (runningEventData != null)
    // {
    // CallGlobalEvent(globalEvent, LiveStateChanged);
    // }
    // return true;

    // case GlobalEvent.SystemTimeChanged:
    // if (SystemTimeChanged == null)
    // {
    // break;
    // }
    // if (runningEventData != null)
    // {
    // CallGlobalEvent(globalEvent, SystemTimeChanged);
    // }
    // return true;

    // case GlobalEvent.ConfigReadEvent:
    // if (HasUnloadFlag || (ConfigReadEvent == null))
    // {
    // break;
    // }
    // if (runningEventData != null)
    // {
    // CallGlobalEvent(globalEvent, ConfigReadEvent);
    // }
    // return true;

    // case GlobalEvent.FatalErrorHandle:
    // {
    // if (((BeforeFatalError == null) && (AfterFatalError == null)) && (HandleFatalError == null))
    // {
    // break;
    // }
    // FatalErrorRunningEventData data = runningEventData as FatalErrorRunningEventData;
    // if (data != null)
    // {
    // CallFatalError(data.FatalErrorType);
    // }
    // return true;
    // }
    // }
    // return false;
    // }
    #region Public Methods and Operators

    /// <summary>
    /// The raise fatal error.
    /// </summary>
    /// <param name="fatalErrorType">
    /// The fatal error type.
    /// </param>
    /// <param name="forceSeparateThread">
    /// The force separate thread.
    /// </param>
    public static void RaiseFatalError(FatalErrorType fatalErrorType, bool forceSeparateThread = true)
    {
      // RaiseGlobalEventProc(GlobalEvent.FatalErrorHandle, forceSeparateThread, evt => new FatalErrorRunningEventData(fatalErrorType), delegate (RunningEventData runningEventData) {
      // ((FatalErrorRunningEventData) runningEventData).PushFatalError(fatalErrorType);
      // });
    }

    /// <summary>
    /// The raise global event.
    /// </summary>
    /// <param name="globalEvent">
    /// The global event.
    /// </param>
    /// <param name="forceSeparateThread">
    /// The force separate thread.
    /// </param>
    public static void RaiseGlobalEvent(GlobalEvent globalEvent, bool forceSeparateThread = false)
    {
      if (globalEvent == GlobalEvent.FatalErrorHandle)
      {
        RaiseFatalError(FatalErrorType.Unknown, forceSeparateThread);
      }
    }

    // private static void RaiseGlobalEventProc(GlobalEvent globalEvent, bool forceSeparateThread, RaiseGlobalEvent_CreateNewEvent CreateNewEvent, RaiseGlobalEvent_QueueNextEvent QueueNextEvent)
    // {
    // try
    // {
    // RunningEventData data;
    // lock (runningEvents)
    // {
    // if (runningEvents.TryGetValue(globalEvent, out data))
    // {
    // QueueNextEvent(data);
    // return;
    // }
    // if (!GlobalEventAction(globalEvent, null))
    // {
    // return;
    // }
    // data = CreateNewEvent(globalEvent);
    // runningEvents.Add(globalEvent, data);
    // }
    // RaiseGlobalEventStart(data, forceSeparateThread);
    // }
    // catch (Exception exception)
    // {
    // FomsLogger.WriteError(exception, null);
    // }
    // }

    /// <summary>
    /// The reset fatal restart interval.
    /// </summary>
    /// <param name="resetFatalRestartInterval">
    /// The reset fatal restart interval.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool ResetFatalRestartInterval(ResetHandler<TimeSpan> resetFatalRestartInterval)
    {
      if (resetFatalRestartInterval != null)
      {
        var fatalRestartInterval = LifeService.fatalRestartInterval;
        resetFatalRestartInterval(ref fatalRestartInterval);
        return ResetFatalRestartInterval(fatalRestartInterval);
      }

      return false;
    }

    /// <summary>
    /// The reset fatal restart interval.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool ResetFatalRestartInterval(TimeSpan value)
    {
      if (value <= TimeSpan.Zero)
      {
        return false;
      }

      lock (runningEvents)
      {
        fatalRestartInterval = value;
      }

      return true;
    }

    /// <summary>
    /// The start config read.
    /// </summary>
    /// <param name="forceSeparateThread">
    /// The force separate thread.
    /// </param>
    public static void StartConfigRead(bool forceSeparateThread = true)
    {
      RaiseGlobalEvent(GlobalEvent.ConfigReadEvent, forceSeparateThread);
    }

    // private static void SystemEvents_TimeChanged(object sender, EventArgs e)
    // {
    // CultureInfo.CurrentCulture.ClearCachedData();
    // if (SystemTimeChanged != null)
    // {
    // SystemTimeChanged();
    // }
    // }

    /// <summary>
    /// The try change live state.
    /// </summary>
    /// <param name="state">
    /// The state.
    /// </param>
    /// <param name="forceSeparateThreadEvent">
    /// The force separate thread event.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool TryChangeLiveState(LiveState state, bool forceSeparateThreadEvent = false)
    {
      bool flag;
      lock (runningEvents)
      {
        if (CanChangeLiveState(state))
        {
          liveState = state;
          flag = true;
        }
        else
        {
          flag = false;
        }
      }

      if (flag)
      {
        RaiseGlobalEvent(GlobalEvent.LiveStateChanged, forceSeparateThreadEvent);
      }

      return flag;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The call global event.
    /// </summary>
    /// <param name="globalEvent">
    /// The global event.
    /// </param>
    /// <param name="globalEventHandler">
    /// The global event handler.
    /// </param>
    private static void CallGlobalEvent(GlobalEvent globalEvent, VoidHandler globalEventHandler)
    {
      // CallGlobalEventStaging(globalEvent, SimplePrecedence.Before);
      try
      {
        globalEventHandler();
      }
      finally
      {
        // CallGlobalEventStaging(globalEvent, SimplePrecedence.After);
      }
    }

    // private static void CallGlobalEventStaging(GlobalEvent globalEvent, SimplePrecedence stage)
    // {
    // try
    // {
    // if (GlobalEventStaging != null)
    // {
    // GlobalEventStaging(globalEvent, stage);
    // }
    // }
    // catch (Exception exception)
    // {
    // FomsLogger.WriteError(exception, null);
    // }
    // }

    /// <summary>
    /// The can change live state.
    /// </summary>
    /// <param name="state">
    /// The state.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool CanChangeLiveState(LiveState state)
    {
      switch (liveState)
      {
        case LiveState.Normal:
          switch (state)
          {
            case LiveState.Fatal:
            case LiveState.ExitExpected:
            case LiveState.RestartExpected:
            case LiveState.FatalRestartExpected:
              return true;
          }

          break;

        case LiveState.Fatal:
          switch (state)
          {
            case LiveState.ExitExpected:
            case LiveState.RestartExpected:
            case LiveState.FatalRestartExpected:
              return true;
          }

          break;

        case LiveState.RestartExpected:
          switch (state)
          {
            case LiveState.ExitExpected:
            case LiveState.FatalRestartExpected:
              return true;
          }

          break;

        case LiveState.FatalRestartExpected:
          if (state != LiveState.ExitExpected)
          {
            break;
          }

          return true;
      }

      return false;
    }

    /// <summary>
    /// The do raise global event.
    /// </summary>
    /// <param name="runningEventData">
    /// The running event data.
    /// </param>
    private static void DoRaiseGlobalEvent(RunningEventData runningEventData)
    {
      bool flag;
      do
      {
        flag = false;
        try
        {
          // GlobalEventAction(runningEventData);
        }
        catch (Exception exception)
        {
          FomsLogger.WriteError(exception, null);
        }

        try
        {
          lock (runningEvents)
          {
            // if (runningEventData.TakeNextPending() && GlobalEventAction(runningEventData.globalEvent, null))
            // {
            // if (runningEventData.separateThread != null)
            // {
            // flag = true;
            // }
            // else
            // {
            // bool forceSeparateThread = true;
            {
              // RaiseGlobalEventStart(runningEventData, forceSeparateThread);
              // }
              // }

              // else
              runningEvents.Remove(runningEventData.globalEvent);
            }
          }
        }
        catch (Exception exception2)
        {
          FomsLogger.WriteError(exception2, null);
        }
      }
      while (flag);
    }

    /// <summary>
    /// The do raise global event.
    /// </summary>
    /// <param name="runningEventData">
    /// The running event data.
    /// </param>
    private static void DoRaiseGlobalEvent(object runningEventData)
    {
      DoRaiseGlobalEvent((RunningEventData)runningEventData);
    }

    /// <summary>
    /// The raise global event start.
    /// </summary>
    /// <param name="runningEventData">
    /// The running event data.
    /// </param>
    /// <param name="forceSeparateThread">
    /// The force separate thread.
    /// </param>
    private static void RaiseGlobalEventStart(RunningEventData runningEventData, bool forceSeparateThread)
    {
      if (forceSeparateThread)
      {
        runningEventData.separateThread = new Thread(DoRaiseGlobalEvent);
        runningEventData.separateThread.Start(runningEventData);
      }
      else
      {
        DoRaiseGlobalEvent(runningEventData);
      }
    }

    #endregion

    /// <summary>
    ///   The fatal error running event data.
    /// </summary>
    private sealed class FatalErrorRunningEventData : RunningEventData
    {
      #region Fields

      /// <summary>
      ///   The fatal error type.
      /// </summary>
      private FatalErrorType fatalErrorType;

      /// <summary>
      ///   The pending fatal errors.
      /// </summary>
      private List<FatalErrorType> pendingFatalErrors;

      #endregion

      #region Constructors and Destructors

      /// <summary>
      /// Initializes a new instance of the <see cref="FatalErrorRunningEventData"/> class.
      /// </summary>
      /// <param name="fatalErrorType">
      /// The fatal error type.
      /// </param>
      internal FatalErrorRunningEventData(FatalErrorType fatalErrorType)
        : base(GlobalEvent.FatalErrorHandle)
      {
        this.fatalErrorType = fatalErrorType;
      }

      #endregion

      #region Properties

      /// <summary>
      ///   Gets the fatal error type.
      /// </summary>
      internal FatalErrorType FatalErrorType
      {
        get
        {
          return fatalErrorType;
        }
      }

      #endregion

      #region Methods

      /// <summary>
      /// The push fatal error.
      /// </summary>
      /// <param name="fatalErrorType">
      /// The fatal error type.
      /// </param>
      internal void PushFatalError(FatalErrorType fatalErrorType)
      {
        if (pendingFatalErrors == null)
        {
          pendingFatalErrors = new List<FatalErrorType>();
        }

        pendingFatalErrors.Add(fatalErrorType);
      }

      /// <summary>
      ///   The take next pending.
      /// </summary>
      /// <returns>
      ///   The <see cref="bool" />.
      /// </returns>
      internal override bool TakeNextPending()
      {
        if ((pendingFatalErrors != null) && (pendingFatalErrors.Count > 0))
        {
          fatalErrorType = pendingFatalErrors[0];
          pendingFatalErrors.RemoveAt(0);
          return true;
        }

        return false;
      }

      #endregion
    }

    /// <summary>
    ///   The running event data.
    /// </summary>
    private abstract class RunningEventData
    {
      #region Fields

      /// <summary>
      ///   The global event.
      /// </summary>
      internal readonly GlobalEvent globalEvent;

      /// <summary>
      ///   The separate thread.
      /// </summary>
      internal Thread separateThread;

      #endregion

      #region Constructors and Destructors

      /// <summary>
      /// Initializes a new instance of the <see cref="RunningEventData"/> class.
      /// </summary>
      /// <param name="globalEvent">
      /// The global event.
      /// </param>
      internal RunningEventData(GlobalEvent globalEvent)
      {
        this.globalEvent = globalEvent;
      }

      #endregion

      #region Methods

      /// <summary>
      ///   The take next pending.
      /// </summary>
      /// <returns>
      ///   The <see cref="bool" />.
      /// </returns>
      internal abstract bool TakeNextPending();

      #endregion
    }

    /// <summary>
    ///   The simple running event data.
    /// </summary>
    private sealed class SimpleRunningEventData : RunningEventData
    {
      #region Fields

      /// <summary>
      ///   The restart when done.
      /// </summary>
      internal bool restartWhenDone;

      #endregion

      #region Constructors and Destructors

      /// <summary>
      /// Initializes a new instance of the <see cref="SimpleRunningEventData"/> class.
      /// </summary>
      /// <param name="globalEvent">
      /// The global event.
      /// </param>
      internal SimpleRunningEventData(GlobalEvent globalEvent)
        : base(globalEvent)
      {
      }

      #endregion

      #region Methods

      /// <summary>
      ///   The take next pending.
      /// </summary>
      /// <returns>
      ///   The <see cref="bool" />.
      /// </returns>
      internal override bool TakeNextPending()
      {
        if (restartWhenDone)
        {
          restartWhenDone = false;
          return true;
        }

        return false;
      }

      #endregion
    }
  }
}