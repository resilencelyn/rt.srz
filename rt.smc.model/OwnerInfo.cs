//-----------------------------------------------------------------------
// <copyright file="OwnerInfo.cs" company="Rintech" author="Syurov">
//     Copyright (c) 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace rt.smc.model
{
  /// <summary>
  /// ���������� � ���������
  /// </summary>
  [ComVisible(true)]
  public class OwnerInfo
  {
    /// <summary>
    /// �������
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// ���
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// ��������
    /// </summary>
    public string MiddleName { get; set; }

    /// <summary>
    /// ���
    /// </summary>
    public string Sex { get; set; }

    /// <summary>
    /// ���� ��������
    /// </summary>
    public string Birthday { get; set; }

    /// <summary>
    /// ���� ��������
    /// </summary>
    public string BirthPlace { get; set; }

    /// <summary>
    /// ����������
    /// </summary>
    public string CitizenschipCode { get; set; }

    /// <summary>
    /// ����������
    /// </summary>
    public string CitizenschipName { get; set; }

    /// <summary>
    /// ����� ������
    /// </summary>
    public string PolisNumber { get; set; }

    /// <summary>
    /// ���� ������
    /// </summary>
    public string PolisDateFrom { get; set; }

    /// <summary>
    /// ���� ��������� (���� ����)
    /// </summary>
    public string PolisDateTo { get; set; }

    /// <summary>
    /// ��������� ���
    /// </summary>
    public string StateEcp { get; set; }

    /// <summary>
    /// �����
    /// </summary>
    public string Snils { get; set; }
  }
}