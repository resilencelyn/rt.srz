// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseReneval.cs" company="������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ������� ������ ��������� �� ������ ���������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary> ������� ������ ��������� �� ������ ��������� </summary>
  public class CauseReneval : Concept
  {
    #region Constants

    /// <summary> ��������� ������ � ��, �� ��������� ������ ������ ������ ��� </summary>
    public const int Edit = 456;

    /// <summary> ��������� ������� </summary>
    public const int GettingTheFirst = 307;

    /// <summary> ��������� �������� ������ </summary>
    public const int RenevalChangePersonDetails = 308;

    /// <summary> ��������� ����� �������� </summary>
    public const int RenevalExpiration = 312;

    /// <summary> ���������� ������ </summary>
    public const int RenevalInaccuracy = 309;

    /// <summary> ������ </summary>
    public const int RenevalLoss = 311;

    /// <summary> ������������� � ������������� </summary>
    public const int RenevalUnusable = 310;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The is reneval.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsReneval(int id)
    {
      switch (id)
      {
        case GettingTheFirst:
        case RenevalChangePersonDetails:
        case RenevalInaccuracy:
        case RenevalUnusable:
        case RenevalLoss:
        case RenevalExpiration:
        case Edit:
          return true;
      }

      return false;
    }

    #endregion
  }
}