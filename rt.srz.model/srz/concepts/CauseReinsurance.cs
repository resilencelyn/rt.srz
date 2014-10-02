// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseFiling.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   ������� ������ ��������� �� ����� ��� ������ ���
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace rt.srz.model.srz.concepts
{
  /// <summary> ������� ������ ��������� �� ����� ��� ������ ��� </summary>
  public class CauseReinsurance : Concept
  {
    /// <summary> ��������� �� ����� ��� ������ ��� �� ���������� </summary>
    public const int Initialization = 302;

    /// <summary> ����� ��� </summary>
    public const int Choice = 303;

    /// <summary> ������ ��� �� ������� </summary>
    public const int ReinsuranceAtWill = 304;

    /// <summary> ������ ��� � ����� � ��������� </summary>
    public const int ReinsuranceWithTheMove = 305;

    /// <summary> ������ ��� � ����� � ������������ �������������� </summary>
    public const int ReinsuranceStopFinance = 306;

    public static bool IsReinsurance(int id)
    {
      switch (id)
      {
        case Initialization:
        case Choice:
        case ReinsuranceAtWill:
        case ReinsuranceWithTheMove:
        case ReinsuranceStopFinance:
          return true;
      }
      return false;
    }
  }
}