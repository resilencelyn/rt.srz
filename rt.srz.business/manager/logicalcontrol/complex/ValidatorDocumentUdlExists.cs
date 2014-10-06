// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorDocumentUdlExists.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator document udl exists.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.complex
{
  #region

  using System;

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The validator document udl exists.
  /// </summary>
  public class ValidatorDocumentUdlExists : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorDocumentUdlExists"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorDocumentUdlExists(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Complex, sessionFactory)
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
        return Resource.CaptionValidatorDocumentUdlExists;
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
    /// <exception cref="FaultDocumentUdlExistsException">
    /// Документ у другого лица
    /// </exception>
    public override void CheckObject(Statement statement)
    {
      if (statement == null)
      {
        throw new ArgumentNullException("statement");
      }

      if (statement.DocumentUdl == null)
      {
        return;
      }

      if (statement.DocumentUdl.ExistDocument)
      {
        return;
      }

      Document d = null;
      var insuredPersonId = statement.InsuredPerson != null ? statement.InsuredPerson.Id : Guid.Empty;
      var docTypeId = statement.DocumentUdl.DocumentType.Id;
      var series = statement.DocumentUdl.Series;
      var number = statement.DocumentUdl.Number;

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var query =
        session.QueryOver<Statement>()
               .JoinAlias(x => x.DocumentUdl, () => d)
               .Where(x => d.DocumentType.Id == docTypeId)
               .And(x => d.Series == series)
               .And(x => d.Number == number)
               .And(x => x.Id != statement.Id)
               .And(x => x.Status.Id != StatusStatement.Cancelled)
               .And(x => x.Status.Id != StatusStatement.Declined);
      if (insuredPersonId != Guid.Empty)
      {
        query.Where(x => x.InsuredPerson.Id != insuredPersonId);
      }

      var count = query.RowCount();

      if (count > 0)
      {
        throw new FaultDocumentUdlExistsException();
      }
    }

    #endregion
  }
}