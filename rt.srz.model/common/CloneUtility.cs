﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CloneUtility.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The clone utility.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.common
{
  using System.IO;
  using System.Runtime.Serialization.Formatters.Binary;

  /// <summary>
  /// The clone utility.
  /// </summary>
  public class CloneUtility
  {
    #region Public Methods and Operators

    /// <summary>
    /// The deep clone.
    /// </summary>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T DeepClone<T>(T obj)
    {
      using (var ms = new MemoryStream())
      {
        var formatter = new BinaryFormatter();
        formatter.Serialize(ms, obj);
        ms.Position = 0;
        return (T)formatter.Deserialize(ms);
      }
    }

    #endregion
  }
}