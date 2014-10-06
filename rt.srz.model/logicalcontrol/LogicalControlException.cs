// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogicalControlException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The flk exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol
{
  #region

  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Linq;
  using System.Runtime.Serialization;

  #endregion

  /// <summary>
  ///   The flk exception.
  /// </summary>
  [Serializable]
  public abstract class LogicalControlException : Exception
  {
    #region Fields

    /// <summary>
    ///   Gets or sets the logical control exceptions.
    /// </summary>
    private readonly List<LogicalControlException> logicalControlExceptions;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="LogicalControlException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    public LogicalControlException(ExceptionInfo info, string message)
      : base(message)
    {
      Info = info;
      logicalControlExceptions = new List<LogicalControlException>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogicalControlException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected LogicalControlException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      logicalControlExceptions = new List<LogicalControlException>();
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="LogicalControlException" /> class.
    /// </summary>
    protected LogicalControlException()
    {
    }

    #endregion

    // To prevent serialization error 
    #region Public Properties

    /// <summary>
    ///   Gets the data.
    /// </summary>
    public override IDictionary Data
    {
      get
      {
        return null;
      }
    }

    /// <summary>
    ///   Gets or sets the info.
    /// </summary>
    [DataMember]
    public ExceptionInfo Info { get; set; }

    /// <summary>
    ///   Gets the logical control exceptions.
    /// </summary>
    [DataMember]
    public List<LogicalControlException> LogicalControlExceptions
    {
      get
      {
        return logicalControlExceptions;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The add exception.
    /// </summary>
    /// <param name="exception">
    /// The exception.
    /// </param>
    public void AddException(LogicalControlException exception)
    {
      logicalControlExceptions.Add(exception);
    }

    /// <summary>
    ///   The contains.
    /// </summary>
    /// <typeparam name="T"> Тип ошибки </typeparam>
    /// <returns> The <see cref="bool" /> . </returns>
    public bool Contains<T>() where T : LogicalControlException
    {
      return (this as T) != null || LogicalControlExceptions.Any(x => (x as T) != null);
    }

    /// <summary>
    ///   The get all messages.
    /// </summary>
    /// <returns> The <see cref="string" /> . </returns>
    public string GetAllMessages()
    {
      var strList = new List<string>(logicalControlExceptions.Count + 1) { Message };
      strList.AddRange(logicalControlExceptions.Select(x => x.Message));

      return strList.GroupBy(x => x).Aggregate(string.Empty, (x, y) => x + y.Key + ";");
    }

    /// <summary>
    ///   The to step.
    /// </summary>
    /// <returns> The <see cref="int" /> . </returns>
    public int ToStep()
    {
      var val2 = logicalControlExceptions.Any() ? logicalControlExceptions.Min(x => x.Step()) : int.MaxValue;
      return Math.Min(Step(), val2);
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Gets the step.
    /// </summary>
    /// <returns>
    ///   The <see cref="int" />.
    /// </returns>
    protected abstract int Step();

    #endregion
  }
}