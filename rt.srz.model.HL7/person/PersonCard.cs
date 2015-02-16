// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonCard.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The person card.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The person card.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "ZPIMessageBatch", Namespace = "urn:hl7-org:v2xml")]
  public class PersonCard : BasePersonTemplate
  {
  }
}