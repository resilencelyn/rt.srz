// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateManager.cs" company="јль€нс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The TemplateManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;

  using NHibernate;

  using rt.srz.model.common;
  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  ///   The TemplateManager.
  /// </summary>
  public partial class TemplateManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// ƒобавление или обновление записи
    /// </summary>
    /// <param name="template">
    /// The template.
    /// </param>
    public void AddOrUpdateTemplate(Template template)
    {
      // если в текущем шаблоне делаем по умолчанию использовать то во всех остальных надо этот признак обнулить
      if (template.Default.HasValue && template.Default.Value)
      {
        foreach (var item in GetAll(int.MaxValue))
        {
          if (item.Id != template.Id)
          {
            item.Default = false;
            SaveOrUpdate(item);
          }
        }
      }

      SaveOrUpdate(template);
    }

    /// <summary>
    /// —оздание копии шаблона печати
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="Template"/>.
    /// </returns>
    public Template CreateCopyOfTemplate(Guid id)
    {
      var fromTemplate = GetById(id);
      var result = CloneUtility.DeepClone(fromTemplate);
      result.Id = Guid.Empty;
      result.Default = false;
      result.Name = string.Format(" опи€ - {0}", result.Name);
      if (result.Name.Length > 500)
      {
        result.Name = result.Name.Substring(0, 500);
      }

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      session.Evict(fromTemplate);
      SaveOrUpdate(result);
      session.Flush();
      return result;
    }

    #endregion
  }
}