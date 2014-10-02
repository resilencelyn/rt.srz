using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace rt.srz.model.dto
{
  /// <summary>
  /// Результат поиска батчей
  /// </summary>
  [Serializable]
  [DataContract]
  public class SearchBatchResult
  {
    /// <summary>
    /// Идентификатор
    /// </summary>
    [XmlElement]
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>
    /// Имя отправителя
    /// </summary>
    [XmlElement]
    [DataMember]
    public string SenderName { get; set; }

    /// <summary>
    /// Имя получателя
    /// </summary>
    [XmlElement]
    [DataMember]
    public string ReceiverName { get; set; }

    /// <summary>
    /// Год периода
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime PeriodYear { get; set; }

    /// <summary>
    /// Месяц периода
    /// </summary>
    [XmlElement]
    [DataMember]
    public string PeriodMonth { get; set; }

    /// <summary>
    /// Номер батча
    /// </summary>
    [XmlElement]
    [DataMember]
    public int Number { get; set; }

    /// <summary>
    /// Количество записей
    /// </summary>
    [XmlElement]
    [DataMember]
    public int RecordCount { get; set; }

    /// <summary>
    /// Имя файла
    /// </summary>
    [XmlElement]
    [DataMember]
    public string FileName { get; set; }

    /// <summary>
    /// Код подтверждения
    /// </summary>
    [XmlElement]
    [DataMember]
    public string CodeConfirm { get; set; }
  }
}
