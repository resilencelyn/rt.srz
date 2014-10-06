// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISmoService.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The SMOService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service
{
  #region

  using System;
  using System.Collections.Generic;
  using System.ServiceModel;

  using rt.core.model.dto;
  using rt.srz.model.dto;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The SMOService interface.
  /// </summary>
  [ServiceContract]
  public interface ISmoService
  {
    #region Public Methods and Operators

    /// <summary>
    /// �������� pdp (set ������� IsActive=false)
    /// </summary>
    /// <param name="pdpId">
    /// </param>
    [OperationContract]
    void DeletePdp(Guid pdpId);

    /// <summary>
    /// �������� ��� (set ������� IsActive=false)
    /// </summary>
    /// <param name="smoId">
    /// </param>
    [OperationContract]
    void DeleteSmo(Guid smoId);

    /// <summary>
    ///   ������ ���� ���
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<Organisation> GetAllSmo();

    /// <summary>
    ///   ���������� ������ ���� ������������������ �����
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<Organisation> GetAllTfoms();

    /// <summary>
    /// The get childres.
    /// </summary>
    /// <param name="parentId">
    /// The parent id.
    /// </param>
    /// <returns>
    /// The <see cref="List{T}"/> .
    /// </returns>
    IList<Organisation> GetChildres(Guid parentId);

    /// <summary>
    /// ���������� ����� ������ �������
    /// </summary>
    /// <param name="pdpId">
    /// The pdp Id.
    /// </param>
    /// <returns>
    /// The <see cref="PointDistributionPolicy"/> .
    /// </returns>
    [OperationContract]
    Organisation GetPDP(Guid pdpId);

    /// <summary>
    /// ���������� ������ ���� ������������������ ������ ������ ������� ��� ��������� ���
    /// </summary>
    /// <param name="smoId">
    /// The smo Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    [OperationContract]
    IList<Organisation> GetPDPsBySmo(Guid smoId);

    /// <summary>
    /// �������� ������ ���� ������� ������
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    [OperationContract]
    SearchResult<Organisation> GetPdps(SearchPdpCriteria criteria);

    /// <summary>
    /// ���������� ���
    /// </summary>
    /// <param name="smoId">
    /// </param>
    /// <returns>
    /// The <see cref="Smo"/> .
    /// </returns>
    [OperationContract]
    Organisation GetSmo(Guid smoId);

    /// <summary>
    /// ���������� ���
    /// </summary>
    /// <param name="okato">
    /// The okato.
    /// </param>
    /// <param name="ogrn">
    /// The ogrn.
    /// </param>
    /// <returns>
    /// The <see cref="Smo"/> .
    /// </returns>
    [OperationContract]
    Organisation GetSmoByOkatoAndOgrn(string okato, string ogrn);

    /// <summary>
    /// �������� ������ ���� �����������
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    [OperationContract]
    SearchResult<Organisation> GetSmos(SearchSmoCriteria criteria);

    /// <summary>
    /// ���������� ������ ���� ������������������ ��� ��� ���������� �����
    /// </summary>
    /// <param name="tfomId">
    /// The tfom Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    [OperationContract]
    IList<Organisation> GetSmosByTfom(Guid tfomId);

    /// <summary>
    /// �������� ������ ���� �����������
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    [OperationContract]
    SearchResult<Organisation> GetSmosExcludeTfom(SearchSmoCriteria criteria);

    /// <summary>
    /// The get tfoms.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="Tfom"/> .
    /// </returns>
    [OperationContract]
    Organisation GetTfoms(Guid id);

    /// <summary>
    /// �������� ������ ���� ����������� ��� ����
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> .
    /// </returns>
    SearchResult<Organisation> GetTfoms(SearchSmoCriteria criteria);

    /// <summary>
    /// ���������� �����
    /// </summary>
    /// <param name="okato">
    /// The okato.
    /// </param>
    /// <returns>
    /// The <see cref="Organisation"/> .
    /// </returns>
    [OperationContract]
    Organisation GetTfomsByOkato(string okato);

    /// <summary>
    /// �������� ������� ����� �� ��
    /// </summary>
    /// <param name="workstationId">
    /// </param>
    /// <returns>
    /// The <see cref="Workstation"/> .
    /// </returns>
    [OperationContract]
    Workstation GetWorkstation(Guid workstationId);

    /// <summary>
    /// �������� ������ ���� ������� ������� ��� ������ ������
    /// </summary>
    /// <param name="pdpId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    [OperationContract]
    IList<Workstation> GetWorkstationsByPdp(Guid pdpId);

    /// <summary>
    /// ��������� ��������� ������ ��� ����������� � ����. ��� �������� ������� ������������ � ���� ��� ������� ���� ��
    ///   ���������� � ������, ����� �������
    /// </summary>
    /// <param name="mipId">
    /// </param>
    /// <param name="mos">
    /// </param>
    [OperationContract]
    void SaveMos(Guid mipId, List<Organisation> mos);

    /// <summary>
    /// ��������� ���������
    /// </summary>
    /// <param name="pdp">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> .
    /// </returns>
    [OperationContract]
    Guid SavePdp(Organisation pdp);

    /// <summary>
    /// ��������� ��������� ������ ��� � ����. ��� �������� ������� ������������ � ���� ��� ������ ��� �� ���������� �
    ///   ������, ����� �������
    /// </summary>
    /// <param name="smoId">
    /// </param>
    /// <param name="pdps">
    /// </param>
    [OperationContract]
    void SavePdps(Guid smoId, List<Organisation> pdps);

    /// <summary>
    /// ��������� ���������
    /// </summary>
    /// <param name="smo">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> .
    /// </returns>
    [OperationContract]
    Guid SaveSmo(Organisation smo);

    /// <summary>
    /// ������������� ������� IsOnline
    /// </summary>
    /// <param name="id">
    /// </param>
    /// <param name="isOnline">
    /// </param>
    [OperationContract]
    void SetTfomIsOnline(Guid id, bool isOnline);

    /// <summary>
    /// ���������� �� ��� � ��������� ����� �������� �� ���������
    /// </summary>
    /// <param name="smoId">
    /// </param>
    /// <param name="code">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    [OperationContract]
    bool SmoCodeExists(Guid smoId, string code);

    #endregion
  }
}