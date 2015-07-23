// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorChildrenWork.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator children work.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator children work.
  /// </summary>
  public class ValidatorChildrenWork : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorChildrenWork"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorChildrenWork(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory, x => x.InsuredPersonData.Category.Id)
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
        return Resource.CaptionValidatorChildrenWork;
      }
    }

    /// <summary>
    ///   Отобображать проверку или нет в списке на странице
    /// </summary>
    public override bool Visible
    {
      get
      {
        return true;
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
      if (statement.InsuredPersonData.Category == null || statement.InsuredPersonData.Birthday == null)
      {
        return;
      }

      // Дети младше 12 лет не могут быть работающими
      if (statement.InsuredPersonData.Category.Id == CategoryPerson.WorkerRf
          || statement.InsuredPersonData.Category.Id == CategoryPerson.WorkerAlienPermanently
          || statement.InsuredPersonData.Category.Id == CategoryPerson.WorkerAlienTeporary
          || statement.InsuredPersonData.Category.Id == CategoryPerson.WorkerStatelessPermanently
          || statement.InsuredPersonData.Category.Id == CategoryPerson.WorkerRefugee)
      {
        var age = Age.CalculateAge((DateTime)statement.InsuredPersonData.Birthday);
        if (age < 12)
        {
          throw new FaultChildrenWorkException();
        }
      }
    }

    #endregion
  }
}