// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISearchKeyTypeManager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface SearchKeyTypeManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.srz.model.srz;

  /// <summary>
  ///   The interface SearchKeyTypeManager.
  /// </summary>
  public partial interface ISearchKeyTypeManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// �������� ����� ������ (set ������� IsActive=false)
    /// </summary>
    /// <param name="keyTypeId">
    /// The key Type Id.
    /// </param>
    void DeleteSearchKeyType(Guid keyTypeId);

    /// <summary>
    ///   ���������� ��������� ���� ������ ������ ��� ���������� �����
    /// </summary>
    /// <returns>
    ///   The <see cref="IList{SearchKeyType}" />.
    /// </returns>
    IList<SearchKeyType> GetSearchKeyTypesByTFoms();

    /// <summary>
    /// ��������� ���� ������ � ��
    /// </summary>
    /// <param name="keyType">
    /// The key Type.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/>.
    /// </returns>
    Guid SaveSearchKeyType(SearchKeyType keyType);

    #endregion
  }
}