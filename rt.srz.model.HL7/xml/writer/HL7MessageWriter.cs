// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HL7MessageWriter.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The h l 7 message writer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.foms.HL7.xml.writer
{
  #region references

  using System;
  using System.Collections;
  using System.Xml;
  using System.Xml.Linq;

  using rt.foms.HL7.commons.Interfaces;
  using rt.foms.HL7.parameters;

  #endregion

  /// <summary>
  ///   The h l 7 message writer.
  /// </summary>
  [CLSCompliant(false)]
  public sealed class HL7MessageWriter : ISelfWriter<XmlWriter>
  {
    #region Fields

    /// <summary>
    ///   The batch fatal reference.
    /// </summary>
    private readonly bool batchFatalReference;

    /// <summary>
    ///   The errors.
    /// </summary>
    private readonly IEnumerable errors;

    /// <summary>
    ///   The node name.
    /// </summary>
    private readonly XName nodeName;

    /// <summary>
    ///   The parameters.
    /// </summary>
    private readonly Parameters parameters;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="HL7MessageWriter"/> class.
    /// </summary>
    /// <param name="nodeName">
    /// The node name.
    /// </param>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    /// <param name="errors">
    /// The errors.
    /// </param>
    /// <param name="batchFatalReference">
    /// The batch fatal reference.
    /// </param>
    public HL7MessageWriter(XName nodeName, Parameters parameters, IEnumerable errors, bool batchFatalReference = false)
    {
      this.nodeName = nodeName;
      this.parameters = parameters;
      this.errors = errors;
      this.batchFatalReference = batchFatalReference;
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The write to.
    /// </summary>
    /// <param name="target">
    /// The target.
    /// </param>
    public void WriteTo(XmlWriter target)
    {
      target.WriteStartElement(nodeName.LocalName, nodeName.NamespaceName);
      try
      {
        HL7Writer.WriteHL7(target, XmlCreator.CreateMsh(parameters));
        HL7Writer.WriteHL7(target, XmlCreator.CreateMsa(parameters, batchFatalReference));
        if (errors != null)
        {
          foreach (var obj2 in errors)
          {
            HL7Writer.WriteHL7(target, obj2);
          }
        }
      }
      finally
      {
        target.WriteEndElement();
      }
    }

    #endregion
  }
}