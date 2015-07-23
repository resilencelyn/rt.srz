// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiasService.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.fias.services
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using NHibernate;
  using NHibernate.Criterion;

  using rt.core.business.nhibernate;
  using rt.core.model.interfaces;
  using rt.fias.model.fias;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The kladr service.
  /// </summary>
  public class FiasService : IAddressService
  {
    #region Public Methods and Operators

    /// <summary>
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="objectId">
    /// The object Id.
    /// </param>
    /// <returns>
    /// The <see cref="IAddress"/>.
    /// </returns>
    public Address GetAddress(Guid objectId)
    {
      var session = GetSession();

      var entety = session.QueryOver<AObject>().Where(x => x.Id == objectId).Take(1).List().FirstOrDefault()
                   ?? session.QueryOver<AObject>()
                             .Where(x => x.Aoguid == objectId && x.ACTSTATUS.Id == 1)
                             .Take(1)
                             .List()
                             .FirstOrDefault();

      CloseSession(session);

      return entety != null ? entety.GetAddress() : null;
    }

    /// <summary>
    /// Возвращает список адресных объектов для указанного уровня
    /// </summary>
    /// <param name="parentId">
    /// The parent Id.
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="level">
    /// The level.
    /// </param>
    /// <returns>
    /// The <see cref="List{Address}"/> .
    /// </returns>
    public List<Address> GetAddressList(Guid? parentId, string prefix, KladrLevel? level)
    {
      var session = GetSession();

      var queryOver = QueryOver.Of<AObject>().Where(x => x.Id == parentId).Select(x => x.Aoguid);

      var query = session.QueryOver<AObject>().Where(x => x.ACTSTATUS.Id == 1);
      if (parentId != null)
      {
        query.WithSubquery.WhereProperty(x => x.Parentguid).In(queryOver);
        if (level.HasValue)
        {
          query.Where(x => x.Aolevel == level.GetHashCode());
        }
      }
      else
      {
        query.Where(x => x.Aolevel == 1);
      }

      // поиск с префиксом
      if (!string.IsNullOrEmpty(prefix))
      {
        query.WhereRestrictionOn(x => x.Offname).IsLike(prefix);
      }

      var list = query.OrderBy(x => x.Offname).Asc.List();
      var addressList = list.Select(x => x.GetAddress()).ToList();

      // Ключает загрузку домов
      ////if (!addressList.Any() && parentId.HasValue)
      ////{
      ////  var queryHouse = session.QueryOver<House>()
      ////    .Where(x => x.Enddate >= DateTime.Today)
      ////    .WithSubquery.WhereProperty(x => x.Aoguid).In(queryOver);

      ////  // поиск с префиксом
      ////  if (!string.IsNullOrEmpty(prefix))
      ////  {
      ////    queryHouse.WhereRestrictionOn(x => x.Housenum).IsLike(prefix);
      ////  }

      ////  addressList = queryHouse.OrderBy(x => x.Housenum).Asc.List().Select(x => x.GetAddress()).ToList();
      ////}

      CloseSession(session);

      return addressList;
    }

    /// <summary>
    /// The get first level by tfoms.
    /// </summary>
    /// <param name="okato">
    /// The okato.
    /// </param>
    /// <returns>
    /// The <see cref="IAddress"/>.
    /// </returns>
    public Address GetFirstLevelByTfoms(string okato)
    {
      var session = GetSession();
      var o = session.QueryOver<AObject>().Where(x => x.Okato == okato && x.Aolevel == 1).Take(1).List().FirstOrDefault();
      CloseSession(session);
      return o != null ? o.GetAddress() : null;
    }

    /// <summary>
    /// The get structure address.
    /// </summary>
    /// <param name="objectId">
    /// The object id.
    /// </param>
    /// <returns>
    /// The <see cref="StructureAddress"/>.
    /// </returns>
    public StructureAddress GetStructureAddress(Guid objectId)
    {
      var session = GetSession();
      var structureAddress = new StructureAddress();
      var obj = session.QueryOver<AObject>().Where(x => x.Id == objectId).Take(1).List().First();
      structureAddress.Code = obj.Code;
      do
      {
        var strTemp = obj.Name + " " + obj.Socr;
        switch (obj.Level)
        {
          case (int)FiasLevel.Subject:
          case (int)FiasLevel.AutonomousOkrug:
            {
              structureAddress.Subject = strTemp;
              if (structureAddress.OkatoRn == null)
              {
                structureAddress.OkatoRn = obj.Okato;
              }
            }

            break;
          case (int)FiasLevel.Area:
            {
              structureAddress.Area = strTemp;
              structureAddress.OkatoRn = obj.Okato;
            }

            break;
          case (int)FiasLevel.City:
          case (int)FiasLevel.InCity:
            {
              structureAddress.City = strTemp;
              if (structureAddress.OkatoRn == null)
              {
                structureAddress.OkatoRn = obj.Okato;
              }
            }

            break;
          case (int)FiasLevel.Town:
            {
              structureAddress.Town = strTemp;
            }

            break;
          case (int)FiasLevel.Street:
            {
              structureAddress.Street = strTemp;
            }

            break;
        }

        obj = obj.ParentId.HasValue ? session.QueryOver<AObject>().Where(x => x.Aoguid == obj.Parentguid.Value && x.ACTSTATUS.Id == 1).Take(1).List().FirstOrDefault() : null;
      }
      while (obj != null);

      CloseSession(session);
      return structureAddress;
    }

    /// <summary>
    /// The get unstructure address.
    /// </summary>
    /// <param name="objectId">
    /// The object id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string GetUnstructureAddress(Guid objectId)
    {
      var session = GetSession();
      var valueBuilder = new StringBuilder();
      var obj = session.QueryOver<AObject>().Where(x => x.Id == objectId).Take(1).List().First();
      while (obj != null)
      {
        valueBuilder.Insert(0, string.Format("," + obj.Name + " " + obj.Socr + "."));
        obj = session.QueryOver<AObject>().Where(x => x.Aoguid == obj.Parentguid && x.ACTSTATUS.Id == 1).Take(1).List().FirstOrDefault();
      }

      if (valueBuilder.Length > 0)
      {
        valueBuilder.Remove(0, 1);
      }

      valueBuilder.Append(",");
      CloseSession(session);

      return valueBuilder.ToString();
    }

    /// <summary>
    /// The hierarchy build.
    /// </summary>
    /// <param name="objectId">
    /// The object id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string HierarchyBuild(Guid objectId)
    {
      var session = GetSession();
      var hierarchyBuilder = new StringBuilder();
      var obj = session.QueryOver<AObject>().Where(x => x.Id == objectId).Take(1).List().First();
      while (obj != null)
      {
        hierarchyBuilder.Insert(0, string.Format(";" + obj.Id));
        obj = session.QueryOver<AObject>().Where(x => x.Aoguid == obj.Parentguid && x.ACTSTATUS.Id == 1).Take(1).List().FirstOrDefault();
      }

      if (hierarchyBuilder.Length > 0)
      {
        hierarchyBuilder.Remove(0, 1);
      }

      CloseSession(session);

      return hierarchyBuilder.ToString();
    }

    #endregion

    #region Methods

    /// <summary>
    /// The close s ession.
    /// </summary>
    /// <param name="session">
    /// The session.
    /// </param>
    private void CloseSession(ISession session)
    {
      var sessionFactory = ObjectFactory.GetInstance<IManagerSessionFactorys>()
                                        .GetFactoryByName("NHibernateCfgFias.xml");
      if (sessionFactory != null)
      {
        session.Close();
        session.Dispose();
      }
    }

    /// <summary>
    ///   The get session.
    /// </summary>
    /// <returns>
    ///   The <see cref="ISession" />.
    /// </returns>
    private ISession GetSession()
    {
      var sessionFactory = ObjectFactory.GetInstance<IManagerSessionFactorys>()
                                        .GetFactoryByName("NHibernateCfgFias.xml");
      ISession session = null;
      if (sessionFactory != null)
      {
        session = sessionFactory.OpenSession();
      }

      if (session == null)
      {
        session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      }

      return session;
    }

    #endregion
  }
}