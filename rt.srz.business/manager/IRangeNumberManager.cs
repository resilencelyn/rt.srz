// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRangeNumberManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface RangeNumberManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System.Collections.Generic;

  using rt.srz.model.srz;

  /// <summary>
  ///   The interface RangeNumberManager.
  /// </summary>
  public partial interface IRangeNumberManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="range">
    /// The range.
    /// </param>
    void AddOrUpdateRangeNumber(RangeNumber range);

    /// <summary>
    ///   Зачитывает все записи
    /// </summary>
    /// <returns>
    ///   The <see cref="IList" />.
    /// </returns>
    IList<RangeNumber> GetRangeNumbers();

    /// <summary>
    /// Получает шаблон для печати вс по по номеру временного свидетельства заявления
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="Template"/>.
    /// </returns>
    Template GetTemplateByStatement(Statement statement);

    /// <summary>
    /// Пересекается ли указанная запись с другими по диапозону. Только для диапазонов с парент ид = null,
    ///   т.е. это проверка пересечений главных диапазонов из шапки страницы
    /// </summary>
    /// <param name="range">
    /// The range.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool IntersectWithOther(RangeNumber range);

    #endregion
  }
}