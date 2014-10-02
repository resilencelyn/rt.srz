//-------------------------------------------------------------------------------------
// <copyright file="TemplateManager.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

using rt.srz.model.common;
using rt.srz.model.srz;
using System;
namespace rt.srz.business.manager
{
  using NHibernate;

  using StructureMap;

  /// <summary>
  /// The TemplateManager.
  /// </summary>
  public partial class TemplateManager
  {
    /// <summary>
    /// ���������� ��� ���������� ������
    /// </summary>
    /// <param name="template"></param>
    public void AddOrUpdateTemplate(Template template)
    {
      //���� � ������� ������� ������ �� ��������� ������������ �� �� ���� ��������� ���� ���� ������� ��������
      if (template.Default.HasValue && template.Default.Value)
      {
        foreach (Template item in GetAll(int.MaxValue))
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
    /// �������� ����� ������� ������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Template CreateCopyOfTemplateVs(Guid id)
    {
      Template fromTemplate = GetById(id);
      Template result = CloneUtility.DeepClone(fromTemplate);
      result.Id = Guid.Empty;
      result.Default = false;
      result.Name = string.Format("����� - {0}", result.Name);
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

  }
}