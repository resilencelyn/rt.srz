namespace rt.srz.business.exchange.import.zags
{
  using System;
  using System.IO;
  using System.Reflection;
  using System.Xml;
  using System.Xml.Schema;

  using rt.srz.business.configuration.algorithms.serialization;
  using rt.srz.model.HL7.zags;

  public abstract class ZagsImporter<TypeXmlObj> : IZagsImporter where TypeXmlObj: new()
  {
    public ZagsImporter()
    {

    }

    protected abstract string XsdResourceName { get; }

    protected abstract Zags_VNov Convert(TypeXmlObj data);

    public virtual Zags_VNov GetImportData(string xmlFilePath)
    {
      XmlSchema schema;
      //зачитываем из ресурсов xsd
      var resourceName = string.Format("rt.srz.business.exchange.import.zags.Implementation.xsd.{0}", XsdResourceName);
      Assembly myAssembly = Assembly.GetExecutingAssembly();
      using (Stream schemaStream = myAssembly.GetManifestResourceStream(resourceName))
      {
        schema = XmlSchema.Read(schemaStream, null);
      }

      //проверяем документ на соответствие
      XmlReaderSettings settings = new XmlReaderSettings();
      settings.Schemas.Add(schema);
      settings.ValidationType = ValidationType.Schema;
      settings.ValidationEventHandler += ValidationHandler;
      XmlReader reader = XmlTextReader.Create(xmlFilePath, settings);

      TypeXmlObj obj = new TypeXmlObj();
      obj = (TypeXmlObj)XmlSerializationHelper.Deserialize(obj, reader);

      return Convert(obj);
    }

    private static void ValidationHandler(object sender, ValidationEventArgs _args)
    {
      if (_args.Severity == XmlSeverityType.Warning)
        throw new InvalidOperationException("Предупреждение: " + _args.Message, _args.Exception);
      else
        throw new InvalidOperationException("Ошибка: " + _args.Message, _args.Exception);
    }

    //public Zags_VNov GetImportData(string xmlFilePath)
    //{
    //  TypeXmlObj result;
    //  var serializer = XmlSerializationHelper.GetSerializer(typeof(TypeXmlObj));
    //  using (var fs = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read))
    //  {
    //    result = (TypeXmlObj)serializer.Deserialize(fs);
    //  }
    //  return Convert(result);
    //}

  }
}
