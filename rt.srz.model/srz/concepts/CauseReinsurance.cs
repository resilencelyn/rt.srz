// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseReinsurance.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Причина подачи заявления на выбор или замену СМО
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary> Причина подачи заявления на выбор или замену СМО </summary>
  public class CauseReinsurance : Concept
  {
    #region Constants

    /// <summary> Выбор СМО </summary>
    public const int Choice = 303;

    /// <summary> Заявление на выбор или замену СМО не подавалось </summary>
    public const int Initialization = 302;

    /// <summary> Замена СМО по желанию </summary>
    public const int ReinsuranceAtWill = 304;

    /// <summary> Замена СМО в связи с прекращением финансирования </summary>
    public const int ReinsuranceStopFinance = 306;

    /// <summary> Замена СМО в связи с переездом </summary>
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