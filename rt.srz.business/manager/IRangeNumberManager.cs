// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRangeNumberManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface RangeNumberManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System.Collections.Generic;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The interface RangeNumberManager.
  /// </summary>
  public partial interface IRangeNumberManager
  {
    /// <summary>
    /// ƒобавление или обновление записи
    /// </summary>
    /// <param name="range"></param>
    void AddOrUpdateRangeNumber(RangeNumber range);

    /// <summary>
    /// ѕересекаетс€ ли указанна€ запись с другими по диапозону. “олько дл€ диапазонов с парент ид = null, 
    /// т.е. это проверка пересечений главных диапазонов из шапки страницы
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    bool IntersectWithOther(RangeNumber range);

    /// <summary>
    /// ѕолучает шаблон дл€ печати вс по по номеру временного свидетельства за€влени€
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    Template GetTemplateVsByStatement(Statement statement);

    /// <summary>
    /// «ачитывает все записи
    /// </summary>
    /// <returns></returns>
    IList<RangeNumber> GetRangeNumbers();


  }
}