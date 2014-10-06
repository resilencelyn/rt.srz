// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Oid.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The Oid.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  /// <summary>
  ///   The Oid.
  /// </summary>
  public partial class Oid
  {
    #region Constants

    /// <summary> Типы полей автозаполнения </summary>
    public const string AutoCompleteType = "1.1.1.2";

    /// <summary> Таблица Б.59 Код подтверждения </summary>
    public const string CodeConfirm = "1.2.643.2.40.5.100.8";

    /// <summary> Типы вложений, использующихся при обменах /// </summary>
    public const string ContentType = "1.2.643.2.40.3.3.0.7.2";

    /// <summary> Источник информации о занятости </summary>
    public const string EmploymentSourceType = "1.1.1.10";

    /// <summary> Код причины совпадения </summary>
    public const string KeyType = "1.2.643.2.40.1.14";

    /// <summary> Тип связи заявления и сообщения </summary>
    public const string MessageStatementType = "1.1.1.14";

    /// <summary> Перечень сообщений изменения данных </summary>
    public const string MessageType = "1.1.1.12";

    /// <summary> Медицинские организации </summary>
    public const string Mo = "1.2.643.2.40.3.1.3";

    /// <summary> Тип использования ключа поиска </summary>
    public const string OperationKey = "1.1.1.6";

    /// <summary> Отделения пенсионного фонда России </summary>
    public const string Opfr = "1.1.1.13";

    /// <summary> Коды отчётных периодов </summary>
    public const string PeriodCode = "1.2.643.2.40.3.3.0.6.14";

    /// <summary> Признак идентификации застрахованного лица в ОПФР </summary>
    public const string PfrFeature = "1.1.1.9";

    /// <summary> Пункт выдачи полисов </summary>
    public const string Pvp = "1.2.643.2.40.3.3.1.0";

    /// <summary>
    ///   The Классификатор причин внесения изменений в РС ЕРЗ.
    /// </summary>
    public const string R001 = "R001";

    /// <summary> Перечень событий изменения данных в ИС ЕРП  </summary>
    public const string ReasonType = "1.2.643.2.40.5.100.62";

    /// <summary> Страховые медицинские организации </summary>
    public const string Smo = "1.2.643.2.40.3.1.4";

    /// <summary> Статус застрахованного </summary>
    public const string StatusPerson = "1.1.1.3";

    /// <summary> Территориальные фонды </summary>
    public const string Tfoms = "1.2.643.2.40.3.3.1";

    /// <summary> Перечень персональных данных застрахованного лица </summary>
    public const string TypeFields = "1.1.1.11";

    /// <summary> Тип файла взаимодействия </summary>
    public const string TypeFile = "1.1.1.7";

    /// <summary> Код типа идентификатора </summary>
    public const string TypeIdentificator = "1.2.643.2.40.5.100.203";

    /// <summary> Тип сертификата </summary>
    public const string TypeSertificate = "1.1.1.4";

    /// <summary> Субъект взаимодействия </summary>
    public const string TypeSubject = "1.1.1.8";

    /// <summary> Тип дубля по человеку </summary>
    public const string TypeTwin = "1.1.1.5";

    /// <summary>
    ///   The документудл.
    /// </summary>
    public const string ДокументУдл = "1.2.643.2.40.5.100.203.1";

    /// <summary>
    ///   The категориязастрахованноголица.
    /// </summary>
    public const string Категориязастрахованноголица = "1.2.643.2.40.3.3.0.6.6";

    /// <summary>
    ///   The кодтипазаявления.
    /// </summary>
    public const string Кодтипазаявления = "1.2.643.2.40.3.3.0.6.8";

    /// <summary>
    ///   The отношениекзастрахованномулицу.
    /// </summary>
    public const string Отношениекзастрахованномулицу = "1.2.643.2.40.5.0.18.11";

    /// <summary>
    ///   The пол.
    /// </summary>
    public const string Пол = "1.2.643.2.40.5.0.18.1";

    /// <summary>
    ///   The причинаподачизаявлениянавыборилизаменусмо.
    /// </summary>
    public const string ПричинаподачизаявлениянавыборилизаменуСмо = "1.2.643.2.40.3.3.0.6.7";

    /// <summary>
    ///   The причинаподачизаявлениянавыдачудубликата.
    /// </summary>
    public const string Причинаподачизаявлениянавыдачудубликата = "1.2.643.2.40.3.3.0.6.9";

    /// <summary>
    ///   The способподачизаявления.
    /// </summary>
    public const string Способподачизаявления = "1.2.643.2.40.3.3.0.6.11";

    /// <summary>
    ///   The статусызаявлениянавыдачуполисаомс.
    /// </summary>
    public const string СтатусызаявлениянавыдачуполисаОмс = "1.2.643.2.40.3.3.0.1.4.2";

    /// <summary>
    ///   The страна.
    /// </summary>
    public const string Страна = "1.2.643.2.40.5.0.25.3";

    /// <summary>
    ///   The Страна для места рождения.
    /// </summary>
    public const string Странадляместарождения = "1.2.643.2.40.5.0.25.3.1.1";

    /// <summary>
    ///   The типсобытия.
    /// </summary>
    public const string ТипСобытия = "1.1.1.1";

    /// <summary>
    ///   The формаизготовленияполиса.
    /// </summary>
    public const string Формаизготовленияполиса = "1.2.643.2.40.5.100.86";

    #endregion
  }
}