// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionCode.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The permission code.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model
{
  /// <summary>
  ///   The permission code.
  /// </summary>
  public enum PermissionCode
  {
    /// <summary>
    ///   The attach to own region.
    /// </summary>
    AttachToOwnRegion = 1, 

    /// <summary>
    ///   The attach to own smo.
    /// </summary>
    AttachToOwnSmo = 2, 

    /// <summary>
    ///   The edit smos.
    /// </summary>
    EditSmos = 3, 

    /// <summary>
    ///   "Заявления застрахованных лиц"
    /// </summary>
    Statements = 5, 

    /// <summary>
    ///   6 Пакетные операции эскпорта для СМО
    /// </summary>
    ExportSmoBatches = 6, 

    /// <summary>
    ///   7 Обработка дубликатов
    /// </summary>
    Twins = 7, 

    /// <summary>
    ///   8 Ключи поиска
    /// </summary>
    SearchKeys = 8, 

    /// <summary>
    ///   9 Статистика ПФР
    /// </summary>
    PfrStatistic = 9, 

    /// <summary>
    ///   10 Просмотр ошибок синхронизации из СРЗ
    /// </summary>
    ErrorSynchronizationView = 10, 

    /// <summary>
    ///   11 Территориальные фонды
    /// </summary>
    Tfoms = 11, 

    /// <summary>
    ///   12 СМО
    /// </summary>
    Smos = 12, 

    /// <summary>
    ///   13 МО
    /// </summary>
    Mos = 13, 

    /// <summary>
    ///   14 Кодификаторы
    /// </summary>
    Concepts = 14, 

    /// <summary>
    ///   15 Имена и отчества
    /// </summary>
    FirstMiddleNames = 15, 

    /// <summary>
    ///   16 Диапазоны номеров ВС
    /// </summary>
    RangeNumbers = 16, 

    /// <summary>
    ///   17 Настройка проверок ФЛК
    /// </summary>
    ManageChecks = 17, 

    /// <summary>
    ///   18 Пользователи и группы
    /// </summary>
    Users = 18, 

    /// <summary>
    ///   19 Роли
    /// </summary>
    Roles = 19, 

    /// <summary>
    ///   20 Разрешения
    /// </summary>
    Permissions = 20, 

    /// <summary>
    ///   21 Расписание задач
    /// </summary>
    SchedulerTask = 21, 

    /// <summary>
    ///   22 Текущие задачи
    /// </summary>
    JobsProgress = 22, 

    /// <summary>
    ///   23 Установка
    /// </summary>
    Installation = 23, 

    /// <summary>
    ///   Форма поиска заявлений. Создать
    /// </summary>
    Search_Create = 24, 

    /// <summary>
    ///   Форма поиска заявлений. Переоформить
    /// </summary>
    Search_Reneval = 25, 

    /// <summary>
    ///   Форма поиска заявлений. Открыть
    /// </summary>
    Search_Edit = 26, 

    /// <summary>
    ///   Форма поиска заявлений. Отменить
    /// </summary>
    Search_Delete = 27, 

    /// <summary>
    ///   Форма поиска заявлений. Прочитать УЭК
    /// </summary>
    Search_ReadUec = 28, 

    /// <summary>
    ///   Форма поиска заявлений. Записать на УЭК
    /// </summary>
    Search_WriteUec = 29, 

    /// <summary>
    ///   Форма поиска заявлений. Прочитать эл. полис
    /// </summary>
    Search_ReadSmartCard = 30, 

    /// <summary>
    ///   Форма поиска заявлений. Разделение объединённых
    /// </summary>
    Search_Separate = 31, 

    /// <summary>
    ///   Форма поиска заявлений. История страхования
    /// </summary>
    Search_InsuranceHistory = 32, 

    /// <summary>
    ///   Отмена смерти
    /// </summary>
    CancelDeath = 33, 

    /// <summary>
    ///   Шаблоны печати вс
    /// </summary>
    TemplatesVs = 34, 

    /// <summary>
    ///   Форма поиска заявлений. Выдать
    /// </summary>
    Search_GiveOut = 35, 

    /// <summary>
    ///   Форма поиска заявлений. Открыть заявление не своего СМО
    /// </summary>
    Search_OpenNotOwnSmo = 36, 

    /// <summary>
    ///   Настройка задач
    /// </summary>
    JobsSettings = 37, 

    /// <summary>
    ///   СЕртификаты УЦ
    /// </summary>
    AssignUecCertificates = 38
  };
}