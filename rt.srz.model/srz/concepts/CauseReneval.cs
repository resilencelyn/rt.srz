// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseReneval.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
    /// <summary> Получение впервые </summary>
    public const int GettingTheFirst = 307;

    /// <summary> Изменение анкетных данных </summary>
    public const int RenevalChangePersonDetails = 308;

    /// <summary> Неточность данных </summary>
    public const int RenevalInaccuracy = 309;

    /// <summary> Непригодность к использованию </summary>
    public const int RenevalUnusable = 310;

    /// <summary> Утрата </summary>
    public const int RenevalLoss = 311;

    /// <summary> Окончание срока действия </summary>
    public const int RenevalExpiration = 312;

    /// <summary> Изменение данных о ЗЛ, не требующих выдачи нового полиса ОМС </summary>
    public const int Edit = 456;

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
  }
}