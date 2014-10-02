// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PolicyDataVersion1.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The policy data version 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace rt.srz.model.barcode
{
  #region references

  

  #endregion

  /// <summary>The policy data version 1.</summary>
  [Serializable]
  [ComVisible(true)]
  [XmlRoot(ElementName = "BarcodeData")]
  public class PolicyDataVersion1 : PolicyData
  {
  }
}