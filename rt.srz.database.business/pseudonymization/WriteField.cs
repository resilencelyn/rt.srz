//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace rt.srz.database.business.pseudonymization
{
  using System;
  using System.IO;
  using System.Text;
  using System.Linq.Expressions;

  using rt.srz.database.business.interfaces.pseudonymization;
  using rt.srz.database.business.model;

  public abstract class WriteFieldBase : IWriteField
  {
    #region Constants
    /// <summary>
    /// The mask_ date time_ local.
    /// </summary>
    private const long MaskDateTimeLocal = unchecked(0x4000000000000000);

    /// <summary>
    /// The mask_ date time_ unspecified.
    /// </summary>
    private const long MaskDateTimeUnspecified = unchecked((long)0x8000000000000000);

    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the expression.
    /// </summary>
    public Expression<Func<ModelAdapter, object>> Expression { get; private set; }

    #endregion

    #region Constructors and Destructors
    
    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="expression"></param>
    protected WriteFieldBase(Expression<Func<ModelAdapter, object>> expression)
    {
      Expression = expression;
    }
    #endregion 

    #region Protected Methods
    protected string PrepareString(string source, short length, bool deleteTwinChar, string identicalLetters)
    {
      if (string.IsNullOrEmpty(source))
        return source;
      
      //�������� ��������
      string destination = source.Trim();
      
      //��������� ������
      if (destination.Length > length)
        destination = destination.Substring(0, length);

      //�������� ������������� ��������
      if (deleteTwinChar)
      { 
        StringBuilder builder = new StringBuilder();
        char prevSymbol = '\x0';
        foreach (char currentSymbol in destination)
        {
            if (prevSymbol != currentSymbol)
            {
              builder.Append(currentSymbol);
              prevSymbol = currentSymbol;
            }
        }

        destination = builder.ToString();
      }

      //������ �������� �� ��������� ������� �����


      return destination;
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
    protected void WriteField(string field, BinaryWriter writer)
    {
      var hasValue = field != null;
      WriteField(hasValue, writer);
      if (hasValue)
        writer.Write(field);
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
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="field"></param>
    /// <param name="binaryWriter"></param>
    protected void WriteField<T>(T? field, BinaryWriter binaryWriter) where T : struct, IConvertible
    {
      var hasValue = field.HasValue;
      binaryWriter.Write(hasValue);
      if (hasValue)
        WriteField(field.Value, binaryWriter);
    }
    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public abstract void WriteField(ModelAdapter model, BinaryWriter binaryWriter);

    #endregion
  }
  
  /// <summary>
  /// ��������������� �������
  /// </summary>
  public class WriteLastNameField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteLastNameField() : base(x => x.PersonData.LastName)
    { 
    }
    
    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.LastName)
        return;

      //������������� � ������ ����
      SearchKeyType searchKeyType = model.SearchKeyType;
      string preparedField = PrepareString(model.PersonData.LastName, searchKeyType.LastNameLength, 
        searchKeyType.DeleteTwinChar, searchKeyType.IdenticalLetters);
      WriteField(preparedField, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� �������
  /// </summary>
  public class WriteFirstNameField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteFirstNameField()
      : base(x => x.PersonData.FirstName)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.FirstName)
        return;

      SearchKeyType searchKeyType = model.SearchKeyType;
      string preparedField = PrepareString(model.PersonData.FirstName, searchKeyType.FirstNameLength,
        searchKeyType.DeleteTwinChar, searchKeyType.IdenticalLetters);
      WriteField(preparedField, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ��������
  /// </summary>
  public class WriteMiddleNameField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteMiddleNameField()
      : base(x => x.PersonData.MiddleName)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.MiddleName)
        return;

      SearchKeyType searchKeyType = model.SearchKeyType;
      string preparedField = PrepareString(model.PersonData.MiddleName, searchKeyType.MiddleNameLength,
        searchKeyType.DeleteTwinChar, searchKeyType.IdenticalLetters);
      WriteField(preparedField, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ���� ��������
  /// </summary>
  public class WriteBirthdayField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteBirthdayField()
      : base(x => x.PersonData.Birthday)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.Birthday)
        return;

      //����������� ���������
      if (model.PersonData.Birthday != null)
        WriteField(model.PersonData.Birthday, binaryWriter);

      DateTime? birthday = null;
      switch (model.SearchKeyType.BirthdayLength)
      { 
        case 4: //������ ���
          birthday = new DateTime(model.PersonData.Birthday.Value.Year, 1, 1);
          break;
        case 6: //��� � �����
          birthday = new DateTime(model.PersonData.Birthday.Value.Year, model.PersonData.Birthday.Value.Month, 1);
          break;
        case 8: //������ ���� ��������
          birthday = model.PersonData.Birthday;
          break;
      }
      WriteField(birthday, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ����� ��������
  /// </summary>
  public class WriteBirtplaceField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteBirtplaceField()
      : base(x => x.PersonData.Birthplace)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.Birthplace)
        return;

      SearchKeyType searchKeyType = model.SearchKeyType;
      string preparedField = PrepareString(model.PersonData.Birthplace, short.MaxValue,
        searchKeyType.DeleteTwinChar, searchKeyType.IdenticalLetters);
      WriteField(preparedField, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� �����
  /// </summary>
  public class WriteSnilsField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteSnilsField()
      : base(x => x.PersonData.Snils)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.Snils)
        WriteField(model.PersonData.Snils, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ���� ���������
  /// </summary>
  public class WriteDocumentTypeField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteDocumentTypeField()
      : base(x => x.Document.DocumentTypeId)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.DocumentType)
        WriteField(model.Document.DocumentTypeId, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ����� ���������
  /// </summary>
  public class WriteDocumentSeriesField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteDocumentSeriesField()
      : base(x => x.Document.Series)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.DocumentSeries)
        WriteField(model.Document.Series, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ������ ���������
  /// </summary>
  public class WriteDocumentNumberField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteDocumentNumberField()
      : base(x => x.Document.Number)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.DocumentNumber)
        WriteField(model.Document.Number, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� �����
  /// </summary>
  public class WriteOkatoField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteOkatoField()
      : base(x => x.Okato)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.Okato)
        WriteField(model.Okato, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ���� ������
  /// </summary>
  public class WritePolisTypeField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WritePolisTypeField()
      : base(x => x.MedicalInsurance.PolisTypeId)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.PolisType)
        WriteField(model.MedicalInsurance.PolisTypeId, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ����� ������
  /// </summary>
  public class WritePolisSeriesField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WritePolisSeriesField()
      : base(x => x.MedicalInsurance.PolisSeria)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.PolisSeria)
        WriteField(model.MedicalInsurance.PolisSeria, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ������ ������
  /// </summary>
  public class WritePolisNumberField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WritePolisNumberField()
      : base(x => x.MedicalInsurance.PolisNumber)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.PolisNumber)
        WriteField(model.MedicalInsurance.PolisNumber, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ����� ��� ������ �����������
  /// </summary>
  public class WriteAddress1StreetField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteAddress1StreetField()
      : base(x => x.Address1.Street)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.AddressStreet)
        return;

      SearchKeyType searchKeyType = model.SearchKeyType;
      string preparedField = PrepareString(model.Address1.Street, searchKeyType.AddressStreetLength,
        searchKeyType.DeleteTwinChar, searchKeyType.IdenticalLetters);
      WriteField(preparedField, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ������ ���� ��� ������ �����������
  /// </summary>
  public class WriteAddress1HouseField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteAddress1HouseField()
      : base(x => x.Address1.House)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.AddressHouse)
        WriteField(model.Address1.House, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ������ �������� ��� ������ �����������
  /// </summary>
  public class WriteAddress1RoomField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteAddress1RoomField()
      : base(x => x.Address1.Room)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.AddressRoom)
        WriteField(model.Address1.Room, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ����� ��� ������ ����������
  /// </summary>
  public class WriteAddress2StreetField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteAddress2StreetField()
      : base(x => x.Address2.Street)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.AddressStreet2)
        return;

      SearchKeyType searchKeyType = model.SearchKeyType;
      string preparedField = null;
      if (model.Address2 != null)
        preparedField = PrepareString(preparedField, searchKeyType.AddressStreetLength2, searchKeyType.DeleteTwinChar, searchKeyType.IdenticalLetters);
      WriteField(preparedField, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ������ ���� ��� ������ �����������
  /// </summary>
  public class WriteAddress2HouseField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteAddress2HouseField()
      : base(x => x.Address2.House)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (!model.SearchKeyType.AddressHouse2)
        return;
       
      string preparedField = null;
      if (model.Address2 != null)
        preparedField = model.Address2.House;
      
      WriteField(preparedField, binaryWriter);
    }
  }

  /// <summary>
  /// ��������������� ������ �������� ��� ������ �����������
  /// </summary>
  public class WriteAddress2RoomField : WriteFieldBase
  {
    /// <summary>
    /// �����������
    /// </summary>
    public WriteAddress2RoomField()
      : base(x => x.Address2.Room)
    {
    }

    /// <summary>
    /// ����������� �������� ���� �� ������ � ����, ����������� �������������� � ���
    /// </summary>
    /// <param name="model">������</param>
    /// <returns>��������������� ��������</returns>
    public override void WriteField(ModelAdapter model, BinaryWriter binaryWriter)
    {
      if (model.SearchKeyType.AddressRoom2)
        WriteField(model.Address2.Room, binaryWriter);
    }
  }
}
