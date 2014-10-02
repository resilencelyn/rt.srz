//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace rt.srz.database.business.model
{
  /// <summary>
  /// ������� ������ ��� ������� �����
  /// </summary>
  public class ModelAdapter
  {
    /// <summary>
    /// ��������� �����
    /// </summary>
    public SearchKeyType SearchKeyType { get; set; }

    /// <summary>
    /// ������������ ������
    /// </summary>
    public Statement Statement { get; set; }
    
    /// <summary>
    /// ������������ ������
    /// </summary>
    public InsuredPersonDatum PersonData { get; set; }

    /// <summary>
    /// �������� ���
    /// </summary>
    public Document Document { get; set; }

    /// <summary>
    /// ����� �����������
    /// </summary>
    public address Address1 { get; set; }

    /// <summary>
    /// ����� ����������
    /// </summary>
    public address Address2 { get; set; }

    /// <summary>
    /// ����������� ���������
    /// </summary>
    public MedicalInsurance MedicalInsurance { get; set; }

    /// <summary>
    /// ����� ���������� �����������
    /// </summary>
    public string Okato { get; set; } 
  }
}
