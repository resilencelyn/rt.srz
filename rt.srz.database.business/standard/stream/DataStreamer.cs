// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStreamer.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The data streamer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.srz.database.business.standard.stream
{
  using System;
  using System.IO;

  // --------------------------------------------------------

  /// <summary>
  /// The data streamer.
  /// </summary>
  public static class DataStreamer
  {
    /// <summary>
    /// The mask_ date time_ local.
    /// </summary>
    private const long MaskDateTimeLocal = unchecked(0x4000000000000000);

    /// <summary>
    /// The mask_ date time_ unspecified.
    /// </summary>
    private const long MaskDateTimeUnspecified = unchecked((long)0x8000000000000000);

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, string data)
    {
      var hasValue = data != null;
      Write(writer, hasValue);
      if (hasValue)
      {
        writer.Write(data);
      }
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write<T>(BinaryWriter writer, T data)
      where T : struct, IConvertible
    {
      var typeCode = data.GetTypeCode();
      switch (typeCode)
      {
        case TypeCode.Byte:
        case TypeCode.SByte:
          writer.Write(data.ToByte(provider: null));
          break;
        case TypeCode.Int16:
        case TypeCode.UInt16:
          writer.Write(data.ToInt16(provider: null));
          break;
        case TypeCode.Int32:
        case TypeCode.UInt32:
          writer.Write(data.ToInt32(provider: null));
          break;
        case TypeCode.Int64:
        case TypeCode.UInt64:
          writer.Write(data.ToInt64(provider: null));
          break;
        case TypeCode.Boolean:
          writer.Write(data.ToBoolean(provider: null));
          break;
        case TypeCode.DateTime:
          {
            var d = data.ToDateTime(provider: null);
            var ticks = d.Ticks;
            switch (d.Kind)
            {
              case DateTimeKind.Local:
                ticks |= MaskDateTimeLocal;
                break;
              case DateTimeKind.Unspecified:
                ticks |= MaskDateTimeUnspecified;
                break;
            }

            writer.Write(ticks);
          }

          break;
        case TypeCode.Double:
          writer.Write(data.ToDouble(provider: null));
          break;
        case TypeCode.Single:
          writer.Write(data.ToSingle(provider: null));
          break;
      }
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write<T>(BinaryWriter writer, T? data) where T : struct, IConvertible
    {
      var hasValue = data.HasValue;
      Write(writer, hasValue);
      if (hasValue)
      {
        Write(writer, data.Value);
      }
    }
  }
}