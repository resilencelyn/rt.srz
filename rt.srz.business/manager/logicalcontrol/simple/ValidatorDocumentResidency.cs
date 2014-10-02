// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorDocumentResidency.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator document udl.
  /// </summary>
  public class ValidatorDocumentResidency : CheckDocumentProperty
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorDocumentResidency"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorDocumentResidency(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.ResidencyDocument)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the caption.
    /// </summary>
    public override string Caption
    {
      get
      {
        return Resource.CaptionValidatorDocumentResidency;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check object.
    /// </summary>
    /// <param name="statement">
    /// The statement. 
    /// </param>
    public override void CheckObject(Statement statement)
    {
      if (statement == null)
      {
        throw new ArgumentNullException("statement");
      }

      // Постоянно проживающее лицо без гражданства может не иметь второй документ
      if (statement.InsuredPersonData != null && statement.InsuredPersonData.Category != null
        && (statement.InsuredPersonData.Category.Id == CategoryPerson.TerritorialStatelessPermanently || statement.InsuredPersonData.Category.Id == CategoryPerson.WorkerStatelessPermanently)
        && statement.ResidencyDocument == null)
      {
        return;
      }

      if (statement.InsuredPersonData != null && statement.InsuredPersonData.Category != null
          && CategoryPerson.IsDocumentResidency(statement.InsuredPersonData.Category.Id))
      {
        if ((statement.InsuredPersonData.Category.Id == CategoryPerson.TerritorialStatelessPermanently
            || statement.InsuredPersonData.Category.Id == CategoryPerson.WorkerStatelessPermanently) && statement.ResidencyDocument.DocumentType == null)
        {
          return;
        }

        base.CheckObject(statement);
      }
    }

    #endregion
  }
}