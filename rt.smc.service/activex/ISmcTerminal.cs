// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISmcTerminal.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Runtime.InteropServices;
using rt.smc.model;

#endregion

namespace rt.smc.service.activex
{
  /// <summary>
  ///   The SmcTerminal interface.
  /// </summary>
  [ComVisible(true)]
  [Guid("8890C815-2A99-4B64-BA3A-1C98C13CFCB5")]
  public interface ISmcTerminal
  {
    /// <summary>
    /// The set card reader.
    /// </summary>
    [ComVisible(true)]
    void SetCardReader();

    /// <summary>
    ///   ��������� ���������� � ����� (���, ����� �����, ������������� � �.�.)
    /// </summary>
    /// <returns> The <see cref="CardInfoStrings" /> . </returns>
    [ComVisible(true)]
    CardInfoStrings GetCardInfo();

    /// <summary>
    ///   ��������� ���������� � ������� ���
    /// </summary>
    /// <returns> The <see cref="SmoInfoStrings" /> . </returns>
    [ComVisible(true)]
    SmoInfoStrings GetCurrentSmo();

    /// <summary>
    ///   ������ ���������� � ��������� ����� (��������������)
    /// </summary>
    /// <returns> The <see cref="OwnerInfo" /> . </returns>
    [ComVisible(true)]
    OwnerInfo GetOwnerInfo();

    /// <summary>
    /// ����� ���
    /// </summary>
    /// <param name="ogrnSmo">
    /// ���� ��� 
    /// </param>
    /// <param name="okatoSmo">
    /// ����� ��� 
    /// </param>
    /// <returns>
    /// true - ���� �� ����������� 
    /// </returns>
    [ComVisible(true)]
    bool ChangeSmo(string ogrnSmo, string okatoSmo, string dateFrom, string dateTo, string securityModulePin, string cardPin);

    /// <summary>
    /// ��������� ���������� � ��� �������
    /// </summary>
    /// <param name="token">
    ///   ��������������� �����
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool OpenConnection(string token);

    /// <summary>
    /// ��������� ����������
    /// </summary>
    void CloseConnection();
  }
}