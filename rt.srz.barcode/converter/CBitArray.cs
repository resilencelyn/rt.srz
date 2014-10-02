// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CBitArray.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The c bit array.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Runtime.InteropServices;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The c bit array.
  /// </summary>
  public class CBitArray
  {
    #region Fields

    /// <summary>
    ///   The inner.
    /// </summary>
    private CByte inner;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CBitArray" /> class.
    /// </summary>
    public CBitArray()
    {
      inner = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CBitArray"/> class.
    /// </summary>
    /// <param name="data">
    /// The data. 
    /// </param>
    public CBitArray(CByte data)
    {
      inner = 0;
      inner = data;
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public bool Get(int index)
    {
      var num = (index - (index%8))/8;
      index -= num*8;
      if (num >= inner.Length)
      {
        throw new ArgumentException("The index is out of the array bounds.");
      }

      return (inner[num] & (0x80 >> index)) > 0;
    }

    /// <summary>
    /// The set.
    /// </summary>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    public void Set(int index, [Optional] [DefaultParameterValue(true)] bool value)
    {
      var num = (index - (index%8))/8;
      index -= num*8;
      if (num >= inner.Length)
      {
        inner += (num + 1) - inner.Length;
      }

      inner[num] = value ? ((byte) (inner[num] | (0x80 >> index))) : ((byte) (inner[num] & ((0x80 >> index) ^ 0xff)));
    }

    /// <summary>
    ///   The to array.
    /// </summary>
    /// <returns> The <see cref="CByte" /> . </returns>
    public CByte ToArray()
    {
      return inner;
    }

    /// <summary>
    ///   The to string.
    /// </summary>
    /// <returns> The <see cref="string" /> . </returns>
    public override string ToString()
    {
      return inner.ToString();
    }

    #endregion
  }
}