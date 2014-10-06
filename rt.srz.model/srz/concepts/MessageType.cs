// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageType.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Перечень сообщений изменения данных
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using rt.srz.model.srz;

/// <summary> Перечень сообщений изменения данных </summary>
public class MessageType : Concept
{
  #region Constants

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

  #endregion
}