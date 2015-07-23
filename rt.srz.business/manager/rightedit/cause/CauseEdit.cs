// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseEdit.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The cause edit.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.cause
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;

  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The cause edit.
  /// </summary>
  public class CauseEdit : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CauseEdit" /> class.
    /// </summary>
    public CauseEdit()
      : base(CauseReneval.Edit)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the not editing property expression.
    /// </summary>
    protected override List<Expression<Func<Statement, object>>> NotEditingPropertyExpression
    {
      get
      {
        return new List<Expression<Func<Statement, object>>>
               {
                 x => x.InsuredPersonData.FirstName, 
                 x => x.InsuredPersonData.LastName, 
                 x => x.InsuredPersonData.MiddleName, 
                 x => x.InsuredPersonData.Birthday, 
                 x => x.InsuredPersonData.IsIncorrectDate, 
                 x => x.InsuredPersonData.Birthday2, 
                 x => x.InsuredPersonData.BirthdayType, 
                 x => x.InsuredPersonData.Gender, 
                 x => x.InsuredPersonData.Birthplace, 
                 x => x.InsuredPersonData.OldCountry, 
                 x => x.NumberPolisCertificate, 
                 x => x.DateIssuePolisCertificate, 
                 x => x.MedicalInsurances[1].Enp, 
                 x => x.MedicalInsurances[1].PolisNumber, 
                 x => x.MedicalInsurances[1].DateFrom, 
                 x => x.MedicalInsurances[1].DateTo, 
                 x => x.MedicalInsurances[1].PolisType
               };
      }
    }

    #endregion
  }
}