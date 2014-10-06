// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITwinManager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface TwinManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.core.model.dto;
  using rt.srz.model.dto;
  using rt.srz.model.srz;

  /// <summary>
  ///   The interface TwinManager.
  /// </summary>
  public partial interface ITwinManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// ������������� ���������
    /// </summary>
    /// <param name="twinId">
    /// </param>
    void AnnulateTwin(Guid twinId);

    /// <summary>
    /// ������� ��� ��������� ������� ���� ���������� ������ �� ����� �����
    /// </summary>
    /// <param name="keyId">
    /// </param>
    void DeleteTwinsCalculatedOnlyByGivenKey(Guid keyId);

    /// <summary>
    /// �������� ��� ���������
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<Twin> GetTwins();

    /// <summary>
    /// ��������� �� �������� ��� �������� �����������
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    SearchResult<Twin> GetTwins(SearchTwinCriteria criteria);

    /// <summary>
    /// ���������� ���������
    /// </summary>
    /// <param name="twinId">
    /// </param>
    /// <param name="mainInsuredPersonId">
    /// </param>
    /// <param name="secondInsuredPersonId">
    /// </param>
    void JoinTwins(Guid twinId, Guid mainInsuredPersonId, Guid secondInsuredPersonId);

    /// <summary>
    /// �������� �������� ��� ���������
    /// </summary>
    /// <param name="Id">
    /// </param>
    void RemoveTwin(Guid Id);

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="personId">
    /// The person Id.
    /// </param>
    /// <param name="statementsToSeparate">
    /// The statements To Separate.
    /// </param>
    /// <param name="copyDeadInfo">
    /// The copy Dead Info.
    /// </param>
    /// <param name="status">
    /// The status.
    /// </param>
    void Separate(Guid personId, IList<Statement> statementsToSeparate, bool copyDeadInfo, int status);

    #endregion
  }
}