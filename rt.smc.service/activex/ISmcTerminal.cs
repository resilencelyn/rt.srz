// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISmcTerminal.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Runtime.InteropServices;
using rt.smc.model;

#endregion

namespace rt.smc.service.activex
{
  /// <summary>
  ///   The SmcTerminal interface.
  /// </summary>
  [ComVisible(true)]
  [Guid("8890C815-2A99-4B64-BA3A-1C98C13CFCB5")]
  public interface ISmcTerminal
  {
    /// <summary>
    /// The set card reader.
    /// </summary>
    [ComVisible(true)]
    void SetCardReader();

    /// <summary>
    ///   Получение информации о карте (чип, номер карты, производитель и т.п.)
    /// </summary>
    /// <returns> The <see cref="CardInfoStrings" /> . </returns>
    [ComVisible(true)]
    CardInfoStrings GetCardInfo();

    /// <summary>
    ///   Получение информации о текущей СМО
    /// </summary>
    /// <returns> The <see cref="SmoInfoStrings" /> . </returns>
    [ComVisible(true)]
    SmoInfoStrings GetCurrentSmo();

    /// <summary>
    ///   Чтение инфомрации о владельце карты (застрахованном)
    /// </summary>
    /// <returns> The <see cref="OwnerInfo" /> . </returns>
    [ComVisible(true)]
    OwnerInfo GetOwnerInfo();

    /// <summary>
    /// Смена СМО
    /// </summary>
    /// <param name="ogrnSmo">
    /// ОГРН СМО 
    /// </param>
    /// <param name="okatoSmo">
    /// ОКАТО СМО 
    /// </param>
    /// <returns>
    /// true - если всё выполнилось 
    /// </returns>
    [ComVisible(true)]
    bool ChangeSmo(string ogrnSmo, string okatoSmo, string dateFrom, string dateTo, string securityModulePin, string cardPin);

    /// <summary>
    /// Открывает соединение к УЭК сервису
    /// </summary>
    /// <param name="token">
    ///   Авторизационный токен
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool OpenConnection(string token);

    /// <summary>
    /// Закрывает соединение
    /// </summary>
    void CloseConnection();
  }
}