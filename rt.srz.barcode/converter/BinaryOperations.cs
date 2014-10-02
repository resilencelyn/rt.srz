// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryOperations.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The binary operations.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The binary operations.
  /// </summary>
  public static class BinaryOperations
  {
    #region Public Methods and Operators

    /// <summary>
    /// The from array.
    /// </summary>
    /// <param name="data">
    /// The data. 
    /// </param>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <typeparam name="T">
    /// T 
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/> . 
    /// </returns>
    public static T FromArray<T>(byte[] data, int index, int length)
    {
      if ((((((typeof (T) != typeof (byte)) && (typeof (T) != typeof (short)))
             && ((typeof (T) != typeof (int)) && (typeof (T) != typeof (long))))
            && (((typeof (T) != typeof (ushort)) && (typeof (T) != typeof (uint)))
                && ((typeof (T) != typeof (ulong)) && (typeof (T) != typeof (byte?)))))
           && ((((typeof (T) != typeof (short?)) && (typeof (T) != typeof (int?)))
                && ((typeof (T) != typeof (long?)) && (typeof (T) != typeof (ushort?)))) &&
               (typeof (T) != typeof (uint?))))
          && (typeof (T) != typeof (ulong?)))
      {
        throw new ArgumentException(string.Format("Type '{0}' is not valid.", typeof (T)));
      }

      ulong number = 0L;
      if (length > 8)
      {
        throw new ArgumentException(string.Format("Length '{0}' is not valid.", length));
      }

      if ((index + length) > data.Length)
      {
        throw new ArgumentException(string.Format("Offset+Length '{0}' is out of array boundaries.", length + index));
      }

      var from = 0;
      for (var i = (index + length) - 1; i >= index; i--)
      {
        number = number.SetBitRange(from, from + 8, data[i]);
        from += 8;
      }

      var underlyingType = Nullable.GetUnderlyingType(typeof (T));
      if (underlyingType != null)
      {
        var constructor = typeof (T).GetConstructor(new[] {underlyingType});
        if (constructor != null)
        {
          return (T) constructor.Invoke(new[] {Convert.ChangeType(number, underlyingType)});
        }
      }

      return (T) Convert.ChangeType(number, typeof (T));
    }

    /// <summary>
    /// The from array.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="data">
    /// The data. 
    /// </param>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <returns>
    /// The <see cref="short"/> . 
    /// </returns>
    public static short FromArray(this short instance, byte[] data, int index, int length)
    {
      return FromArray<short>(data, index, length);
    }

    /// <summary>
    /// The from array.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="data">
    /// The data. 
    /// </param>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public static int FromArray(this int instance, byte[] data, int index, int length)
    {
      return FromArray<int>(data, index, length);
    }

    /// <summary>
    /// The from array.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="data">
    /// The data. 
    /// </param>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <returns>
    /// The <see cref="long"/> . 
    /// </returns>
    public static long FromArray(this long instance, byte[] data, int index, int length)
    {
      return FromArray<long>(data, index, length);
    }

    /// <summary>
    /// The from array.
    /// </summary>
    /// <param name="info">
    /// The info. 
    /// </param>
    /// <param name="data">
    /// The data. 
    /// </param>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <returns>
    /// The <see cref="object"/> . 
    /// </returns>
    public static object FromArray(this PropertyInfo info, byte[] data, int index, int length)
    {
      if ((((((info.PropertyType != typeof (byte)) && (info.PropertyType != typeof (short)))
             && ((info.PropertyType != typeof (int)) && (info.PropertyType != typeof (long))))
            && (((info.PropertyType != typeof (ushort)) && (info.PropertyType != typeof (uint)))
                && ((info.PropertyType != typeof (ulong)) && (info.PropertyType != typeof (byte?)))))
           && ((((info.PropertyType != typeof (short?)) && (info.PropertyType != typeof (int?)))
                && ((info.PropertyType != typeof (long?)) && (info.PropertyType != typeof (ushort?))))
               && (info.PropertyType != typeof (uint?)))) && (info.PropertyType != typeof (ulong?)))
      {
        throw new ArgumentException(string.Format("Type '{0}' is not valid.", info.PropertyType));
      }

      ulong number = 0L;
      if (length > 8)
      {
        throw new ArgumentException(string.Format("Length '{0}' is not valid.", length));
      }

      if ((index + length) > data.Length)
      {
        throw new ArgumentException(string.Format("Offset+Length '{0}' is out of array boundaries.", length + index));
      }

      var from = 0;
      for (var i = (index + length) - 1; i >= index; i--)
      {
        number = number.SetBitRange(from, from + 8, data[i]);
        from += 8;
      }

      var underlyingType = Nullable.GetUnderlyingType(info.PropertyType);
      if (underlyingType != null)
      {
        var constructor = info.PropertyType.GetConstructor(new[] {underlyingType});
        if (constructor != null)
        {
          return constructor.Invoke(new[] {Convert.ChangeType(number, underlyingType)});
        }
      }

      return Convert.ChangeType(number, info.PropertyType);
    }

    /// <summary>
    /// The from array.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="data">
    /// The data. 
    /// </param>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <returns>
    /// The <see cref="ushort"/> . 
    /// </returns>
    public static ushort FromArray(this ushort instance, byte[] data, int index, int length)
    {
      return FromArray<ushort>(data, index, length);
    }

    /// <summary>
    /// The from array.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="data">
    /// The data. 
    /// </param>
    /// <param name="index">
    /// The index. 
    /// </param>
    /// <param name="length">
    /// The length. 
    /// </param>
    /// <returns>
    /// The <see cref="uint"/> . 
    /// </returns>
    public static uint FromArray(this uint instance, byte[] data, int index, int length)
    {
      return FromArray<uint>(data, index, length);
    }

    /// <summary>
    /// The get bit length.
    /// </summary>
    /// <param name="val">
    /// The val. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public static int GetBitLength(this short val)
    {
      var num = 0;
      for (var i = 15; i >= 0; i--)
      {
        if (((val >> i) & 1) > 0)
        {
          num = i;
          break;
        }
      }

      return (num > 0) ? (num + (8 - (num%8))) : 8;
    }

    /// <summary>
    /// The get bit length.
    /// </summary>
    /// <param name="val">
    /// The val. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public static int GetBitLength(this int val)
    {
      var num = 0;
      for (var i = 0x1f; i >= 0; i--)
      {
        if (((val >> i) & 1) > 0)
        {
          num = i;
          break;
        }
      }

      return (num > 0) ? (num + (8 - (num%8))) : 8;
    }

    /// <summary>
    /// The get bit length.
    /// </summary>
    /// <param name="val">
    /// The val. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public static int GetBitLength(this long val)
    {
      var num = 0;
      for (var i = 0x3f; i >= 0; i--)
      {
        if (((val >> i) & 1L) <= 0L)
        {
          continue;
        }

        num = i;
        break;
      }

      return (num > 0) ? (num + (8 - (num%8))) : 8;
    }

    /// <summary>
    /// The get bit length.
    /// </summary>
    /// <param name="val">
    /// The val. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public static int GetBitLength(this ushort val)
    {
      var num = 0;
      for (var i = 15; i >= 0; i--)
      {
        if (((val >> i) & 1) > 0)
        {
          num = i;
          break;
        }
      }

      return (num > 0) ? (num + (8 - (num%8))) : 8;
    }

    /// <summary>
    /// The get bit length.
    /// </summary>
    /// <param name="val">
    /// The val. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public static int GetBitLength(this uint val)
    {
      var num = 0;
      for (var i = 0x1f; i >= 0; i--)
      {
        if (((val >> i) & 1) > 0)
        {
          num = i;
          break;
        }
      }

      return (num > 0) ? (num + (8 - (num%8))) : 8;
    }

    /// <summary>
    /// The get bit length.
    /// </summary>
    /// <param name="val">
    /// The val. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public static int GetBitLength(this ulong val)
    {
      var num = 0;
      for (var i = 0x3f; i >= 0; i--)
      {
        if (((val >> i) & 1L) > 0L)
        {
          num = i;
          break;
        }
      }

      return (num > 0) ? (num + (8 - (num%8))) : 8;
    }

    /// <summary>
    /// The get bit range.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="from">
    /// The from. 
    /// </param>
    /// <param name="to">
    /// The to. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public static int GetBitRange(this byte number, int from, int to)
    {
      return (int) ((long) number).GetBitRange(from, to);
    }

    /// <summary>
    /// The get bit range.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="from">
    /// The from. 
    /// </param>
    /// <param name="to">
    /// The to. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public static int GetBitRange(this short number, int from, int to)
    {
      return (int) ((long) number).GetBitRange(from, to);
    }

    /// <summary>
    /// The get bit range.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="from">
    /// The from. 
    /// </param>
    /// <param name="to">
    /// The to. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public static int GetBitRange(this int number, int from, int to)
    {
      return (int) ((long) number).GetBitRange(from, to);
    }

    /// <summary>
    /// The get bit range.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="from">
    /// The from. 
    /// </param>
    /// <param name="to">
    /// The to. 
    /// </param>
    /// <returns>
    /// The <see cref="long"/> . 
    /// </returns>
    public static long GetBitRange(this long number, int from, int to)
    {
      var num = 0L;
      for (var i = (from >= to) ? from : to; i > ((from >= to) ? (to - 1) : (from - 1)); i--)
      {
        num |= 1L << i;
      }

      return (from >= to) ? ((number >> to) & (num >> to)) : ((number >> from) & (num >> from));
    }

    /// <summary>
    /// The get bit range.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="from">
    /// The from. 
    /// </param>
    /// <param name="to">
    /// The to. 
    /// </param>
    /// <returns>
    /// The <see cref="ulong"/> . 
    /// </returns>
    public static ulong GetBitRange(this ulong number, int from, int to)
    {
      ulong num = 0L;
      for (var i = (from >= to) ? from : to; i > ((from >= to) ? (to - 1) : (from - 1)); i--)
      {
        num |= ((ulong) 1L) << i;
      }

      return (from >= to) ? ((number >> to) & (num >> to)) : ((number >> from) & (num >> from));
    }

    /// <summary>
    /// The get bit state.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public static bool GetBitState(this byte number, int offset)
    {
      if ((offset < 0) || (offset > 7))
      {
        throw new ArgumentException("Error offset. Should be in range 0-7", "offset");
      }

      if (offset > 0)
      {
        number = (byte) (number >> offset);
      }

      return (number & 1) == 1;
    }

    /// <summary>
    /// The get bit state.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public static bool GetBitState(this short number, int offset)
    {
      if ((offset < 0) || (offset > 15))
      {
        throw new ArgumentException("Error offset. Should be in range 0-15", "offset");
      }

      if (offset > 0)
      {
        number = (short) (number >> offset);
      }

      return (number & 1) == 1;
    }

    /// <summary>
    /// The get bit state.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public static bool GetBitState(this int number, int offset)
    {
      if ((offset < 0) || (offset > 0x1f))
      {
        throw new ArgumentException("Error offset. Should be in range 0-31", "offset");
      }

      if (offset > 0)
      {
        number = number >> offset;
      }

      return (number & 1) == 1;
    }

    /// <summary>
    /// The get bit state.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public static bool GetBitState(this long number, int offset)
    {
      if ((offset < 0) || (offset > 0x3f))
      {
        throw new ArgumentException("Error offset. Should be in range 0-63", "offset");
      }

      if (offset > 0)
      {
        number = number >> offset;
      }

      return (number & 1L) == 1L;
    }

    /// <summary>
    /// The get bit state.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public static bool GetBitState(this ushort number, int offset)
    {
      if ((offset < 0) || (offset > 15))
      {
        throw new ArgumentException("Error offset. Should be in range 0-15", "offset");
      }

      if (offset > 0)
      {
        number = (ushort) (number >> offset);
      }

      return (number & 1) == 1;
    }

    /// <summary>
    /// The get bit state.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public static bool GetBitState(this uint number, int offset)
    {
      if ((offset < 0) || (offset > 0x1f))
      {
        throw new ArgumentException("Error offset. Should be in range 0-31", "offset");
      }

      if (offset > 0)
      {
        number = number >> offset;
      }

      return (number & 1) == 1;
    }

    /// <summary>
    /// The get bit state.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public static bool GetBitState(this ulong number, int offset)
    {
      if ((offset < 0) || (offset > 0x3f))
      {
        throw new ArgumentException("Error offset. Should be in range 0-63", "offset");
      }

      if (offset > 0)
      {
        number = number >> offset;
      }

      return (number & 1L) == 1L;
    }

    /// <summary>
    /// The set bit range.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="from">
    /// The from. 
    /// </param>
    /// <param name="to">
    /// The to. 
    /// </param>
    /// <param name="newValue">
    /// The new value. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public static byte SetBitRange(this byte number, int from, int to, int newValue)
    {
      return (byte) ((long) number).SetBitRange(from, to, newValue);
    }

    /// <summary>
    /// The set bit range.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="from">
    /// The from. 
    /// </param>
    /// <param name="to">
    /// The to. 
    /// </param>
    /// <param name="newValue">
    /// The new value. 
    /// </param>
    /// <returns>
    /// The <see cref="short"/> . 
    /// </returns>
    public static short SetBitRange(this short number, int from, int to, int newValue)
    {
      return (short) ((long) number).SetBitRange(from, to, newValue);
    }

    /// <summary>
    /// The set bit range.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="from">
    /// The from. 
    /// </param>
    /// <param name="to">
    /// The to. 
    /// </param>
    /// <param name="newValue">
    /// The new value. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public static int SetBitRange(this int number, int from, int to, int newValue)
    {
      return (int) ((long) number).SetBitRange(from, to, newValue);
    }

    /// <summary>
    /// The set bit range.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="from">
    /// The from. 
    /// </param>
    /// <param name="to">
    /// The to. 
    /// </param>
    /// <param name="newValue">
    /// The new value. 
    /// </param>
    /// <returns>
    /// The <see cref="long"/> . 
    /// </returns>
    public static long SetBitRange(this long number, int from, int to, long newValue)
    {
      int num2;
      var num = 0L;
      for (num2 = (from >= to) ? from : to; num2 > ((from >= to) ? (to - 1) : (from - 1)); num2--)
      {
        num = (num << 1) | 1L;
      }

      newValue = (from >= to) ? ((newValue & num) << to) : ((newValue & num) << from);
      num = 0L;
      for (num2 = (from >= to) ? from : to; num2 > ((from >= to) ? (to - 1) : (from - 1)); num2--)
      {
        num |= 1L << num2;
      }

      return (number & (num ^ 0x1fffffffffffffL)) | newValue;
    }

    /// <summary>
    /// The set bit range.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="from">
    /// The from. 
    /// </param>
    /// <param name="to">
    /// The to. 
    /// </param>
    /// <param name="newValue">
    /// The new value. 
    /// </param>
    /// <returns>
    /// The <see cref="uint"/> . 
    /// </returns>
    public static uint SetBitRange(this uint number, int from, int to, int newValue)
    {
      return (uint) ((long) number).SetBitRange(from, to, newValue);
    }

    /// <summary>
    /// The set bit range.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="from">
    /// The from. 
    /// </param>
    /// <param name="to">
    /// The to. 
    /// </param>
    /// <param name="newValue">
    /// The new value. 
    /// </param>
    /// <returns>
    /// The <see cref="ulong"/> . 
    /// </returns>
    public static ulong SetBitRange(this ulong number, int from, int to, ulong newValue)
    {
      int num2;
      ulong num = 0L;
      for (num2 = (from >= to) ? from : to; num2 > ((from >= to) ? (to - 1) : (from - 1)); num2--)
      {
        num = (num << 1) | 1L;
      }

      newValue = (from >= to) ? ((newValue & num) << to) : ((newValue & num) << from);
      num = 0L;
      for (num2 = (from >= to) ? from : to; num2 > ((from >= to) ? (to - 1) : (from - 1)); num2--)
      {
        num |= ((ulong) 1L) << num2;
      }

      return (number & (num ^ 0x1fffffffffffffL)) | newValue;
    }

    /// <summary>
    /// The set bit state.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <param name="isSet">
    /// The is set. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public static byte SetBitState(this byte number, int offset, bool isSet)
    {
      if ((offset < 0) || (offset > 0x1f))
      {
        throw new ArgumentException("Error offset. Should be in range 0-31", "offset");
      }

      return (byte) ((long) number).SetBitState(offset, isSet);
    }

    /// <summary>
    /// The set bit state.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <param name="isSet">
    /// The is set. 
    /// </param>
    /// <returns>
    /// The <see cref="short"/> . 
    /// </returns>
    public static short SetBitState(this short number, int offset, bool isSet)
    {
      if ((offset < 0) || (offset > 0x1f))
      {
        throw new ArgumentException("Error offset. Should be in range 0-31", "offset");
      }

      return (short) ((long) number).SetBitState(offset, isSet);
    }

    /// <summary>
    /// The set bit state.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <param name="isSet">
    /// The is set. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public static int SetBitState(this int number, int offset, bool isSet)
    {
      if ((offset < 0) || (offset > 0x1f))
      {
        throw new ArgumentException("Error offset. Should be in range 0-31", "offset");
      }

      return (int) ((long) number).SetBitState(offset, isSet);
    }

    /// <summary>
    /// The set bit state.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <param name="isSet">
    /// The is set. 
    /// </param>
    /// <returns>
    /// The <see cref="long"/> . 
    /// </returns>
    public static long SetBitState(this long number, int offset, bool isSet)
    {
      if ((offset < 0) || (offset > 0x3f))
      {
        throw new ArgumentException("Error offset. Should be in range 0-63", "offset");
      }

      long num = 1 << offset;
      return isSet ? (number | num) : (number &= num ^ 0x1fffffffffffffffL);
    }

    /// <summary>
    /// The set bit state.
    /// </summary>
    /// <param name="number">
    /// The number. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <param name="isSet">
    /// The is set. 
    /// </param>
    /// <returns>
    /// The <see cref="uint"/> . 
    /// </returns>
    public static uint SetBitState(this uint number, int offset, bool isSet)
    {
      if ((offset < 0) || (offset > 0x1f))
      {
        throw new ArgumentException("Error offset. Should be in range 0-31", "offset");
      }

      return (uint) ((long) number).SetBitState(offset, isSet);
    }

    /// <summary>
    /// The to hex string.
    /// </summary>
    /// <param name="data">
    /// The data. 
    /// </param>
    /// <param name="offset">
    /// The offset. 
    /// </param>
    /// <param name="len">
    /// The len. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    public static string ToHexString(
      this byte[] data, 
      [Optional] [DefaultParameterValue(0)] int offset, 
      [Optional] [DefaultParameterValue(-1)] int len)
    {
      if (data == null)
      {
        return string.Empty;
      }

      if (len == -1)
      {
        len = data.Length;
      }

      if (len > data.GetLength(0))
      {
        len = data.GetLength(0);
      }

      return BitConverter.ToString(data, offset, len).Replace("-", string.Empty);
    }

    /// <summary>
    /// The to string.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    public static string ToString(this byte[] instance)
    {
      return instance.ToString(Encoding.Default);
    }

    /// <summary>
    /// The to string.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="codepage">
    /// The codepage. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    public static string ToString(this byte[] instance, int codepage)
    {
      return Encoding.GetEncoding(codepage).GetString(instance);
    }

    /// <summary>
    /// The to string.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="encoding">
    /// The encoding. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    public static string ToString(this byte[] instance, Encoding encoding)
    {
      return encoding.GetString(instance);
    }

    /// <summary>
    /// The xor.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="array2">
    /// The array 2. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public static byte[] Xor(this byte[] instance, byte[] array2)
    {
      if (instance == null)
      {
        throw new ArgumentNullException("instance");
      }

      if (instance == null)
      {
        throw new ArgumentNullException("array2");
      }

      var buffer = instance.GetRange(0, instance.Length);
      if (instance.Length != array2.Length)
      {
        throw new ArgumentOutOfRangeException();
      }

      for (var i = 0; i < buffer.Length; i++)
      {
        buffer[i] = (byte) (buffer[i] ^ array2[i]);
      }

      return buffer;
    }

    #endregion
  }
}