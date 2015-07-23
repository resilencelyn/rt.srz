// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAutoCompleteManager.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface AutoCompleteManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using System.Collections.Generic;

  using rt.core.model.dto;
  using rt.srz.model.dto;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The interface AutoCompleteManager.
  /// </summary>
  public partial interface IAutoCompleteManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// ��������� ���������� �� ��� ������ � ���� � ����� �� ������, �����, �����
    /// </summary>
    /// <param name="autoComplete">
    /// The first Middle Name.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    bool AutoCompleteExists(AutoComplete autoComplete);

    /// <summary>
    /// �������� ��������� �� �������� ��� ��� � �������
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult{AutoComplete}"/> .
    /// </returns>
    SearchResult<AutoComplete> GetAutoCompleteByCriteria(SearchAutoCompleteCriteria criteria);

    /// <summary>
    /// ���������� ������ ���������� ��� �����
    /// </summary>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <returns>
    /// The <see cref="IList{AutoComplete}"/>.
    /// </returns>
    IList<AutoComplete> GetFirstNameAutoComplete(string prefix);

    /// <summary>
    /// ���������� ������ ��������� ��� ��������
    /// </summary>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="nameId">
    /// The name Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList{AutoComplete}"/>.
    /// </returns>
    IList<AutoComplete> GetMiddleNameAutoComplete(string prefix, Guid nameId);

    #endregion
  }
}