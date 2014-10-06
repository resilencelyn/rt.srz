// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchBatchResult.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Результат поиска батчей
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.dto
{
  using System;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  /// <summary>
  ///   Результат поиска батчей
  /// </summary>
  [Serializable]
  [DataContract]
  public class SearchBatchResult
  {
    #region Public Properties

    /// <summary>
    ///   Код подтверждения
    /// </summary>
    [XmlElement]
    [DataMember]
    public string CodeConfirm { get; set; }

    /// <summary>
    ///   Имя файла
    /// </summary>
    [XmlElement]
    [DataMember]
    public string FileName { get; set; }

    /// <summary>
    ///   Идентификатор
    /// </summary>
    [XmlElement]
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>
    ///   Номер батча
    /// </summary>
    [XmlElement]
    [DataMember]
    public int Number { get; set; }

    /// <summary>
    ///   Месяц периода
    /// </summary>
    [XmlElement]
    [DataMember]
    public string PeriodMonth { get; set; }

    /// <summary>
    ///   Год периода
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime PeriodYear { get; set; }

    /// <summary>
    ///   Имя получателя
    /// </summary>
    [XmlElement]
    [DataMember]
    public string ReceiverName { get; set; }

    /// <summary>
    ///   Количество записей
    /// </summary>
    [XmlElement]
    [DataMember]
    public int RecordCount { get; set; }

    /// <summary>
    ///   Имя отправителя
    /// </summary>
    [XmlElement]
    [DataMember]
    public string SenderName { get; set; }

    #endregion
  }
}