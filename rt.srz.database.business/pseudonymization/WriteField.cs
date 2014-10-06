// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriteField.cs" company="РусБИТех">
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
    /// Конструктор
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
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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

      // удаление пробелов
      var destination = source.Trim();

      // обрезание строки
      if (destination.Length > length)
      {
        destination = destination.Substring(0, length);
      }

      // Удаление дублированных символов
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

      // Замена символов на основании таблицы замен
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
  ///   Преобразователь Фамилии
  /// </summary>
  public class WriteLastNameField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteLastNameField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteLastNameField()
      : base(x => x.PersonData.LastName)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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

      // Препроцессинг и запись поля
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
  ///   Преобразователь Фамилии
  /// </summary>
  public class WriteFirstNameField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteFirstNameField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteFirstNameField()
      : base(x => x.PersonData.FirstName)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь Отчества
  /// </summary>
  public class WriteMiddleNameField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteMiddleNameField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteMiddleNameField()
      : base(x => x.PersonData.MiddleName)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь даты рождения
  /// </summary>
  public class WriteBirthdayField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteBirthdayField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteBirthdayField()
      : base(x => x.PersonData.Birthday)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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

      // Стандартная обработка
      if (model.PersonData.Birthday != null)
      {
        WriteField(model.PersonData.Birthday, binaryWriter);
      }

      DateTime? birthday = null;
      switch (model.SearchKeyType.BirthdayLength)
      {
        case 4: // Только год
          birthday = new DateTime(model.PersonData.Birthday.Value.Year, 1, 1);
          break;
        case 6: // Год и месяц
          birthday = new DateTime(model.PersonData.Birthday.Value.Year, model.PersonData.Birthday.Value.Month, 1);
          break;
        case 8: // Полная дата рождения
          birthday = model.PersonData.Birthday;
          break;
      }

      WriteField(birthday, binaryWriter);
    }

    #endregion
  }

  /// <summary>
  ///   Преобразователь места рождения
  /// </summary>
  public class WriteBirtplaceField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteBirtplaceField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteBirtplaceField()
      : base(x => x.PersonData.Birthplace)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь СНИЛС
  /// </summary>
  public class WriteSnilsField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteSnilsField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteSnilsField()
      : base(x => x.PersonData.Snils)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь типа документа
  /// </summary>
  public class WriteDocumentTypeField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteDocumentTypeField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteDocumentTypeField()
      : base(x => x.Document.DocumentTypeId)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь серии документа
  /// </summary>
  public class WriteDocumentSeriesField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteDocumentSeriesField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteDocumentSeriesField()
      : base(x => x.Document.Series)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь номера документа
  /// </summary>
  public class WriteDocumentNumberField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteDocumentNumberField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteDocumentNumberField()
      : base(x => x.Document.Number)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь ОКАТО
  /// </summary>
  public class WriteOkatoField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteOkatoField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteOkatoField()
      : base(x => x.Okato)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь типа полиса
  /// </summary>
  public class WritePolisTypeField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WritePolisTypeField"/> class. 
    ///   Конструктор
    /// </summary>
    public WritePolisTypeField()
      : base(x => x.MedicalInsurance.PolisTypeId)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь серии полиса
  /// </summary>
  public class WritePolisSeriesField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WritePolisSeriesField"/> class. 
    ///   Конструктор
    /// </summary>
    public WritePolisSeriesField()
      : base(x => x.MedicalInsurance.PolisSeria)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь номера полиса
  /// </summary>
  public class WritePolisNumberField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WritePolisNumberField"/> class. 
    ///   Конструктор
    /// </summary>
    public WritePolisNumberField()
      : base(x => x.MedicalInsurance.PolisNumber)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь улицы для адреса регистрации
  /// </summary>
  public class WriteAddress1StreetField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteAddress1StreetField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteAddress1StreetField()
      : base(x => x.Address1.Street)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь номера дома для адреса регистрации
  /// </summary>
  public class WriteAddress1HouseField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteAddress1HouseField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteAddress1HouseField()
      : base(x => x.Address1.House)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь номера квартиры для адреса регистрации
  /// </summary>
  public class WriteAddress1RoomField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteAddress1RoomField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteAddress1RoomField()
      : base(x => x.Address1.Room)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь улицы для адреса проживания
  /// </summary>
  public class WriteAddress2StreetField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteAddress2StreetField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteAddress2StreetField()
      : base(x => x.Address2.Street)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь номера дома для адреса регистрации
  /// </summary>
  public class WriteAddress2HouseField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteAddress2HouseField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteAddress2HouseField()
      : base(x => x.Address2.House)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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
  ///   Преобразователь номера квартиры для адреса регистрации
  /// </summary>
  public class WriteAddress2RoomField : WriteFieldBase
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteAddress2RoomField"/> class. 
    ///   Конструктор
    /// </summary>
    public WriteAddress2RoomField()
      : base(x => x.Address2.Room)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Преобразует значение поля из модели к виду, подлежащему преобразованию в хэш
    /// </summary>
    /// <param name="model">
    /// Модель
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