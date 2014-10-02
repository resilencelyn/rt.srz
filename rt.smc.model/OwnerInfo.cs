//-----------------------------------------------------------------------
// <copyright file="OwnerInfo.cs" company="Rintech" author="Syurov">
//     Copyright (c) 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace rt.smc.model
{
  /// <summary>
  /// Информация о владельце
  /// </summary>
  [ComVisible(true)]
  public class OwnerInfo
  {
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string MiddleName { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public string Sex { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public string Birthday { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public string BirthPlace { get; set; }

    /// <summary>
    /// Гржданство
    /// </summary>
    public string CitizenschipCode { get; set; }

    /// <summary>
    /// Гржданство
    /// </summary>
    public string CitizenschipName { get; set; }

    /// <summary>
    /// Номер полиса
    /// </summary>
    public string PolisNumber { get; set; }

    /// <summary>
    /// Дата выдачи
    /// </summary>
    public string PolisDateFrom { get; set; }

    /// <summary>
    /// Дата окончания (если есть)
    /// </summary>
    public string PolisDateTo { get; set; }

    /// <summary>
    /// Состояние ЭЦП
    /// </summary>
    public string StateEcp { get; set; }

    /// <summary>
    /// СНИЛС
    /// </summary>
    public string Snils { get; set; }
  }
}