// --------------------------------------------------------------------------------------------------------------------
// <copyright file="REP.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The rep type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.smo
{
  #region references

  using System;
  using System.IO;
  using System.Text;
  using System.Xml;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The rep type.
  /// </summary>
  [Serializable]
  public class REPType
  {
    #region Static Fields

    /// <summary>
    ///   The serializer.
    /// </summary>
    private static XmlSerializer serializer;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the cod e_ erp.
    /// </summary>
    [XmlElement(Order = 2)]
    public int CODE_ERP { get; set; }

    /// <summary>
    ///   Gets or sets the comment.
    /// </summary>
    [XmlElement(Order = 3)]
    public string COMMENT { get; set; }

    /// <summary>
    ///   Gets or sets the id.
    /// </summary>
    [XmlElement(Order = 1)]
    public Guid ID { get; set; }

    /// <summary>
    ///   Gets or sets the insurance.
    /// </summary>
    [XmlElement(Order = 4)]
    public InsuranceType INSURANCE { get; set; }

    /// <summary>
    ///   Gets or sets the n_ rec.
    /// </summary>
    [XmlElement(Order = 0)]
    public string N_REC { get; set; }

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
          serializer = new XmlSerializer(typeof(REPType));
        }

        return serializer;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Deserializes workflow markup into an REPType object
    /// </summary>
    /// <param name="xml">
    /// string workflow markup to deserialize
    /// </param>
    /// <param name="obj">
    /// Output REPType object
    /// </param>
    /// <param name="exception">
    /// output Exception value if deserialize failed
    /// </param>
    /// <returns>
    /// true if this XmlSerializer can deserialize the object; otherwise, false
    /// </returns>
    public static bool Deserialize(string xml, out REPType obj, out Exception exception)
    {
      exception = null;
      obj = default(REPType);
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
    public static bool Deserialize(string xml, out REPType obj)
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
    /// The <see cref="REPType"/>.
    /// </returns>
    public static REPType Deserialize(string xml)
    {
      StringReader stringReader = null;
      try
      {
        stringReader = new StringReader(xml);
        return (REPType)Serializer.Deserialize(XmlReader.Create(stringReader));
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
    /// Deserializes xml markup from file into an REPType object
    /// </summary>
    /// <param name="fileName">
    /// string xml file to load and deserialize
    /// </param>
    /// <param name="encoding">
    /// The encoding.
    /// </param>
    /// <param name="obj">
    /// Output REPType object
    /// </param>
    /// <param name="exception">
    /// output Exception value if deserialize failed
    /// </param>
    /// <returns>
    /// true if this XmlSerializer can deserialize the object; otherwise, false
    /// </returns>
    public static bool LoadFromFile(string fileName, Encoding encoding, out REPType obj, out Exception exception)
    {
      exception = null;
      obj = default(REPType);
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
    public static bool LoadFromFile(string fileName, out REPType obj, out Exception exception)
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
    public static bool LoadFromFile(string fileName, out REPType obj)
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
    /// The <see cref="REPType"/>.
    /// </returns>
    public static REPType LoadFromFile(string fileName)
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
    /// The <see cref="REPType"/>.
    /// </returns>
    public static REPType LoadFromFile(string fileName, Encoding encoding)
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
    ///   Create a clone of this REPType object
    /// </summary>
    /// <returns>
    ///   The <see cref="REPType" />.
    /// </returns>
    public virtual REPType Clone()
    {
      return (REPType)MemberwiseClone();
    }

    /// <summary>
    /// Serializes current REPType object into file
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
    /// Serializes current REPType object into an XML document
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