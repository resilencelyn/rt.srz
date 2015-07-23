// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumberPolicyCounterManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The NumberPolicyCounterManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Globalization;
  using System.Linq;

  using NHibernate;

  using rt.srz.business.manager.cache;
  using rt.srz.model.algorithms;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  ///   The NumberPolicyCounterManager.
  /// </summary>
  public partial class NumberPolicyCounterManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get next enp number facets.
    /// </summary>
    /// <param name="tfomsId">
    /// The tfoms id.
    /// </param>
    /// <param name="genderId">
    /// The gender id.
    /// </param>
    /// <param name="birthday">
    /// The birthday.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string GetNextEnpNumber(Guid tfomsId, int genderId, DateTime birthday)
    {
      var tfoms = ObjectFactory.GetInstance<IOrganisationCacheManager>().GetBy(x => x.Id == tfomsId).FirstOrDefault();
      if (tfoms == null || tfoms.Code.Length != 2)
      {
        throw new Exception("Не найден ТФОМС для расчета фасеты ЕНП");
      }

      var facet = tfoms.Code + EnpChecker.GetFacet(birthday, genderId == Sex.Sex1);
      NumberPolicyCounter numberPolicyCounter;

      // получение значения счетчика
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      numberPolicyCounter = GetById(facet);
      var number = "1";
      if (numberPolicyCounter == null)
      {
        numberPolicyCounter = new NumberPolicyCounter { Id = facet, CurrentNumber = 2 };
      }
      else
      {
        number = numberPolicyCounter.CurrentNumber.ToString(CultureInfo.InvariantCulture);
        numberPolicyCounter.CurrentNumber++;
      }

      // сохранение
      session.SaveOrUpdate(numberPolicyCounter);

      var enp = string.Format("{0}{1}", facet, number.PadLeft(5, '0'));
      return EnpChecker.AppendCheckSum(enp);
    }

    /// <summary>
    /// The recalculate number policy counter.
    /// </summary>
    /// <param name="numberPolicy">
    /// The number policy.
    /// </param>
    public void RecalculateNumberPolicyCounter(string numberPolicy)
    {
      if (string.IsNullOrEmpty(numberPolicy) || numberPolicy.Length != 16)
      {
        return;
      }

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // Получаем ТФОМС по коду, входящему в ЕНП
      var facet = EnpChecker.GetTfFacet(numberPolicy);

      // ищем запись
      var numberPolicyCounter = GetById(facet) ?? new NumberPolicyCounter { Id = facet, CurrentNumber = 1 };

      // назначем новый номер, либо сохраняем старый
      var number = int.Parse(numberPolicy.Substring(10, 5)) + 1;
      numberPolicyCounter.CurrentNumber = Math.Max(numberPolicyCounter.CurrentNumber, number);
      session.SaveOrUpdate(numberPolicyCounter);
    }

    #endregion
  }
}