//-------------------------------------------------------------------------------------
// <copyright file="IBatchManager.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using rt.core.model;
using rt.srz.model.dto;

namespace rt.srz.business.manager
{
  using rt.core.model.dto;

  /// <summary>
  /// The interface BatchManager.
  /// </summary>
  public partial interface IBatchManager
  {
    /// <summary>
    /// �������� ������ ��� ������ �� �������
    /// </summary>
    /// <param name="periodId"></param>
    /// <returns></returns>
    IList<Batch> GetPfrBatchesByPeriod(Guid periodId);

    /// <summary>
    /// ������������ ����� �������� �������� �������� ��������� ��� ���
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    SearchResult<SearchBatchResult> SearchExportSmoBatches(SearchExportSmoBatchCriteria criteria);

    /// <summary>
    /// �������� ���� ��� �� �����������
    /// </summary>
    /// <param name="batchId"></param>
    void MarkBatchAsUnexported(Guid batchId);

    /// <summary>
    /// The get pfr batches by user.
    /// </summary>
    /// <returns>
    /// The <see cref="IList{T}"/>.
    /// </returns>
    IList<Batch> GetPfrBatchesByUser();

    /// <summary>
    /// ���������� ��� ������� �� ������� ���� ������ �� ������ � ����� �������� ���
    /// </summary>
    /// <returns></returns>
    IList<Period> GetPfrPeriods();

    /// <summary>
    /// ���������� ���������� �� ���������� ���
    /// </summary>
    /// <param name="batchId">
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/> . 
    /// </returns>
    PfrStatisticInfo GetPfrStatisticInfoByBatch(Guid batchId);

    /// <summary>
    /// ���������� ���������� �� ���������� ���
    /// </summary>
    /// <param name="periodId">
    /// </param>
    /// <returns>
    /// The <see cref="PfrStatisticInfo"/> . 
    /// </returns>
    PfrStatisticInfo GetPfrStatisticInfoByPeriod(Guid periodId);




  }
}