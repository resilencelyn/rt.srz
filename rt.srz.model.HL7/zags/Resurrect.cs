//-----------------------------------------------------------------------
// <copyright file="Resurrect.cs" company="SofTrust" author="IKhavkina">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace rt.srz.model.HL7.zags
{
  using rt.srz.model.HL7.person.target;

  /// <summary>
	/// Воскресший
	/// </summary>
	public class Resurrect
	{
		/// <summary>
		/// Информация о умершем
		/// </summary>
		public MessagePid PidList { get; set; }
	}
}