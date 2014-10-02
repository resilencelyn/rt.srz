//-----------------------------------------------------------------------
// <copyright file="Zags.cs" company="SofTrust" author="IKhavkina">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace rt.srz.model.HL7.zags
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.person;

  /// <summary>
	/// Данные ЗАГС по умершим
	/// </summary>
	[Serializable]
	[XmlRoot("ZAGS", Namespace = "urn:hl7-org:v2xml", IsNullable = false)]
	public class Zags : BaseMessageTemplate
	{
		/// <summary>
		/// Список умерших
		/// </summary>
		[XmlElement("DEAD_LIST")]
		public List<Dead> DeadList { get; set; }

		/// <summary>
		/// Список воскресших
		/// </summary>
		[XmlElement("RESURRECT_LIST")]
		public List<Resurrect> ResurrectList { get; set; }
	}
}
