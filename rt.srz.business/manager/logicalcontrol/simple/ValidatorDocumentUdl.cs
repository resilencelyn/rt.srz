﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorDocumentUdl.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The validator document udl.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator document udl.
  /// </summary>
  public class ValidatorDocumentUdl : CheckDocumentProperty
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorDocumentUdl"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorDocumentUdl(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.DocumentUdl)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the caption.
    /// </summary>
    public override string Caption
    {
      get
      {
        return Resource.CaptionValidatorDocumentUdl;
      }
    }

    public override void CheckObject(model.srz.Statement statement)
    {
      base.CheckObject(statement);

      var document = statement.DocumentUdl;
      if (document.DocumentType.Id == DocumentType.DocumentType22 && !document.DateExp.HasValue)
      {
        throw new FaultDocumentDateExpEmptyException();
      }
    }

    #endregion
  }
}