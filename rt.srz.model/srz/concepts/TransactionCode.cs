// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactionCode.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Перечень сообщений изменения данных
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz.concepts
{
  /// <summary> Перечень сообщений изменения данных </summary>
  public class TransactionCode : Concept
  {
    #region Constants

    /// <summary> A03 </summary>
    public const int A03 = 650;

    /// <summary> A08 </summary>
    public const int A08 = 651;

    /// <summary> A13 </summary>
    public const int A13 = 652;

    /// <summary> A24 </summary>
    public const int A24 = 653;

    /// <summary> Протокол форматно-логического контроля </summary>
    public const int F = 624;

    /// <summary> Файлы с изменениями от СМО </summary>
    public const int I = 620;

    /// <summary> Файлы корректировки данных от ТФОМС по отдельным записям или группам записей </summary>
    public const int K = 623;

    /// <summary> Протокол обработки файла с изменениями </summary>
    public const int P = 621;

    /// <summary> Файлы от ТФОМС с извещениями СМО о прекращении страхования </summary>
    public const int S = 622;

    /// <summary> ZWI </summary>
    public const int ZWI = 654;

    #endregion
  }
}