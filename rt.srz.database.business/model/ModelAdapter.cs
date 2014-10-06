// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelAdapter.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ������� ������ ��� ������� �����
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.model
{
  /// <summary>
  ///   ������� ������ ��� ������� �����
  /// </summary>
  public class ModelAdapter
  {
    #region Public Properties

    /// <summary>
    ///   ����� �����������
    /// </summary>
    public address Address1 { get; set; }

    /// <summary>
    ///   ����� ����������
    /// </summary>
    public address Address2 { get; set; }

    /// <summary>
    ///   �������� ���
    /// </summary>
    public Document Document { get; set; }

    /// <summary>
    ///   ����������� ���������
    /// </summary>
    public MedicalInsurance MedicalInsurance { get; set; }

    /// <summary>
    ///   ����� ���������� �����������
    /// </summary>
    public string Okato { get; set; }

    /// <summary>
    ///   ������������ ������
    /// </summary>
    public InsuredPersonDatum PersonData { get; set; }

    /// <summary>
    ///   ��������� �����
    /// </summary>
    public SearchKeyType SearchKeyType { get; set; }

    /// <summary>
    ///   ������������ ������
    /// </summary>
    public Statement Statement { get; set; }

    #endregion
  }
}