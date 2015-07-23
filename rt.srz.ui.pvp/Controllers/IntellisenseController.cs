// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntellisenseController.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.ui.pvp.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;

  using AjaxControlToolkit;

  using rt.core.model.interfaces;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  ///   The intellisense controller.
  /// </summary>
  public static class IntellisenseController
  {
    #region Public Methods and Operators

    /// <summary>
    /// Обслуживает Intellisense для ввода имени
    /// </summary>
    /// <param name="prefixText">
    /// The prefix Text.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <returns>
    /// The <see cref="List"/>.
    /// </returns>
    public static List<string> GetFirstNameAutoComplete(string prefixText, int count)
    {
      var regulatoryService = ObjectFactory.GetInstance<IRegulatoryService>();
      IList<AutoComplete> firstNameList = regulatoryService.GetFirstNameAutoComplete(prefixText);

      var completeList = new List<string>();
      foreach (var complete in firstNameList)
      {
        completeList.Add(AutoCompleteExtender.CreateAutoCompleteItem(complete.Name, complete.Id.ToString()));
      }

      return completeList;
    }

    /// <summary>
    /// Обслуживает Intellisense КЛАДР
    /// </summary>
    /// <param name="prefixText">
    /// The prefix Text.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <param name="contextKey">
    /// The context Key.
    /// </param>
    /// <returns>
    /// The <see cref="List"/>.
    /// </returns>
    public static List<string> GetKladrList(string prefixText, int count, string contextKey)
    {
      // Убираем пробелы
      prefixText = prefixText.Trim();

      if (!string.IsNullOrEmpty(prefixText))
      {
        // подставляем знаки процентов
        var pr = string.Empty;
        if (prefixText.Length > 0)
        {
          pr = prefixText.First().ToString(CultureInfo.InvariantCulture) + "%";
          pr = prefixText.Skip(1).Aggregate(pr, (akk, x) => string.Format("{0}{1}%", akk, x));
        }

        var kladrService = ObjectFactory.GetInstance<IAddressService>();

        if (string.IsNullOrEmpty(contextKey))
        {
          // Регионы
          var completeList =
            kladrService.GetAddressList(null, pr, KladrLevel.Subject)
                        .Select(
                                x =>
                                AutoCompleteExtender.CreateAutoCompleteItem(
                                                                            string.Format("{0} {1}.", x.Name, x.Socr), 
                                                                            x.Id.ToString()))
                        .ToList();

          return completeList;
        }

        var parentKladrId = new Guid(contextKey);
        if (parentKladrId != Guid.Empty)
        {
          var completeList =
            kladrService.GetAddressList(parentKladrId, pr, null)
                        .Select(
                                x =>
                                AutoCompleteExtender.CreateAutoCompleteItem(
                                                                            x.Name
                                                                            + (!string.IsNullOrEmpty(x.Socr)
                                                                                 ? string.Format(" {0}.", x.Socr)
                                                                                 : string.Empty), 
                                                                            x.Id.ToString()))
                        .ToList();

          return completeList;
        }
      }

      return new List<string>();
    }

    /// <summary>
    /// Обслуживает Intellisense для ввода отчества
    /// </summary>
    /// <param name="prefixText">
    /// The prefix Text.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <param name="contextKey">
    /// The context Key.
    /// </param>
    /// <returns>
    /// The <see cref="List"/>.
    /// </returns>
    public static List<string> GetMiddleNameAutoComplete(string prefixText, int count, string contextKey)
    {
      var statementService = ObjectFactory.GetInstance<IRegulatoryService>();

      // Фильтрация отчества в зависимости от имени
      Guid nameId;
      if (!Guid.TryParse(contextKey, out nameId))
      {
        nameId = Guid.Empty;
      }

      IList<AutoComplete> middleNameList = statementService.GetMiddleNameAutoComplete(prefixText, nameId);

      var completeList = new List<string>();
      foreach (var complete in middleNameList)
      {
        completeList.Add(AutoCompleteExtender.CreateAutoCompleteItem(complete.Name, complete.Id.ToString()));
      }

      return completeList;
    }

    /// <summary>
    /// Врзвращает контактную информацию по документу удл, найденному по номеру и серии
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <param name="series">
    /// The series.
    /// </param>
    /// <returns>
    /// The <see cref="Representative"/>.
    /// </returns>
    public static Representative GetRepresentativeContactInfoByUdl(string number, string series)
    {
      var statementService = ObjectFactory.GetInstance<IStatementService>();
      return statementService.GetRepresentativeContactInfoByUdl(number, series);
    }

    #endregion
  }
}