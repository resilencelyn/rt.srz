// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlSerializationHelper.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   ������������
// </summary>
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

  using rt.srz.model.HL7.commons;
  using rt.srz.model.HL7.person;
  using rt.srz.model.HL7.person.messages;
  using rt.srz.model.HL7.person.requests;
  using rt.srz.model.HL7.person.target;

  #endregion

  /// <summary>
  ///   ������������
  /// </summary>
  public class XmlSerializationHelper
  {
    #region Static Fields

    /// <summary>
    ///   ������������ �������������
    /// </summary>
    private static readonly Dictionary<Guid, XmlSerializer> CacheSerializer = new Dictionary<Guid, XmlSerializer>();

    /// <summary>
    ///   ��������������� ����
    /// </summary>
    private static XmlAttributeOverrides myOverrides;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ������������� ��������� ������
    /// </summary>
    /// <param name="file">
    /// ������ ���� � �����
    /// </param>
    /// <param name="p">
    /// ������ ������
    /// </param>
    /// <returns>
    /// ������ ������
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
        // ���� ���� ��� ������, �� ������ ��� ����������. �������� ���� ��� �� ����� �������� ����.
        return null;
      }
    }

    /// <summary>
    /// ������������� ��������� ������
    /// </summary>
    /// <param name="p">
    /// ������ ������
    /// </param>
    /// <param name="stream">
    /// �����
    /// </param>
    /// <returns>
    /// ������ ������
    /// </returns>
    public static object Deserialize(object p, XmlTextReader stream)
    {
      var serializer = GetSerializer(p);
      return serializer.CanDeserialize(stream) ? serializer.Deserialize(stream) : null;
    }

    public static object Deserialize(object p, XmlReader stream)
    {
      var serializer = GetSerializer(p);
      return serializer.CanDeserialize(stream) ? serializer.Deserialize(stream) : null;
    }

    /// <summary>
    /// ������������� ��������� ������
    /// </summary>
    /// <param name="p">
    /// ������ ������
    /// </param>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <returns>
    /// ������ ������
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
    ///   ������������� ��������������� �����
    /// </summary>
    /// <returns>XmlAttributeOverrides</returns>
    public static XmlAttributeOverrides GetAttributeOverrides()
    {
      if (myOverrides == null)
      {
        myOverrides = new XmlAttributeOverrides();

        GetElementAttributeIn1();
        GetElementAttributeIn1Card();
        GetElementAttributeRspZk1();
        GetElementAttributeRspZk2();
        GetElementAttributeRspZk4();
        GetElementAttributeRspZk5();
      }

      return myOverrides;
    }

    /// <summary>
    /// ������� � ���������� ������������ � ����������������� ������, ������������ ����������� ��������������
    /// </summary>
    /// <param name="type">
    /// ��� ��� �������� ��������� ������������
    /// </param>
    /// <returns>
    /// ������������
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
    /// ������� � ���������� ������������ � ����������������� ������, ������������ ����������� ��������������
    /// </summary>
    /// <param name="o">
    /// ��� ��� �������� ��������� ������������
    /// </param>
    /// <returns>
    /// ������������
    /// </returns>
    public static XmlSerializer GetSerializer(object o)
    {
      return GetSerializer(o.GetType());
    }

    /// <summary>
    /// ����������� ������ � ������ (������������ ��������)
    /// </summary>
    /// <param name="o">
    /// ������ ��� ������������
    /// </param>
    /// <returns>
    /// �������������� ������
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
    /// ������������ ����������� PersonCard
    /// </summary>
    /// <param name="person">
    /// ������ ������
    /// </param>
    /// <param name="fileName">
    /// ��� �����
    /// </param>
    public static void SerializePersonCard(PersonCard person, string fileName)
    {
      SerializeBasePersonTemplate(person, fileName, "ZPIMessageBatch");
    }

    /// <summary>
    /// ������������ ����������� PersonCard
    /// </summary>
    /// <param name="person">
    /// ������ ������
    /// </param>
    /// <returns>
    /// The <see cref="MemoryStream"/>.
    /// </returns>
    public static MemoryStream SerializePersonCard(PersonCard person)
    {
      return SerializeToMemoryStream(person, "ZPIMessageBatch");
    }

    /// <summary>
    /// ������������ ����������� PersonErp
    /// </summary>
    /// <param name="person">
    /// ������ ������
    /// </param>
    /// <param name="fileName">
    /// ��� �����
    /// </param>
    public static void SerializePersonErp(PersonErp person, string fileName)
    {
      SerializeBasePersonTemplate(person, fileName, "UPRMessageBatch");
    }

    /// <summary>
    /// ������������ �������
    /// </summary>
    /// <param name="person">
    /// ������ ������
    /// </param>
    /// <param name="fileName">
    /// ��� �����
    /// </param>
    /// <param name="tableName">
    /// �������� ���
    /// </param>
    /// <param name="settings">
    /// ��������� ������ xml
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
      var nameres = new XmlNamespaceManager(nt);
      nameres.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
      nameres.PushScope();
      nameres.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
      nameres.PushScope();
      nameres.AddNamespace("rtc", "http://www.rintech.ru");
      nameres.PushScope();

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
    /// ������������ �������
    /// </summary>
    /// <param name="person">
    /// ������ ������
    /// </param>
    /// <param name="tableName">
    /// �������� ���
    /// </param>
    /// <param name="settings">
    /// ��������� ������ xml
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
      var nameres = new XmlNamespaceManager(nt);
      nameres.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
      nameres.PushScope();
      nameres.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
      nameres.PushScope();
      nameres.AddNamespace("rtc", "http://www.rintech.ru");
      nameres.PushScope();

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
    /// ������������ �������
    /// </summary>
    /// <param name="person">
    /// ������ ������
    /// </param>
    /// <param name="fileName">
    /// ��� �����
    /// </param>
    public static void SerializeZwpList(ZpiZwi person, string fileName)
    {
      SerializeToFile(person, fileName, "UPRMessageBatch");
    }

    /// <summary>
    /// ����������� �����
    /// </summary>
    /// <param name="path">
    /// ������ ���� � �����, � ������� ���� ����� ������
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
    ///   ������������� ���������������� ����� ��� ������ In1
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
    ///   ������������� ���������������� ����� ��� ������ In1Card
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
    ///   ������������� ���������������� ����� ��� ������ RspZk1
    /// </summary>
    private static void GetElementAttributeRspZk1()
    {
      var myElementAttribute = new XmlElementAttribute("RSP_ZK1.QUERY_RESPONSE");
      var myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(RSP_ZK1), "QueryResponseList", myAttributes);
    }

    /// <summary>
    ///   ������������� ���������������� ����� ��� ������ RspZk2
    /// </summary>
    private static void GetElementAttributeRspZk2()
    {
      var myElementAttribute = new XmlElementAttribute("RSP_ZK2.QUERY_RESPONSE");
      var myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(RSP_ZK2), "QueryResponseList", myAttributes);
    }

    /// <summary>
    ///   ������������� ���������������� ����� ��� ������ RspZk4
    /// </summary>
    private static void GetElementAttributeRspZk4()
    {
      var myElementAttribute = new XmlElementAttribute("RSP_ZK4.QUERY_RESPONSE");
      var myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(RSP_ZK4), "QueryResponseList", myAttributes);
    }

    /// <summary>
    ///   ������������� ���������������� ����� ��� ������ RspZk5
    /// </summary>
    private static void GetElementAttributeRspZk5()
    {
      var myElementAttribute = new XmlElementAttribute("RSP_ZK5.QUERY_RESPONSE");
      var myAttributes = new XmlAttributes();
      myAttributes.XmlElements.Add(myElementAttribute);
      myOverrides.Add(typeof(RSP_ZK5), "QueryResponseList", myAttributes);
    }

    /// <summary>
    /// ������������ ����������� BasePersonTemplate
    /// </summary>
    /// <param name="person">
    /// ������ ������
    /// </param>
    /// <param name="fileName">
    /// ��� �����
    /// </param>
    /// <param name="tableName">
    /// �������� ���
    /// </param>
    private static void SerializeBasePersonTemplate(BasePersonTemplate person, string fileName, string tableName)
    {
      var nt = new NameTable();
      nt.Add(tableName);
      var nameres = new XmlNamespaceManager(nt);
      nameres.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
      nameres.PushScope();
      nameres.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
      nameres.PushScope();
      nameres.AddNamespace("rtc", "http://www.rintech.ru");
      nameres.PushScope();

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
            throw new InvalidOperationException("�� ������� ��������� ����������� �����");
          }
        }
      }
    }

    #endregion
  }
}