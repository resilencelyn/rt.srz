//-------------------------------------------------------------------------------------
// <copyright file="ISearchKeyTypeManager.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using rt.srz.model.srz;

namespace rt.srz.business.manager
{
  /// <summary>
  /// The interface SearchKeyTypeManager.
  /// </summary>
  public partial interface ISearchKeyTypeManager
  {
    /// <summary>
    /// ���������� ��������� ���� ������ ������ ��� ���������� ����� 
    /// </summary>
    /// <returns></returns>
    IList<SearchKeyType> GetSearchKeyTypesByTFoms();
    
    /// <summary>
    /// ��������� ���� ������ � ��
    /// </summary>
    /// <param name="keyType"></param>
    /// <returns></returns>
    Guid SaveSearchKeyType(SearchKeyType keyType);

    /// <summary>
    /// �������� ����� ������ (set ������� IsActive=false)
    /// </summary>
    /// <param name="pdpId">
    /// </param>
    void DeleteSearchKeyType(Guid keyTypeId);
  }
}