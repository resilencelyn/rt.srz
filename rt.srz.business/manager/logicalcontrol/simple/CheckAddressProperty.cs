// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckAddressProperty.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The check address property.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;
  using System.Linq.Expressions;

  using NHibernate;

  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step3;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The check address property.
  /// </summary>
  public abstract class CheckAddressProperty : Check
  {
    #region Fields

    /// <summary>
    /// The delegate field.
    /// </summary>
    private readonly Func<Statement, object> delegateField;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckAddressProperty"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    /// <param name="expression">
    /// The expression.
    /// </param>
    protected CheckAddressProperty(ISessionFactory sessionFactory, Expression<Func<Statement, object>> expression)
      : base(CheckLevelEnum.Simple, sessionFactory, expression)
    {
      delegateField = expression.Compile();
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check object.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// </exception>
    /// <exception cref="FaultAddressSubjectEmptyException">
    /// </exception>
    public override void CheckObject(Statement statement)
    {
      if (statement == null)
      {
        throw new ArgumentNullException("statement");
      }

      // Пропускаем проверку если причина - "Заявление на выбор или замену СМО не подавалось"
      if (statement.CauseFiling != null && statement.CauseFiling.Id == CauseReinsurance.Initialization)
      {
        return;
      }

      try
      {
        var address = (address)delegateField(statement);
        if (address == null || string.IsNullOrEmpty(address.Subject))
        {
          throw new FaultAddressSubjectEmptyException();
        }

        // если адрес заполнен по кладру то если Level 1 или 2 значит ввод адреса не завершён, ругаемся
        if (address.Kladr != null && !(address.IsNotStructureAddress.HasValue && address.IsNotStructureAddress.Value))
        {
          if (!address.Kladr.Level.HasValue || address.Kladr.Level.Value <= 2)
          {
            throw new FaultAddressNotComplete();
          }
        }
        else
        {
          if (string.IsNullOrEmpty(address.Subject))
          {
            throw new FaultAddressNotComplete();
          }

          if (!string.IsNullOrEmpty(address.City) && string.IsNullOrEmpty(address.Street))
          {
            throw new FaultAddressNotComplete();
          }

          // if (!string.IsNullOrEmpty(address.Area) && string.IsNullOrEmpty(address.Town))
          // {
          // throw new FaultAddressNotComplete();
          // }
          if (string.IsNullOrEmpty(address.City) && string.IsNullOrEmpty(address.Area)
              && string.IsNullOrEmpty(address.Town))
          {
            throw new FaultAddressNotComplete();
          }
        }

        // если заполнен номер корпуса или квартира, но не заполнен номер дома кидаем ошибку
        if (string.IsNullOrEmpty(address.House) && (address.Room.HasValue || !string.IsNullOrEmpty(address.Housing)))
        {
          throw new FaultHouseEmptyException();
        }
      }
      catch (NullReferenceException)
      {
        throw new FaultAddressSubjectEmptyException();
      }
    }

    #endregion
  }
}