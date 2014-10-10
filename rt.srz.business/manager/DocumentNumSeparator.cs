// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentNumSeparator.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The document num separator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System.Linq;

  using NHibernate;

  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  ///   The document num separator.
  /// </summary>
  public class DocumentNumSeparator
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get full string.
    /// </summary>
    /// <param name="dt">
    /// The dt.
    /// </param>
    /// <param name="seria">
    /// The seria.
    /// </param>
    /// <param name="num">
    /// The num.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetFullString(Concept dt, string seria, string num)
    {
      var splited = dt.Description.Split(' ').Select(s => s.Substring(s.IndexOf('=') + 1).Trim('"')).ToArray();
      for (var i = 0; i < splited[1].Count(); i++)
      {
        if (splited[1][i] < num[i])
        {
          return null;
        }
      }

      if (seria != string.Empty)
      {
        for (var i = 0; i < splited[0].Count(); i++)
        {
          if (splited[0][i] < seria[i])
          {
            return null;
          }
        }

        return dt.Description.Replace(splited[0], seria).Replace(splited[1], num);
      }

      return dt.Description.Split(' ').ToArray()[1].Replace(splited[1], num);
    }

    /// <summary>
    /// The get full string.
    /// </summary>
    /// <param name="dt">
    /// The dt.
    /// </param>
    /// <param name="seria">
    /// The seria.
    /// </param>
    /// <param name="num">
    /// The num.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GetFullString(string dt, string seria, string num)
    {
      var c = GetDocType(dt);
      if (c == null)
      {
        return null;
      }

      return GetFullString(c, seria, num);
    }

    /// <summary>
    /// The separate spec format.
    /// </summary>
    /// <param name="sf">
    /// The sf.
    /// </param>
    /// <returns>
    /// The <see cref="string[]"/>.
    /// </returns>
    public static string[] SeparateSpecFormat(string sf)
    {
      var r = sf.Split('№').Select(m => m.Trim()).ToList();
      if (r.Count < 2)
      {
        r.Insert(0, null);
      }

      return r.ToArray();
    }

    /// <summary>
    /// The spec format.
    /// </summary>
    /// <param name="seria">
    /// The seria.
    /// </param>
    /// <param name="num">
    /// The num.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string SpecFormat(string seria, string num)
    {
      return seria == null ? string.Format("{0}", num) : string.Format("{0} № {1}", seria, num);
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get doc type.
    /// </summary>
    /// <param name="typeName">
    /// The type name.
    /// </param>
    /// <returns>
    /// The <see cref="Concept"/>.
    /// </returns>
    private static Concept GetDocType(string typeName)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var c = session.QueryOver<Concept>().Where(f => f.Name == typeName || f.ShortName == typeName).List();
      return c.Count > 0 ? c.First() : null;
    }

    #endregion
  }
}