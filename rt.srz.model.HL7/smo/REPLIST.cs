﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="REPLIST.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The rep list type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.smo
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text;
  using System.Xml;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The rep list type.
  /// </summary>
  [Serializable]
  [XmlRoot("REPLIST", Namespace = "http://tempuri.org/XMLSchema.xsd", IsNullable = false)]
  public class REPListType
  {
    #region Static Fields

    /// <summary>
    ///   The serializer.
    /// </summary>
    private static XmlSerializer serializer;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the filename.
    /// </summary>
    [XmlElement(Order = 1)]
    public string FILENAME { get; set; }

    /// <summary>
    ///   Gets or sets the nerr.
    /// </summary>
    [XmlElement(Order = 5)]
    public int NERR { get; set; }

    /// <summary>
    ///   Gets or sets the nrecords.
    /// </summary>
    [XmlElement(Order = 4)]
    public int NRECORDS { get; set; }

    /// <summary>
    ///   Gets or sets the przcod.
    /// </summary>
    [XmlElement(Order = 3)]
    public string PRZCOD { get; set; }

    /// <summary>
    ///   Gets or sets the rep.
    /// </summary>
    [XmlElement("REP", Order = 6)]
    public List<REPType> REP { get; set; }

    /// <summary>
    ///   Gets or sets the smocod.
    /// </summary>
    [XmlElement(Order = 2)]
    public string SMOCOD { get; set; }

    /// <summary>
    ///   Gets or sets the vers.
    /// </summary>
    [XmlElement(Order = 0)]
    public string VERS { get; set; }

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
          serializer = new XmlSerializer(typeof(REPListType));
        }

        return serializer;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Deserializes workflow markup into an REPListType object
    /// </summary>
    /// <param name="xml">
    /// string workflow markup to deserialize
    /// </param>
    /// <param name="obj">
    /// Output REPListType object
    /// </param>
    /// <param name="exception">
    /// output Exception value if deserialize failed
    /// </param>
    /// <returns>
    /// true if this XmlSerializer can deserialize the object; otherwise, false
    /// </returns>
    public static bool Deserialize(string xml, out REPListType obj, out Exception exception)
    {
      exception = null;
      obj = default(REPListType);
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
    public static bool Deserialize(string xml, out REPListType obj)
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
    /// The <see cref="REPListType"/>.
    /// </returns>
    public static REPListType Deserialize(string xml)
    {
      StringReader stringReader = null;
      try
      {
        stringReader = new StringReader(xml);
        return (REPListType)Serializer.Deserialize(XmlReader.Create(stringReader));
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
    /// Deserializes xml markup from file into an REPListType object
    /// </summary>
    /// <param name="fileName">
    /// string xml file to load and deserialize
    /// </param>
    /// <param name="encoding">
    /// The encoding.
    /// </param>
    /// <param name="obj">
    /// Output REPListType object
    /// </param>
    /// <param name="exception">
    /// output Exception value if deserialize failed
    /// </param>
    /// <returns>
    /// true if this XmlSerializer can deserialize the object; otherwise, false
    /// </returns>
    public static bool LoadFromFile(string fileName, Encoding encoding, out REPListType obj, out Exception exception)
    {
      exception = null;
      obj = default(REPListType);
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
    public static bool LoadFromFile(string fileName, out REPListType obj, out Exception exception)
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
    public static bool LoadFromFile(string fileName, out REPListType obj)
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
    /// The <see cref="REPListType"/>.
    /// </returns>
    public static REPListType LoadFromFile(string fileName)
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
    /// The <see cref="REPListType"/>.
    /// </returns>
    public static REPListType LoadFromFile(string fileName, Encoding encoding)
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
    ///   Create a clone of this REPListType object
    /// </summary>
    /// <returns>
    ///   The <see cref="REPListType" />.
    /// </returns>
    public virtual REPListType Clone()
    {
      return (REPListType)MemberwiseClone();
    }

    /// <summary>
    /// Serializes current REPListType object into file
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
    /// Serializes current REPListType object into an XML document
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