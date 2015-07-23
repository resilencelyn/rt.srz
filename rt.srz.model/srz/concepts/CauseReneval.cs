// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseReneval.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Причина подачи заявления на выдачу дубликата
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary> Причина подачи заявления на выдачу дубликата </summary>
  public class CauseReneval : Concept
  {
    #region Constants

    /// <summary> Изменение данных о ЗЛ, не требующих выдачи нового полиса ОМС </summary>
    public const int Edit = 456;

    /// <summary> Получение впервые </summary>
    public const int GettingTheFirst = 307;

    /// <summary> Изменение анкетных данных </summary>
    public const int RenevalChangePersonDetails = 308;

    /// <summary> Окончание срока действия </summary>
    public const int RenevalExpiration = 312;

    /// <summary> Неточность данных </summary>
    public const int RenevalInaccuracy = 309;

    /// <summary> Утрата </summary>
    public const int RenevalLoss = 311;

    /// <summary> Непригодность к использованию </summary>
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