// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MedicalInsuranceManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The MedicalInsuranceManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;

  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The MedicalInsuranceManager.
  /// </summary>
  public partial class MedicalInsuranceManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The reflection medical insured 2.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="medicalInsurances">
    /// The medical insurances.
    /// </param>
    /// <param name="saveInSession">
    /// The save in session.
    /// </param>
    public void ReflectionMedicalInsured2(Statement statement, IList<MedicalInsurance> medicalInsurances, bool saveInSession)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // В случае причины "Заявление на выбор или замену СМО не подавалось" просто сохраняем страховки без расчета
      if (statement.CauseFiling.Id == CauseReinsurance.Initialization)
      {
        if (saveInSession)
        {
          SaveMedicalInsurances(statement);
        }

        return;
      }

      if (statement.MedicalInsurances == null)
      {
        statement.MedicalInsurances = new List<MedicalInsurance>();
      }

      // Помечаем все как неактивные
      foreach (var mi in statement.MedicalInsurances)
      {
        mi.IsActive = false;
        session.SaveOrUpdate(mi);
      }

      session.Flush();

      // временное свидетельство
      var temp = medicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.В);
      if (temp != null)
      {
        var mi = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.В);
        if (mi != null)
        {
          temp.Id = mi.Id;
        }

        if (temp.Id == Guid.Empty)
        {
          session.Save(temp);
        }
        else
        {
          session.Replicate(temp, ReplicationMode.Overwrite);
        }
      }

      // полис новго образца
      var ints = new[] { PolisType.П, PolisType.К, PolisType.Э };
      var polis = medicalInsurances.FirstOrDefault(x => ints.Contains(x.PolisType.Id));
      if (polis != null)
      {
        var mi = statement.MedicalInsurances.FirstOrDefault(x => ints.Contains(x.PolisType.Id));
        if (mi != null)
        {
          polis.Id = mi.Id;
        }

        if (polis.Id == Guid.Empty)
        {
          session.Save(polis);
        }
        else
        {
          session.Replicate(polis, ReplicationMode.Overwrite);
        }
      }
    }

    /// <summary>
    /// The save medical insurances.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public void SaveMedicalInsurances(Statement statement)
    {
      foreach (var insurance in statement.MedicalInsurances)
      {
        insurance.Statement = statement;
        ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Save(insurance);
      }
    }

    #endregion
  }
}