// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Content.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The Content.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System;

  using NHibernate;

  using StructureMap;

  /// <summary>
  ///   The Content.
  /// </summary>
  public partial class Content
  {
    #region Public Properties

    /// <summary>
    /// Gets the content interior.
    /// </summary>
    public virtual byte[] ContentInterior
    {
      get
      {
        if (DocumentContent == null && !string.IsNullOrEmpty(DocumentContent64))
        {
          DocumentContent = Base64ToByte(DocumentContent64);
          DocumentContent64 = null;
          ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Save(this);
        }

        return DocumentContent;
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The base 64 to byte.
    /// </summary>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    private byte[] Base64ToByte(string str)
    {
      if (str == null)
      {
        return null;
      }

      return Convert.FromBase64String(str);
    }

    #endregion
  }
}