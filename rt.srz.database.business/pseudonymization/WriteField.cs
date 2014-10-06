// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriteField.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The write field base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.pseudonymization
{
  using System;
  using System.IO;
  using System.Linq.Expressions;
  using System.Text;

  using rt.srz.database.business.interfaces.pseudonymization;
  using rt.srz.database.business.model;

  /// <summary>
  /// The write field base.
  /// </summary>
  public abstract class WriteFieldBase : IWriteField
  {
    #region Constants

    /// <summary>
    ///   The mask_ date time_ local.
    /// </summary>
    private const long MaskDateTimeLocal = unchecked(0x4000000000000000);

    /// <summary>
    ///   The mask_ date time_ unspecified.
    /// </summary>
    private const long MaskDateTimeUnspecified = unchecked((long)0x8000000000000000);

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteFieldBase"/> class. 
    /// �����������
    /// </summary>
    /// <param name="expression">
    /// </param>
    protected WriteFieldBase(Expression<Func<ModelAdapter, object>> expression)
    {
      Expression = expression;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the expression.
    /// </summary>
    public Expression<Func<ModelAdapter, object>> Expression { get; private set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public abstract void WriteField(ModelAdapter model, BinaryWriter binaryWriter);

    #endregion

    #region Methods

    /// <summary>
    /// The prepare string.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="length">
    /// The length.
    /// </param>
    /// <param name="deleteTwinChar">
    /// The delete twin char.
    /// </param>
    /// <param name="identicalLetters">
    /// The identical letters.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    protected string PrepareString(string source, short length, bool deleteTwinChar, string identicalLetters)
    {
      if (string.IsNullOrEmpty(source))
      {
        return source;
      }

      // �������� ��������
      var destination = source.Trim();

      // ��������� ������
      if (destination.Length > length)
      {
        destination = destination.Substring(0, length);
      }

      // �������� ������������� ��������
      if (deleteTwinChar)
      {
        var builder = new StringBuilder();
        var prevSymbol = '\x0';
        foreach (var currentSymbol in destination)
        {
          if (prevSymbol != currentSymbol)
          {
            builder.Append(currentSymbol);
            prevSymbol = currentSymbol;
          }
        }

        destination = builder.ToString();
      }

      // ������ �������� �� ��������� ������� �����
      return destination;
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="field">
    /// The field.
    /// </param>
    /// <param name="writer">
    /// The writer.
    /// </param>
    protected void WriteField(string field, BinaryWriter writer)
    {
      var hasValue = field != null;
      WriteField(hasValue, writer);
      if (hasValue)
      {
        writer.Write(field);
      }
    }

    /// <summary>
    /// The write.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <param name="field">
    /// The field.
    /// </param>
    /// <param name="writer">
    /// The writer.
    /// </param>
    protected void WriteField<T>(T field, BinaryWriter writer) where T : struct, IConvertible
    {
      var typeCode = field.GetTypeCode();
      switch (typeCode)
      {
        case TypeCode.Byte:
        case TypeCode.SByte:
          writer.Write(field.ToByte(null));
          break;
        case TypeCode.Int16:
        case TypeCode.UInt16:
          writer.Write(field.ToInt16(null));
          break;
        case TypeCode.Int32:
        case TypeCode.UInt32:
          writer.Write(field.ToInt32(null));
          break;
        case TypeCode.Int64:
        case TypeCode.UInt64:
          writer.Write(field.ToInt64(null));
          break;
        case TypeCode.Boolean:
          writer.Write(field.ToBoolean(null));
          break;
        case TypeCode.DateTime:
        {
          var d = field.ToDateTime(null);
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
          writer.Write(field.ToDouble(null));
          break;
        case TypeCode.Single:
          writer.Write(field.ToSingle(null));
          break;
      }
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <param name="field">
    /// </param>
    /// <param name="binaryWriter">
    /// </param>
    protected void WriteField<T>(T? field, BinaryWriter binaryWriter) where T : struct, IConvertible
    {
      var hasValue = field.HasValue;
      binaryWriter.Write(hasValue);
      if (hasValue)
      {
        WriteField(field.Value, binaryWriter);
      }
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� �������
  /// </summary>
  public class WriteLastNameField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteLastNameField"/> class. 
    ///   �����������
    /// </summary>
    public WriteLastNameField()
      : base(x => x.PersonData.LastName)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.LastName)
      {
        return;
      }

      // ������������� � ������ ����
      var searchKeyType = model.SearchKeyType;
      var preparedField = PrepareString(
                                        model.PersonData.LastName, 
                                        searchKeyType.LastNameLength, 
                                        searchKeyType.DeleteTwinChar, 
                                        searchKeyType.IdenticalLetters);
      WriteField(preparedField, binaryWriter);
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� �������
  /// </summary>
  public class WriteFirstNameField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteFirstNameField"/> class. 
    ///   �����������
    /// </summary>
    public WriteFirstNameField()
      : base(x => x.PersonData.FirstName)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.FirstName)
      {
        return;
      }

      var searchKeyType = model.SearchKeyType;
      var preparedField = PrepareString(
                                        model.PersonData.FirstName, 
                                        searchKeyType.FirstNameLength, 
                                        searchKeyType.DeleteTwinChar, 
                                        searchKeyType.IdenticalLetters);
      WriteField(preparedField, binaryWriter);
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ��������
  /// </summary>
  public class WriteMiddleNameField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteMiddleNameField"/> class. 
    ///   �����������
    /// </summary>
    public WriteMiddleNameField()
      : base(x => x.PersonData.MiddleName)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.MiddleName)
      {
        return;
      }

      var searchKeyType = model.SearchKeyType;
      var preparedField = PrepareString(
                                        model.PersonData.MiddleName, 
                                        searchKeyType.MiddleNameLength, 
                                        searchKeyType.DeleteTwinChar, 
                                        searchKeyType.IdenticalLetters);
      WriteField(preparedField, binaryWriter);
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ���� ��������
  /// </summary>
  public class WriteBirthdayField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteBirthdayField"/> class. 
    ///   �����������
    /// </summary>
    public WriteBirthdayField()
      : base(x => x.PersonData.Birthday)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.Birthday)
      {
        return;
      }

      // ����������� ���������
      if (model.PersonData.Birthday != null)
      {
        WriteField(model.PersonData.Birthday, binaryWriter);
      }

      DateTime? birthday = null;
      switch (model.SearchKeyType.BirthdayLength)
      {
        case 4: // ������ ���
          birthday = new DateTime(model.PersonData.Birthday.Value.Year, 1, 1);
          break;
        case 6: // ��� � �����
          birthday = new DateTime(model.PersonData.Birthday.Value.Year, model.PersonData.Birthday.Value.Month, 1);
          break;
        case 8: // ������ ���� ��������
          birthday = model.PersonData.Birthday;
          break;
      }

      WriteField(birthday, binaryWriter);
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ����� ��������
  /// </summary>
  public class WriteBirtplaceField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteBirtplaceField"/> class. 
    ///   �����������
    /// </summary>
    public WriteBirtplaceField()
      : base(x => x.PersonData.Birthplace)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.Birthplace)
      {
        return;
      }

      var searchKeyType = model.SearchKeyType;
      var preparedField = PrepareString(
                                        model.PersonData.Birthplace, 
                                        short.MaxValue, 
                                        searchKeyType.DeleteTwinChar, 
                                        searchKeyType.IdenticalLetters);
      WriteField(preparedField, binaryWriter);
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� �����
  /// </summary>
  public class WriteSnilsField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteSnilsField"/> class. 
    ///   �����������
    /// </summary>
    public WriteSnilsField()
      : base(x => x.PersonData.Snils)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.Snils)
      {
        WriteField(model.PersonData.Snils, binaryWriter);
      }
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ���� ���������
  /// </summary>
  public class WriteDocumentTypeField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteDocumentTypeField"/> class. 
    ///   �����������
    /// </summary>
    public WriteDocumentTypeField()
      : base(x => x.Document.DocumentTypeId)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.DocumentType)
      {
        WriteField(model.Document.DocumentTypeId, binaryWriter);
      }
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ����� ���������
  /// </summary>
  public class WriteDocumentSeriesField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteDocumentSeriesField"/> class. 
    ///   �����������
    /// </summary>
    public WriteDocumentSeriesField()
      : base(x => x.Document.Series)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.DocumentSeries)
      {
        WriteField(model.Document.Series, binaryWriter);
      }
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ������ ���������
  /// </summary>
  public class WriteDocumentNumberField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteDocumentNumberField"/> class. 
    ///   �����������
    /// </summary>
    public WriteDocumentNumberField()
      : base(x => x.Document.Number)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.DocumentNumber)
      {
        WriteField(model.Document.Number, binaryWriter);
      }
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� �����
  /// </summary>
  public class WriteOkatoField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteOkatoField"/> class. 
    ///   �����������
    /// </summary>
    public WriteOkatoField()
      : base(x => x.Okato)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.Okato)
      {
        WriteField(model.Okato, binaryWriter);
      }
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ���� ������
  /// </summary>
  public class WritePolisTypeField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WritePolisTypeField"/> class. 
    ///   �����������
    /// </summary>
    public WritePolisTypeField()
      : base(x => x.MedicalInsurance.PolisTypeId)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.PolisType)
      {
        WriteField(model.MedicalInsurance.PolisTypeId, binaryWriter);
      }
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ����� ������
  /// </summary>
  public class WritePolisSeriesField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WritePolisSeriesField"/> class. 
    ///   �����������
    /// </summary>
    public WritePolisSeriesField()
      : base(x => x.MedicalInsurance.PolisSeria)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.PolisSeria)
      {
        WriteField(model.MedicalInsurance.PolisSeria, binaryWriter);
      }
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ������ ������
  /// </summary>
  public class WritePolisNumberField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WritePolisNumberField"/> class. 
    ///   �����������
    /// </summary>
    public WritePolisNumberField()
      : base(x => x.MedicalInsurance.PolisNumber)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.PolisNumber)
      {
        WriteField(model.MedicalInsurance.PolisNumber, binaryWriter);
      }
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ����� ��� ������ �����������
  /// </summary>
  public class WriteAddress1StreetField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteAddress1StreetField"/> class. 
    ///   �����������
    /// </summary>
    public WriteAddress1StreetField()
      : base(x => x.Address1.Street)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.AddressStreet)
      {
        return;
      }

      var searchKeyType = model.SearchKeyType;
      var preparedField = PrepareString(
                                        model.Address1.Street, 
                                        searchKeyType.AddressStreetLength, 
                                        searchKeyType.DeleteTwinChar, 
                                        searchKeyType.IdenticalLetters);
      WriteField(preparedField, binaryWriter);
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ������ ���� ��� ������ �����������
  /// </summary>
  public class WriteAddress1HouseField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteAddress1HouseField"/> class. 
    ///   �����������
    /// </summary>
    public WriteAddress1HouseField()
      : base(x => x.Address1.House)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.AddressHouse)
      {
        WriteField(model.Address1.House, binaryWriter);
      }
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ������ �������� ��� ������ �����������
  /// </summary>
  public class WriteAddress1RoomField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteAddress1RoomField"/> class. 
    ///   �����������
    /// </summary>
    public WriteAddress1RoomField()
      : base(x => x.Address1.Room)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.AddressRoom)
      {
        WriteField(model.Address1.Room, binaryWriter);
      }
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ����� ��� ������ ����������
  /// </summary>
  public class WriteAddress2StreetField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteAddress2StreetField"/> class. 
    ///   �����������
    /// </summary>
    public WriteAddress2StreetField()
      : base(x => x.Address2.Street)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.AddressStreet2)
      {
        return;
      }

      var searchKeyType = model.SearchKeyType;
      string preparedField = null;
      if (model.Address2 != null)
      {
        preparedField = PrepareString(
                                      preparedField, 
                                      searchKeyType.AddressStreetLength2, 
                                      searchKeyType.DeleteTwinChar, 
                                      searchKeyType.IdenticalLetters);
      }

      WriteField(preparedField, binaryWriter);
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ������ ���� ��� ������ �����������
  /// </summary>
  public class WriteAddress2HouseField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteAddress2HouseField"/> class. 
    ///   �����������
    /// </summary>
    public WriteAddress2HouseField()
      : base(x => x.Address2.House)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.AddressHouse2)
      {
        return;
      }

      string preparedField = null;
      if (model.Address2 != null)
      {
        preparedField = model.Address2.House;
      }

      WriteField(preparedField, binaryWriter);
    }

    #endregion
  }

  /// <summary>
  ///   ��������������� ������ �������� ��� ������ �����������
  /// </summary>
  public class WriteAddress2RoomField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteAddress2RoomField"/> class. 
    ///   �����������
    /// </summary>
    public WriteAddress2RoomField()
      : base(x => x.Address2.Room)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">
    /// ������
    /// </param>
    /// <param name="binaryWriter">
    /// The binary Writer.
    /// </param>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.AddressRoom2)
      {
        WriteField(model.Address2.Room, binaryWriter);
      }
    }

    #endregion
  }
}