// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateProxyUtils.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The n hibernate proxy utils.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.nhibernate
{
  using System;

  using NHibernate;
  using NHibernate.Metadata;
  using NHibernate.Proxy;

  /// <summary>
  ///   The n hibernate proxy utils.
  /// </summary>
  public static class NHibernateProxyUtils
  {
    #region Public Methods and Operators

    /// <summary>
    /// Gets the underlying class type of a persistent object that may be proxied
    /// </summary>
    /// <typeparam name="T">
    /// Тип
    /// </typeparam>
    /// <param name="persistentObject">
    /// The persistent Object.
    /// </param>
    /// <returns>
    /// The <see cref="Type"/>.
    /// </returns>
    public static Type GetUnproxiedType<T>(this T persistentObject)
    {
      var proxy = persistentObject as INHibernateProxy;
      return proxy != null ? proxy.HibernateLazyInitializer.PersistentClass : persistentObject.GetType();
    }

    /// <summary>
    /// Force initialization of a proxy or persistent collection.
    /// </summary>
    /// <typeparam name="T">
    /// Тип
    /// </typeparam>
    /// <param name="persistentObject">
    /// a persistable object, proxy, persistent collection or null
    /// </param>
    /// <param name="sessionFactory">
    /// The session Factory.
    /// </param>
    /// <exception cref="HibernateException">
    /// if we can't initialize the proxy at this time, eg. the Session was closed
    /// </exception>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T Unproxy<T>(this T persistentObject, ISessionFactory sessionFactory)
    {
      var proxy = persistentObject as INHibernateProxy;

      if (proxy != null)
      {
        try
        {
          return (T)proxy.HibernateLazyInitializer.GetImplementation();
        }
        catch (LazyInitializationException)
        {
          var persistentType = persistentObject.GetUnproxiedType();

          var classMetadata = sessionFactory.GetClassMetadata(persistentType);

          return CreatePlaceholder(persistentObject, persistentType, classMetadata);
        }
      }

      return persistentObject;
    }

    /// <summary>
    /// Force initialzation of a possibly proxied object tree up to the maxDepth.
    ///   Once the maxDepth is reached, entity properties will be replaced with
    ///   placeholder objects having only the identifier property populated.
    /// </summary>
    /// <typeparam name="T">
    /// Тип
    /// </typeparam>
    /// <param name="persistentObject">
    /// The persistent Object.
    /// </param>
    /// <param name="sessionFactory">
    /// The session Factory.
    /// </param>
    /// <param name="maxDepth">
    /// The max Depth.
    /// </param>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T UnproxyObjectTree<T>(this T persistentObject, ISessionFactory sessionFactory, int maxDepth)
    {
      // Determine persistent type of the object
      var persistentType = persistentObject.GetUnproxiedType();

      var classMetadata = sessionFactory.GetClassMetadata(persistentType);

      // If we've already reached the max depth, we will return a placeholder object
      if (maxDepth < 0)
      {
        return CreatePlaceholder(persistentObject, persistentType, classMetadata);
      }

      // Now lets go ahead and make sure everything is unproxied
      var unproxiedObject = persistentObject.Unproxy(sessionFactory);

      // Iterate through each property and unproxy entity types
      for (var i = 0; i < classMetadata.PropertyTypes.Length; i++)
      {
        var propertyType = classMetadata.PropertyTypes[i];
        var propertyName = classMetadata.PropertyNames[i];
        var propertyInfo = persistentType.GetProperty(propertyName);

        // Unproxy of collections is supported. Iterate over the collection and unproxy in turn.
        object propertyValue;
        if (propertyType.IsCollectionType)
        {
          propertyValue = propertyInfo.GetValue(unproxiedObject, null);
          var collection = propertyValue as System.Collections.ICollection;
          if (collection != null)
          {
            foreach (var item in collection)
            {
              item.Unproxy(sessionFactory);
            }
          }

          continue;
        }

        if (!propertyType.IsEntityType)
        {
          continue;
        }

        propertyValue = propertyInfo.GetValue(unproxiedObject, null);

        if (propertyValue == null)
        {
          continue;
        }

        propertyInfo.SetValue(unproxiedObject, propertyValue.UnproxyObjectTree(sessionFactory, maxDepth - 1), null);
      }

      return unproxiedObject;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Return an empty placeholder object with the Identifier set.  We can safely access the identifier
    ///   property without the object being initialized.
    /// </summary>
    /// <typeparam name="T">
    /// Тип
    /// </typeparam>
    /// <param name="persistentObject">
    /// The persistent Object.
    /// </param>
    /// <param name="persistentType">
    /// The persistent Type.
    /// </param>
    /// <param name="classMetadata">
    /// The class Metadata.
    /// </param>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    private static T CreatePlaceholder<T>(T persistentObject, Type persistentType, IClassMetadata classMetadata)
    {
      var placeholderObject = (T)Activator.CreateInstance(persistentType);

      if (classMetadata.HasIdentifierProperty)
      {
        var identifier = classMetadata.GetIdentifier(persistentObject, EntityMode.Poco);
        classMetadata.SetIdentifier(placeholderObject, identifier, EntityMode.Poco);
      }

      return placeholderObject;
    }

    #endregion
  }
}