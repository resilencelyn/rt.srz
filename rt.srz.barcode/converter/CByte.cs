// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CByte.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The c byte.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The c byte.
  /// </summary>
  public class CByte
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CByte"/> class.
    /// </summary>
    /// <param name="inner">
    /// The inner. 
    /// </param>
    public CByte(params byte[] inner)
    {
      Inner = inner ?? new byte[0];
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the count.
    /// </summary>
    public int Count
    {
      get { return Inner.Length; }
    }

    /// <summary>
    ///   Gets the length.
    /// </summary>
    public int Length
    {
      get { return Inner.Length; }
    }

    #endregion

    #region Properties

    /// <summary>
    ///   The inner.
    /// </summary>
    protected byte[] Inner { get; set; }

    #endregion

    #region Public Indexers

    /// <summary>
    /// The this.
    /// </summary>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <returns>
    /// The <see cref="CByte"/> . 
    /// </returns>
    public CByte this[int index, int length]
    {
      get { return Inner.GetRange(index, length); }

      set { Inner.SetRange(index, value); }
    }

    /// <summary>
    /// The this.
    /// </summary>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public byte this[int index]
    {
      get { return Inner[index]; }

      set { Inner[index] = value; }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The +.
    /// </summary>
    /// <param name="a"> The a. </param>
    /// <param name="b"> The b. </param>
    /// <returns> result operation </returns>
    public static CByte operator +(byte[] a, CByte b)
    {
      return new CByte(a.AddRange(b));
    }

    /// <summary>
    ///   The +.
    /// </summary>
    /// <param name="a"> The a. </param>
    /// <param name="b"> The b. </param>
    /// <returns> result operation </returns>
    public static CByte operator +(CByte a, CByte b)
    {
      a.AddRange(b);
      return a;
    }

    /// <summary>
    ///   The +.
    /// </summary>
    /// <param name="a"> The a. </param>
    /// <param name="b"> The b. </param>
    /// <returns> result operation </returns>
    public static CByte operator +(CByte a, byte[] b)
    {
      a.AddRange(b);
      return a;
    }

    /// <summary>
    ///   The |.
    /// </summary>
    /// <param name="a"> The a. </param>
    /// <param name="b"> The b. </param>
    /// <returns> result operation </returns>
    public static CByte operator |(byte[] a, CByte b)
    {
      return new CByte(a.Xor(b));
    }

    /// <summary>
    ///   The |.
    /// </summary>
    /// <param name="a"> The a. </param>
    /// <param name="b"> The b. </param>
    /// <returns> result operation </returns>
    public static CByte operator |(CByte a, byte[] b)
    {
      return new CByte(a.Inner.Xor(b));
    }

    /// <summary>
    ///   The |.
    /// </summary>
    /// <param name="a"> The a. </param>
    /// <param name="b"> The b. </param>
    /// <returns> result operation </returns>
    public static CByte operator |(CByte a, CByte b)
    {
      return new CByte(a.Inner.Xor(b));
    }

    /// <summary>
    ///   The ==.
    /// </summary>
    /// <param name="a"> The a. </param>
    /// <param name="b"> The b. </param>
    /// <returns> result operation </returns>
    public static bool operator ==(CByte a, object b)
    {
      if ((a != null) || (b == null))
      {
        if ((a != null) && (b == null))
        {
          return false;
        }

        if ((object) a == null)
        {
          return true;
        }

        var cbyte = b as CByte;
        if (cbyte != null)
        {
          return a.ToHexString() == cbyte.ToHexString();
        }

        var bytes = b as byte[];
        if (bytes != null)
        {
          return a.ToHexString() == bytes.ToHexString();
        }
      }

      return false;
    }

    /// <summary>
    ///   The op_ implicit.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <returns> </returns>
    public static implicit operator List<byte>(CByte value)
    {
      return value.Inner.ToList();
    }

    /// <summary>
    ///   The op_ implicit.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <returns> </returns>
    public static implicit operator string(CByte value)
    {
      return value.ToHexString();
    }

    /// <summary>
    ///   The op_ implicit.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <returns> </returns>
    public static implicit operator CByte(byte[] value)
    {
      return new CByte(value);
    }

    /// <summary>
    ///   The op_ implicit.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <returns> </returns>
    public static implicit operator byte[](CByte value)
    {
      return value.Inner;
    }

    /// <summary>
    ///   The op_ implicit.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <returns> </returns>
    public static implicit operator CByte(int value)
    {
      return new CByte(new byte[value]);
    }

    /// <summary>
    ///   The op_ implicit.
    /// </summary>
    /// <param name="value"> The value. </param>
    /// <returns> </returns>
    public static implicit operator CByte(string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return null;
      }

      if (value.ToUpper().StartsWith("S:"))
      {
        return new CByte(value.Substring(2).GetBytes());
      }

      if (value.ToUpper().StartsWith("BASE64:"))
      {
        return new CByte(value.Substring(7).FromBase64String());
      }

      return value.IsValidHexString() ? new CByte(value.FromHexString()) : new CByte(value.GetBytes());
    }

    /// <summary>
    ///   The !=.
    /// </summary>
    /// <param name="a"> The a. </param>
    /// <param name="b"> The b. </param>
    /// <returns> result operation </returns>
    public static bool operator !=(CByte a, object b)
    {
      if (((object) a != null) || (b == null))
      {
        if (((object) a != null) && (b == null))
        {
          return true;
        }

        if ((object) a == null)
        {
          return false;
        }

        var cbyte = b as CByte;
        if ((object) cbyte != null)
        {
          return a.ToHexString() != cbyte.ToHexString();
        }

        var bytes = b as byte[];
        if (bytes != null)
        {
          return a.ToHexString() != bytes.ToHexString();
        }
      }

      return true;
    }

    /// <summary>
    /// The add range.
    /// </summary>
    /// <param name="data">
    /// The data. 
    /// </param>
    public void AddRange(byte[] data)
    {
      Inner = Inner.AddRange(data);
    }

    /// <summary>
    ///   The clear.
    /// </summary>
    public void Clear()
    {
      Inner = new byte[0];
    }

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="obj">
    /// The obj. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public override bool Equals(object obj)
    {
      var bytes = obj as byte[];
      if (bytes != null)
      {
        return GetHashCode() == bytes.GetHashCode();
      }

      return (obj is CByte) && (GetHashCode() == obj.GetHashCode());
    }

    /// <summary>
    /// The fill.
    /// </summary>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    public void Fill(int index, int length, byte value)
    {
      Inner.SetValue(index, length, value);
    }

    /// <summary>
    ///   The get hash code.
    /// </summary>
    /// <returns> The <see cref="int" /> . </returns>
    public override int GetHashCode()
    {
      return Inner.GetHashCode();
    }

    /// <summary>
    /// The get range.
    /// </summary>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <returns>
    /// The <see cref="CByte"/> . 
    /// </returns>
    public CByte GetRange(int index, int length)
    {
      return new CByte(Inner.GetRange(index, length));
    }

    /// <summary>
    ///   The reverse.
    /// </summary>
    /// <returns> The <see cref="CByte" /> . </returns>
    public CByte Reverse()
    {
      return Inner.Reverse().ToArray();
    }

    /// <summary>
    ///   The to array.
    /// </summary>
    /// <returns> The <see cref="byte" /> . </returns>
    public byte[] ToArray()
    {
      return Inner;
    }

    /// <summary>
    ///   The to hex string.
    /// </summary>
    /// <returns> The <see cref="string" /> . </returns>
    public string ToHexString()
    {
      return Inner.ToHexString();
    }

    /// <summary>
    ///   The to list.
    /// </summary>
    /// <returns> The List. </returns>
    public List<byte> ToList()
    {
      return Inner.ToList();
    }

    /// <summary>
    ///   The to string.
    /// </summary>
    /// <returns> The <see cref="string" /> . </returns>
    public override string ToString()
    {
      return this;
    }

    #endregion
  }
}