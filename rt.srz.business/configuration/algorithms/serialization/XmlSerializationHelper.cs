// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlSerializationHelper.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.configuration.algorithms.serialization
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text;
  using System.Xml;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.commons;
  using rt.srz.model.Hl7.person;
  using rt.srz.model.Hl7.person.messages;
  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   Сериализатор
  /// </summary>
  public class XmlSerializationHelper
  {
    #region Static Fields

    /// <summary>
    ///   Кешированные сериализаторы
    /// </summary>
    private static readonly Dictionary<Guid, XmlSerializer> CacheSerializer = new Dictionary<Guid, XmlSerializer>();

    /// <summary>
    ///   Переопределеные теги
    /// </summary>
    private static XmlAttributeOverrides myOverrides;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Десериализует указанный объект
    /// </summary>
    /// <param name="file">
    /// Полный путь к файлу
    /// </param>
    /// <param name="p">
    /// объект пакета
    /// </param>
    /// <returns>
    /// объект пакета
    /// </returns>
    public static object Deserialize(string file, object p)
    {
      var fileInfo = new FileInfo(file);
      try
      {
        using (var stream = fileInfo.Open(FileMode.Open, FileAccess.Read))
        {
          using (var reader = new XmlTextReader(stream))
          {
            return Deserialize(p, reader);
          }
        }
      }
      catch (IOException)
      {
        // Если файл уже открыт, то просто его пропускаем. Возможно шлюз еще не успел записать файл.
        return null;
      }
    }

    /// <summary>
    /// Десериализует указанный объект
    /// </summary>
    /// <param name="p">
    /// объект пакета
    /// </param>
    /// <param name="stream">
    /// поток
    /// </param>
    /// <returns>
    /// объект пакета
    /// </returns>
    public static object Deserialize(object p, XmlTextReader stream)
    {
      var serializer = GetSerializer(p);
      return serializer.CanDeserialize(stream) ? serializer.Deserialize(stream) : null;
    }

    /// <summary>
    /// The deserialize.
    /// </summary>
    /// <param name="p">
    /// The p.
    /// </param>
    /// <param name="stream">
    /// The stream.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    public static object Deserialize(object p, XmlReader stream)
    {
      var serializer = GetSerializer(p);
      return serializer.CanDeserialize(stream) ? serializer.Deserialize(stream) : null;
    }

    /// <summary>
    /// Десериализует указанный объект
    /// </summary>
    /// <param name="p">
    /// объект пакета
    /// </param>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <returns>
    /// объект пакета
    /// </returns>
    public static object Deserialize(object p, string str)
    {
      using (var reader = new StringReader(str))
      {
        using (var xmlReader = new XmlTextReader(reader))
        {
          return Deserialize(p, xmlReader);
        }
      }
    }

    /// <summary>
    ///   Инициализация переопределеных тегов
    /// </summary>
    /// <returns>XmlAttributeOverrides</returns>
    public static XmlAttributeOverrides GetAttributeOverrides()
    {
      if (myOverrides == null)
      {
        myOverrides = new XmlAttributeOverrides();

        GetElementAttributeIn1();
        GetElementAttributeIn1Card();
      }

      return myOverrides;
    }

    /// <summary>
    /// Создает и возвращает сериализатор с переопределенными тегами, поддерживает кеширование сериализаторов
    /// </summary>
    /// <param name="type">
    /// Тип для которого необходим сериализатор
    /// </param>
    /// <returns>
    /// Сериализатор
    /// </returns>
    public static XmlSerializer GetSerializer(Type type)
    {
      var g = type.GUID;
      XmlSerializer serializer;
      lock (CacheSerializer)
      {
        if (CacheSerializer.TryGetValue(g, out serializer))
        {
          return serializer;
        }

        myOverrides = GetAttributeOverrides();
        serializer = new XmlSerializer(type, myOverrides);
        CacheSerializer.Add(g, serializer);
      }

      return serializer;
    }

    /// <summary>
    /// Создает и возвращает сериализатор с переопределенными тегами, поддерживает кеширование сериализаторов
    /// </summary>
    /// <param name="o">
    /// Тип для которого необходим сериализатор
    /// </param>
    /// <returns>
    /// Сериализатор
    /// </returns>
    public static XmlSerializer GetSerializer(object o)
    {
      return GetSerializer(o.GetType());
    }

    /// <summary>
    /// Сериализует объект в строку (сериализатор кеширует)
    /// </summary>
    /// <param name="o">
    /// Объект для сериализации
    /// </param>
    /// <returns>
    /// сериализованый объект
    /// </returns>
    public static string SerializeObject(object o)
    {
      var serializer = GetSerializer(o);
      using (var wr = new StringWriter())
      {
        serializer.Serialize(wr, o);
        return wr.ToString();
      }
    }

    /// <summary>
    /// Сериализация наследников PersonCard
    /// </summary>
    /// <param name="person">
    /// Объект пакета
    /// </param>
    /// <param name="fileName">
    /// Имя файла
    /// </param>
    public static void SerializePersonCard(PersonCard person, string fileName)
    {
      SerializeBasePersonTemplate(person, fileName, "ZPIMessageBatch");
    }

    /// <summary>
    /// Сериализация наследников PersonCard
    /// </summary>
    /// <param name="person">
    /// Объект пакета
    /// </param>
    /// <returns>
    /// The <see cref="MemoryStream"/>.
    /// </returns>
    public static MemoryStream SerializePersonCard(PersonCard person)
    {
      return SerializeToMemoryStream(person, "ZPIMessageBatch");
    }

    /// <summary>
    /// Сериализация наследников PersonErp
    /// </summary>
    /// <param name="person">
    /// Объект пакета
    /// </param>
    /// <param name="fileName">
    /// Имя файла
    /// </param>
    public static void SerializePersonErp(PersonErp person, string fileName)
    {
      SerializeBasePersonTemplate(person, fileName, "UPRMessageBatch");
    }

    /// <summary>
    /// Сериализация объекта
    /// </summary>
    /// <param name="person">
    /// Объект пакета
    /// </param>
    /// <param name="fileName">
    /// Имя файла
    /// </param>
    /// <param name="tableName">
    /// Корневой тег
    /// </param>
    /// <param name="settings">
    /// Настройки записи xml
    /// </param>
    /// ///
    public static void SerializeToFile(
      object person,
      string fileName,
      string tableName,
      XmlWriterSettings settings = null)
    {
      var nt = new NameTable();
      nt.Add(tableName);
      var nameres = GetNamespaces(nt);

      var directory = new FileInfo(fileName).Directory;
      if (directory != null && !directory.Exists)
      {
        directory.Create();
      }

      using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
      {
        using (var writer = new XmlTextWriter(stream, Encoding.Default))
        {
          var serializer = GetSerializer(person);
          var namespaces = XmlHelper.ResolveNamespacesForSerializer(nameres);
          if (namespaces != null)
          {
            writer.Formatting = Formatting.Indented;
            serializer.Serialize(settings != null ? XmlWriter.Create(writer, settings) : writer, person, namespaces);
          }
          else
          {
            serializer.Serialize(settings != null ? XmlWriter.Create(writer, settings) : writer, person);
          }
        }
      }
    }

    /// <summary>
    /// Сериализация объекта
    /// </summary>
    /// <param name="person">
    /// Объект пакета
    /// </param>
    /// <param name="tableName">
    /// Корневой тег
    /// </param>
    /// <param name="settings">
    /// Настройки записи xml
    /// </param>
    /// ///
    /// <returns>
    /// The <see cref="FileStream"/>.
    /// </returns>
    public static MemoryStream SerializeToMemoryStream(
      object person,
      string tableName,
      XmlWriterSettings settings = null)
    {
      var nt = new NameTable();
      nt.Add(tableName);
      var nameres = GetNamespaces(nt);

      var stream = new MemoryStream();
      {
        using (var writer = new XmlTextWriter(stream, Encoding.Default))
        {
          var serializer = GetSerializer(person);
          var namespaces = XmlHelper.ResolveNamespacesForSerializer(nameres);
          if (namespaces != null)
          {
            writer.Formatting = Formatting.Indented;
            serializer.Serialize(settings != null ? XmlWriter.Create(writer, settings) : writer, person, namespaces);
          }
          else
          {
            serializer.Serialize(settings != null ? XmlWriter.Create(writer, settings) : writer, person);
          }
        }
      }

      return stream;
    }

    /// <summary>
    /// Сериализация объекта
    /// </summary>
    /// <param name="person">
    /// Объект пакета
    /// </param>
    /// <param name="fileName">
    /// Имя файла
    /// </param>
    public static void SerializeZwpList(ZpiZwi person, string fileName)
    {
      SerializeToFile(person, fileName, "UPRMessageBatch");
    }

    /// <summary>
    /// подписывает файлы
    /// </summary>
    /// <param name="path">
    /// полный путь к папке, в которой есть файлы пакета
    /// </param>
    public void SignFiles(string path)
    {
      var allFiles = new List<string>(Directory.GetFiles(path));
      foreach (var f in allFiles)
      {
        using (var stream = new FileStream(f, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
        {
          XmlStreamer.CalculateAndSaveHash(stream);
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Инициализация переопределенных тегов для класса In1
    /// </summary>
    private static void GetElementAttributeIn1()
    {
      var myElementAttribute = new XmlElementAttribute("IN1.1") { Order = 0 };
      var myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "Id", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.2", Order = 1 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "PlanId", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.3", Order = 2 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "CompanyId", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.4", Order = 3 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "CompanyName", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.5", Order = 4 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "AddressSmo", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.6", Order = 5 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "FioInSmo", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.7", Order = 6 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "Phone", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.12", Order = 7 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "DateBeginInsurence", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.13", Order = 8 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "DateEndInsurence", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.15", Order = 9 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "CodeOfRegion", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.16", Order = 10 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "FioList", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.18", Order = 11 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "BirthDay", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.19", Order = 12 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "AddressList", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.35", Order = 13 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "InsuranceType", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.36", Order = 14 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "InsuranceSerNum", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.42", Order = 15 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "Employment", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.43", Order = 16 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "Sex", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.49", Order = 17 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "IdentificatorsList", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.52", Order = 18 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(IN1), "PlaceOfBirth", myAttributes);
    }

    /// <summary>
    ///   Инициализация переопределенных тегов для класса In1Card
    /// </summary>
    private static void GetElementAttributeIn1Card()
    {
      var myElementAttribute = new XmlElementAttribute("IN1.1") { Order = 0 };
      var myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "Id", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.2", Order = 1 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "PlanId", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.3", Order = 2 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "CompanyId", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.4", Order = 3 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "CompanyName", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.5", Order = 4 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "AddressSmo", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.6", Order = 5 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "FioInSmo", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.7", Order = 6 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "Phone", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.12", Order = 7 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "DateBeginInsurence", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.13", Order = 8 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "DateEndInsurence", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.15", Order = 9 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "CodeOfRegion", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.16", Order = 10 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "FioList", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.18", Order = 11 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "BirthDay", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.19", Order = 12 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "AddressList", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.35", Order = 13 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "InsuranceType", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.36", Order = 14 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "InsuranceSerNum", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.42", Order = 15 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "Employment", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.43", Order = 16 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "Sex", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.49", Order = 17 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "IdentificatorsList", myAttributes);

      myElementAttribute = new XmlElementAttribute { ElementName = "IN1.52", Order = 18 };
      myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(In1Card), "PlaceOfBirth", myAttributes);
    }

    /// <summary>
    /// The get namespaces.
    /// </summary>
    /// <param name="nt">
    /// The nt.
    /// </param>
    /// <returns>
    /// The <see cref="XmlNamespaceManager"/>.
    /// </returns>
    private static XmlNamespaceManager GetNamespaces(NameTable nt)
    {
      var ns = new XmlNamespaceManager(new NameTable());
      ns.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
      ns.AddNamespace("rtc", "http://www.rintech.ru");
      ns.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
      return ns;
    }

    /// <summary>
    /// Сериализация наследников BasePersonTemplate
    /// </summary>
    /// <param name="person">
    /// Объект пакета
    /// </param>
    /// <param name="fileName">
    /// Имя файла
    /// </param>
    /// <param name="tableName">
    /// Корневой тег
    /// </param>
    private static void SerializeBasePersonTemplate(BasePersonTemplate person, string fileName, string tableName)
    {
      var nt = new NameTable();
      nt.Add(tableName);
      var nameres = GetNamespaces(nt);

      var directoryName = new FileInfo(fileName).DirectoryName;
      if (directoryName != null && !Directory.Exists(directoryName))
      {
        Directory.CreateDirectory(directoryName);
      }

      using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
      {
        using (var writer = new XmlTextWriter(stream, Encoding.GetEncoding(1251)))
        {
          var serializer = GetSerializer(person);
          var namespaces = XmlHelper.ResolveNamespacesForSerializer(nameres);
          if (namespaces != null)
          {
            serializer.Serialize(writer, person, namespaces);
          }
          else
          {
            serializer.Serialize(writer, person);
          }

          if (!XmlStreamer.CalculateAndSaveHash(stream, out person.EndPacket.Hash))
          {
            throw new InvalidOperationException("Не удалось прописать контрольную сумму");
          }
        }
      }
    }

    #endregion
  }
}