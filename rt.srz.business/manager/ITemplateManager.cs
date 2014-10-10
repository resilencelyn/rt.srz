// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITemplateManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface TemplateManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;

  using rt.srz.model.srz;

  /// <summary>
  ///   The interface TemplateManager.
  /// </summary>
  public partial interface ITemplateManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Добавление или обновление записи
    /// </summary>
    /// <param name="template">
    /// The template.
    /// </param>
    void AddOrUpdateTemplate(Template template);

    /// <summary>
    /// Создание копии шаблона печати
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="Template"/>.
    /// </returns>
    Template CreateCopyOfTemplate(Guid id);

    #endregion
  }
}