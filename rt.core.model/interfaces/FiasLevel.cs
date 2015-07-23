// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiasLevel.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.interfaces
{
  /// <summary>
  ///   C������� ����� ������ ������������� �������� ��������.
  /// </summary>
  public enum FiasLevel
  {
    /// <summary>
    ///  ������� ������� 
    /// </summary>
    Subject = 1,

    /// <summary>
    /// ������� ����������� ������
    /// </summary>
    AutonomousOkrug = 2,

    /// <summary>
    ///  ������� ������
    /// </summary>
    Area = 3,

    /// <summary>
    ///   ������� ������
    /// </summary>
    City = 4,

    /// <summary>
    /// ������� ��������������� ����������
    /// </summary>
    InCity = 5,

    /// <summary>
    ///   ������� ����������� ������
    /// </summary>
    Town = 6,

    /// <summary>
    ///   ������� �����
    /// </summary>
    Street = 7
  }
}