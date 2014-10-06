// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryPerson.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Категория застрахованного лица
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  #region

  using System.Collections.Generic;

  #endregion

  /// <summary>
  ///   Категория застрахованного лица
  /// </summary>
  public class CategoryPerson : Concept
  {
    #region Constants

    /// <summary>
    ///   Неработающий постоянно проживающий в Российской Федерации иностранный гражданин
    /// </summary>
    public const int TerritorialAlienPermanently = 281;

    /// <summary>
    ///   Неработающий временно проживающий в Российской Федерации иностранный гражданин
    /// </summary>
    public const int TerritorialAlienTeporary = 282;

    /// <summary>
    ///   Неработающее лицо, имеющее право на медицинскую помощь в соответствии с Федеральным законом "О беженцах"
    /// </summary>
    public const int TerritorialRefugee = 284;

    /// <summary>
    ///   Неработающий гражданин Российской Федерации
    /// </summary>
    public const int TerritorialRf = 280;

    /// <summary>
    ///   Неработающее постоянно проживающее в Российской Федерации лицо без гражданства
    /// </summary>
    public const int TerritorialStatelessPermanently = 283;

    /// <summary>
    ///   Неработающее временно проживающее в Российской Федерации лицо без гражданства
    /// </summary>
    public const int TerritorialStatelessTeporary = 632;

    /// <summary>
    ///   Категория не известна
    /// </summary>
    public const int Unknown = 604;

    /// <summary>
    ///   Работающий постоянно проживающий в Российской Федерации иностранный гражданин
    /// </summary>
    public const int WorkerAlienPermanently = 276;

    /// <summary>
    ///   Работающий временно проживающий в Российской Федерации иностранный гражданин
    /// </summary>
    public const int WorkerAlienTeporary = 277;

    /// <summary>
    ///   Работающее лицо, имеющее право на медицинскую помощь в соответствии с Федеральным законом "О беженцах"
    /// </summary>
    public const int WorkerRefugee = 279;

    /// <summary>
    ///   Работающий гражданин Российской Федерации
    /// </summary>
    public const int WorkerRf = 275;

    /// <summary>
    ///   Работающее постоянно проживающее в Российской Федерации лицо без гражданства
    /// </summary>
    public const int WorkerStatelessPermanently = 278;

    /// <summary>
    ///   Работающее временно проживающее в Российской Федерации лицо без гражданства
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