// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeStatement.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Код типа заявления
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace rt.srz.model.srz.concepts
{
  /// <summary> Код типа заявления </summary>
  public class TypeStatement : Concept
  {
    /// <summary> Выбор (замена) СМО </summary>
    public const int TypeStatement1 = 292;

    /// <summary> Переоформление полиса ОМС </summary>
    public const int TypeStatement2 = 293;

    /// <summary> Выдача дубликата </summary>
    public const int TypeStatement3 = 294;

    /// <summary> Изменение данных о ЗЛ, не требующих выдачи нового полиса ОМС </summary>
    public const int TypeStatement4 = 473;
  }
}