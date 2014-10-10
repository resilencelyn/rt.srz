// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataStreamer.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The data streamer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.commons
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text;

  #endregion

  /// <summary>
  ///   The data streamer.
  /// </summary>
  public static class DataStreamer
  {
    #region Static Fields

    /// <summary>
    ///   The mask_ date time_ local.
    /// </summary>
    private static readonly long mask_DateTime_Local = 0x4000000000000000L;

    /// <summary>
    ///   The mask_ date time_ unspecified.
    /// </summary>
    private static readonly long mask_DateTime_Unspecified = -9223372036854775808L;

    #endregion

    #region Delegates

    /// <summary>
    /// The read array handler.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="size">
    /// The size.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    private delegate void ReadArrayHandler<T>(BinaryReader reader, out T[] data, int size);

    /// <summary>
    /// The read item handler.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    private delegate void ReadItemHandler<T>(BinaryReader reader, out T item);

    /// <summary>
    /// The write item handler.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    private delegate void WriteItemHandler<T>(BinaryWriter writer, T item);

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check data version.
    /// </summary>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <param name="validVersion">
    /// The valid version.
    /// </param>
    /// <param name="actualVersion">
    /// The actual version.
    /// </param>
    [CLSCompliant(false)]
    public static void CheckDataVersion(object obj, ulong validVersion, ulong actualVersion)
    {
      if (actualVersion > validVersion)
      {
        ThrowIncompatibleDataVersion(obj, actualVersion);
      }
    }

    // [CLSCompliant(false)]
    // public static void Read<T>(BinaryReader reader, out HashSet<T?> data) where T: struct, IConvertible
    // {
    // Read_collection<T?, HashSet<T?>>(reader, out data, new ReadItemHandler<T?>(DataStreamer.Read<T>));
    // }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out HashSet<string> data)
    {
      Read_collection(reader, out data, new ReadItemHandler<string>(Read));
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Read<T>(BinaryReader reader, out HashSet<T> data) where T : struct, IConvertible
    {
      Read_collection(reader, out data, new ReadItemHandler<T>(Read));
    }

    // [CLSCompliant(false)]
    // public static void Read<T>(BinaryReader reader, out List<T?> data) where T: struct, IConvertible
    // {
    // Read_collection<T?, List<T?>>(reader, out data, new ReadItemHandler<T?>(DataStreamer.Read<T>));
    // }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out List<string> data)
    {
      Read_collection(reader, out data, new ReadItemHandler<string>(Read));
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Read<T>(BinaryReader reader, out List<T> data) where T : struct, IConvertible
    {
      Read_collection(reader, out data, new ReadItemHandler<T>(Read));
    }

    // [CLSCompliant(false)]
    // public static void Read<T>(BinaryReader reader, out T? data) where T: struct, IConvertible
    // {
    // bool flag;
    // Read(reader, out flag);
    // if (flag)
    // {
    // T local;
    // Read<T>(reader, out local);
    // data = new T?(local);
    // }
    // else
    // {
    // data = 0;
    // }
    // }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out bool data)
    {
      data = reader.ReadBoolean();
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out byte data)
    {
      data = reader.ReadByte();
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out DateTime data)
    {
      var ticks = reader.ReadInt64();
      var utc = DateTimeKind.Utc;
      if ((ticks & mask_DateTime_Local) == mask_DateTime_Local)
      {
        ticks &= ~mask_DateTime_Local;
        utc = DateTimeKind.Local;
      }

      if ((ticks & mask_DateTime_Unspecified) == mask_DateTime_Unspecified)
      {
        ticks &= ~mask_DateTime_Unspecified;
        utc = DateTimeKind.Unspecified;
      }

      data = new DateTime(ticks, utc);
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out double data)
    {
      data = reader.ReadDouble();
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out Guid data)
    {
      byte[] buffer;
      var fixedSize = 0x10;
      Read(reader, out buffer, fixedSize);
      data = new Guid(buffer);
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out short data)
    {
      data = reader.ReadInt16();
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out int data)
    {
      data = reader.ReadInt32();
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out long data)
    {
      data = reader.ReadInt64();
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    [CLSCompliant(false)]
    public static void Read(BinaryReader reader, out sbyte data)
    {
      data = reader.ReadSByte();
    }

    // [CLSCompliant(false)]
    // public static void Read<T>(BinaryReader reader, out T?[] data) where T: struct, IConvertible
    // {
    // Read_array_with_header<T?>(reader, out data, new ReadArrayHandler<T?>(DataStreamer.Read<T>));
    // }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out byte[] data)
    {
      Read_array_with_header(reader, out data, Read);
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    [CLSCompliant(false)]
    public static void Read(BinaryReader reader, out ushort data)
    {
      data = reader.ReadUInt16();
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out char[] data)
    {
      Read_array_with_header(reader, out data, Read);
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out float data)
    {
      data = reader.ReadSingle();
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Read<T>(BinaryReader reader, out T data) where T : struct, IConvertible
    {
      var typeCode = Type.GetTypeCode(typeof(T));
      switch (typeCode)
      {
        case TypeCode.Boolean:
          bool flag;
          Read(reader, out flag);
          data = ConversionHelper.ConvertToValue<T>(flag);
          return;

        case TypeCode.SByte:
        case TypeCode.Byte:
          byte num;
          Read(reader, out num);
          data = ConversionHelper.ConvertToValue<T>(num);
          return;

        case TypeCode.Int16:
        case TypeCode.UInt16:
          short num2;
          Read(reader, out num2);
          data = ConversionHelper.ConvertToValue<T>(num2);
          return;

        case TypeCode.Int32:
        case TypeCode.UInt32:
          int num3;
          Read(reader, out num3);
          data = ConversionHelper.ConvertToValue<T>(num3);
          return;

        case TypeCode.Int64:
        case TypeCode.UInt64:
          long num4;
          Read(reader, out num4);
          data = ConversionHelper.ConvertToValue<T>(num4);
          return;

        case TypeCode.Single:
          float num6;
          Read(reader, out num6);
          data = ConversionHelper.ConvertToValue<T>(num6);
          return;

        case TypeCode.Double:
          double num5;
          Read(reader, out num5);
          data = ConversionHelper.ConvertToValue<T>(num5);
          return;

        case TypeCode.DateTime:
          DateTime time;
          Read(reader, out time);
          data = ConversionHelper.ConvertToValue<T>(time);
          return;
      }

      ThrowUnsupportedTypeCode(typeCode);
      data = default(T);
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out string data)
    {
      bool flag;
      Read(reader, out flag);
      data = flag ? reader.ReadString() : null;
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    [CLSCompliant(false)]
    public static void Read(BinaryReader reader, out uint data)
    {
      data = reader.ReadUInt32();
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Read<T>(BinaryReader reader, out T[] data) where T : struct, IConvertible
    {
      Read_array_with_header(reader, out data, Read);
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Read(BinaryReader reader, out string[] data)
    {
      Read_array_with_header(reader, out data, Read);
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    [CLSCompliant(false)]
    public static void Read(BinaryReader reader, out ulong data)
    {
      data = reader.ReadUInt64();
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="fixedSize">
    /// The fixed size.
    /// </param>
    public static void Read(BinaryReader reader, out char[] data, int fixedSize)
    {
      data = reader.ReadChars(fixedSize);
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="fixedSize">
    /// The fixed size.
    /// </param>
    public static void Read(BinaryReader reader, out string[] data, int fixedSize)
    {
      data = new string[fixedSize];
      for (var i = 0; i < fixedSize; i++)
      {
        Read(reader, out data[i]);
      }
    }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="fixedSize">
    /// The fixed size.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Read<T>(BinaryReader reader, out T[] data, int fixedSize) where T : struct, IConvertible
    {
      data = new T[fixedSize];
      for (var i = 0; i < fixedSize; i++)
      {
        Read(reader, out data[i]);
      }
    }

    // [CLSCompliant(false)]
    // public static void Read<T>(BinaryReader reader, out T?[] data, int fixedSize) where T: struct, IConvertible
    // {
    // data = new T?[fixedSize];
    // for (int i = 0; i < fixedSize; i++)
    // {
    // Read<T>(reader, out data[i]);
    // }
    // }

    /// <summary>
    /// The read.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="fixedSize">
    /// The fixed size.
    /// </param>
    public static void Read(BinaryReader reader, out byte[] data, int fixedSize)
    {
      data = reader.ReadBytes(fixedSize);
    }

    /// <summary>
    /// The throw incompatible data version.
    /// </summary>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <param name="ver">
    /// The ver.
    /// </param>
    /// <exception cref="InvalidDataException">
    /// </exception>
    public static void ThrowIncompatibleDataVersion(object obj, object ver)
    {
      var builder = new StringBuilder("Загрузка объекта ");
      if (obj != null)
      {
        builder.Append((obj is string) ? obj.ToString() : obj.GetType().Name);
        builder.Append(' ');
      }

      builder.Append("невозможна по причине несовместимой версии данных");
      if (ver != null)
      {
        builder.AppendFormat(": {0}", ver);
      }

      throw new InvalidDataException(builder.ToString());
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, bool data)
    {
      writer.Write(data);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Write<T>(BinaryWriter writer, HashSet<T> data) where T : struct, IConvertible
    {
      Write_collection(writer, data, Write);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Write<T>(BinaryWriter writer, HashSet<T?> data) where T : struct, IConvertible
    {
      Write_collection(writer, data, Write);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, HashSet<string> data)
    {
      Write_collection(writer, data, Write);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Write<T>(BinaryWriter writer, List<T?> data) where T : struct, IConvertible
    {
      Write_collection(writer, data, Write);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, List<string> data)
    {
      Write_collection(writer, data, Write);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, byte data)
    {
      writer.Write(data);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Write<T>(BinaryWriter writer, List<T> data) where T : struct, IConvertible
    {
      Write_collection(writer, data, Write);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, string[] data)
    {
      var fixedSize = false;
      Write(writer, data, fixedSize);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, DateTime data)
    {
      var ticks = data.Ticks;
      switch (data.Kind)
      {
        case DateTimeKind.Unspecified:
          ticks |= mask_DateTime_Unspecified;
          break;

        case DateTimeKind.Local:
          ticks |= mask_DateTime_Local;
          break;
      }

      writer.Write(ticks);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Write<T>(BinaryWriter writer, T?[] data) where T : struct, IConvertible
    {
      var fixedSize = false;
      Write(writer, data, fixedSize);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Write<T>(BinaryWriter writer, T[] data) where T : struct, IConvertible
    {
      var fixedSize = false;
      Write(writer, data, fixedSize);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, byte[] data)
    {
      var fixedSize = false;
      Write(writer, data, fixedSize);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, char[] data)
    {
      var fixedSize = false;
      Write(writer, data, fixedSize);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Write<T>(BinaryWriter writer, T data) where T : struct, IConvertible
    {
      var typeCode = data.GetTypeCode();
      switch (typeCode)
      {
        case TypeCode.Boolean:
        {
          IFormatProvider provider5 = null;
          Write(writer, data.ToBoolean(provider5));
          return;
        }

        case TypeCode.SByte:
        case TypeCode.Byte:
        {
          IFormatProvider provider = null;
          Write(writer, data.ToByte(provider));
          return;
        }

        case TypeCode.Int16:
        case TypeCode.UInt16:
        {
          IFormatProvider provider2 = null;
          Write(writer, data.ToInt16(provider2));
          return;
        }

        case TypeCode.Int32:
        case TypeCode.UInt32:
        {
          IFormatProvider provider3 = null;
          Write(writer, data.ToInt32(provider3));
          return;
        }

        case TypeCode.Int64:
        case TypeCode.UInt64:
        {
          IFormatProvider provider4 = null;
          Write(writer, data.ToInt64(provider4));
          return;
        }

        case TypeCode.Single:
        {
          IFormatProvider provider8 = null;
          Write(writer, data.ToSingle(provider8));
          return;
        }

        case TypeCode.Double:
        {
          IFormatProvider provider7 = null;
          Write(writer, data.ToDouble(provider7));
          return;
        }

        case TypeCode.DateTime:
        {
          IFormatProvider provider6 = null;
          Write(writer, data.ToDateTime(provider6));
          return;
        }
      }

      ThrowUnsupportedTypeCode(typeCode);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Write<T>(BinaryWriter writer, T? data) where T : struct, IConvertible
    {
      var hasValue = data.HasValue;
      Write(writer, hasValue);
      if (hasValue)
      {
        Write(writer, data.Value);
      }
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, double data)
    {
      writer.Write(data);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, Guid data)
    {
      var fixedSize = true;
      Write(writer, data.ToByteArray(), fixedSize);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, short data)
    {
      writer.Write(data);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, int data)
    {
      writer.Write(data);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, long data)
    {
      writer.Write(data);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    [CLSCompliant(false)]
    public static void Write(BinaryWriter writer, sbyte data)
    {
      writer.Write(data);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    public static void Write(BinaryWriter writer, float data)
    {
      writer.Write(data);
    }

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
      var flag = data != null;
      Write(writer, flag);
      if (flag)
      {
        writer.Write(data);
      }
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    [CLSCompliant(false)]
    public static void Write(BinaryWriter writer, ushort data)
    {
      writer.Write(data);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    [CLSCompliant(false)]
    public static void Write(BinaryWriter writer, uint data)
    {
      writer.Write(data);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    [CLSCompliant(false)]
    public static void Write(BinaryWriter writer, ulong data)
    {
      writer.Write(data);
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="fixedSize">
    /// The fixed size.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Write<T>(BinaryWriter writer, T?[] data, bool fixedSize) where T : struct, IConvertible
    {
      var num = Write_array_header(writer, data, fixedSize);
      for (var i = 0; i < num; i++)
      {
        Write(writer, data[i]);
      }
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="fixedSize">
    /// The fixed size.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    [CLSCompliant(false)]
    public static void Write<T>(BinaryWriter writer, T[] data, bool fixedSize) where T : struct, IConvertible
    {
      var num = Write_array_header(writer, data, fixedSize);
      for (var i = 0; i < num; i++)
      {
        Write(writer, data[i]);
      }
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="fixedSize">
    /// The fixed size.
    /// </param>
    public static void Write(BinaryWriter writer, byte[] data, bool fixedSize)
    {
      if (Write_array_header(writer, data, fixedSize) > 0)
      {
        writer.Write(data);
      }
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="fixedSize">
    /// The fixed size.
    /// </param>
    public static void Write(BinaryWriter writer, char[] data, bool fixedSize)
    {
      if (Write_array_header(writer, data, fixedSize) > 0)
      {
        writer.Write(data);
      }
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="fixedSize">
    /// The fixed size.
    /// </param>
    public static void Write(BinaryWriter writer, string[] data, bool fixedSize)
    {
      var num = Write_array_header(writer, data, fixedSize);
      for (var i = 0; i < num; i++)
      {
        Write(writer, data[i]);
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// The read_array_with_header.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="handler">
    /// The handler.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    private static void Read_array_with_header<T>(BinaryReader reader, out T[] data, ReadArrayHandler<T> handler)
    {
      int num;
      Read(reader, out num);
      if (num < 0)
      {
        data = null;
      }
      else if (num == 0)
      {
        data = new T[0];
      }
      else
      {
        handler(reader, out data, num);
      }
    }

    /// <summary>
    /// The read_collection.
    /// </summary>
    /// <param name="reader">
    /// The reader.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="handler">
    /// The handler.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="L">
    /// </typeparam>
    private static void Read_collection<T, L>(BinaryReader reader, out L data, ReadItemHandler<T> handler)
      where L : class, ICollection<T>, new()
    {
      int num;
      Read(reader, out num);
      if (num < 0)
      {
        data = default(L);
      }
      else
      {
        data = Activator.CreateInstance<L>();
        for (var i = 0; i < num; i++)
        {
          T local;
          handler(reader, out local);
          data.Add(local);
        }
      }
    }

    /// <summary>
    /// The throw unsupported type code.
    /// </summary>
    /// <param name="typeCode">
    /// The type code.
    /// </param>
    /// <exception cref="ArgumentException">
    /// </exception>
    private static void ThrowUnsupportedTypeCode(TypeCode typeCode)
    {
      throw new ArgumentException(string.Format("Unsupported type-code: {0}", typeCode));
    }

    /// <summary>
    /// The write_array_header.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="fixedSize">
    /// The fixed size.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    private static int Write_array_header<T>(BinaryWriter writer, T[] data, bool fixedSize)
    {
      if (!fixedSize)
      {
        if (data == null)
        {
          Write(writer, -1);
          return 0;
        }

        Write(writer, data.Length);
      }

      return data.Length;
    }

    /// <summary>
    /// The write_collection.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <param name="handler">
    /// The handler.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    private static void Write_collection<T>(BinaryWriter writer, ICollection<T> data, WriteItemHandler<T> handler)
    {
      var num = (data != null) ? data.Count : -1;
      Write(writer, num);
      if (num > 0)
      {
        foreach (var local in data)
        {
          handler(writer, local);
        }
      }
    }

    #endregion
  }
}