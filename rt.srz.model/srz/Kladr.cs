// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Kladr.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.srz
{
  using System;
  using System.IO;
  using System.Runtime.Serialization;
  using System.Text;
  using System.Xml;
  using System.Xml.Serialization;

  using rt.core.model.interfaces;

  /// <summary>
  ///   The Kladr.
  /// </summary>
  public partial class Kladr : IAddress
  {
    #region Public Properties

    /// <summary>
    ///   Gets the parent.
    /// </summary>
    [XmlIgnore]
    [IgnoreDataMember]
    public virtual Guid? ParentId
    {
      get
      {
        return KLADRPARENT.Id;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// десериализация из формата XML
    /// </summary>
    /// <param name="xml">
    /// xmlDocument для десериализации
    /// </param>
    /// <returns>
    /// Объект Kladr
    /// </returns>
    public static Kladr Deserializating(XmlDocument xml)
    {
      var stream = new MemoryStream();
      xml.Save(stream);
      stream.Position = 0;
      var sr = new StreamReader(stream);
      var str = sr.ReadToEnd();
      return Deserializating(str);
    }

    /// <summary>
    /// десериализация из формата XML
    /// </summary>
    /// <param name="xml">
    /// xml-строка подлежащая десериализации
    /// </param>
    /// <returns>
    /// Объект Kladr
    /// </returns>
    public static Kladr Deserializating(string xml)
    {
      var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
      var reader = new XmlSerializer(typeof(Kladr));
      var file = new StreamReader(stream);
      return (Kladr)reader.Deserialize(file);
    }

    /// <summary>
    ///   The get address.
    /// </summary>
    /// <returns>
    ///   The <see cref="Address" />.
    /// </returns>
    public virtual Address GetAddress()
    {
      return new Address
             {
               Id = Id, 
               Code = Code, 
               Index = Index, 
               Level = Level, 
               Name = Name, 
               Okato = Okato, 
               Socr = Socr, 
               ParentId = ParentId
             };
    }

    #endregion
  }
}