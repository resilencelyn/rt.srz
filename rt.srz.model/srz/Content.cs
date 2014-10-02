// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Content.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The Content.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System;
namespace rt.srz.model.srz
{
  using NHibernate;

  using StructureMap;

  /// <summary>
  ///   The Content.
  /// </summary>
  public partial class Content
  {
    private byte[] Base64ToByte(string str)
    {
      if (str == null)
      {
        return null;
      }
      return Convert.FromBase64String(str);
    }

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
  }
}