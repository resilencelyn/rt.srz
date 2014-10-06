// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseReinsurance.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
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
    #region Constants

    /// <summary> ����� ��� </summary>
    public const int Choice = 303;

    /// <summary> ��������� �� ����� ��� ������ ��� �� ���������� </summary>
    public const int Initialization = 302;

    /// <summary> ������ ��� �� ������� </summary>
    public const int ReinsuranceAtWill = 304;

    /// <summary> ������ ��� � ����� � ������������ �������������� </summary>
    public const int ReinsuranceStopFinance = 306;

    /// <summary> ������ ��� � ����� � ��������� </summary>
    public const int ReinsuranceWithTheMove = 305;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The is reinsurance.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
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

    #endregion
  }
}