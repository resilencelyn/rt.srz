// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUecTerminal.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The UecTerminal interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.InteropServices;
using rt.uec.service.dto;

namespace rt.uec.service.activex.terminal
{
  #region references

  

  #endregion

  /// <summary>
  /// The UecTerminal interface.
  /// </summary>
  [Guid("4B67DFD6-1C40-46AB-AF82-E8A3BD58CF47")]
  [ComVisible(true)]
  public interface IUecTerminal
  {
    #region Public Methods and Operators

    /// <summary>
    /// Авторизация держателя карты
    /// </summary>
    /// <param name="pszPinIn">
    /// Пин код 1
    /// </param>
    /// <returns>
    /// Результат авторизации
    /// </returns>
    AuthorizeResult Authorize(string pszPinIn);

    /// <summary>
    ///   Закрытие карты
    /// </summary>
    /// <returns>Результат закрытия карты</returns>
    CloseResult CloseCard();

    /// <summary>
    ///   Считывание персональных данных держателя карты
    /// </summary>
    /// <returns>Персональные данные держателя карты</returns>
    GetCardInfoResult GetCardInfo();

    /// <summary>
    /// Считывает значение из конфигурационного файла - terminal.ini
    /// </summary>
    /// <param name="key">
    /// </param>
    /// <param name="value">
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    string GetCurrentReaderSettings();

    /// <summary>
    /// Чтение списка подключенных карт ридеров
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    string GetReaderList();

    /// <summary>
    /// Открытие карты
    /// </summary>
    /// <param name="uekServiceToken">
    /// The uek Service Token.
    /// </param>
    /// <returns>
    /// Результат открытия карты, или описание ошибки прикладного уровня
    /// </returns>
    OperationResult OpenCard(string uekServiceToken);

    /// <summary>
    /// Записывает значение в конфигурационный файл - terminal.ini
    /// </summary>
    /// <param name="value">
    /// </param>
    void SaveCurrentReaderSetting(string value);

    /// <summary>
    /// Запись текущей страховки на карту УЭК
    /// </summary>
    /// <param name="omsData">
    /// The oms Data.
    /// </param>
    /// <returns>
    /// The <see cref="OperationResult"/>.
    /// </returns>
    OperationResult WriteOmsData(string lastName, string firstName, string middleName, string birthDate, 
      string ogrn, string okato, string dateFrom, string dateTo);

		/// <summary>
		/// Имя компьютера
		/// </summary>
		/// <returns></returns>
		string GetWorkstationName();

    /// <summary>
    /// Чтение текущих данных о страховке
    /// </summary>
    /// <returns></returns>
    OMSDataResult ReadMainOmsData();

    #endregion
  }
}