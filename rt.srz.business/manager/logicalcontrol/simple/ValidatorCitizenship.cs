// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorCitizenship.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The validator citizenship.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Activities.Expressions;
using System.Linq;
using NHibernate.Util;
using rt.srz.model.logicalcontrol;
using StructureMap;
using rt.srz.model.srz.concepts;

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System.Collections.Generic;
  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.logicalcontrol.exceptions;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The validator citizenship.
  /// </summary>
  public class ValidatorCitizenship : CheckConceptProperty<FaultCitizenshipException>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorCitizenship"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorCitizenship(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.InsuredPersonData.Citizenship.Id)
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
        return Resource.CaptionValidatorCitizenship;
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
      if (!statement.InsuredPersonData.IsNotCitizenship)
      {
        base.CheckObject(statement);
      }
    }

    #endregion
  }
}