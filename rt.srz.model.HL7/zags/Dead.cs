//-----------------------------------------------------------------------
// <copyright file="Dead.cs" company="SofTrust" author="IKhavkina">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace rt.srz.model.HL7.zags
{
  using rt.srz.model.HL7.person.target;

  /// <summary>
	/// Умерший
	/// </summary>
	public class Dead
	{
		/// <summary>
		/// Информация о умершем
		/// </summary>
		public MessagePid PidList { get; set; }
	}
}