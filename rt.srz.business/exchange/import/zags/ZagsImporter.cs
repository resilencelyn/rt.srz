// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZagsImporter.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The zags importer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.zags
{
  using System;
  using System.Reflection;
  using System.Xml;
  using System.Xml.Schema;

  using rt.srz.business.configuration.algorithms.serialization;
  using rt.srz.model.Hl7.zags;

  /// <summary>
  /// The zags importer.
  /// </summary>
  /// <typeparam name="TTypeXmlObj">
  /// Тип пакета
  /// </typeparam>
  public abstract class ZagsImporter<TTypeXmlObj> : IZagsImporter
    where TTypeXmlObj : new()
  {
    #region Properties

    /// <summary>
    ///   Gets the xsd resource name.
    /// </summary>
    protected abstract string XsdResourceName { get; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The get import data.
    /// </summary>
    /// <param name="xmlFilePath">
    /// The xml file path.
    /// </param>
    /// <returns>
    /// The <see cref="Zags_VNov"/>.
    /// </returns>
    public virtual Zags_VNov GetImportData(string xmlFilePath)
    {
      XmlSchema schema;

      // зачитываем из ресурсов xsd
      var resourceName = string.Format("rt.srz.business.exchange.import.zags.Implementation.xsd.{0}", XsdResourceName);
      var myAssembly = Assembly.GetExecutingAssembly();
      using (var schemaStream = myAssembly.GetManifestResourceStream(resourceName))
      {
        schema = XmlSchema.Read(schemaStream, null);
      }

      // проверяем документ на соответствие
      var settings = new XmlReaderSettings();
      settings.Schemas.Add(schema);
      settings.ValidationType = ValidationType.Schema;
      settings.ValidationEventHandler += ValidationHandler;
      var reader = XmlReader.Create(xmlFilePath, settings);

      var obj = new TTypeXmlObj();
      obj = (TTypeXmlObj)XmlSerializationHelper.Deserialize(obj, reader);

      return Convert(obj);
    }

    #endregion

    #region Methods

    /// <summary>
    /// The convert.
    /// </summary>
    /// <param name="data">
    /// The data.
    /// </param>
    /// <returns>
    /// The <see cref="Zags_VNov"/>.
    /// </returns>
    protected abstract Zags_VNov Convert(TTypeXmlObj data);

    /// <summary>
    /// The validation handler.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    private static void ValidationHandler(object sender, ValidationEventArgs args)
    {
      if (args.Severity == XmlSeverityType.Warning)
      {
        throw new InvalidOperationException("Предупреждение: " + args.Message, args.Exception);
      }

      throw new InvalidOperationException("Ошибка: " + args.Message, args.Exception);
    }

    #endregion

    // public Zags_VNov GetImportData(string xmlFilePath)
    // {
    // TypeXmlObj result;
    // var serializer = XmlSerializationHelper.GetSerializer(typeof(TypeXmlObj));
    // using (var fs = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read))
    // {
    // result = (TypeXmlObj)serializer.Deserialize(fs);
    // }
    // return Convert(result);
    // }
  }
}