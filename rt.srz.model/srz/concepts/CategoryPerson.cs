// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryPerson.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ��������� ��������������� ����
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  #region

  using System.Collections.Generic;

  #endregion

  /// <summary>
  ///   ��������� ��������������� ����
  /// </summary>
  public class CategoryPerson : Concept
  {
    #region Constants

    /// <summary>
    ///   ������������ ��������� ����������� � ���������� ��������� ����������� ���������
    /// </summary>
    public const int TerritorialAlienPermanently = 281;

    /// <summary>
    ///   ������������ �������� ����������� � ���������� ��������� ����������� ���������
    /// </summary>
    public const int TerritorialAlienTeporary = 282;

    /// <summary>
    ///   ������������ ����, ������� ����� �� ����������� ������ � ������������ � ����������� ������� "� ��������"
    /// </summary>
    public const int TerritorialRefugee = 284;

    /// <summary>
    ///   ������������ ��������� ���������� ���������
    /// </summary>
    public const int TerritorialRf = 280;

    /// <summary>
    ///   ������������ ��������� ����������� � ���������� ��������� ���� ��� �����������
    /// </summary>
    public const int TerritorialStatelessPermanently = 283;

    /// <summary>
    ///   ������������ �������� ����������� � ���������� ��������� ���� ��� �����������
    /// </summary>
    public const int TerritorialStatelessTeporary = 632;

    /// <summary>
    ///   ��������� �� ��������
    /// </summary>
    public const int Unknown = 604;

    /// <summary>
    ///   ���������� ��������� ����������� � ���������� ��������� ����������� ���������
    /// </summary>
    public const int WorkerAlienPermanently = 276;

    /// <summary>
    ///   ���������� �������� ����������� � ���������� ��������� ����������� ���������
    /// </summary>
    public const int WorkerAlienTeporary = 277;

    /// <summary>
    ///   ���������� ����, ������� ����� �� ����������� ������ � ������������ � ����������� ������� "� ��������"
    /// </summary>
    public const int WorkerRefugee = 279;

    /// <summary>
    ///   ���������� ��������� ���������� ���������
    /// </summary>
    public const int WorkerRf = 275;

    /// <summary>
    ///   ���������� ��������� ����������� � ���������� ��������� ���� ��� �����������
    /// </summary>
    public const int WorkerStatelessPermanently = 278;

    /// <summary>
    ///   ���������� �������� ����������� � ���������� ��������� ���� ��� �����������
    /// </summary>
    public const int WorkerStatelessTeporary = 631;

    #endregion

    #region Static Fields

    /// <summary>
    ///   The working.
    /// </summary>
    private static readonly List<int> HasDocumentResidency = new List<int>
                                                             {
                                                               WorkerAlienPermanently, 
                                                               TerritorialAlienPermanently, 
                                                               WorkerAlienTeporary, 
                                                               TerritorialAlienTeporary, 
                                                               WorkerStatelessPermanently, 
                                                               TerritorialStatelessPermanently, 
                                                             };

    /// <summary>
    ///   The working.
    /// </summary>
    private static readonly List<int> Working = new List<int>
                                                {
                                                  WorkerRf, 
                                                  WorkerAlienPermanently, 
                                                  WorkerAlienTeporary, 
                                                  WorkerStatelessPermanently, 
                                                  WorkerStatelessTeporary, 
                                                  WorkerRefugee, 
                                                  Unknown
                                                };

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The is document residency.
    /// </summary>
    /// <param name="category">
    /// The category.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    public static bool IsDocumentResidency(int category)
    {
      return HasDocumentResidency.Contains(category);
    }

    /// <summary>
    /// The is working.
    /// </summary>
    /// <param name="category">
    /// The category.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    public static bool IsWorking(int category)
    {
      return Working.Contains(category);
    }

    #endregion
  }
}