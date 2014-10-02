// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PolicyData.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The policy data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using rt.srz.model.barcode.Properties;

namespace rt.srz.model.barcode
{
  #region references

  

  #endregion

  /// <summary>The policy data.</summary>
  [Serializable]
  [XmlRoot(ElementName = "BarcodeData")]
  [ComVisible(true)]
  public class PolicyData
  {
    #region Fields

    /// <summary>The eds.</summary>
    private byte[] eds;

    #endregion

    #region Public Properties

    /// <summary>Gets or sets the barcode type.</summary>
    [XmlElement(ElementName = "BarcodeType")]
    public virtual string BarcodeType { get; set; }

    /// <summary>Gets or sets the birth date.</summary>
    [XmlElement(ElementName = "BirthDate")]
    public virtual string BirthDate { get; set; }

    /// <summary>Gets or sets the eds.</summary>
    [XmlElement(DataType = "base64Binary", ElementName = "EDS")]
    public virtual byte[] Eds
    {
      get
      {
        return eds;
      }

      set
      {
        eds = new byte[value.Length];
        value.CopyTo(eds, 0);
      }
    }

    /// <summary>Gets or sets the expire date.</summary>
    [XmlElement(ElementName = "ExpireDate")]
    public virtual DateTime ExpireDate { get; set; }

    /// <summary>Gets or sets the first name.</summary>
    [XmlElement(ElementName = "FirstName")]
    public virtual string FirstName { get; set; }

    /// <summary>Gets or sets the last name.</summary>
    [XmlElement(ElementName = "LastName")]
    public virtual string LastName { get; set; }

    /// <summary>Gets or sets the ogrn.</summary>
    [XmlElement(ElementName = "Ogrn")]
    public virtual string Ogrn { get; set; }

    /// <summary>Gets or sets the okato.</summary>
    [XmlElement(ElementName = "Okato")]
    public virtual string Okato { get; set; }

    /// <summary>Gets or sets the patronymic.</summary>
    [XmlElement(ElementName = "Patronymic")]
    public virtual string Patronymic { get; set; }

    /// <summary>Gets or sets the policy number.</summary>
    [XmlElement(ElementName = "PolicyNumber")]
    public virtual string PolicyNumber { get; set; }

    /// <summary>Gets or sets the sex.</summary>
    [XmlElement(ElementName = "Sex")]
    public virtual string Sex { get; set; }

    #endregion

    #region Properties

    /// <summary>The bad xml.</summary>
    protected bool BadXml { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Initializes a new instance of the <see cref="PolicyData"/> class.
    /// </summary>
    /// <param name="strXml">
    /// The str xml.
    /// </param>
    /// <returns>
    /// The <see cref="PolicyData"/>.
    /// </returns>
    public PolicyData Create(string strXml)
    {
      BadXml = false;
      var schemas = new XmlSchemaSet();

      // todo заменить на десериализацию
      schemas.Add(string.Empty, XmlReader.Create(new StringReader(Resource.PolicyXSD1)));
      var doc = XDocument.Parse(strXml);
      doc.Validate(schemas, ValidationEventHandler);
      if (BadXml)
      {
        BadXml = false;
        schemas = new XmlSchemaSet();
        schemas.Add(string.Empty, XmlReader.Create(new StringReader(Resource.PolicyXSD2)));
        doc = XDocument.Parse(strXml);
        doc.Validate(schemas, validationEventHandler2);
      }

      if (!BadXml)
      {
        if (doc.Root != null)
        {
          var elements = doc.Root.Elements();

          foreach (var element in
            elements.Select(element => new { element, nodeName = element.Name.LocalName })
              .Where(@t => @t.nodeName.Equals("BarcodeType"))
              .Select(@t => @t.element))
          {
            byte temp;
            byte.TryParse(element.Value, out temp);
            BarcodeType = temp.ToString();
            break;
          }
        }

        switch (BarcodeType)
        {
          case "1":
            return Deserialize<PolicyDataVersion1>(strXml);
          case "2":
            return Deserialize<PolicyDataVersion2>(strXml);
        }
      }

      return null;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The deserialize.
    /// </summary>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    private T Deserialize<T>(string str)
    {
      using (var reader = new StringReader(str))
      {
        using (var xmlReader = new XmlTextReader(reader))
        {
          var serializer = new XmlSerializer(typeof(T));
          return (T)serializer.Deserialize(xmlReader);
        }
      }
    }

    /// <summary>
    /// При валидации не вызывать последней.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void ValidationEventHandler(object sender, ValidationEventArgs e)
    {
      BadXml = true;
    }

    /// <summary>
    /// При валидации вызывать последний раз. Т.к. вызывает эксепшн.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void validationEventHandler2(object sender, ValidationEventArgs e)
    {
      BadXml = true;
    }

    #endregion
  }
}