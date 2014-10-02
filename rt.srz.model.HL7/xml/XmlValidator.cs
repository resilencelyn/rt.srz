// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlValidator.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The xml validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.foms.HL7.xml
{
  #region references

  using System;
  using System.Xml;
  using System.Xml.Schema;

  using rt.foms.HL7.commons;
  using rt.foms.HL7.commons.Enumerations;
  using rt.foms.HL7.dotNetX;
  using rt.foms.HL7.xml.enumerations;

  #endregion

  /// <summary>
  ///   The xml validator.
  /// </summary>
  public class XmlValidator
  {
    #region Static Fields

    /// <summary>
    ///   The error delimiter.
    /// </summary>
    private static readonly string errorDelimiter = "  ";

    #endregion

    #region Fields

    /// <summary>
    ///   The ignore schema location attribute.
    /// </summary>
    public readonly bool IgnoreSchemaLocationAttribute;

    /// <summary>
    ///   The xml file path.
    /// </summary>
    public readonly string XmlFilePath;

    /// <summary>
    ///   The xsd file path.
    /// </summary>
    public readonly string XsdFilePath;

    /// <summary>
    ///   The error message.
    /// </summary>
    private string errorMessage = string.Empty;

    /// <summary>
    ///   The validation status.
    /// </summary>
    private FileXmlStatus validationStatus;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlValidator"/> class.
    /// </summary>
    /// <param name="XmlFilePath">
    /// The xml file path.
    /// </param>
    /// <param name="XsdFilePath">
    /// The xsd file path.
    /// </param>
    /// <param name="IgnoreSchemaLocationAttribute">
    /// The ignore schema location attribute.
    /// </param>
    public XmlValidator(string XmlFilePath, string XsdFilePath, bool IgnoreSchemaLocationAttribute = false)
    {
      this.XmlFilePath = XmlFilePath;
      this.XsdFilePath = XsdFilePath;
      this.IgnoreSchemaLocationAttribute = IgnoreSchemaLocationAttribute;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the error message.
    /// </summary>
    public string ErrorMessage
    {
      get
      {
        return errorMessage;
      }
    }

    /// <summary>
    ///   Gets the validation status.
    /// </summary>
    public FileXmlStatus ValidationStatus
    {
      get
      {
        return validationStatus;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The validate.
    /// </summary>
    /// <returns>
    ///   The <see cref="FileXmlStatus" />.
    /// </returns>
    public FileXmlStatus Validate()
    {
      string str;
      return Validate(out str);
    }

    /// <summary>
    /// The validate.
    /// </summary>
    /// <param name="schemaLocationAttribute">
    /// The schema location attribute.
    /// </param>
    /// <returns>
    /// The <see cref="FileXmlStatus"/>.
    /// </returns>
    public FileXmlStatus Validate(out string schemaLocationAttribute)
    {
      schemaLocationAttribute = null;
      validationStatus = FileXmlStatus.Unknown;
      try
      {
        var set = new XmlSchemaSet();
        set.Add(null, XsdFilePath);
        var settings2 = new XmlReaderSettings
                          {
                            ValidationType = ValidationType.Schema, 
                            Schemas = set, 
                            IgnoreComments = true, 
                            IgnoreProcessingInstructions = true, 
                            IgnoreWhitespace = true
                          };
        var settings = settings2;
        settings.ValidationEventHandler += ValidationHandler;
        settings.ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints
                                   | XmlSchemaValidationFlags.ReportValidationWarnings
                                   | XmlSchemaValidationFlags.ProcessInlineSchema;
        if (!IgnoreSchemaLocationAttribute)
        {
          settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
        }

        using (var reader = XmlReader.Create(XmlFilePath, settings))
        {
          if ((reader.MoveToContent() == XmlNodeType.Element) && reader.MoveToFirstAttribute())
          {
            do
            {
              if ((string.Compare(
                reader.NamespaceURI, 
                "http://www.w3.org/2001/XMLSchema-instance", 
                StringComparison.Ordinal) == 0)
                  && (string.Compare(reader.LocalName, "schemaLocation", StringComparison.Ordinal) == 0))
              {
                schemaLocationAttribute = reader.Value;
                break;
              }
            }
            while (reader.MoveToNextAttribute());
          }

          while (reader.Read())
          {
          }
        }

        validationStatus = string.IsNullOrEmpty(errorMessage)
                             ? FileXmlStatus.ValidatedSuccessfully
                             : FileXmlStatus.XsdValidationFailed;
      }
      catch (Exception exception)
      {
        var fomsLogPrefix = HL7Helper.FomsLogPrefix;
        FomsLogger.WriteError(
          LogType.Local, 
          string.Format(
            "Ошибка во время проверки XSD-схемой. File name: {1}{0}{2}", 
            Environment.NewLine, 
            XmlFilePath, 
            exception), 
          fomsLogPrefix);
        errorMessage = exception.Message;
        validationStatus = FileXmlStatus.XmlValidationFailed;
      }

      return validationStatus;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The validation handler.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    private void ValidationHandler(object sender, ValidationEventArgs args)
    {
      var errorDelimiter = XmlValidator.errorDelimiter;
      errorMessage = TStringHelper.CombineStrings(errorMessage, args.Message, errorDelimiter);
    }

    #endregion
  }
}