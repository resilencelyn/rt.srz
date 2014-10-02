// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAutoCompleteManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
  using rt.srz.model.srz;
  using rt.srz.model.dto;
  using rt.core.model;

  #endregion

  /// <summary>
  ///   The interface AutoCompleteManager.
  /// </summary>
  public partial interface IAutoCompleteManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// ���������� ������ ���������� ��� �����
    /// </summary>
    /// <param name="prefix">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<AutoComplete> GetFirstNameAutoComplete(string prefix);

    /// <summary>
    /// ���������� ������ ��������� ��� ��������
    /// </summary>
    /// <param name="prefix">
    /// </param>
    /// <param name="nameId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<AutoComplete> GetMiddleNameAutoComplete(string prefix, Guid nameId);

    /// <summary>
    /// �������� ��������� �� �������� ��� ��� � �������
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> . 
    /// </returns>
    SearchResult<AutoComplete> GetFirstMiddleNames(SearchAutoCompleteCriteria criteria);

    /// <summary>
    /// ��������� ���������� �� ��� ������ � ���� � ����� �� ������, �����, �����
    /// </summary>
    /// <param name="firstMiddleName">
    /// The first Middle Name. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    bool FirstMiddleNameExists(AutoComplete firstMiddleName);


    #endregion
  }
}