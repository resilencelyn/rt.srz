// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorSnilsExists.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator snils exists.
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
  ///   The validator snils exists.
  /// </summary>
  public class ValidatorSnilsExists : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorSnilsExists"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorSnilsExists(ISessionFactory sessionFactory)
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
        return Resource.CaptionValidatorSnilsExists;
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
      InsuredPersonDatum d = null;
      var insuredPersonId = statement.InsuredPerson != null ? statement.InsuredPerson.Id : Guid.Empty;
      var snils = statement.InsuredPersonData.Snils;

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      if (string.IsNullOrEmpty(snils))
      {
        return;
      }

      if (!statement.InsuredPersonData.NotCheckExistsSnils)
      {
        var count =
          session.QueryOver<Statement>()
                 .JoinAlias(x => x.InsuredPersonData, () => d)
                 .Where(x => d.Snils == snils)
                 .And(x => x.InsuredPerson.Id != insuredPersonId)
                 .And(x => x.Id != statement.Id)
                 .And(x => x.Status.Id != StatusStatement.Cancelled)
                 .And(x => x.Status.Id != StatusStatement.Declined)
                 .RowCount();

        if (count > 0)
        {
          throw new FaultSnilsExistsException();
        }
      }
    }

    #endregion
  }
}