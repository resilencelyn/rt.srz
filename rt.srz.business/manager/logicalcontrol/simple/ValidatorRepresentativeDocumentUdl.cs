// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorRepresentativeDocumentUdl.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The validator representative document udl.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator representative document udl.
  /// </summary>
  public class ValidatorRepresentativeDocumentUdl : CheckDocumentProperty
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorRepresentativeDocumentUdl"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorRepresentativeDocumentUdl(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.Representative.Document)
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
        return Resource.CaptionValidatorRepresentativeDocumentUdl;
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
      if (statement.ModeFiling.Id != ModeFiling.ModeFiling2)
      {
        return;
      }

      base.CheckObject(statement);
    }

    #endregion
  }
}