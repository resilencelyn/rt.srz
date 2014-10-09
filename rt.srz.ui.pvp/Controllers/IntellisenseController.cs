using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using StructureMap;
using rt.srz.model.srz;
using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;

namespace rt.srz.ui.pvp.Controllers
{
  public static class IntellisenseController
  {
    /// <summary>
    /// Обслуживает Intellisense КЛАДР
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <param name="contextKey"></param>
    /// <returns></returns>
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

        var _kladrService = ObjectFactory.GetInstance<IKladrService>();

        if (string.IsNullOrEmpty(contextKey)) //Регионы
        {
          var completeList = _kladrService.GetKladrs(null, pr, KladrLevel.Subject)
                                          .Select(
                                            x =>
                                            AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                                              x.Name + " " + x.Socr + ".", x.Id.ToString()))
                                          .ToList();

          return completeList;
        }

        var parentKladrID = new Guid(contextKey);
        if (parentKladrID != Guid.Empty)
        {
          var completeList = _kladrService.GetKladrs(parentKladrID, pr, null)
                                          .Select(
                                            x =>
                                            AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                                              x.Name + " " + x.Socr + ".", x.Id.ToString()))
                                          .ToList();

          return completeList;
        }
      }

      return new List<string>();
    }

    /// <summary>
    /// Обслуживает Intellisense для ввода имени
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <param name="contextKey"></param>
    /// <returns></returns>
    public static List<string> GetFirstNameAutoComplete(string prefixText, int count)
    {
      IStatementService statementService = ObjectFactory.GetInstance<IStatementService>();
      IList<AutoComplete> firstNameList = statementService.GetFirstNameAutoComplete(prefixText);

      List<string> completeList = new List<string>();
      foreach (AutoComplete complete in firstNameList)
        completeList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(complete.Name, complete.Id.ToString()));

      return completeList;
    }

    /// <summary>
    /// Обслуживает Intellisense для ввода отчества
    /// </summary>
    /// <param name="prefixText"></param>
    /// <param name="count"></param>
    /// <param name="contextKey"></param>
    /// <returns></returns>
    public static List<string> GetMiddleNameAutoComplete(string prefixText, int count, string contextKey)
    {
      IStatementService statementService = ObjectFactory.GetInstance<IStatementService>();

      //Фильтрация отчества в зависимости от имени
      Guid nameId;
      if (!Guid.TryParse(contextKey, out nameId))
        nameId = Guid.Empty;
      IList<AutoComplete> middleNameList = statementService.GetMiddleNameAutoComplete(prefixText, nameId);

      List<string> completeList = new List<string>();
      foreach (AutoComplete complete in middleNameList)
        completeList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(complete.Name, complete.Id.ToString()));

      return completeList;
    }

		/// <summary>
		/// Врзвращает контактную информацию по документу удл, найденному по номеру и серии
		/// </summary>
		/// <param name="number"></param>
		/// <param name="series"></param>
		/// <returns></returns>
		public static Representative GetRepresentativeContactInfoByUdl(string number, string series)
		{
			IStatementService statementService = ObjectFactory.GetInstance<IStatementService>();
			return statementService.GetRepresentativeContactInfoByUdl(number, series);
		}
  }
}