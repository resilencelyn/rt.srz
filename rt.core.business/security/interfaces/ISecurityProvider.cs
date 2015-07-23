// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISecurityProvider.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Интерфейс, необходимый для аутентификации и авторизации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.security.interfaces
{
  #region references

  using rt.core.model.client;
  using rt.core.model.core;
  using rt.core.model.security;

  #endregion

  /// <summary>
  ///   Интерфейс, необходимый для аутентификации и авторизации
  /// </summary>
  public interface ISecurityProvider
  {
    #region Public Methods and Operators

    /// <summary>
    /// Сохраняет пользователя
    /// </summary>
    /// <param name="user">
    /// </param>
    /// <returns>
    /// User
    /// </returns>
    User AddUser(User user);

    /// <summary>
    /// Проверка пользователя и возврат информации о нём
    /// </summary>
    /// <param name="userName">
    /// Имя пользователя
    /// </param>
    /// <param name="password">
    /// Пароль
    /// </param>
    /// <returns>
    /// Информация о пользователе
    /// </returns>
    User CheckCridentials(string userName, string password);

    /// <summary>
    /// The get auth response.
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <returns>
    /// The <see cref="AuthResponse"/>.
    /// </returns>
    AuthResponse GetAuthToken(User user);

    /// <summary>
    ///   The get current user.
    /// </summary>
    /// <returns>
    ///   The <see cref="User" />.
    /// </returns>
    User GetCurrentUser();

    /// <summary>
    /// The get date from token.
    /// </summary>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="User"/>.
    /// </returns>
    User GetDateFromToken(Token token);

    /// <summary>
    /// Возвращает пользователя по имени
    /// </summary>
    /// <param name="name">
    /// </param>
    /// <returns>
    /// User
    /// </returns>
    User GetUserByName(string name);

    /// <summary>
    /// Возвращает имя пользователя по email
    /// </summary>
    /// <param name="email">
    /// </param>
    /// <returns>
    /// UserName
    /// </returns>
    string GetUserNameByEmail(string email);

    /// <summary>
    /// Возвращает список ролей для пользователя
    /// </summary>
    /// <param name="userName">
    /// Имя пользователя
    /// </param>
    /// <returns>
    /// Список ролей
    /// </returns>
    string[] GetUserRoles(string userName);

    /// <summary>
    /// Добавляет или сохраняет пользователя
    /// </summary>
    /// <param name="user">
    /// </param>
    /// <returns>
    /// The <see cref="User"/>.
    /// </returns>
    User SaveUser(User user);

    /// <summary>
    /// Обновляет пароль пользователя
    /// </summary>
    /// <param name="name">
    /// </param>
    /// <param name="newPassword">
    /// </param>
    /// <returns>
    /// User
    /// </returns>
    User UpdatePassword(string name, string newPassword);

    #endregion
  }
}