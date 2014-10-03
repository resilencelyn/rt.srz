// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchKeyTypeManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region

  using System;
  using System.Collections.Generic;

  using NHibernate;

  using rt.core.business.security.interfaces;
  using rt.srz.business.server;
  using rt.srz.model.srz;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The SearchKeyTypeManager.
  /// </summary>
  public partial class SearchKeyTypeManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Удаление ключа поиска (set пометка IsActive=false)
    /// </summary>
    /// <param name="keyTypeId">
    /// The key Type Id.
    /// </param>
    public void DeleteSearchKeyType(Guid keyTypeId)
    {
      var keyType = GetById(keyTypeId);
      if (keyType == null)
      {
        return;
      }

      // Ключ не активный
      keyType.IsActive = false;

      // сохранение в БД
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      session.SaveOrUpdate(keyType);
      session.Flush();
    }

    /// <summary>
    /// Возвращает описатели всех ключей поиска для ТФОМС текущего пользователя
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<SearchKeyType> GetSearchKeyTypesByTFoms()
    {
      var tfomsId = Guid.NewGuid();
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      if (currentUser != null && currentUser.HasTf())
      {
        tfomsId = currentUser.GetTf().Id;
      }

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var query =
        session.QueryOver<SearchKeyType>().Where(x => x.Tfoms.Id == null || x.Tfoms.Id == tfomsId).And(x => x.IsActive)
               .OrderBy(x => x.Name).Asc;
      return query.List();
    }

    /// <summary>
    /// Сохраняет ключ поиска в БД
    /// </summary>
    /// <param name="keyType">
    /// The key Type.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/>.
    /// </returns>
    public Guid SaveSearchKeyType(SearchKeyType keyType)
    {
      // Назначаем ТФОМС текущего пользователя
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      if (currentUser != null && currentUser.HasTf())
      {
        keyType.Tfoms = currentUser.GetTf();
      }

      if (!EqualsBd(keyType))
      {
        keyType.Recalculated = false;
        CalculateKeysPool.Instance.AddJobForUserKey(keyType);
      }

      // Ключ активный
      keyType.IsActive = true;

      // Сохранение в БД
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      session.SaveOrUpdate(keyType);
      session.Flush();
      return keyType.Id;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The equals bd.
    /// </summary>
    /// <param name="searchKeyType">
    /// The search key type.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private bool EqualsBd(SearchKeyType searchKeyType)
    {
      using (var session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession())
      {
        var keyType = session.QueryOver<SearchKeyType>().Where(x => x.Id == searchKeyType.Id).SingleOrDefault();
        if (keyType != null)
        {
          if (keyType.FirstName != searchKeyType.FirstName)
          {
            return false;
          }

          if (keyType.FirstNameLength != searchKeyType.FirstNameLength)
          {
            return false;
          }

          if (keyType.LastName != searchKeyType.LastName)
          {
            return false;
          }

          if (keyType.LastNameLength != searchKeyType.LastNameLength)
          {
            return false;
          }

          if (keyType.MiddleName != searchKeyType.MiddleName)
          {
            return false;
          }

          if (keyType.MiddleNameLength != searchKeyType.MiddleNameLength)
          {
            return false;
          }

          if (keyType.IdenticalLetters != searchKeyType.IdenticalLetters)
          {
            return false;
          }

          if (keyType.Okato != searchKeyType.Okato)
          {
            return false;
          }

          if (keyType.PolisNumber != searchKeyType.PolisNumber)
          {
            return false;
          }

          if (keyType.PolisSeria != searchKeyType.PolisSeria)
          {
            return false;
          }

          if (keyType.PolisType != searchKeyType.PolisType)
          {
            return false;
          }

          if (keyType.Snils != searchKeyType.Snils)
          {
            return false;
          }

          if (keyType.AddressHouse != searchKeyType.AddressHouse)
          {
            return false;
          }

          if (keyType.AddressHouse2 != searchKeyType.AddressHouse2)
          {
            return false;
          }

          if (keyType.AddressRoom != searchKeyType.AddressRoom)
          {
            return false;
          }

          if (keyType.AddressRoom2 != searchKeyType.AddressRoom2)
          {
            return false;
          }

          if (keyType.AddressStreet != searchKeyType.AddressStreet)
          {
            return false;
          }

          if (keyType.AddressStreet2 != searchKeyType.AddressStreet2)
          {
            return false;
          }

          if (keyType.AddressStreetLength != searchKeyType.AddressStreetLength)
          {
            return false;
          }

          if (keyType.AddressStreetLength2 != searchKeyType.AddressStreetLength2)
          {
            return false;
          }

          if (keyType.Birthday != searchKeyType.Birthday)
          {
            return false;
          }

          if (keyType.BirthdayLength != searchKeyType.BirthdayLength)
          {
            return false;
          }

          if (keyType.Birthplace != searchKeyType.Birthplace)
          {
            return false;
          }

          if (keyType.DeleteTwinChar != searchKeyType.DeleteTwinChar)
          {
            return false;
          }

          if (keyType.DocumentNumber != searchKeyType.DocumentNumber)
          {
            return false;
          }

          if (keyType.DocumentSeries != searchKeyType.DocumentSeries)
          {
            return false;
          }

          if (keyType.DocumentType != searchKeyType.DocumentType)
          {
            return false;
          }
        }
      }

      return true;
    }

    #endregion
  }
}