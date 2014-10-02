//-------------------------------------------------------------------------------------
// <copyright file="ITemplateManager.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System;
namespace rt.srz.business.manager
{
  /// <summary>
  /// The interface TemplateManager.
  /// </summary>
  public partial interface ITemplateManager
  {
    /// <summary>
    /// ���������� ��� ���������� ������
    /// </summary>
    /// <param name="template"></param>
    void AddOrUpdateTemplate(Template template);

    /// <summary>
    /// �������� ����� ������� ������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Template CreateCopyOfTemplateVs(Guid id);

  }
}