// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OP.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The op type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.smo
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Runtime.Serialization;
  using System.Text;
  using System.Xml;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The op type.
  /// </summary>
  [Serializable]
  public class OPType
  {
    #region Static Fields

    /// <summary>
    ///   The serializer.
    /// </summary>
    private static XmlSerializer serializer;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the addres g.
    /// </summary>
    [XmlElement("ADDRES_G", Order = 8)]
    [DataMember(Name = "ADDRES_G", Order = 8)]
    public AddressType ADDRES_G { get; set; }

    /// <summary>
    ///   Gets or sets the addres p.
    /// </summary>
    [XmlElement("ADDRES_P", Order = 9)]
    [DataMember(Name = "ADDRES_P", Order = 9)]
    public AddressType ADDRES_P { get; set; }

    /// <summary>
    ///   Gets or sets the doc.
    /// </summary>
    [XmlElement("DOC_LIST", Order = 13)]
    [DataMember(Name = "DOC_LIST", Order = 13)]
    public List<DocType> DOC_LIST { get; set; }

    /// <summary>
    ///   Gets or sets the id.
    /// </summary>
    [XmlElement("ID", Order = 2)]
    [DataMember(Name = "ID", Order = 2)]
    public Guid ID { get; set; }

    /// <summary>
    ///   Gets or sets the insurance.
    /// </summary>
    [XmlElement("INSURANCE", Order = 11)]
    [DataMember(Name = "INSURANCE", Order = 11)]
    public InsuranceType INSURANCE { get; set; }

    /// <summary>
    ///   Gets or sets the is active.
    /// </summary>
    [XmlElement("IS_ACTIVE", Order = 3)]
    [DataMember(Name = "IS_ACTIVE", Order = 3)]
    public string ISACTIVE { get; set; }

    /// <summary>
    ///   Gets or sets the id.
    /// </summary>
    [XmlElement("N_REC", Order = 1)]
    [DataMember(Name = "N_REC", Order = 1)]
    public string N_REC { get; set; }

    /// <summary>
    ///   Gets or sets the id.
    /// </summary>
    [XmlElement("OLDDOC_LIST", Order = 14)]
    [DataMember(Name = "OLDDOC_LIST", Order = 14)]
    public List<DocType> OLD_DOC { get; set; }

    /// <summary>
    ///   Gets or sets the person.
    /// </summary>
    [XmlElement("OLD_PERSON", Order = 7)]
    [DataMember(Name = "OLD_PERSON", Order = 7)]
    public List<OldPersonType> OLD_PERSON { get; set; }

    /// <summary>
    ///   Gets or sets the person.
    /// </summary>
    [XmlElement("PERSON", Order = 6)]
    [DataMember(Name = "PERSON", Order = 6)]
    public PersonType PERSON { get; set; }

    /// <summary>
    ///   Gets or sets the person b.
    /// </summary>
    [XmlElement("PERSONB", Order = 12)]
    [DataMember(Name = "PERSONB", Order = 12)]
    public List<PersonBType> PERSONB { get; set; }

    /// <summary>
    ///   Gets or sets the state begin date.
    /// </summary>
    [XmlElement("VERSION", Order = 4)]
    [DataMember(Name = "VERSION", Order = 4)]
    public string VERSION { get; set; }

    /// <summary>
    ///   Gets or sets the person b.
    /// </summary>
    [XmlElement("TIP_OP", Order = 5)]
    [DataMember(Name = "TIP_OP", Order = 5)]
    public string TIP_OP { get; set; }

    /// <summary>
    ///   Gets or sets the vizit.
    /// </summary>
    /// <summary>
    ///   Gets or sets the vizit.
    /// </summary>
    [XmlElement("VIZIT", Order = 10)]
    [DataMember(Name = "VIZIT", Order = 10)]
    public VizitType VIZIT { get; set; }

     /// <summary>
    /// Gets or sets the statement change.
    /// </summary>
    [XmlElement("CHANGES", Order = 15)]
    [DataMember(Name = "CHANGES", Order = 15)]
    public List<StatementChange> STATEMENT_CHANGE { get; set; }

    /// <summary>
    /// Gets or sets the statement change.
    /// </summary>
    [XmlElement("NEED_NEW_POLICY", Order = 16)]
    [DataMember(Name = "NEED_NEW_POLICY", Order = 16)]
    public bool NEED_NEW_POLICY { get; set; }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the serializer.
    /// </summary>
    private static XmlSerializer Serializer
    {
      get
      {
        if (serializer == null)
        {
          serializer = new XmlSerializer(typeof(OPType));
        }

        return serializer;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Deserializes workflow markup into an OPType object
    /// </summary>
    /// <param name="xml">
    /// string workflow markup to deserialize
    /// </param>
    /// <param name="obj">
    /// Output OPType object
    /// </param>
    /// <param name="exception">
    /// output Exception value if deserialize failed
    /// </param>
    /// <returns>
    /// true if this XmlSerializer can deserialize the object; otherwise, false
    /// </returns>
    public static bool Deserialize(string xml, out OPType obj, out Exception exception)
    {
      exception = null;
      obj = default(OPType);
      try
      {
        obj = Deserialize(xml);
        return true;
      }
      catch (Exception ex)
      {
        exception = ex;
        return false;
      }
    }

    /// <summary>
    /// The deserialize.
    /// </summary>
    /// <param name="xml">
    /// The xml.
    /// </param>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool Deserialize(string xml, out OPType obj)
    {
      Exception exception = null;
      return Deserialize(xml, out obj, out exception);
    }

    /// <summary>
    /// The deserialize.
    /// </summary>
    /// <param name="xml">
    /// The xml.
    /// </param>
    /// <returns>
    /// The <see cref="OPType"/>.
    /// </returns>
    public static OPType Deserialize(string xml)
    {
      StringReader stringReader = null;
      try
      {
        stringReader = new StringReader(xml);
        return (OPType)Serializer.Deserialize(XmlReader.Create(stringReader));
      }
      finally
      {
        if (stringReader != null)
        {
          stringReader.Dispose();
        }
      }
    }

    /// <summary>
    /// Deserializes xml markup from file into an OPType object
    /// </summary>
    /// <param name="fileName">
    /// string xml file to load and deserialize
    /// </param>
    /// <param name="encoding">
    /// The encoding.
    /// </param>
    /// <param name="obj">
    /// Output OPType object
    /// </param>
    /// <param name="exception">
    /// output Exception value if deserialize failed
    /// </param>
    /// <returns>
    /// true if this XmlSerializer can deserialize the object; otherwise, false
    /// </returns>
    public static bool LoadFromFile(string fileName, Encoding encoding, out OPType obj, out Exception exception)
    {
      exception = null;
      obj = default(OPType);
      try
      {
        obj = LoadFromFile(fileName, encoding);
        return true;
      }
      catch (Exception ex)
      {
        exception = ex;
        return false;
      }
    }

    /// <summary>
    /// The load from file.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <param name="exception">
    /// The exception.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool LoadFromFile(string fileName, out OPType obj, out Exception exception)
    {
      return LoadFromFile(fileName, Encoding.GetEncoding(1251), out obj, out exception);
    }

    /// <summary>
    /// The load from file.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool LoadFromFile(string fileName, out OPType obj)
    {
      Exception exception = null;
      return LoadFromFile(fileName, out obj, out exception);
    }

    /// <summary>
    /// The load from file.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    /// <returns>
    /// The <see cref="OPType"/>.
    /// </returns>
    public static OPType LoadFromFile(string fileName)
    {
      return LoadFromFile(fileName, Encoding.GetEncoding(1251));
    }

    /// <summary>
    /// The load from file.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    /// <param name="encoding">
    /// The encoding.
    /// </param>
    /// <returns>
    /// The <see cref="OPType"/>.
    /// </returns>
    public static OPType LoadFromFile(string fileName, Encoding encoding)
    {
      FileStream file = null;
      StreamReader sr = null;
      try
      {
        file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        sr = new StreamReader(file, encoding);
        var xmlString = sr.ReadToEnd();
        sr.Close();
        file.Close();
        return Deserialize(xmlString);
      }
      finally
      {
        if (file != null)
        {
          file.Dispose();
        }

        if (sr != null)
        {
          sr.Dispose();
        }
      }
    }

    /// <summary>
    ///   Create a clone of this OPType object
    /// </summary>
    /// <returns>
    ///   The <see cref="OPType" />.
    /// </returns>
    public virtual OPType Clone()
    {
      return (OPType)MemberwiseClone();
    }

    /// <summary>
    /// Serializes current OPType object into file
    /// </summary>
    /// <param name="fileName">
    /// full path of outupt xml file
    /// </param>
    /// <param name="encoding">
    /// The encoding.
    /// </param>
    /// <param name="exception">
    /// output Exception value if failed
    /// </param>
    /// <returns>
    /// true if can serialize and save into file; otherwise, false
    /// </returns>
    public virtual bool SaveToFile(string fileName, Encoding encoding, out Exception exception)
    {
      exception = null;
      try
      {
        SaveToFile(fileName, encoding);
        return true;
      }
      catch (Exception e)
      {
        exception = e;
        return false;
      }
    }

    /// <summary>
    /// The save to file.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    /// <param name="exception">
    /// The exception.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public virtual bool SaveToFile(string fileName, out Exception exception)
    {
      return SaveToFile(fileName, Encoding.GetEncoding(1251), out exception);
    }

    /// <summary>
    /// The save to file.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    public virtual void SaveToFile(string fileName)
    {
      SaveToFile(fileName, Encoding.GetEncoding(1251));
    }

    /// <summary>
    /// The save to file.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    /// <param name="encoding">
    /// The encoding.
    /// </param>
    public virtual void SaveToFile(string fileName, Encoding encoding)
    {
      StreamWriter streamWriter = null;
      try
      {
        var xmlString = Serialize(encoding);
        streamWriter = new StreamWriter(fileName, false, Encoding.GetEncoding(1251));
        streamWriter.WriteLine(xmlString);
        streamWriter.Close();
      }
      finally
      {
        if (streamWriter != null)
        {
          streamWriter.Dispose();
        }
      }
    }

    /// <summary>
    /// Serializes current OPType object into an XML document
    /// </summary>
    /// <param name="encoding">
    /// The encoding.
    /// </param>
    /// <returns>
    /// string XML value
    /// </returns>
    public virtual string Serialize(Encoding encoding)
    {
      StreamReader streamReader = null;
      MemoryStream memoryStream = null;
      try
      {
        memoryStream = new MemoryStream();
        var xmlWriterSettings = new XmlWriterSettings();
        xmlWriterSettings.Encoding = encoding;
        var xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
        Serializer.Serialize(xmlWriter, this);
        memoryStream.Seek(0, SeekOrigin.Begin);
        streamReader = new StreamReader(memoryStream);
        return streamReader.ReadToEnd();
      }
      finally
      {
        if (streamReader != null)
        {
          streamReader.Dispose();
        }

        if (memoryStream != null)
        {
          memoryStream.Dispose();
        }
      }
    }

    /// <summary>
    ///   The serialize.
    /// </summary>
    /// <returns>
    ///   The <see cref="string" />.
    /// </returns>
    public virtual string Serialize()
    {
      return Serialize(Encoding.GetEncoding(1251));
    }

    #endregion
  }
}