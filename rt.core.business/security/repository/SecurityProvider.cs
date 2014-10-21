// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityProvider.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Репозиторий Пользователь
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.security.repository
{
  #region references

  using System;
  using System.Collections;
  using System.Linq;
  using System.Security.Authentication;
  using System.Security.Cryptography;
  using System.ServiceModel;
  using System.Text;
  using System.Web;

  using NHibernate;
  using NHibernate.Context;
  using NHibernate.Linq;

  using rt.core.business.security.interfaces;
  using rt.core.model.client;
  using rt.core.model.core;
  using rt.core.model.security;

  using StructureMap;

  #endregion

  /// <summary>
  ///   Репозиторий Пользователь
  /// </summary>
  public class SecurityProvider : ISecurityProvider
  {
    #region Static Fields

    /// <summary>
    /// The user.
    /// </summary>
    private static User user;

    #endregion

    #region Fields

    /// <summary>
    ///   Алгоритм
    /// </summary>
    private readonly RSACryptoServiceProvider alg;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="SecurityProvider" /> class.
    /// </summary>
    public SecurityProvider()
    {
      alg = new RSACryptoServiceProvider();
      alg.FromXmlString(
                        "<RSAKeyValue><Modulus>mjjXXni9qZbQBI8BxDuSVyF8xfWui7EE0az5yZ5OLJH+ZOCaqODhwxmACT6GihIq4Z0hrm7j8qH5wW9PK+aMEelFCQ1Sdxuh9Gwj2xraWTCTxj9RV4e1zXwR7t1ijBRCGN7HmRGijnFwOijiP0+RxVzXZE68JREhKrxT7JfTWEk=</Modulus><Exponent>AQAB</Exponent><P>zjmhDQz01Oj/90bG+gsCj9rsQQoN6/7GL7+bT3V6cwKoN72WRZl7/65tq1gV9Xvpvmamvt+T2UOYQC2Y5urxLQ==</P><Q>v3IDKWN9Xmd+dkqKS/QC6m0abjlfbDVRqNvoj6koxeGaY2BoRTGpdb4AiZoR03cvS82+RibMm7MQ+BZhkL8dDQ==</Q><DP>m8CIs0uaygbj84VgGE8iczWcA48tbpSwaDWlfkCy55QVKmwkx5IhRb0elS9k/k/E/QmYXEaN6qSTo70MYzMETQ==</DP><DQ>H+/wMRZk0rvnL+qteZBCcEM1NpAhqBaZAdd1y4mHwMMrE0sA+hIX2AmTY2Eteh6W6ElxZZiRZ6QOv6RUMGaBfQ==</DQ><InverseQ>V1AlCokYF8SU2tCODVQ+lTAcmjFTVX5IkldOfd84pDQQEJ7udoeixQLdW3l5mzMwhquhEU4Wlp3CwEDstNspwQ==</InverseQ><D>frVW1aqUAYMEM8qfI+/h4y6DSk35c5IkKHVa4Pjst5fXkGAtEbV6J4aK+I1jkossqiMkqiE3rYDBJ9lhDeukhkgL9prKJkgNrclT8mRtMxEimSgtFoyjoG3F/FAd0XJyXXlIHsrMrtfn8qjgSSP0uBZJD0ArVHcOQU0UoNUlt1E=</D></RSAKeyValue>");
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Добавляет пользователя
    /// </summary>
    /// <param name="user">
    /// </param>
    /// <returns>
    /// User
    /// </returns>
    public User AddUser(User user)
    {
      var hash = new PasswordHash(user.Password);
      user.Password = Convert.ToBase64String(hash.Hash);
      user.Salt = Convert.ToBase64String(hash.Salt);
      user.CreationDate = DateTime.Now;
      user.LastLoginDate = DateTime.Now;
      user.IsApproved = true;
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      session.SaveOrUpdate(user);
      session.Flush();
      return user;
    }

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
    public User CheckCridentials(string userName, string password)
    {
      // используем сессию, так как вызовы идут из ядра и до привязки контекста к сессии.
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var user = session.QueryOver<User>().Where(x => x.Login == userName && x.IsApproved).Take(1).SingleOrDefault();
      if (user == null)
      {
        throw new AuthenticationException("Incorrect credentials!");
      }

      var hash = new PasswordHash(Convert.FromBase64String(user.Salt), Convert.FromBase64String(user.Password));

      if (!hash.Verify(password))
      {
        throw new AuthenticationException("Incorrect credentials!");
      }

      ////if (CheckPasswordExpires(user))
      ////{
      ////  throw new AuthenticationException("Password is expired");
      ////}

      // todo придумать что делать вот с таким кодом (Организация пользователя была удалена.)
      ////if (user.PointDistributionPolicyId != null && (!user.PointDistributionPolicy.IsActive || !user.PointDistributionPolicy.Parent.IsActive))
      ////{
      ////  throw new AuthenticationException("Организация пользователя была удалена.");
      ////}
      user.LastLoginDate = DateTime.Now;
      session.Update(user);
      session.Flush();
      return user;
    }

    /// <summary>
    /// The get auth response.
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <returns>
    /// The <see cref="AuthResponse"/>.
    /// </returns>
    public AuthResponse GetAuthToken(User user)
    {
      var response = new AuthResponse { IsAuthenticated = true, FriendlyName = user.Login };
      var token = user.Login;
      var array = Encoding.GetEncoding(1251).GetBytes(token);
      response.AuthToken = new Token
                           {
                             Id = Guid.NewGuid(),
                             Signature = Convert.ToBase64String(alg.Encrypt(array, false)),
                             ExpTime = new DateTime(2200, 1, 1)
                           };

      return response;
    }

    /// <summary>
    ///   The get current user.
    /// </summary>
    /// <returns>
    ///   The <see cref="User" />.
    /// </returns>
    public User GetCurrentUser()
    {
      if (HttpContext.Current != null)
      {
        var context = ReflectiveHttpContext.HttpContextCurrentItems["User"] as IDictionary;
        if (context != null)
        {
          var userInSession = context["Current"] as User;
          if (userInSession != null)
          {
            return userInSession;
          }
        }
        else
        {
          var userByName = GetUserByName(HttpContext.Current.User.Identity.Name);
          context = new Hashtable { { "Current", userByName } };
          ReflectiveHttpContext.HttpContextCurrentItems["User"] = context;
          return userByName;
        }

        // if (userInSession != null && userInSession.Login == HttpContext.Current.User.Identity.Name)
        // {
        // return userInSession;
        // }
        return GetUserByName(HttpContext.Current.User.Identity.Name);
      }

      if (ServiceSecurityContext.Current != null)
      {
        return GetUserByName(ServiceSecurityContext.Current.PrimaryIdentity.Name);
      }

      //// GetUserByName(Thread.CurrentPrincipal.Identity.Name)
      if (user == null)
      {
        var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
        var session = sessionFactory.GetCurrentSession();
        user =
          session.QueryOver<User>()
                 .Where(x => x.Id == new Guid("01000000-0000-0000-0000-000000000000"))
                 .List()
                 .FirstOrDefault();
      }

      return user;
    }

    /// <summary>
    /// The get date from token.
    /// </summary>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="User"/>.
    /// </returns>
    public User GetDateFromToken(Token token)
    {
      if (token != null)
      {
        var data = Convert.FromBase64String(token.Signature);
        var decoded = alg.Decrypt(data, false);
        var login = Encoding.GetEncoding(1251).GetString(decoded);
        var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        var user = session.QueryOver<User>().Where(x => x.Login == login).Take(1).SingleOrDefault();
        return user;
      }

      return null;
    }

    /// <summary>
    /// Возвращает пользователя по имени
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// User
    /// </returns>
    public User GetUserByName(string name)
    {
      using (var session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession())
      {
        return session.QueryOver<User>().Where(x => x.Login == name && x.IsApproved).List().FirstOrDefault();
      }
    }

    /// <summary>
    /// Возвращает имя пользователя по email
    /// </summary>
    /// <param name="email">
    /// The email.
    /// </param>
    /// <returns>
    /// UserName
    /// </returns>
    public string GetUserNameByEmail(string email)
    {
      return (from user in ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Query<User>()
              where user.Email == email && user.IsApproved
              select user.Login).FirstOrDefault();
    }

    /// <summary>
    /// Возвращает список ролей для пользователя
    /// </summary>
    /// <param name="userName">
    /// Имя пользователя
    /// </param>
    /// <returns>
    /// Список ролей
    /// </returns>
    public string[] GetUserRoles(string userName)
    {
      ////// используем StateLess сессию, так как вызовы идут из ядра и до привязки контекста к сессии.
      ////using (var session = SessionFactory.OpenStatelessSession())
      ////{
      ////  var user = session.QueryOver<User>().Where(x => x.Name == userName).SingleOrDefault();
      ////  if (user != null)
      ////  {
      ////    var userRoles = QueryOver.Of<UserRole>().Where(x => x.UserId == user.UserId).Select(x => x.RoleId);
      ////    var userGroups = QueryOver.Of<UserGroup>().Where(x => x.UserId == user.UserId).Select(x => x.GroupId);
      ////    var userGroupRoles = QueryOver.Of<GroupRole>().Where(Subqueries.WhereProperty<GroupRole>(x => x.GroupId).In(userGroups)).Select(x => x.RoleId);
      ////    return session.QueryOver<Role>().Where(Restrictions.Or(
      ////      Subqueries.WhereProperty<Role>(x => x.RoleId).In(userRoles),
      ////      Subqueries.WhereProperty<Role>(x => x.RoleId).In(userGroupRoles)))
      ////      .Select(Projections.Group<Role>(x => x.Name))
      ////      .List<object>()
      ////      .Select(x => x.ToString()).ToArray();
      ////  }
      ////}
      return null;
    }

    /// <summary>
    /// Сохраняет или добавляет пользователя
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <returns>
    /// The <see cref="User"/>.
    /// </returns>
    public User SaveUser(User user)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      session.SaveOrUpdate(user);
      session.Flush();
      return user;
    }

    /// <summary>
    /// Обновляет пароль пользователя
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="newPassword">
    /// The new Password.
    /// </param>
    /// <returns>
    /// User
    /// </returns>
    public User UpdatePassword(string name, string newPassword)
    {
      var hash = new PasswordHash(newPassword);
      var password = Convert.ToBase64String(hash.Hash);
      var salt = Convert.ToBase64String(hash.Salt);

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var user = GetUserByName(name);
      user.Password = password;
      user.Salt = salt;
      session.Update(user);
      session.Flush();

      return GetUserByName(name);
    }

    #endregion

    /////// <summary>
    /////// Проверка количества фейлов
    /////// </summary>
    /////// <param name="user">
    /////// Пользователь
    /////// </param>
    ////private static void CheckFailCounter(User user)
    ////{
    ////  ////if (ConfigManager.SecuritySection.AutoBlockFailCount > 0 &&
    ////  ////    user.FailLoginCounter >= ConfigManager.SecuritySection.AutoBlockFailCount)
    ////  ////{
    ////  ////  user.IsLocked = true;
    ////  ////  user.LockReason = User.AutoBlockByFailCounterOverflow;
    ////  ////  user.LockedDate = DateTime.Now;
    ////  ////}
    ////}
    #region Methods

    /// <summary>
    /// Проверка пароля на срок годности
    /// </summary>
    /// <param name="user">
    /// Пользователь
    /// </param>
    /// <returns>
    /// Тру - гуд
    /// </returns>
    private static bool CheckPasswordExpires(User user)
    {
      ////if (ConfigManager.SecuritySection.PasswordExpireDays > 0 &&
      ////    user.PasswordDate.GetValueOrDefault().AddDays(ConfigManager.SecuritySection.PasswordExpireDays) < DateTime.Now)
      ////{
      ////  user.IsLocked = true;
      ////  user.LockReason = User.PasswordExpires;
      ////  user.LockedDate = DateTime.Now;
      ////  return true;
      ////}
      return false;
    }

    /// <summary>
    /// Проверка пароля
    /// </summary>
    /// <param name="password">
    /// Пароль
    /// </param>
    private static void ValidatePassword(string password)
    {
      ////if (password.Length < ConfigManager.SecuritySection.PasswordMinimalLen)
      ////{
      ////  throw new SecurityException("Password is too short");
      ////}
    }

    #endregion
  }
}